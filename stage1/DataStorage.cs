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

        private struct CodeLine // Строка исходного кода
        {
            string Label;
            string MKOP;
            string FirstOperand;
            string SecondOperand;
        }
        BindingList<CodeLine> codeLines; // Список строк исходного кода

        struct OperationCode // Строка таблицы кодов операций
        {
            string MKOP;
            UInt32 HexCode;
            UInt32 CodeLength;
        }
        BindingList<OperationCode> operationCodes; // Список кодов операций

        private struct SymbolicName // Строка таблицы символьных имен
        {
            string Name;
            string Address;
        }
        BindingList<SymbolicName> symbolicNames; // Список строк таблицы символьных имен

        private struct SupportLine // Строка вспомогательной таблицы
        {
            string Label;
            string MKOP;
            string FirstOperand;
            string SecondOperand;
        }

        private List<OperationCode> directives;
    }
}
