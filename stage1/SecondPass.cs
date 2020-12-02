using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stage1
{
    public partial class Form1 : Form
    {
        private void AppendBinCodeLine (string str)
        {
            textBoxBinCode.AppendText(str + "\n");
        }

        private void SecondPass()
        {
            foreach (SupportLine supportLine in tableSupport)
            {
                if (!StartFlag)
                {
                    AppendBinCodeLine($"H {supportLine.Label} {string.Format("{0:X6}", startAddress).ToUpper()} {string.Format("{0:X6}", programLength).ToUpper()}");
                    StartFlag = true;
                }
                else if (supportLine.MKOP.Equals("END"))
                {
                    AppendBinCodeLine($"E {string.Format("{0:X6}", endAddress).ToUpper()}");
                    EndFlag = true;
                }
                else
                {
                    string firstOperand = supportLine.FirstOperand;
                    string secondOperand = supportLine.SecondOperand;
                    
                    if (firstOperand != null && secondOperand != null)
                    {
                        firstOperand = string.Format("{0:X2}", registers.ToList().IndexOf(firstOperand));
                        secondOperand = string.Format("{0:X2}", registers.ToList().IndexOf(secondOperand));
                    }
                    else if (firstOperand != null && secondOperand == null)
                    {
                        // Это строка байт
                        if (Regex.IsMatch(firstOperand, @"^[cCxX]+.+$")) 
                        {
                            char type = firstOperand[0];
                            firstOperand = firstOperand.Substring(2, firstOperand.Length - 3);
                            if (type == 'c' || type == 'C')
                            {
                                string temp = "";
                                foreach (byte symb in System.Text.Encoding.ASCII.GetBytes(firstOperand))
                                    temp += symb.ToString("X");
                                firstOperand = temp;
                            }
                            else if (type == 'x' || type == 'X')
                            {
                                if (firstOperand.Length % 2 == 1)
                                    firstOperand = firstOperand.Insert(0, "0");
                                if (firstOperand.Contains('\''))
                                    NewException($"Некорректное значение в строке: {supportLine.Label} {supportLine.MKOP} {firstOperand} {secondOperand}");
                            }
                            else
                                firstOperand = firstOperand.ToUpper();
                        }
                        // Это символическое имя
                        else if (IsSymbolicName(firstOperand))
                        {
                            if (tableSymbolicNames.Any(x => x.Name.Equals(firstOperand.ToUpper())))
                                firstOperand = string.Format("{0:X6}", tableSymbolicNames.FirstOrDefault(x => x.Name.Equals(firstOperand.ToUpper())).Address);
                            else
                                NewException($"Символическое имя {firstOperand} не найдено в ТСИ");       
                        }
                        // Это число
                        else
                        {
                            int x;
                            if (int.TryParse(firstOperand, out x))
                                firstOperand = string.Format("{0:X}", x);
                        }
                    
                }

                    if (supportLine.MKOP != null)
                    {
                        if (IsDirective(supportLine.MKOP))
                            AppendBinCodeLine($"T {string.Format("{0:X6}", Convert.ToInt32(supportLine.Label, 16))} {firstOperand}");
                        else
                        {
                            string code = string.Format("{0:X2}", ((Convert.ToInt32(supportLine.MKOP, 16) - 1) >> 2));
                            int codeLength = operationCodes.FirstOrDefault(x => x.HexCode.Equals(code)).CodeLength;
                            AppendBinCodeLine($"T {string.Format("{0:X6}", Convert.ToInt32(supportLine.Label, 16))} {string.Format("{0:X2}", codeLength)} {supportLine.MKOP} {firstOperand} {secondOperand}");
                        }
                    }
                    else
                        AppendBinCodeLine($"T {string.Format("{0:X6}", Convert.ToInt32(supportLine.Label, 16))}");
                }
                
            }
            
        }
    }
}
