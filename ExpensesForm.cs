using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    public partial class ExpensesForm1 : UserControl
    {
        string StringConnection = Db.ConnectionString;

        private int getID = 0;
        private FilterBar filterBar;
        private List<ExpensesData> allExpenses = new List<ExpensesData>();

        public ExpensesForm1()
        {
            InitializeComponent();
            // Do not run DB logic at design time
            if (!Db.InDesigner(this))
            {
                BuildFilterBar();
                DisplaycategoryList();
                DisplayExpenseData();
            }
        }

        // MainForm calls this every time the Expenses page is opened
        public void refreshData()
        {
            if (Db.InDesigner(this)) return;
            DisplaycategoryList();
            DisplayExpenseData();
        }

        public void DisplayExpenseData()
        {
            try
            {
                ExpensesData eData = new ExpensesData();
                allExpenses = eData.expenseListData();
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
                        cmd.Parameters.AddWithValue("@type", "Expense");
                        Expense_category.Items.Clear();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Expense_category.Items.Add(reader["category"].ToString());
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

        // Checks the form fields. On success returns true and the cost as a number.
        private bool ValidateInputs(bool isNew, out decimal amount)
        {
            amount = 0m;

            bool categoryMissing = isNew ? Expense_category.SelectedIndex == -1
                                         : string.IsNullOrWhiteSpace(Expense_category.Text);

            if (categoryMissing || string.IsNullOrWhiteSpace(Expense_Item.Text) ||
                string.IsNullOrWhiteSpace(Expense_cost.Text) || string.IsNullOrWhiteSpace(Expense_Description.Text))
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Db.TryParseAmount(Expense_cost.Text, out amount))
            {
                MessageBox.Show("Please enter a valid cost greater than 0 (for example 25 or 25.50).",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Expense_cost.Focus();
                return false;
            }
            return true;
        }

        private void btn_Expense_Add_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!ValidateInputs(true, out amount)) return;

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();
                    string insertData = "INSERT INTO expenses (category, item, cost, description, date_expense, date_insert) " +
                        "VALUES (@cat, @item, @cost, @desc, @date_ex, @date)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cat", Expense_category.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@item", Expense_Item.Text.Trim());
                        cmd.Parameters.AddWithValue("@cost", Db.AmountText(amount));
                        cmd.Parameters.AddWithValue("@desc", Expense_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@date_ex", Expense_date.Value.Date);
                        cmd.Parameters.AddWithValue("@date", DateTime.Today);

                        cmd.ExecuteNonQuery();
                        clearfields();
                        DisplayExpenseData();

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
            Expense_category.SelectedIndex = -1;
            Expense_Item.Text = "";
            Expense_cost.Text = "";
            Expense_Description.Text = "";
            getID = 0;
        }

        private void btn_Expense_Clear_Click(object sender, EventArgs e)
        {
            clearfields();
        }

        private void btn_Expense_Update_Click(object sender, EventArgs e)
        {
            if (getID == 0)
            {
                MessageBox.Show("Please select the item from the list to update first!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string updateData = "UPDATE expenses SET category = @cat, item = @item, cost = @cost, description = @desc, date_expense = @date_ex WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(updateData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cat", Expense_category.Text);
                        cmd.Parameters.AddWithValue("@item", Expense_Item.Text.Trim());
                        cmd.Parameters.AddWithValue("@cost", Db.AmountText(amount));
                        cmd.Parameters.AddWithValue("@desc", Expense_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@date_ex", Expense_date.Value.Date);
                        cmd.Parameters.AddWithValue("@id", getID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Update successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found to update with ID: " + getID, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        clearfields();
                        DisplayExpenseData();
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
            Expense_category.Text = Convert.ToString(row.Cells[1].Value);
            Expense_Item.Text = Convert.ToString(row.Cells[2].Value);
            Expense_cost.Text = Convert.ToString(row.Cells[3].Value);
            Expense_Description.Text = Convert.ToString(row.Cells[4].Value);

            string dateText = Convert.ToString(row.Cells[5].Value);
            if (!string.IsNullOrEmpty(dateText))
            {
                // Culture-safe: the grid text is MM-dd-yyyy on every PC
                Expense_date.Value = Db.ParseDisplayDate(dateText);
            }
        }

        private void btn_Expense_Delete_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only administrators can delete records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (getID == 0)
            {
                MessageBox.Show("Please select the item from the list to delete first!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string deleteData = "DELETE FROM expenses WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", getID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Delete successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found to Delete with ID: " + getID, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        clearfields();
                        DisplayExpenseData();
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
            filterBar.Location = new Point(160, 10);
            filterBar.FilterChanged += delegate { ApplyFilter(); };
            filterBar.ExportClicked += delegate { CsvHelper.ExportGrid(dataGridView2, "expenses"); };
            panel1.Controls.Add(filterBar);
        }

        private void UpdateFilterCategories()
        {
            if (filterBar == null) return;
            filterBar.SetCategories(
                allExpenses.Select(r => r.Category)
                    .Concat(Expense_category.Items.Cast<object>().Select(o => o.ToString())));
        }

        private void ApplyFilter()
        {
            if (filterBar == null)
            {
                dataGridView2.DataSource = allExpenses;
            }
            else
            {
                dataGridView2.DataSource = allExpenses
                    .Where(r => filterBar.Matches(r.Category, r.DateExpense, r.Item, r.Description, r.Category, r.Cost))
                    .ToList();
            }
            if (dataGridView2.Columns.Contains("DateExpense"))
            {
                dataGridView2.Columns["DateExpense"].HeaderText = "Date Expense";
            }
        }

        // ---------- roles ----------
        // Staff can add / view / update, but only an Administrator can delete.
        public void ApplyRole()
        {
            btn_Expense_Delete.BackColor = Session.IsAdmin ? Color.FromArgb(49, 121, 84) : Color.Gray;
        }
    }
}
