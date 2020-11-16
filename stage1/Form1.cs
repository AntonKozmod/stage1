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
            button1.Enabled = false;

            textBoxSource.ReadOnly = true;
            textBoxTKO.ReadOnly = true;
            textBoxFirstErrors.Text = "";
            textBoxSecondErrors.Text = "";


            try
            {
                FirstPass(textBoxSource.Lines.ToList(), textBoxTKO.Lines.ToList());
                dataGridViewSupport.DataSource = tableSupport;
                dataGridViewTSI.DataSource = tableSymbolicNames;
            }
            catch (Exception ex)
            {
                textBoxFirstErrors.Text += ex.Message + "\n";
                button2.Enabled = false;
                button1.Enabled = true;
                textBoxSource.ReadOnly = false;
                textBoxTKO.ReadOnly = false;
                StartFlag = false;
                EndFlag = false;
            }

        }
    }
}
