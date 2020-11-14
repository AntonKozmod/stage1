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
        private bool FirstPass(List<string> sourceCodeLines)
        {
            if (sourceCodeLines.Count() != 0)
            {
                codeLines = new BindingList<CodeLine>();
                foreach (string el in sourceCodeLines)
                {
                    // Разбиваем на 3 составляющие (если 2 слова, то на первом месте будет пусто)
                    CodeLine codeLine = new CodeLine();
                    string[] x = el.Split(' ');
                    if (x.Length == 2)
                    {
                        codeLine.Label = null;
                        codeLine.MKOP = x[0];
                        codeLine.Operands = x[1];
                    }
                    else if (x.Length == 3)
                    {
                        codeLine.Label = x[0];
                        codeLine.MKOP = x[1];
                        codeLine.Operands = x[2];
                    }
                    else
                    {
                        textBoxFirstErrors.Text += "Обнаружена недопустимая команда: " + el + "\n";
                        return false;
                    }
                    codeLines.Add(codeLine);
                }
                return true;

            }
            else
            {
                //MessageBox.Show("Исходный текст не задан", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstErrors.Text += "Исходный текст не задан\n";
            }       
            
            return false;
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