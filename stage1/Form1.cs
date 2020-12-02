using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        }
    }
}
