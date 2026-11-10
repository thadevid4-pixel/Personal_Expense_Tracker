using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>Administrator tool: list users, change role, reset password, delete.</summary>
    internal class UsersPanel : UserControl
    {
        private static readonly Color Green = Color.FromArgb(49, 121, 84);
        private readonly DataGridView grid = new DataGridView();

        public UsersPanel()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Green;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            grid.Font = new Font("Microsoft Sans Serif", 11F);

            // keep the technical id column hidden every time the grid is (re)filled
            grid.DataBindingComplete += delegate
            {
                if (grid.Columns.Contains("id")) grid.Columns["id"].Visible = false;
            };

            Label info = new Label();
            info.Dock = DockStyle.Top;
            info.Height = 34;
            info.Text = "Select a user, then use the buttons below. You cannot change or delete your own account here.";
            info.TextAlign = ContentAlignment.MiddleLeft;
            info.Padding = new Padding(8, 0, 0, 0);

            FlowLayoutPanel buttons = new FlowLayoutPanel();
            buttons.Dock = DockStyle.Bottom;
            buttons.Height = 52;
            buttons.Padding = new Padding(6, 8, 0, 0);

            Button btnAdmin = MakeButton("Set as Admin", 120);
            btnAdmin.Click += delegate { ChangeRole(Db.RoleAdmin); };
            Button btnStaff = MakeButton("Set as Staff", 120);
            btnStaff.Click += delegate { ChangeRole(Db.RoleStaff); };
            Button btnReset = MakeButton("Reset Password", 140);
            btnReset.Click += delegate { ResetPassword(); };
            Button btnDelete = MakeButton("Delete User", 120);
            btnDelete.Click += delegate { DeleteUser(); };
            Button btnRefresh = MakeButton("Refresh", 90);
            btnRefresh.Click += delegate { refreshData(); };
            buttons.Controls.AddRange(new Control[] { btnAdmin, btnStaff, btnReset, btnDelete, btnRefresh });

            // Fill first, then the docked edges
            this.Controls.Add(grid);
            this.Controls.Add(buttons);
            this.Controls.Add(info);
        }

        private static Button MakeButton(string text, int width)
        {
            Button b = new Button();
            b.Text = text;
            b.Size = new Size(width, 34);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Green;
            b.ForeColor = Color.White;
            b.UseVisualStyleBackColor = false;
            return b;
        }

        public void refreshData()
        {
            if (Db.InDesigner(this)) return;
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT id, username AS Username, user_role AS Role, date_create AS Created FROM users ORDER BY id", connect))
                {
                    adapter.Fill(table);
                }
                grid.DataSource = table;
                if (grid.Columns.Contains("id")) grid.Columns["id"].Visible = false;
                if (grid.Columns.Contains("Created")) grid.Columns["Created"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
            catch (Exception ex)
            {
                Db.ShowError(ex);
            }
        }

        private bool GetSelected(out int id, out string username, out string role)
        {
            id = 0; username = ""; role = "";
            if (grid.CurrentRow == null)
            {
                MessageBox.Show("Please select a user first.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            DataGridViewRow row = grid.CurrentRow;
            id = Convert.ToInt32(row.Cells["id"].Value);
            username = Convert.ToString(row.Cells["Username"].Value);
            role = Convert.ToString(row.Cells["Role"].Value);
            return true;
        }

        private void ChangeRole(string newRole)
        {
            int id; string username; string role;
            if (!GetSelected(out id, out username, out role)) return;

            if (id == Session.UserId)
            {
                MessageBox.Show("You cannot change your own role.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.Equals(role, newRole, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(username + " is already " + newRole + ".", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Change the role of '" + username + "' to " + newRole + "?", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            Execute("UPDATE users SET user_role = @role WHERE id = @id", newRole, id, "Role updated successfully!");
        }

        private void ResetPassword()
        {
            int id; string username; string role;
            if (!GetSelected(out id, out username, out role)) return;

            string pw = Prompt.Show("Reset password", "New password for '" + username + "' (at least 8 characters):", true);
            if (pw == null) return;
            pw = pw.Trim();
            if (pw.Length < 8)
            {
                MessageBox.Show("Invalid password, At least 8 characters are needed", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Reset the password of '" + username + "'?", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            Execute("UPDATE users SET password = @value WHERE id = @id", PasswordHasher.Hash(pw), id, "Password reset successfully!");
        }

        private void DeleteUser()
        {
            int id; string username; string role;
            if (!GetSelected(out id, out username, out role)) return;

            if (id == Session.UserId)
            {
                MessageBox.Show("You cannot delete your own account.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Delete the user '" + username + "'? This cannot be undone.", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            Execute("DELETE FROM users WHERE id = @id", null, id, "User deleted successfully!");
        }

        // one place for the update/delete calls (parameters: @role / @value is optional, @id)
        private void Execute(string sql, string value, int id, string successMessage)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        if (value != null)
                        {
                            cmd.Parameters.AddWithValue(sql.Contains("@role") ? "@role" : "@value", value);
                        }
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                refreshData();
                MessageBox.Show(successMessage, "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Db.ShowError(ex);
            }
        }
    }
}
