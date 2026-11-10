using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    public partial class CategoryControlForm1 : UserControl
    {
        string StringConnection = Db.ConnectionString;

        private int getID = 0;

        public CategoryControlForm1()
        {
            InitializeComponent();
            // Avoid database calls in design-time (Designer will create instances)
            if (!Db.InDesigner(this))
            {
                DisplayCategoryList();
            }
        }

        // Method សម្រាប់ Refresh ទិន្នន័យ Category ឡើងវិញ
        public void refreshData()
        {
            if (!Db.InDesigner(this))
            {
                DisplayCategoryList();
            }
        }

        public void DisplayCategoryList()
        {
            try
            {
                CategoryData cData = new CategoryData();
                List<CategoryData> listData = cData.categoryListData();
                dataGridView2.DataSource = listData;
            }
            catch (Exception ex)
            {
                Db.ShowError(ex);
            }
        }

        private bool FieldsAreEmpty()
        {
            return string.IsNullOrWhiteSpace(Category_category.Text) ||
                   Category_Type.SelectedIndex == -1 || Category_Status.SelectedIndex == -1;
        }

        // True when another row already has the same name + type
        private static bool CategoryExists(SqlConnection connect, string category, string type, int excludeId)
        {
            string query = "SELECT COUNT(*) FROM Categories WHERE category = @category AND type = @type AND id <> @id";
            using (SqlCommand cmd = new SqlCommand(query, connect))
            {
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@id", excludeId);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void Category_btnAdd_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only administrators can manage categories.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FieldsAreEmpty())
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();

                    string name = Category_category.Text.Trim();
                    string type = Category_Type.SelectedItem.ToString();

                    if (CategoryExists(connect, name, type, 0))
                    {
                        MessageBox.Show("This " + type + " category already exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string insertData = "INSERT INTO Categories (category, type, status, date_insert) VALUES (@category, @type, @status, @date)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@category", name);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@status", Category_Status.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date", DateTime.Today);

                        cmd.ExecuteNonQuery();
                        Clearfields();

                        MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
            DisplayCategoryList();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

            getID = Convert.ToInt32(row.Cells[0].Value);
            Category_category.Text = Convert.ToString(row.Cells[1].Value);
            Category_Type.SelectedItem = Convert.ToString(row.Cells[2].Value);
            Category_Status.SelectedItem = Convert.ToString(row.Cells[3].Value);
        }

        private void Category_btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only administrators can manage categories.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (getID == 0)
            {
                MessageBox.Show("Please select item first from the list", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (FieldsAreEmpty())
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to update ID : " + getID + "?", "Confirm Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();

                    string name = Category_category.Text.Trim();
                    string type = Category_Type.SelectedItem.ToString();

                    if (CategoryExists(connect, name, type, getID))
                    {
                        MessageBox.Show("This " + type + " category already exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string updateData = "UPDATE Categories SET category = @category, type = @type, status = @status WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(updateData, connect))
                    {
                        cmd.Parameters.AddWithValue("@category", name);
                        cmd.Parameters.AddWithValue("@id", getID);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@status", Category_Status.SelectedItem.ToString());

                        int rows = cmd.ExecuteNonQuery();
                        Clearfields();

                        if (rows > 0)
                            MessageBox.Show("Update successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("That category no longer exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
            DisplayCategoryList();
        }

        public void Clearfields()
        {
            Category_category.Text = "";
            Category_Type.SelectedIndex = -1;
            Category_Status.SelectedIndex = -1;
            getID = 0;   // forget the selected row, so Delete/Update can never hit an old row by mistake
        }

        private void Category_Clear_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        private void Category_btnDelete_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only administrators can manage categories.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (getID == 0)
            {
                MessageBox.Show("Please select item first from the list", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Delete ID : " + getID + "?", "Confirm Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                try
                {
                    connect.Open();
                    string deleteData = "DELETE FROM Categories WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", getID);

                        int rows = cmd.ExecuteNonQuery();
                        Clearfields();

                        if (rows > 0)
                            MessageBox.Show("Delete successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("That category no longer exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
            DisplayCategoryList();
        }

        // ---------- roles ----------
        // Staff can look at the categories; only an Administrator can add / update / delete them.
        public void ApplyRole()
        {
            Color color = Session.IsAdmin ? Color.FromArgb(49, 121, 84) : Color.Gray;
            Category_btnAdd.BackColor = color;
            Category_btnUpdate.BackColor = color;
            Category_btnDelete.BackColor = color;
        }
    }
}
