using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stage1
{
    public partial class Form1 : Form
    {
        private void FirstPass(List<string> sourceCodeLines)
        {
            if (sourceCodeLines.Count() != 0)
                codeLines = ToCodeLines(sourceCodeLines); // преобразуем исходный текст к списку команд, с которым можно работать
            else
                throw new Exception("Исходный текст не задан");

            tableSymbolicNames = new BindingList<SymbolicName>(); // инициализируем таблицу символьных имен
            tableSupport = new BindingList<SupportLine>(); // инициализируем вспомогательную таблицу 

            foreach (CodeLine codeLine in codeLines)
            {
                if (!StartFlag)
                {
                    // Сюда попадем только при первой итерации
                    // Если здесь не директива START, то генерируется ошибка
                    if (!codeLine.MKOP.ToUpper().Equals("START"))
                        throw new Exception("В начале программы директива START не была обнаружена");
                }

                if (EndFlag)
                    break;

                if (!TsiContainsName(codeLine.Label))
                {
                    if (codeLine.Label != null && StartFlag)
                    {
                        SymbolicName symbolicName = new SymbolicName();
                        symbolicName.Name = codeLine.Label;
                        symbolicName.Address = Convert.ToString(addressCounter, 16);
                        symbolicName.Address = new string('0', 6 - symbolicName.Address.Length) + symbolicName.Address; // дописываем нули
                        tableSymbolicNames.Add(symbolicName);
                    }
                }
                else
                    throw new Exception("Найдена уже существующая метка: " + codeLine.Label);

                if (IsDirective(codeLine.MKOP))
                {
                    switch (codeLine.MKOP.ToUpper())
                    {
                        case "START":
                            {
                                if (StartFlag)
                                    throw new Exception("Обнаружено второе появление директивы START");
                                
                                StartFlag = true;
                                
                                if (codeLine.Label == null)
                                    throw new Exception("Имя программы не задано");
                                if (codeLine.Label.Length > 10)
                                    throw new Exception("Превышена длина имени программы (больше чем 10 символов)");                               
                                // Проверяем, что адрес начала программы состоит из символов шестнадцатеричной СС
                                if (!Regex.IsMatch(codeLine.FirstOperand.ToUpper(), @"^[A-F0-9]+$"))
                                    throw new Exception("Неверный адрес начала программы");
                                
                                // Преобразуем в 10чную СС
                                addressCounter = Convert.ToInt32(codeLine.FirstOperand, 16);
                                startCounter = addressCounter;
                                
                                if (startCounter == 0)
                                    throw new Exception("Адрес начала программы не может быть равен 0");
                                if (startCounter > maxMemoryAdr)
                                    throw new Exception("Адрес программы выходит за диапазон памяти");

                                SupportLine supportLine = new SupportLine {
                                                                               Label = codeLine.Label,
                                                                               MKOP = codeLine.MKOP,
                                                                               FirstOperand = new string('0', 6 - codeLine.FirstOperand.Length) + codeLine.FirstOperand
                                                                           };
                                tableSupport.Add(supportLine);
                                programName = supportLine.Label;
                            }
                            break;
                        case "WORD":
                            {
                                // Допускаются только числа (положительные и до maxMemoryAddr)

                                int operand = 0;
                                if (!int.TryParse(codeLine.FirstOperand, out operand))
                                    if (!codeLine.FirstOperand.Equals("?")) // ? резервирует слово в памяти
                                        throw new Exception("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand);

                                if (operand < 0 || operand > maxMemoryAdr)
                                    throw new Exception("Число вне допустимого диапазона. Строка кода: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);

                                SupportLine supportLine = new SupportLine();
                                if (!codeLine.FirstOperand.Equals("?"))
                                    supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), Convert.ToString(operand), "");
                                else
                                    supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand, ""); // резервируем слово в памяти
                                tableSupport.Add(supportLine);
                                addressCounter += 3;
                            }
                            break;
                        case "BYTE":
                            {
                                // Допускаются только числа от 0 до 255
                                int operand;
                                if (int.TryParse(codeLine.FirstOperand, out operand)) 
                                {
                                    if (operand < 0 || operand > 255)
                                        throw new Exception("Операнд вне допустимого диапазона. Строка кода: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                    SupportLine supportLine = new SupportLine();
                                    supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), Convert.ToString(operand), "");
                                    tableSupport.Add(supportLine);
                                    addressCounter++;
                                }
                                // проверяем, это строка или это шестнадцатеричное число
                                else if ((codeLine.FirstOperand.Length > 3) && (codeLine.FirstOperand[1].Equals('"')) && (codeLine.FirstOperand[codeLine.FirstOperand.Length - 1].Equals('"')))
                                {
                                    // если C, то это строка
                                    if (codeLine.FirstOperand[0].Equals('C'))
                                    {
                                        string str = codeLine.FirstOperand.Substring(2, codeLine.FirstOperand.Length - 3);
                                        addressCounter += str.Length;
                                        if (addressCounter > maxMemoryAdr)
                                            throw new Exception("Произошло переполнение памяти");
                                        SupportLine supportLine = new SupportLine();
                                        supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand, "");
                                        tableSupport.Add(supportLine);
                                    }
                                    // если X, то это шестнадцатеричное число
                                    else if (codeLine.FirstOperand[0].Equals('X'))
                                    {
                                        string str = codeLine.FirstOperand.Substring(2, codeLine.FirstOperand.Length - 3);
                                        if ((str.Length % 2) != 0)
                                            throw new Exception("Невозможно преобразовать BYTE: нечетное количество символов. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                        addressCounter += str.Length / 2;
                                        if (addressCounter > maxMemoryAdr)
                                            throw new Exception("Произошло переполнение памяти");
                                        if (!Regex.IsMatch(codeLine.FirstOperand.ToUpper(), @"^[A-F0-9]+$"))
                                            throw new Exception("Шестнадцатеричное число введено неверно. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);

                                        SupportLine supportLine = new SupportLine();
                                        supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand.ToUpper(), "");
                                        tableSupport.Add(supportLine);
                                    }
                                    else
                                        throw new Exception("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                }
                                // если там "?", то просто резервируем один байт
                                else if (codeLine.FirstOperand.Equals("?"))
                                {
                                    addressCounter++;
                                    SupportLine supportLine = new SupportLine();
                                    supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand, "");
                                    tableSupport.Add(supportLine);
                                }
                                else
                                    throw new Exception("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);

                            }
                            break;
                        case "RESB":
                            {

                            }
                            break;
                        case "RESW":
                            {

                            }
                            break;
                        case "END":
                            {

                            }
                            break;
                    }
                }
                // значит это команда
                else 
                {

                }

                // проверка памяти
                if (addressCounter > maxMemoryAdr)
                    throw new Exception("Произошло переполнение памяти");
            }

            if (!EndFlag)
                throw new Exception("Точка выхода из программы не была найдена");
        }

        private BindingList<CodeLine> ToCodeLines(List<string> sourceCodeLines)
        {
            BindingList<CodeLine> cLines = new BindingList<CodeLine>();
            foreach (string el in sourceCodeLines)
            {
                // Разбиваем на 3 составляющие (если 2 слова, то на первом месте будет пусто)
                CodeLine codeLine = new CodeLine();
                string[] x = el.Split(' ');
                if (x.Length == 2)
                {
                    codeLine.Label = null;
                    codeLine.MKOP = x[0];
                    codeLine.FirstOperand = x[1].Split(',')[0];
                    codeLine.SecondOperand = x[1].Split(',')[1];
                }
                else if (x.Length == 3)
                {
                    codeLine.Label = x[0];
                    codeLine.MKOP = x[1];
                    codeLine.FirstOperand = x[2].Split(',')[0];
                    codeLine.SecondOperand = x[2].Split(',')[1];
                }
                else if (x.Length == 0)
                {
                    continue;
                }
                else
                {
                    throw new Exception("Обнаружена недопустимая команда: " + el + "\n");
                }
                cLines.Add(codeLine);
            }
            return cLines;
        }

        private bool TsiContainsName (string name)
        {
            foreach (SymbolicName symbolicName in tableSymbolicNames)
                if (symbolicName.Name.Equals(name))
                    return true;
            return false;
        }

        private static bool IsDirective(string MKOP)
        {
            string[] system_directives = { "START", "END", "BYTE", "WORD", "RESB", "RESW" };

            if (Array.IndexOf(system_directives, MKOP.ToUpper()) > -1)
                return true;
               
            return false;
        }

        private string GetAddressCounter()
        {
            string addr = Convert.ToString(addressCounter, 16);
            return new string('0', 6 - addr.Length) + addr;
        }
    }
}

/*
Program START 100
JMP L1
A1 RESB 0A
A2 RESW 10
B1 WORD 100
B2 BYTE 0F1
S1 BYTE x'2f4c0008'
S2 BYTE C'HEL0_o'
L1 LOADR1 B1
LOADR2 B2
ADD R1,R2
SAVER1 B1
END 100
*/