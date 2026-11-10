using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>Administrator tool: save all data to a file and restore it later.</summary>
    internal class BackupPanel : UserControl
    {
        private static readonly Color Green = Color.FromArgb(49, 121, 84);

        // raised after a successful restore (MainForm signs the user out because the users table changed)
        public event EventHandler RestoreCompleted;

        private readonly Label lblStatus = new Label();

        public BackupPanel()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            Label title = new Label();
            title.Text = "Backup and Restore";
            title.Font = new Font("Times New Roman", 16F, FontStyle.Bold);
            title.ForeColor = Green;
            title.AutoSize = true;
            title.Location = new Point(25, 20);

            Label text = new Label();
            text.AutoSize = false;
            text.SetBounds(28, 62, 640, 90);
            text.Font = new Font("Microsoft Sans Serif", 10.5F);
            text.Text = "Backup saves users, categories, income and expenses into ONE .xml file.\r\n" +
                        "Restore replaces ALL current data with the data in a backup file.\r\n" +
                        "Before every restore a safety copy of the current data is saved automatically " +
                        "(folder: Backups, next to the program).\r\n" +
                        "Keep backup files private: they contain the (hashed) passwords.";

            Button btnBackup = MakeButton("Backup now...", 28, 170);
            btnBackup.Click += delegate { DoBackup(); };

            Button btnRestore = MakeButton("Restore from file...", 218, 170);
            btnRestore.Click += delegate { DoRestore(); };

            lblStatus.AutoSize = false;
            lblStatus.SetBounds(28, 230, 640, 80);
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.Text = "";

            this.Controls.AddRange(new Control[] { title, text, btnBackup, btnRestore, lblStatus });
        }

        private static Button MakeButton(string text, int x, int y)
        {
            Button b = new Button();
            b.Text = text;
            b.SetBounds(x, y, 175, 42);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Green;
            b.ForeColor = Color.White;
            b.Font = new Font("Microsoft Sans Serif", 10.5F);
            b.UseVisualStyleBackColor = false;
            return b;
        }

        private void DoBackup()
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Backup file (*.xml)|*.xml";
                dlg.FileName = "ExpenseBackup_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xml";
                dlg.Title = "Save backup";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    string summary = BackupService.Backup(dlg.FileName);
                    lblStatus.Text = "Last backup: " + dlg.FileName + "\r\n" + summary;
                    MessageBox.Show("Backup saved successfully!\n\n" + summary, "Information Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Db.ShowError(ex);
                }
            }
        }

        private void DoRestore()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Backup file (*.xml)|*.xml";
                dlg.Title = "Select a backup file";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // check the file first, so a wrong file gives a clear message before any warning
                    DataSet check = BackupService.ReadAndValidate(dlg.FileName);

                    if (MessageBox.Show(
                        "This will REPLACE all current data with the backup:\n\n" + BackupService.Summary(check) +
                        "\n\nA safety copy of the current data is saved first.\nYou will be signed out afterwards.\n\nContinue?",
                        "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    string result = BackupService.Restore(dlg.FileName);
                    MessageBox.Show("Restore completed successfully!\n\n" + result + "\n\nPlease sign in again.",
                        "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (RestoreCompleted != null) RestoreCompleted(this, EventArgs.Empty);
                }
                catch (InvalidDataException ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The restore failed and NO data was changed.\n\n" + ex.Message,
                        "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
