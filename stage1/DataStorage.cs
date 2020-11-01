using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stage1
{
    class DataStorage
    {
        // Класс для ввода и вывода информации на главную форму.
        // Приводит данные, полученные с формы, к виду, пригодному для ассемблирования.
        // Хранит необходимую промежуточную информацию.

        // Элементы главной формы
        private RichTextBox textBoxSource;
        private RichTextBox textBoxTKO;
        private DataGridView dataGridViewSupport;
        private DataGridView dataGridViewTSI;
        private RichTextBox textBoxFirstErrors;
        private RichTextBox textBoxSecondErrors;
        private RichTextBox textBoxBinCode;

        public DataStorage(RichTextBox _textBoxSource,
                    RichTextBox _textBoxTKO,
                    DataGridView _dataGridViewSupport,
                    DataGridView _dataGridViewTSI,
                    RichTextBox _textBoxFirstErrors,
                    RichTextBox _textBoxSecondErrors,
                    RichTextBox _textBoxBinCode)
        {
            textBoxSource = _textBoxSource;
            textBoxTKO = _textBoxTKO;
            dataGridViewSupport = _dataGridViewSupport;
            dataGridViewTSI = _dataGridViewTSI;
            textBoxFirstErrors = _textBoxFirstErrors;
            textBoxSecondErrors = _textBoxSecondErrors;
            textBoxBinCode = _textBoxBinCode;
        }

        public bool initializeFirst ()
        {
            return false;
        }
    }
}
