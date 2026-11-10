using System;
using System.Drawing;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    public partial class MainForm : Form
    {
        // True while the user is logging out (so closing this window must NOT exit the whole app)
        private bool isLoggingOut = false;

        // pages and buttons that are created in code (not in the designer)
        private ReportsPage reportsPage;
        private AdminPage adminPage;
        private Button btnReports;
        private Button btnAdmin;

        public MainForm()
        {
            InitializeComponent();

            BuildExtraPages();
            ApplyRole();

            // Asks before closing with the X button
            this.FormClosing += MainForm_FormClosing;

            // When this window is really closed, the application exits
            // (before: the hidden login window kept the program running in the background)
            this.FormClosed += delegate
            {
                if (!isLoggingOut) Application.Exit();
            };
        }

        // ---------- new pages: Reports (everyone) and Admin (administrators only) ----------
        private void BuildExtraPages()
        {
            reportsPage = new ReportsPage();
            adminPage = new AdminPage();
            adminPage.RestoreCompleted += delegate { SignOut(); };
            panel3.Controls.Add(reportsPage);
            panel3.Controls.Add(adminPage);

            btnReports = CreateNavButton("Reports", 1, 405, Properties.Resources.money_growth);
            btnReports.Click += btnReports_Click;

            btnAdmin = CreateNavButton("Admin", 1, 457, DrawUserIcon());
            btnAdmin.Click += btnAdmin_Click;

            Label who = new Label();
            who.AutoSize = false;
            who.SetBounds(5, 560, 175, 64);
            who.ForeColor = Color.White;
            who.Font = new Font("Times New Roman", 11F, FontStyle.Bold);
            who.TextAlign = ContentAlignment.MiddleCenter;
            who.Text = "Signed in as:\r\n" + Session.Username + " (" + Session.Role + ")";

            panel2.Controls.Add(btnReports);
            panel2.Controls.Add(btnAdmin);
            panel2.Controls.Add(who);
        }

        // a button that looks exactly like the existing navigation buttons
        private Button CreateNavButton(string text, int x, int y, Image icon)
        {
            Button b = new Button();
            b.Text = text;
            b.Location = new Point(x, y);
            b.Size = btnDashboard.Size;
            b.BackColor = btnDashboard.BackColor;
            b.ForeColor = btnDashboard.ForeColor;
            b.Font = btnDashboard.Font;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Image = icon;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            b.UseVisualStyleBackColor = false;
            return b;
        }

        // small white "person" icon for the Admin button
        private static Image DrawUserIcon()
        {
            Bitmap bmp = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.White, 10, 3, 12, 12);
                g.FillEllipse(Brushes.White, 4, 18, 24, 20);
            }
            return bmp;
        }

        // Staff: no Admin page, no delete, no category changes
        private void ApplyRole()
        {
            btnAdmin.Visible = Session.IsAdmin;
            btnCategory.Text = Session.IsAdmin ? "Add Category" : "Categories";
            incomeForm1.ApplyRole();
            expensesForm1.ApplyRole();
            categoryControlForm1.ApplyRole();
        }

        // shows ONE page and hides all the others
        private void ShowOnly(Control page)
        {
            Control[] pages = { dashboard_Form1, categoryControlForm1, incomeForm1, expensesForm1, reportsPage, adminPage };
            foreach (Control c in pages)
            {
                c.Visible = (c == page);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLoggingOut && e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure want to close?", "Confirmation Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        // Method សម្រាប់បង្ហាញឈ្មោះ User (អ្នកអាចយក Label មកដាក់ទុករួច assign តម្លៃនៅទីនេះ)
        public void displayUsername()
        {
            // ឧទាហរណ៍៖ label1.Text = "Welcome, " + UserData.username;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to close?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SignOut();
            }
        }

        // back to the login window (used by Logout and after a database restore)
        private void SignOut()
        {
            isLoggingOut = true;
            Session.Clear();

            Form1 loginForm = new Form1();
            loginForm.Show();

            this.Close();   // really close (not just hide) so no windows pile up in memory
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowOnly(dashboard_Form1);
            dashboard_Form1.refreshData();
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            ShowOnly(incomeForm1);
            incomeForm1.refreshData();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ShowOnly(categoryControlForm1);
            categoryControlForm1.refreshData();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            ShowOnly(expensesForm1);
            expensesForm1.refreshData();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ShowOnly(reportsPage);
            reportsPage.refreshData();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only administrators can open this page.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ShowOnly(adminPage);
            adminPage.refreshData();
        }
    }
}
