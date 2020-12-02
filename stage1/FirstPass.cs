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
        private void FirstPass(List<string> sourceCodeLines, List<string> sourceOperationCodesLines)
        {
            if (sourceCodeLines.Count() != 0)
                codeLines = ToCodeLines(sourceCodeLines); // преобразуем исходный текст к списку команд, с которым можно работать
            else
                NewException("Исходный текст не задан");

            if (sourceOperationCodesLines.Count() != 0) // преобразуем исходную таблицу кодов операций в списку структур, с которым можно работать
                operationCodes = ToOperationCodes(sourceOperationCodesLines);
            else
                NewException("Таблица кодов операций не задана");


            tableSymbolicNames = new BindingList<SymbolicName>(); // инициализируем таблицу символьных имен
            tableSupport = new BindingList<SupportLine>(); // инициализируем вспомогательную таблицу 

            foreach (CodeLine codeLine in codeLines)
            {
                if (!StartFlag)
                {
                    // Сюда попадем только при первой итерации
                    // Если здесь не директива START, то генерируется ошибка
                    if (!codeLine.MKOP.ToUpper().Equals("START"))
                        NewException("В начале программы директива START не была обнаружена");
                }

                if (EndFlag)
                    break;

                if (!TsiContainsName(codeLine.Label))
                {
                    if (codeLine.Label != null && StartFlag)
                    {
                        SymbolicName symbolicName = new SymbolicName(codeLine.Label, string.Format("{0:X6}", addressCounter).ToUpper());
                        tableSymbolicNames.Add(symbolicName);
                    }
                }
                else
                    NewException("Найдена уже существующая метка: " + codeLine.Label);

                if (IsDirective(codeLine.MKOP))
                {
                    switch (codeLine.MKOP.ToUpper())
                    {
                        case "START":
                            {
                                if (StartFlag)
                                    NewException("Обнаружено второе появление директивы START");
                                
                                StartFlag = true;
                                
                                if (codeLine.Label == null)
                                    NewException("Имя программы не задано");
                                if (codeLine.Label.Length > 10)
                                    NewException("Превышена длина имени программы (больше чем 10 символов)");
                                // Проверяем, что адрес начала программы состоит из символов шестнадцатеричной СС
                                if (!Regex.IsMatch(codeLine.FirstOperand.ToUpper(), @"^[A-F0-9]+$"))
                                    NewException("Неверный адрес начала программы");
                                
                                // Преобразуем в 10чную СС
                                addressCounter = Convert.ToInt32(codeLine.FirstOperand, 16);
                                startAddress = addressCounter;
                                
                                if (startAddress == 0)
                                    NewException("Адрес начала программы не может быть равен 0");
                                if (startAddress > maxMemoryAdr)
                                    NewException("Адрес программы выходит за диапазон памяти");

                                SupportLine supportLine = new SupportLine(codeLine.Label, codeLine.MKOP.ToUpper(), string.Format("{0:X6}", addressCounter).ToUpper(), null);
                                tableSupport.Add(supportLine);
                                programName = supportLine.Label;
                            }
                            break;
                        case "WORD":
                            {
                                // Допускаются только 16-чные числа (положительные и до maxMemoryAddr)

                                int operand = 0;
                                
                                try
                                {
                                    operand = Convert.ToInt32(codeLine.FirstOperand, 16);
                                }
                                catch (Exception ex)
                                {
                                    if (!codeLine.FirstOperand.Equals("?")) // ? резервирует слово в памяти
                                        NewException("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand);
                                }

                                if (operand < 0 || operand > maxMemoryAdr)
                                    NewException("Число вне допустимого диапазона. Строка кода: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);

                                SupportLine supportLine = new SupportLine();
                                if (!codeLine.FirstOperand.Equals("?"))
                                    supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), Convert.ToString(operand), null);
                                else
                                    supportLine.FillSupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand, null); // резервируем слово в памяти
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
                                        NewException("Операнд вне допустимого диапазона. Строка кода: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                    SupportLine supportLine = new SupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), operand.ToString(), null);
                                    tableSupport.Add(supportLine);
                                    addressCounter++;
                                }
                                // проверяем, это строка или это шестнадцатеричное число
                                else if ((codeLine.FirstOperand.Length > 3) && (codeLine.FirstOperand[1].Equals('"')) && (codeLine.FirstOperand[codeLine.FirstOperand.Length - 1].Equals('"')))
                                {
                                    // если C, то это строка
                                    if (codeLine.FirstOperand[0].Equals('C'))
                                    {
                                        if (addressCounter > maxMemoryAdr)
                                            NewException("Произошло переполнение памяти");
                                        SupportLine supportLine = new SupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand, null);
                                        tableSupport.Add(supportLine);
                                        string str = codeLine.FirstOperand.Substring(2, codeLine.FirstOperand.Length - 3);
                                        addressCounter += str.Length;
                                    }
                                    // если X, то это шестнадцатеричное число
                                    else if (codeLine.FirstOperand[0].Equals('X'))
                                    {
                                        string str = codeLine.FirstOperand.Substring(2, codeLine.FirstOperand.Length - 3);
                                        if ((str.Length % 2) != 0)
                                            NewException("Невозможно преобразовать BYTE: нечетное количество символов. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                        if (!Regex.IsMatch(str.ToUpper(), @"^[A-F0-9]+$"))
                                            NewException("Шестнадцатеричное число введено неверно. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);

                                        SupportLine supportLine = new SupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand.ToUpper(), null);
                                        tableSupport.Add(supportLine);
                                        addressCounter += str.Length / 2;
                                    }
                                    else
                                        NewException("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                }
                                // если там "?", то просто резервируем один байт
                                else if (codeLine.FirstOperand.Equals("?"))
                                {
                                    SupportLine supportLine = new SupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), codeLine.FirstOperand, null);
                                    tableSupport.Add(supportLine);
                                    addressCounter++;
                                }
                                else
                                    NewException("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);

                            }
                            break;
                        case "RESB":
                            {

                                int operand = 0;
                                try
                                {
                                    operand = Convert.ToInt32(codeLine.FirstOperand, 16);
                                } 
                                catch (Exception ex)
                                {
                                    NewException("Невозможно преобразовать в число. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                }
                                if (operand <= 0)
                                    NewException("Указано недопустимое количество байт. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                SupportLine supportLine = new SupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), operand.ToString(), null);
                                tableSupport.Add(supportLine);
                                addressCounter += operand;
                            }
                            break;
                        case "RESW":
                            {
                                int operand = 0;
                                try
                                {
                                    operand = Convert.ToInt32(codeLine.FirstOperand, 16);
                                }
                                catch (Exception ex)
                                {
                                    NewException("Невозможно преобразовать в число. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                }
                                if (operand <= 0)
                                    NewException("Недопустимое количество слов. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                SupportLine supportLine = new SupportLine(GetAddressCounter(), codeLine.MKOP.ToUpper(), operand.ToString(), null);
                                tableSupport.Add(supportLine);
                                addressCounter += operand * 3;
                            }
                            break;
                        case "END":
                            {
                                if (!StartFlag || EndFlag)
                                    NewException("Ошибка в директиве END");
                                EndFlag = true;
                                if (codeLine.FirstOperand == null)
                                    endAddress = startAddress;
                                else
                                {
                                    if (!Regex.IsMatch(codeLine.FirstOperand.ToUpper(), @"^[A-F0-9]+$"))
                                        NewException("Неверный адрес выхода из программы");
                                    endAddress = Convert.ToInt32(codeLine.FirstOperand.ToUpper(), 16);
                                    if (endAddress < startAddress || endAddress > addressCounter)
                                        NewException("Неверный адрес выхода из программы");
                                }
                                programLength = addressCounter - startAddress;
                                // преобразуем обратно в 16 сс и допишем нули
                                SupportLine supportLine = new SupportLine(null, codeLine.MKOP.ToUpper(), string.Format("{0:X6}", endAddress).ToUpper(), null);
                                tableSupport.Add(supportLine);
                            }
                            break;
                    }
                }
                // значит это команда
                else 
                {
                    OperationCode operationCode;
                    if (!TkoContainsMkop(codeLine.MKOP.ToUpper(), out operationCode))
                        NewException("МКОП не найден в таблице кодов операций. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                    switch (operationCode.CodeLength)
                    {
                        case 1: // Длина команды 1
                            {
                                // Просто сдвигаем на два разряда влево, т.к. это число и адресация непосредственная
                                int addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4;
                                // переводим обратно в 16 с.с.
                                string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                SupportLine supportLine = new SupportLine(GetAddressCounter(), mkop.ToUpper(), null, null);
                                tableSupport.Add(supportLine);
                                addressCounter++;
                            }
                            break;
                        case 2: // Длина команды 2
                            {
                                int firstOperand = 0;
                                // Попробуем преобразовать операнд в число
                                bool parsed = false;
                                try
                                {
                                    firstOperand = Convert.ToInt32(codeLine.FirstOperand, 16);
                                    parsed = true;
                                }
                                catch (Exception ex)
                                {
                                    // значит не число
                                    parsed = false;
                                }
                                if (parsed)
                                {
                                    if (firstOperand < 0 || firstOperand > 255)
                                        NewException("Значение первого операнда вне допустимого диапазона. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                    // Просто сдвигаем на два разряда влево, т.к. это число и адресация непосредственная
                                    int addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4;
                                    string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                    SupportLine supportLine = new SupportLine(GetAddressCounter(), mkop, firstOperand.ToString(), null);
                                    tableSupport.Add(supportLine);
                                    addressCounter += 2;
                                }
                                // Если в операнде не число, значит регистр
                                else
                                {
                                    if ((!registers.Contains(codeLine.FirstOperand.ToUpper())) || (!registers.Contains(codeLine.SecondOperand.ToUpper())))
                                        NewException("Ошибка в операндах. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                                    // т.к. используются регистры, то это непосредственная адресация - сдвигаем на два разряда влево
                                    int addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4;
                                    string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                    SupportLine supportLine = new SupportLine(GetAddressCounter(), mkop, codeLine.FirstOperand.ToUpper(), codeLine.SecondOperand.ToUpper());
                                    tableSupport.Add(supportLine);
                                    addressCounter += 2;
                                }
                            }
                            break;
                        case 3: // Длина команды 3
                            {
                                // Используется прямая адресация
                                int addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4 + 1;
                                string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                SupportLine supportLine = new SupportLine(GetAddressCounter(), mkop, codeLine.FirstOperand, null);
                                tableSupport.Add(supportLine);
                                addressCounter += 3;
                            }
                            break;
                        case 4: // Длина команды 4
                            {
                                // Используется прямая адресация
                                int addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4 + 1;
                                string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                SupportLine supportLine = new SupportLine(GetAddressCounter(), mkop, codeLine.FirstOperand, codeLine.SecondOperand);
                                tableSupport.Add(supportLine);
                                addressCounter += 4;
                            }
                            break;
                        default:
                            NewException("Превышен размер команды. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                            break;
                    }
                }

                // проверка памяти
                if (addressCounter > maxMemoryAdr)
                    NewException("Произошло переполнение памяти");
            }

            if (!EndFlag)
                NewException("Точка выхода из программы не была найдена");
        }

        private BindingList<CodeLine> ToCodeLines(List<string> sourceCodeLines)
        {
            BindingList<CodeLine> cLines = new BindingList<CodeLine>();
            foreach (string el in sourceCodeLines)
            {
                if (el.Length == 0)
                    continue;
                // Разбиваем на 3 составляющие (если 2 слова, то на первом месте будет пусто)
                CodeLine codeLine = new CodeLine();
                string[] x = el.Split(' ');
                if (x.Length == 1)
                {
                    codeLine.Label = null;
                    codeLine.MKOP = x[0];
                    codeLine.FirstOperand = codeLine.SecondOperand = null;
                }
                if (x.Length == 2)
                {
                    codeLine.Label = null;
                    codeLine.MKOP = x[0];
                    string[] opernads = x[1].Split(',');
                    if (opernads.Length == 1)
                    {
                        codeLine.FirstOperand = opernads[0];
                        codeLine.SecondOperand = null;
                    }
                    else if (opernads.Length == 2)
                    {
                        codeLine.FirstOperand = opernads[0];
                        codeLine.SecondOperand = opernads[1];
                    }
                    else
                        NewException("Ошибка ввода операндов. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                }
                else if (x.Length == 3)
                {
                    codeLine.Label = x[0];
                    codeLine.MKOP = x[1];
                    string[] opernads = x[2].Split(',');
                    if (opernads.Length == 1)
                    {
                        codeLine.FirstOperand = opernads[0];
                        codeLine.SecondOperand = null;
                    }
                    else if (opernads.Length == 2)
                    {
                        codeLine.FirstOperand = opernads[0];
                        codeLine.SecondOperand = opernads[1];
                    }
                    else
                        NewException("Ошибка ввода операндов. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.FirstOperand + " " + codeLine.SecondOperand);
                }
                else if (x.Length == 0)
                    continue;
                else
                    NewException("Обнаружена недопустимая команда: " + el + "\n");
                cLines.Add(codeLine);
            }
            return cLines;
        }

        private BindingList<OperationCode> ToOperationCodes(List<string> sourceOperationCodes)
        {
            BindingList<OperationCode> tkoLines = new BindingList<OperationCode>();
            foreach (string el in sourceOperationCodes)
            {
                if (el.Length == 0)
                    continue;
                // Разбиваем на 3 составляющие
                string[] x = el.Split(' ');
                
                try
                {
                    if (x.Length < 3)
                        NewException("Ошибка в таблице кодов операций. Строка: " + el);
                    if (!Regex.IsMatch(x[1].ToUpper(), @"^[A-F0-9]+$"))
                        NewException("Ошибка кода операции в строке: " + el);
                    if (Convert.ToInt32(x[1].ToUpper(), 16) > Convert.ToInt32("3F", 16))
                        NewException("Код операции должен быть меньше 3F");
                    int num;
                    if (!int.TryParse(x[2], out num) || num < 0 || num > 4)
                        NewException("Ошибка длины операции в строке: " + el);
                    OperationCode operationCode = new OperationCode
                    {
                        MKOP = x[0].ToUpper(),
                        HexCode = x[1].ToUpper(),
                        CodeLength = num
                    };
                    tkoLines.Add(operationCode);
                }
                catch (Exception ex)
                {
                    NewException(ex.Message);
                }
                
            }
            return tkoLines;
        }


        private string GetAddressCounter()
        {
            return string.Format("{0:X6}", addressCounter).ToUpper();
        }
    }
}