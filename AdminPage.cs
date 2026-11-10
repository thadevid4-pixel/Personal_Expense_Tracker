using System;
using System.Drawing;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>Administrator area: Users tab + Backup and Restore tab.</summary>
    internal class AdminPage : UserControl
    {
        private readonly UsersPanel users = new UsersPanel();
        private readonly BackupPanel backup = new BackupPanel();

        public event EventHandler RestoreCompleted;

        public AdminPage()
        {
            this.Dock = DockStyle.Fill;
            this.Visible = false;
            this.BackColor = Color.White;

            TabControl tabs = new TabControl();
            tabs.Dock = DockStyle.Fill;
            tabs.Font = new Font("Microsoft Sans Serif", 11F);

            TabPage tabUsers = new TabPage("Users");
            tabUsers.Controls.Add(users);
            TabPage tabBackup = new TabPage("Backup and Restore");
            tabBackup.Controls.Add(backup);
            tabs.TabPages.Add(tabUsers);
            tabs.TabPages.Add(tabBackup);

            backup.RestoreCompleted += delegate
            {
                if (RestoreCompleted != null) RestoreCompleted(this, EventArgs.Empty);
            };

            this.Controls.Add(tabs);
        }

        public void refreshData()
        {
            if (Db.InDesigner(this)) return;
            users.refreshData();
        }
    }
}
