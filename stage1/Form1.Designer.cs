﻿namespace stage1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxTKO = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxFirstErrors = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewTSI = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewSupport = new System.Windows.Forms.DataGridView();
            this.Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxBinCode = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSecondErrors = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTSI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSupport)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxTKO);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxSource);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 544);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // textBoxTKO
            // 
            this.textBoxTKO.Location = new System.Drawing.Point(6, 345);
            this.textBoxTKO.Name = "textBoxTKO";
            this.textBoxTKO.Size = new System.Drawing.Size(243, 193);
            this.textBoxTKO.TabIndex = 4;
            this.textBoxTKO.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Таблица кодов операций";
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(6, 32);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.Size = new System.Drawing.Size(243, 294);
            this.textBoxSource.TabIndex = 2;
            this.textBoxSource.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Исходный текст";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxFirstErrors);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dataGridViewTSI);
            this.groupBox2.Controls.Add(this.dataGridViewSupport);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(267, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 544);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(143, 399);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Ошибки первого прохода";
            // 
            // textBoxFirstErrors
            // 
            this.textBoxFirstErrors.Location = new System.Drawing.Point(6, 415);
            this.textBoxFirstErrors.Name = "textBoxFirstErrors";
            this.textBoxFirstErrors.ReadOnly = true;
            this.textBoxFirstErrors.Size = new System.Drawing.Size(422, 123);
            this.textBoxFirstErrors.TabIndex = 5;
            this.textBoxFirstErrors.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Таблица символических имен";
            // 
            // dataGridViewTSI
            // 
            this.dataGridViewTSI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTSI.ColumnHeadersVisible = false;
            this.dataGridViewTSI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewTSI.Location = new System.Drawing.Point(85, 244);
            this.dataGridViewTSI.Name = "dataGridViewTSI";
            this.dataGridViewTSI.ReadOnly = true;
            this.dataGridViewTSI.RowHeadersVisible = false;
            this.dataGridViewTSI.Size = new System.Drawing.Size(251, 152);
            this.dataGridViewTSI.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 115;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 115;
            // 
            // dataGridViewSupport
            // 
            this.dataGridViewSupport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSupport.ColumnHeadersVisible = false;
            this.dataGridViewSupport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Label,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridViewSupport.Location = new System.Drawing.Point(6, 32);
            this.dataGridViewSupport.Name = "dataGridViewSupport";
            this.dataGridViewSupport.ReadOnly = true;
            this.dataGridViewSupport.RowHeadersVisible = false;
            this.dataGridViewSupport.Size = new System.Drawing.Size(422, 193);
            this.dataGridViewSupport.TabIndex = 2;
            // 
            // Label
            // 
            this.Label.DataPropertyName = "Label";
            this.Label.HeaderText = "Label";
            this.Label.Name = "Label";
            this.Label.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MKOP";
            this.Column2.HeaderText = "MKOP";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "FirstOperand";
            this.Column3.HeaderText = "FirstOperand";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "SecondOperand";
            this.Column4.HeaderText = "SecondOperand";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Вспомогательная таблица";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxBinCode);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBoxSecondErrors);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(703, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(254, 544);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // textBoxBinCode
            // 
            this.textBoxBinCode.Location = new System.Drawing.Point(6, 32);
            this.textBoxBinCode.Name = "textBoxBinCode";
            this.textBoxBinCode.ReadOnly = true;
            this.textBoxBinCode.Size = new System.Drawing.Size(242, 294);
            this.textBoxBinCode.TabIndex = 10;
            this.textBoxBinCode.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Ошибки второго прохода";
            // 
            // textBoxSecondErrors
            // 
            this.textBoxSecondErrors.Location = new System.Drawing.Point(6, 415);
            this.textBoxSecondErrors.Name = "textBoxSecondErrors";
            this.textBoxSecondErrors.ReadOnly = true;
            this.textBoxSecondErrors.Size = new System.Drawing.Size(242, 123);
            this.textBoxSecondErrors.TabIndex = 7;
            this.textBoxSecondErrors.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Двоичный код";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(591, 562);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Первый проход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(709, 562);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Второй проход";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(18, 562);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(117, 23);
            this.buttonUpload.TabIndex = 6;
            this.buttonUpload.Text = "Загрузить из файла";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(147, 562);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить в файл";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 590);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Логика работы двухпросмотрового ассемблера";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTSI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSupport)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox textBoxSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox textBoxTKO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewSupport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewTSI;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox textBoxFirstErrors;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox textBoxSecondErrors;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox textBoxBinCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonSave;
    }
}

