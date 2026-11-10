using System;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show a clear message (instead of a crash) when Expense.mdf is missing
            // or SQL Server LocalDB is not available.
            string problem = Db.CheckDatabase();
            if (problem != null)
            {
                MessageBox.Show(problem, "Database problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Adds the role column and hashes old plain-text passwords (does nothing when already done)
            try
            {
                Db.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The database could not be upgraded.\n\n" + ex.Message, "Database problem",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new Form1());
        }
    }
}
