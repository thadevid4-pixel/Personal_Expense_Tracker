namespace Personal_Expense_Tracker
{
    partial class CategoryControlForm1
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Category_btnDelete = new System.Windows.Forms.Button();
            this.Category_Clear = new System.Windows.Forms.Button();
            this.Category_btnUpdate = new System.Windows.Forms.Button();
            this.Category_btnAdd = new System.Windows.Forms.Button();
            this.Category_Status = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Category_Type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Category_category = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Location = new System.Drawing.Point(263, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 685);
            this.panel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Categories List";
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
            this.dataGridView2.Location = new System.Drawing.Point(17, 51);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(658, 628);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.Category_btnDelete);
            this.panel1.Controls.Add(this.Category_Clear);
            this.panel1.Controls.Add(this.Category_btnUpdate);
            this.panel1.Controls.Add(this.Category_btnAdd);
            this.panel1.Controls.Add(this.Category_Status);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Category_Type);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Category_category);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 685);
            this.panel1.TabIndex = 2;
            // 
            // Category_btnDelete
            // 
            this.Category_btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.Category_btnDelete.FlatAppearance.BorderSize = 0;
            this.Category_btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category_btnDelete.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Category_btnDelete.Location = new System.Drawing.Point(134, 324);
            this.Category_btnDelete.Name = "Category_btnDelete";
            this.Category_btnDelete.Size = new System.Drawing.Size(82, 35);
            this.Category_btnDelete.TabIndex = 9;
            this.Category_btnDelete.Text = "Delete";
            this.Category_btnDelete.UseVisualStyleBackColor = false;
            this.Category_btnDelete.Click += new System.EventHandler(this.Category_btnDelete_Click);
            // 
            // Category_Clear
            // 
            this.Category_Clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.Category_Clear.FlatAppearance.BorderSize = 0;
            this.Category_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category_Clear.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_Clear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Category_Clear.Location = new System.Drawing.Point(22, 324);
            this.Category_Clear.Name = "Category_Clear";
            this.Category_Clear.Size = new System.Drawing.Size(82, 35);
            this.Category_Clear.TabIndex = 8;
            this.Category_Clear.Text = "Clear";
            this.Category_Clear.UseVisualStyleBackColor = false;
            this.Category_Clear.Click += new System.EventHandler(this.Category_Clear_Click);
            // 
            // Category_btnUpdate
            // 
            this.Category_btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.Category_btnUpdate.FlatAppearance.BorderSize = 0;
            this.Category_btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category_btnUpdate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_btnUpdate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Category_btnUpdate.Location = new System.Drawing.Point(134, 257);
            this.Category_btnUpdate.Name = "Category_btnUpdate";
            this.Category_btnUpdate.Size = new System.Drawing.Size(82, 35);
            this.Category_btnUpdate.TabIndex = 7;
            this.Category_btnUpdate.Text = "Update";
            this.Category_btnUpdate.UseVisualStyleBackColor = false;
            this.Category_btnUpdate.Click += new System.EventHandler(this.Category_btnUpdate_Click);
            // 
            // Category_btnAdd
            // 
            this.Category_btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(121)))), ((int)(((byte)(84)))));
            this.Category_btnAdd.FlatAppearance.BorderSize = 0;
            this.Category_btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category_btnAdd.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_btnAdd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Category_btnAdd.Location = new System.Drawing.Point(22, 257);
            this.Category_btnAdd.Name = "Category_btnAdd";
            this.Category_btnAdd.Size = new System.Drawing.Size(82, 35);
            this.Category_btnAdd.TabIndex = 6;
            this.Category_btnAdd.Text = "Add";
            this.Category_btnAdd.UseVisualStyleBackColor = false;
            this.Category_btnAdd.Click += new System.EventHandler(this.Category_btnAdd_Click);
            // 
            // Category_Status
            // 
            this.Category_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_Status.FormattingEnabled = true;
            this.Category_Status.Items.AddRange(new object[] {
            "active",
            "inactive"});
            this.Category_Status.Location = new System.Drawing.Point(22, 211);
            this.Category_Status.Name = "Category_Status";
            this.Category_Status.Size = new System.Drawing.Size(194, 28);
            this.Category_Status.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Status";
            // 
            // Category_Type
            // 
            this.Category_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_Type.FormattingEnabled = true;
            this.Category_Type.Items.AddRange(new object[] {
            "Income",
            "Expense"});
            this.Category_Type.Location = new System.Drawing.Point(22, 139);
            this.Category_Type.Name = "Category_Type";
            this.Category_Type.Size = new System.Drawing.Size(194, 28);
            this.Category_Type.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type";
            // 
            // Category_category
            // 
            this.Category_category.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Category_category.Location = new System.Drawing.Point(22, 73);
            this.Category_category.Name = "Category_category";
            this.Category_category.Size = new System.Drawing.Size(196, 26);
            this.Category_category.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category";
            // 
            // CategoryControlForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CategoryControlForm1";
            this.Size = new System.Drawing.Size(612, 504);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Category_btnDelete;
        private System.Windows.Forms.Button Category_Clear;
        private System.Windows.Forms.Button Category_btnUpdate;
        private System.Windows.Forms.Button Category_btnAdd;
        private System.Windows.Forms.ComboBox Category_Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Category_Type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox Category_category;
        private System.Windows.Forms.Label label1;
    }
}
