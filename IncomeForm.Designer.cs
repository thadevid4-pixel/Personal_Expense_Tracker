namespace Personal_Expense_Tracker
{
    partial class IncomeForm1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Income_Clear = new System.Windows.Forms.Button();
            this.btn_Income_Update = new System.Windows.Forms.Button();
            this.btn_Income_Delete = new System.Windows.Forms.Button();
            this.btn_Income_Add = new System.Windows.Forms.Button();
            this.Income_date = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.Income_Description = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Income_income = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Income_Item = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Income_category = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(35, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 351);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Income List";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btn_Income_Clear);
            this.panel2.Controls.Add(this.btn_Income_Update);
            this.panel2.Controls.Add(this.btn_Income_Delete);
            this.panel2.Controls.Add(this.btn_Income_Add);
            this.panel2.Controls.Add(this.Income_date);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.Income_Description);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.Income_income);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Income_Item);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Income_category);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(33, 406);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 320);
            this.panel2.TabIndex = 1;
            // 
            // btn_Income_Clear
            // 
            this.btn_Income_Clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Income_Clear.FlatAppearance.BorderSize = 0;
            this.btn_Income_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Income_Clear.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Income_Clear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Income_Clear.Location = new System.Drawing.Point(611, 256);
            this.btn_Income_Clear.Name = "btn_Income_Clear";
            this.btn_Income_Clear.Size = new System.Drawing.Size(82, 35);
            this.btn_Income_Clear.TabIndex = 14;
            this.btn_Income_Clear.Text = "Clear";
            this.btn_Income_Clear.UseVisualStyleBackColor = false;
            this.btn_Income_Clear.Click += new System.EventHandler(this.btn_Income_Clear_Click);
            // 
            // btn_Income_Update
            // 
            this.btn_Income_Update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Income_Update.FlatAppearance.BorderSize = 0;
            this.btn_Income_Update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Income_Update.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Income_Update.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Income_Update.Location = new System.Drawing.Point(336, 256);
            this.btn_Income_Update.Name = "btn_Income_Update";
            this.btn_Income_Update.Size = new System.Drawing.Size(82, 35);
            this.btn_Income_Update.TabIndex = 13;
            this.btn_Income_Update.Text = "Update";
            this.btn_Income_Update.UseVisualStyleBackColor = false;
            this.btn_Income_Update.Click += new System.EventHandler(this.btn_Income_Update_Click);
            // 
            // btn_Income_Delete
            // 
            this.btn_Income_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Income_Delete.FlatAppearance.BorderSize = 0;
            this.btn_Income_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Income_Delete.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Income_Delete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Income_Delete.Location = new System.Drawing.Point(483, 256);
            this.btn_Income_Delete.Name = "btn_Income_Delete";
            this.btn_Income_Delete.Size = new System.Drawing.Size(82, 35);
            this.btn_Income_Delete.TabIndex = 12;
            this.btn_Income_Delete.Text = "Delete";
            this.btn_Income_Delete.UseVisualStyleBackColor = false;
            this.btn_Income_Delete.Click += new System.EventHandler(this.btn_Income_Delete_Click);
            // 
            // btn_Income_Add
            // 
            this.btn_Income_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Income_Add.FlatAppearance.BorderSize = 0;
            this.btn_Income_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Income_Add.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Income_Add.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Income_Add.Location = new System.Drawing.Point(204, 256);
            this.btn_Income_Add.Name = "btn_Income_Add";
            this.btn_Income_Add.Size = new System.Drawing.Size(82, 35);
            this.btn_Income_Add.TabIndex = 11;
            this.btn_Income_Add.Text = "Add";
            this.btn_Income_Add.UseVisualStyleBackColor = false;
            this.btn_Income_Add.Click += new System.EventHandler(this.btn_Income_Add_Click);
            // 
            // Income_date
            // 
            this.Income_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Income_date.Location = new System.Drawing.Point(590, 148);
            this.Income_date.Name = "Income_date";
            this.Income_date.Size = new System.Drawing.Size(260, 22);
            this.Income_date.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(517, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 19);
            this.label6.TabIndex = 9;
            this.label6.Text = "Date : ";
            // 
            // Income_Description
            // 
            this.Income_Description.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Income_Description.Location = new System.Drawing.Point(590, 47);
            this.Income_Description.Multiline = true;
            this.Income_Description.Name = "Income_Description";
            this.Income_Description.Size = new System.Drawing.Size(375, 85);
            this.Income_Description.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(477, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "Description : ";
            // 
            // Income_income
            // 
            this.Income_income.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Income_income.Location = new System.Drawing.Point(158, 149);
            this.Income_income.Name = "Income_income";
            this.Income_income.Size = new System.Drawing.Size(237, 26);
            this.Income_income.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(82, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Income : ";
            // 
            // Income_Item
            // 
            this.Income_Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Income_Item.Location = new System.Drawing.Point(158, 99);
            this.Income_Item.Name = "Income_Item";
            this.Income_Item.Size = new System.Drawing.Size(237, 26);
            this.Income_Item.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(102, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Item : ";
            // 
            // Income_category
            // 
            this.Income_category.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Income_category.FormattingEnabled = true;
            this.Income_category.Location = new System.Drawing.Point(158, 47);
            this.Income_category.Name = "Income_category";
            this.Income_category.Size = new System.Drawing.Size(237, 28);
            this.Income_category.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Category : ";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(66, 78);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(932, 278);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // IncomeForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "IncomeForm1";
            this.Size = new System.Drawing.Size(1145, 748);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Income_category;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox Income_Item;
        private System.Windows.Forms.MaskedTextBox Income_income;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Income_Description;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker Income_date;
        private System.Windows.Forms.Button btn_Income_Clear;
        private System.Windows.Forms.Button btn_Income_Update;
        private System.Windows.Forms.Button btn_Income_Delete;
        private System.Windows.Forms.Button btn_Income_Add;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}
