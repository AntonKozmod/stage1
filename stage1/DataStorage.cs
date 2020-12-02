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
    public class SupportLine // Строка вспомогательной таблицы
    {
        public string Label { get; set; }
        public string MKOP { get; set; }
        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public SupportLine(string Label, string MKOP, string FirstOperand, string SecondOperand)
        {
            this.Label = Label;
            this.MKOP = MKOP;
            this.FirstOperand = FirstOperand;
            this.SecondOperand = SecondOperand;
        }
        public SupportLine() { }
        public void FillSupportLine(string Label, string MKOP, string FirstOperand, string SecondOperand)
        {
            this.Label = Label;
            this.MKOP = MKOP;
            this.FirstOperand = FirstOperand;
            this.SecondOperand = SecondOperand;
        }
    }
    public class SymbolicName // Строка таблицы символьных имен
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public SymbolicName(string Name, string Address)
        {
            this.Name = Name;
            this.Address = Address;
        }
    }
    public partial class Form1 : Form
    {
        // Класс хранит необходимую промежуточную информацию.

        private int addressCounter;
        private int startAddress;
        private int endAddress;
        private int programLength;
        private string programName;

        private bool StartFlag = false; // Директива "START" найдена
        private bool EndFlag = false; // Директива "END" найдена

        private bool errorDetected = false;
        private string errors;

        struct CodeLine // Строка исходного кода
        {
            public string Label;
            public string MKOP;
            public string FirstOperand;
            public string SecondOperand;
        }
        private BindingList<CodeLine> codeLines; // Список строк исходного кода

        struct OperationCode // Строка таблицы кодов операций
        {
            public string MKOP;
            public string HexCode;
            public int CodeLength;
        }
        private BindingList<OperationCode> operationCodes; // Список кодов операций
                
        private BindingList<SymbolicName> tableSymbolicNames; // Список строк таблицы символьных имен

        private BindingList<SupportLine> tableSupport;

        private static string[] registers = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10", "R11", "R12", "R13", "R14", "R15" };

        private static string[] system_directives = { "START", "END", "BYTE", "WORD", "RESB", "RESW" };

        // Проверки
        private bool TsiContainsName(string name)
        {
            foreach (SymbolicName symbolicName in tableSymbolicNames)
                if (symbolicName.Name.Equals(name))
                    return true;
            return false;
        }
        private bool TkoContainsMkop(string MKOP, out OperationCode operationCode)
        {
            operationCode = new OperationCode { MKOP = null, HexCode = null, CodeLength = 0 };
            foreach (OperationCode oc in operationCodes)
                if (oc.MKOP.Equals(MKOP.ToUpper()))
                {
                    operationCode = oc;
                    return true;
                }
            return false;
        }
        private static bool IsDirective(string MKOP)
        {
            if (Array.IndexOf(system_directives, MKOP.ToUpper()) > -1)
                return true;
            return false;
        }
        private bool IsOperation (string str)
        {
            return operationCodes.Any(x => x.MKOP.Equals(str.ToUpper()));
        } 
        private bool IsRegister (string str)
        {
            return registers.Contains(str.ToUpper());
        }
        private bool IsSymbolicName(string str)
        {
            if (Regex.IsMatch(str, @"[^A-Za-z0-9_]"))
                return false;
            if (!Regex.IsMatch(str, @"\A[A-Za-z]"))
                return false;
            if (IsDirective(str))
                return false;
            if (IsOperation(str))
                return false;
            if (IsRegister(str))
                return false;
            return true;
        }
        private void NewException(string message)
        {
            errorDetected = true;
            errors += message + "\n";
        }
    }

   
}
