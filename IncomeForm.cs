using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    public partial class IncomeForm1 : UserControl
    {
        string StringConnection = Db.ConnectionString;
        private int getID = 0;
        private FilterBar filterBar;
        private List<IncomeData> allIncome = new List<IncomeData>();

        public IncomeForm1()
        {
            InitializeComponent();
            if (!Db.InDesigner(this))
            {
                BuildFilterBar();
                DisplaycategoryList();
                DisplayIncomeData();
            }
        }

        // MainForm calls this every time the Income page is opened
        public void refreshData()
        {
            if (Db.InDesigner(this)) return;
            DisplaycategoryList();
            DisplayIncomeData();
        }

        public void DisplayIncomeData()
        {
            try
            {
                IncomeData iData = new IncomeData();
                allIncome = iData.incomeListData();
                UpdateFilterCategories();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                Db.ShowError(ex);
            }
        }

        public void DisplaycategoryList()
        {
            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();
                    string SelectData = "SELECT category FROM categories WHERE type = @type";

                    using (SqlCommand cmd = new SqlCommand(SelectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@type", "Income");
                        Income_category.Items.Clear();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Income_category.Items.Add(reader["category"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
        }

        // Checks the form fields. On success returns true and the amount as a number.
        private bool ValidateInputs(bool isNew, out decimal amount)
        {
            amount = 0m;

            bool categoryMissing = isNew ? Income_category.SelectedIndex == -1
                                         : string.IsNullOrWhiteSpace(Income_category.Text);

            if (categoryMissing || string.IsNullOrWhiteSpace(Income_Item.Text) ||
                string.IsNullOrWhiteSpace(Income_income.Text) || string.IsNullOrWhiteSpace(Income_Description.Text))
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Db.TryParseAmount(Income_income.Text, out amount))
            {
                MessageBox.Show("Please enter a valid income amount greater than 0 (for example 25 or 25.50).",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Income_income.Focus();
                return false;
            }
            return true;
        }

        private void btn_Income_Add_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!ValidateInputs(true, out amount)) return;

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();
                    string insertData = "INSERT INTO income (category, item, income, description, date_income, date_insert) " +
                        "VALUES (@cat, @item, @income, @desc, @date_in, @date_insert)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cat", Income_category.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@item", Income_Item.Text.Trim());
                        cmd.Parameters.AddWithValue("@income", Db.AmountText(amount));
                        cmd.Parameters.AddWithValue("@desc", Income_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@date_in", Income_date.Value.Date);
                        cmd.Parameters.AddWithValue("@date_insert", DateTime.Today);

                        cmd.ExecuteNonQuery();
                        DisplayIncomeData();
                        clearfields();

                        MessageBox.Show("Added successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
        }

        public void clearfields()
        {
            Income_Item.Text = "";
            Income_category.SelectedIndex = -1;
            Income_income.Text = "";
            Income_Description.Text = "";
            getID = 0;
        }

        private void btn_Income_Clear_Click(object sender, EventArgs e)
        {
            clearfields();
        }

        private void btn_Income_Update_Click(object sender, EventArgs e)
        {
            if (getID == 0)
            {
                MessageBox.Show("Please select item first from the list", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal amount;
            if (!ValidateInputs(false, out amount)) return;

            if (MessageBox.Show("Are you sure you want to Update ID : " + getID + "?", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();
                    string updateData = "UPDATE income SET category = @cat, item = @item, income = @income, description = @desc, date_income = @date_in WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(updateData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cat", Income_category.Text);
                        cmd.Parameters.AddWithValue("@item", Income_Item.Text.Trim());
                        cmd.Parameters.AddWithValue("@income", Db.AmountText(amount));
                        cmd.Parameters.AddWithValue("@desc", Income_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@date_in", Income_date.Value.Date);
                        cmd.Parameters.AddWithValue("@id", getID);

                        int rows = cmd.ExecuteNonQuery();

                        DisplayIncomeData();
                        clearfields();

                        if (rows > 0)
                            MessageBox.Show("Update successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("That record no longer exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

            getID = Convert.ToInt32(row.Cells[0].Value);
            Income_category.Text = Convert.ToString(row.Cells[1].Value);
            Income_Item.Text = Convert.ToString(row.Cells[2].Value);
            Income_income.Text = Convert.ToString(row.Cells[3].Value);
            Income_Description.Text = Convert.ToString(row.Cells[4].Value);

            string dateText = Convert.ToString(row.Cells[5].Value);
            if (!string.IsNullOrEmpty(dateText))
            {
                // Culture-safe: the grid text is MM-dd-yyyy on every PC
                Income_date.Value = Db.ParseDisplayDate(dateText);
            }
        }

        private void btn_Income_Delete_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only administrators can delete records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (getID == 0)
            {
                MessageBox.Show("Please select item first from the list", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Delete ID : " + getID + "?", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();
                    string deleteData = "DELETE FROM income WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", getID);

                        int rows = cmd.ExecuteNonQuery();
                        DisplayIncomeData();
                        clearfields();

                        if (rows > 0)
                            MessageBox.Show("Deleted successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("That record no longer exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
        }

        // ---------- search / filter ----------
        private void BuildFilterBar()
        {
            filterBar = new FilterBar();
            filterBar.Location = new Point(140, 18);
            filterBar.FilterChanged += delegate { ApplyFilter(); };
            filterBar.ExportClicked += delegate { CsvHelper.ExportGrid(dataGridView2, "income"); };
            panel1.Controls.Add(filterBar);
        }

        private void UpdateFilterCategories()
        {
            if (filterBar == null) return;
            filterBar.SetCategories(
                allIncome.Select(r => r.Category)
                    .Concat(Income_category.Items.Cast<object>().Select(o => o.ToString())));
        }

        private void ApplyFilter()
        {
            if (filterBar == null)
            {
                dataGridView2.DataSource = allIncome;
            }
            else
            {
                dataGridView2.DataSource = allIncome
                    .Where(r => filterBar.Matches(r.Category, r.DateIncome, r.Item, r.Description, r.Category, r.Cost))
                    .ToList();
            }
            
        }

        // ---------- roles ----------
        // Staff can add / view / update, but only an Administrator can delete.
        public void ApplyRole()
        {
            btn_Income_Delete.BackColor = Session.IsAdmin ? Color.FromArgb(49, 121, 84) : Color.Gray;
        }
    }
}
