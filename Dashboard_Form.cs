using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    public partial class Dashboard_Form : UserControl
    {
        public Dashboard_Form()
        {
            InitializeComponent();

            // Reload the numbers every time this page is shown, so new Income/Expenses appear immediately
            // (no need to register or restart the app).
            this.Load += delegate { refreshData(); };
            this.VisibleChanged += delegate { if (this.Visible) refreshData(); };
        }

        // Reloads ALL dashboard numbers from the database
        public void refreshData()
        {
            if (Db.InDesigner(this)) return;

            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                DateTime yesterday = today.AddDays(-1);
                DateTime monthStart = new DateTime(today.Year, today.Month, 1);
                DateTime nextMonth = monthStart.AddMonths(1);
                DateTime yearStart = new DateTime(today.Year, 1, 1);
                DateTime nextYear = yearStart.AddYears(1);

                using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
                {
                    connect.Open();

                    // Income
                    income_totalincome.Text = Db.Money(Db.SumAmount(connect, "income", "income", "date_income", today, tomorrow));
                    income_yesterdayincome.Text = Db.Money(Db.SumAmount(connect, "income", "income", "date_income", yesterday, today));
                    income_monthincome.Text = Db.Money(Db.SumAmount(connect, "income", "income", "date_income", monthStart, nextMonth));
                    income_yearincome.Text = Db.Money(Db.SumAmount(connect, "income", "income", "date_income", yearStart, nextYear));
                    income_total_label.Text = Db.Money(Db.SumAmount(connect, "income", "income", "date_income", null, null));

                    // Expenses
                    expense_totalincome.Text = Db.Money(Db.SumAmount(connect, "expenses", "cost", "date_expense", today, tomorrow));
                    expense_yesterdayincome.Text = Db.Money(Db.SumAmount(connect, "expenses", "cost", "date_expense", yesterday, today));
                    expense_monthincome.Text = Db.Money(Db.SumAmount(connect, "expenses", "cost", "date_expense", monthStart, nextMonth));
                    expense_yearincome.Text = Db.Money(Db.SumAmount(connect, "expenses", "cost", "date_expense", yearStart, nextYear));
                    expense_total_label.Text = Db.Money(Db.SumAmount(connect, "expenses", "cost", "date_expense", null, null));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the dashboard data.\n\n" + ex.Message,
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
