using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stage1
{
    public partial class Form1 : Form
    {
        // Класс хранит необходимую промежуточную информацию.

        private int addressCounter;
        private int startAddress;
        private int endAddress;
        private string programName;

        private bool StartFlag = false; // Директива "START" найдена
        private bool EndFlag = false; // Директива "END" найдена

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

        //struct SymbolicName // Строка таблицы символьных имен
        //{
        //    public string Name;
        //    public string Address;
        //}
        private BindingList<SymbolicName> tableSymbolicNames; // Список строк таблицы символьных имен

        //struct SupportLine // Строка вспомогательной таблицы
        //{
        //    public string Label;
        //    public string MKOP;
        //    public string FirstOperand;
        //    public string SecondOperand;

        //    public void FillSupportLine(string Label, string MKOP, string FirstOperand, string SecondOperand)
        //    {
        //        this.Label = Label;
        //        this.MKOP = MKOP;
        //        this.FirstOperand = FirstOperand;
        //        this.SecondOperand = SecondOperand;
        //    }
        //}
        private BindingList<SupportLine> tableSupport;

        string[] registers = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10", "R11", "R12", "R13", "R14", "R15" };
    }

    public class SupportLine // Строка вспомогательной таблицы
    {
        public string Label { get; set; }
        public string MKOP { get; set; }
        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
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
    }
}
