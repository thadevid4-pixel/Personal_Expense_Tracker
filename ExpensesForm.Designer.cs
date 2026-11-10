namespace Personal_Expense_Tracker
{
    partial class ExpensesForm1
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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btn_Expense_Clear = new System.Windows.Forms.Button();
            this.btn_Expense_Update = new System.Windows.Forms.Button();
            this.btn_Expense_Delete = new System.Windows.Forms.Button();
            this.btn_Expense_Add = new System.Windows.Forms.Button();
            this.Expense_date = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Expense_Description = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Expense_cost = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Expense_Item = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Expense_category = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGridView2.Location = new System.Drawing.Point(31, 52);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(932, 278);
            this.dataGridView2.TabIndex = 7;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // btn_Expense_Clear
            // 
            this.btn_Expense_Clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Expense_Clear.FlatAppearance.BorderSize = 0;
            this.btn_Expense_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Expense_Clear.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Expense_Clear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Expense_Clear.Location = new System.Drawing.Point(611, 256);
            this.btn_Expense_Clear.Name = "btn_Expense_Clear";
            this.btn_Expense_Clear.Size = new System.Drawing.Size(82, 35);
            this.btn_Expense_Clear.TabIndex = 14;
            this.btn_Expense_Clear.Text = "Clear";
            this.btn_Expense_Clear.UseVisualStyleBackColor = false;
            this.btn_Expense_Clear.Click += new System.EventHandler(this.btn_Expense_Clear_Click);
            // 
            // btn_Expense_Update
            // 
            this.btn_Expense_Update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Expense_Update.FlatAppearance.BorderSize = 0;
            this.btn_Expense_Update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Expense_Update.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Expense_Update.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Expense_Update.Location = new System.Drawing.Point(336, 256);
            this.btn_Expense_Update.Name = "btn_Expense_Update";
            this.btn_Expense_Update.Size = new System.Drawing.Size(82, 35);
            this.btn_Expense_Update.TabIndex = 13;
            this.btn_Expense_Update.Text = "Update";
            this.btn_Expense_Update.UseVisualStyleBackColor = false;
            this.btn_Expense_Update.Click += new System.EventHandler(this.btn_Expense_Update_Click);
            // 
            // btn_Expense_Delete
            // 
            this.btn_Expense_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Expense_Delete.FlatAppearance.BorderSize = 0;
            this.btn_Expense_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Expense_Delete.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Expense_Delete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Expense_Delete.Location = new System.Drawing.Point(483, 256);
            this.btn_Expense_Delete.Name = "btn_Expense_Delete";
            this.btn_Expense_Delete.Size = new System.Drawing.Size(82, 35);
            this.btn_Expense_Delete.TabIndex = 12;
            this.btn_Expense_Delete.Text = "Delete";
            this.btn_Expense_Delete.UseVisualStyleBackColor = false;
            this.btn_Expense_Delete.Click += new System.EventHandler(this.btn_Expense_Delete_Click);
            // 
            // btn_Expense_Add
            // 
            this.btn_Expense_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.btn_Expense_Add.FlatAppearance.BorderSize = 0;
            this.btn_Expense_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Expense_Add.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Expense_Add.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Expense_Add.Location = new System.Drawing.Point(204, 256);
            this.btn_Expense_Add.Name = "btn_Expense_Add";
            this.btn_Expense_Add.Size = new System.Drawing.Size(82, 35);
            this.btn_Expense_Add.TabIndex = 11;
            this.btn_Expense_Add.Text = "Add";
            this.btn_Expense_Add.UseVisualStyleBackColor = false;
            this.btn_Expense_Add.Click += new System.EventHandler(this.btn_Expense_Add_Click);
            // 
            // Expense_date
            // 
            this.Expense_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expense_date.Location = new System.Drawing.Point(590, 148);
            this.Expense_date.Name = "Expense_date";
            this.Expense_date.Size = new System.Drawing.Size(260, 22);
            this.Expense_date.TabIndex = 10;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Expenses List";
            // 
            // Expense_Description
            // 
            this.Expense_Description.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expense_Description.Location = new System.Drawing.Point(590, 47);
            this.Expense_Description.Multiline = true;
            this.Expense_Description.Name = "Expense_Description";
            this.Expense_Description.Size = new System.Drawing.Size(375, 85);
            this.Expense_Description.TabIndex = 8;
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
            // Expense_cost
            // 
            this.Expense_cost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expense_cost.Location = new System.Drawing.Point(158, 149);
            this.Expense_cost.Name = "Expense_cost";
            this.Expense_cost.Size = new System.Drawing.Size(237, 26);
            this.Expense_cost.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(94, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cost : ";
            // 
            // Expense_Item
            // 
            this.Expense_Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expense_Item.Location = new System.Drawing.Point(158, 99);
            this.Expense_Item.Name = "Expense_Item";
            this.Expense_Item.Size = new System.Drawing.Size(237, 26);
            this.Expense_Item.TabIndex = 4;
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
            // Expense_category
            // 
            this.Expense_category.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expense_category.FormattingEnabled = true;
            this.Expense_category.Location = new System.Drawing.Point(158, 47);
            this.Expense_category.Name = "Expense_category";
            this.Expense_category.Size = new System.Drawing.Size(237, 28);
            this.Expense_category.TabIndex = 2;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btn_Expense_Clear);
            this.panel2.Controls.Add(this.btn_Expense_Update);
            this.panel2.Controls.Add(this.btn_Expense_Delete);
            this.panel2.Controls.Add(this.btn_Expense_Add);
            this.panel2.Controls.Add(this.Expense_date);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.Expense_Description);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.Expense_cost);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Expense_Item);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Expense_category);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(32, 406);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1031, 320);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(34, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 351);
            this.panel1.TabIndex = 5;
            // 
            // ExpensesForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ExpensesForm1";
            this.Size = new System.Drawing.Size(1145, 748);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_Expense_Clear;
        private System.Windows.Forms.Button btn_Expense_Update;
        private System.Windows.Forms.Button btn_Expense_Delete;
        private System.Windows.Forms.Button btn_Expense_Add;
        private System.Windows.Forms.DateTimePicker Expense_date;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Expense_Description;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox Expense_cost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox Expense_Item;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Expense_category;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}
