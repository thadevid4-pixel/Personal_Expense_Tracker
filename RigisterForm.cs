using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Personal_Expense_Tracker
{
    public partial class RigisterForm : Form
    {
        SqlConnection connect = new SqlConnection(Db.ConnectionString);
        public RigisterForm()
        {
            InitializeComponent();

            // Closing this window with the X button really exits the application
            // (hiding it to go back to the login window does NOT raise this event)
            this.FormClosed += delegate { Application.Exit(); };
        }
        public bool checkConnection()
        {
            return (connect.State == ConnectionState.Closed) ? true : false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Form1 btnLogin = new Form1();
            btnLogin.Show();
            this.Hide();
        }

        private void RigisterForm_Load(object sender, EventArgs e)
        {
           
        }

        private void checkRegisterShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtRegisterConfirmPassword.PasswordChar = checkRegisterShowPassword.Checked ? '\0' : '*';
            txtRegisterPassword.PasswordChar= checkRegisterShowPassword.Checked ? '\0':'*';
        }

       private void btnSignup_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtRegisterUsername.Text) || 
        string.IsNullOrWhiteSpace(txtRegisterPassword.Text) || 
        string.IsNullOrWhiteSpace(txtRegisterConfirmPassword.Text))
    {
        MessageBox.Show("Please fill all the blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    else
    {
        try
        {
            connect.Open();

            // ១. ឆែកមើលថាមាន Username នេះក្នុង Database ហើយឬនៅ
            bool isUserExists = false;
            string selectUsername = "SELECT * FROM users WHERE username = @usern";
            
            using (SqlCommand checkUser = new SqlCommand(selectUsername, connect))
            {
                checkUser.Parameters.AddWithValue("@usern", txtRegisterUsername.Text.Trim());

                using (SqlDataAdapter adapter = new SqlDataAdapter(checkUser))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        isUserExists = true;
                    }
                }
            }

            // ២. ផ្ទៀងផ្ទាត់លក្ខខណ្ឌ (បំបែកចេញពី using block ខាងលើ)
            if (isUserExists)
            {
                string typedName = txtRegisterUsername.Text.Trim();
                string tempUsern = typedName.Substring(0, 1).ToUpper() + typedName.Substring(1);
                MessageBox.Show(tempUsern + " is existing already", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtRegisterPassword.Text.Length < 8)
            {
                MessageBox.Show("Invalid password, At least 8 characters are needed", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtRegisterPassword.Text != txtRegisterConfirmPassword.Text)
            {
                MessageBox.Show("Password does not match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // ៣. បញ្ចូល Account ថ្មីទៅក្នុង Database
                // The very first account becomes the Administrator, every later account is Staff
                string role = Db.RoleStaff;
                using (SqlCommand countUsers = new SqlCommand("SELECT COUNT(*) FROM users", connect))
                {
                    if (Convert.ToInt32(countUsers.ExecuteScalar()) == 0) role = Db.RoleAdmin;
                }

                string insertData = "INSERT INTO users (username, password, date_create, user_role) VALUES (@usern, @pass, @date, @role)";
                
                using (SqlCommand insertUser = new SqlCommand(insertData, connect))
                {
                    insertUser.Parameters.AddWithValue("@usern", txtRegisterUsername.Text.Trim());
                    insertUser.Parameters.AddWithValue("@pass", PasswordHasher.Hash(txtRegisterPassword.Text.Trim()));   // never store the real password
                    insertUser.Parameters.AddWithValue("@role", role);
                    insertUser.Parameters.AddWithValue("@date", DateTime.Today);

                    insertUser.ExecuteNonQuery();

                    MessageBox.Show("Registered successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form1 loginForm = new Form1();
                    loginForm.Show();
                    this.Hide();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            connect.Close();
        }
    }
}
    }
}
