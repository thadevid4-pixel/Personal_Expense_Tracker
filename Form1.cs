using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Pressing Enter in the username/password boxes logs in
            this.AcceptButton = btnLogin;

            // Closing the login window with the X button really exits the application
            // (hiding it to open another window does NOT raise this event)
            this.FormClosed += delegate { Application.Exit(); };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            RigisterForm regForm = new RigisterForm();
            regForm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter your username and password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int userId = 0;
                string dbUsername = "";
                string storedPassword = "";
                string role = "";
                bool found = false;

                using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
                {
                    connect.Open();

                    // Look the user up by name only; the password is checked in code against its hash
                    using (SqlCommand cmd = new SqlCommand("SELECT id, username, password, user_role FROM users WHERE username = @usern", connect))
                    {
                        cmd.Parameters.AddWithValue("@usern", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                found = true;
                                userId = Convert.ToInt32(reader["id"]);
                                dbUsername = reader["username"].ToString();
                                storedPassword = reader["password"] == DBNull.Value ? "" : reader["password"].ToString();
                                role = reader["user_role"] == DBNull.Value ? "" : reader["user_role"].ToString();
                            }
                        }
                    }

                    if (found && PasswordHasher.Verify(password, storedPassword))
                    {
                        // safety net: if an old plain-text password is still there, hash it now
                        if (!PasswordHasher.IsHashed(storedPassword))
                        {
                            using (SqlCommand upgrade = new SqlCommand("UPDATE users SET password = @pass WHERE id = @id", connect))
                            {
                                upgrade.Parameters.AddWithValue("@pass", PasswordHasher.Hash(password));
                                upgrade.Parameters.AddWithValue("@id", userId);
                                upgrade.ExecuteNonQuery();
                            }
                        }

                        Session.Start(userId, dbUsername, role);

                        MessageBox.Show("Login successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm mForm = new MainForm();
                        mForm.Show();

                        this.Hide();
                        return;
                    }
                }

                MessageBox.Show("Incorrect username/password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not log in.\n\n" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = (checkShowPassword.Checked) ? '\0' : '*';
        }
    }
}
