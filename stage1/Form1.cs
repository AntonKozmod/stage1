using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stage1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonUpload_Click(new object(), new EventArgs());
        }

        private int maxMemoryAdr = 16777215;

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;

            textBoxFirstErrors.Text = "";
            textBoxSecondErrors.Text = "";
            textBoxBinCode.Text = "";
            errorDetected = false;
            errors = "";
            StartFlag = false;
            EndFlag = false;

            try
            {
                FirstPass(textBoxSource.Lines.ToList(), textBoxTKO.Lines.ToList());
                if (errorDetected)
                {
                    textBoxFirstErrors.Text += errors;
                    button2.Enabled = false;
                    StartFlag = false;
                    EndFlag = false;
                }
                else
                {
                    dataGridViewSupport.DataSource = tableSupport;
                    dataGridViewTSI.DataSource = tableSymbolicNames;
                }
            }
            catch (Exception ex)
            {
                textBoxFirstErrors.Text += errors + "\n" + "Необработанное исключение: " + ex.Message + "\n";
                button2.Enabled = false;
                StartFlag = false;
                EndFlag = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            textBoxSecondErrors.Text = "";
            errorDetected = false;
            errors = "";
            StartFlag = false;
            EndFlag = false;

            try
            {
                SecondPass();
                if (errorDetected)
                {
                    textBoxSecondErrors.Text += errors;
                    button2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                textBoxFirstErrors.Text += errors + "\n" + "Необработанное исключение: " + ex.Message + "\n";
                button2.Enabled = false;
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            string path = "source_data.txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("Файл данных \"source_data.txt\" не найден", "Ошибка загрузки из файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] lines = File.ReadAllLines(path);
            textBoxSource.Text = textBoxTKO.Text = "";
            bool sourceCode = false; // true - если вводим строки исходного кода
                                     // false - если вводим строки таблицы кодов операций
            foreach (string line in lines)
            {
                if (line.Length == 0)
                    continue;
                if (!sourceCode && line.Equals("*source code*"))
                {
                    sourceCode = true;
                    continue;
                }
                if (sourceCode && line.Equals("*table of operation codes*"))
                {
                    sourceCode = false;
                    continue;
                }

                if (sourceCode)
                {
                    textBoxSource.AppendText(line + "\n");
                }
                else
                {
                    textBoxTKO.AppendText(line + "\n");
                }

            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string path = "source_data.txt";
            string text = "*source code*\n" + textBoxSource.Text + "\n*table of operation codes*\n" + textBoxTKO.Text;
            File.Delete(path);
            File.AppendAllText(path, text);
        }
    }
}
