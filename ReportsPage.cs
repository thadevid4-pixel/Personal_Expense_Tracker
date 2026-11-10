using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>
    /// Summary report: totals, income/expenses by category for a date range, and a monthly chart.
    /// Built in code (no designer file).
    /// </summary>
    internal class ReportsPage : UserControl
    {
        private static readonly Color Green = Color.FromArgb(49, 121, 84);
        private static readonly Color Red = Color.FromArgb(192, 57, 43);
        private static readonly Color Blue = Color.FromArgb(41, 98, 156);

        private readonly DateTimePicker dtFrom = new DateTimePicker();
        private readonly DateTimePicker dtTo = new DateTimePicker();
        private readonly Label lblIncome = new Label();
        private readonly Label lblExpense = new Label();
        private readonly Label lblNet = new Label();
        private readonly Panel cardNet = new Panel();
        private readonly DataGridView gridIncome = new DataGridView();
        private readonly DataGridView gridExpense = new DataGridView();
        private readonly MonthlyChart chart = new MonthlyChart();

        // last generated report (used by Export)
        private DataTable incomeTable;
        private DataTable expenseTable;
        private decimal totalIncome;
        private decimal totalExpense;
        private string periodText = "";

        public ReportsPage()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(240, 244, 242);
            this.Visible = false;
            BuildLayout();
        }

        private void BuildLayout()
        {
            TableLayoutPanel root = new TableLayoutPanel();
            root.Dock = DockStyle.Fill;
            root.ColumnCount = 1;
            root.RowCount = 5;
            root.Padding = new Padding(15, 8, 15, 10);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));   // title
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));   // filters
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));   // cards
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 225));  // grids
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // chart

            Label title = new Label();
            title.Text = "Reports";
            title.Dock = DockStyle.Fill;
            title.Font = new Font("Times New Roman", 18F, FontStyle.Bold);
            title.ForeColor = Green;
            title.TextAlign = ContentAlignment.MiddleLeft;
            root.Controls.Add(title, 0, 0);

            root.Controls.Add(BuildFilterRow(), 0, 1);
            root.Controls.Add(BuildCards(), 0, 2);
            root.Controls.Add(BuildGrids(), 0, 3);

            chart.Dock = DockStyle.Fill;
            chart.Title = "Income vs Expenses (6 months)";
            root.Controls.Add(chart, 0, 4);

            this.Controls.Add(root);
        }

        private Control BuildFilterRow()
        {
            FlowLayoutPanel row = new FlowLayoutPanel();
            row.Dock = DockStyle.Fill;
            row.WrapContents = false;

            Label from = new Label();
            from.Text = "From:";
            from.AutoSize = true;
            from.Margin = new Padding(0, 8, 3, 0);

            Label to = new Label();
            to.Text = "To:";
            to.AutoSize = true;
            to.Margin = new Padding(10, 8, 3, 0);

            DateTime today = DateTime.Today;
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Width = 105;
            dtFrom.Value = new DateTime(today.Year, today.Month, 1);
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Width = 105;
            dtTo.Value = today;

            Button btnGenerate = MakeButton("Generate", 90);
            btnGenerate.Click += delegate { GenerateReport(); };

            Button btnMonth = MakeButton("This month", 90);
            btnMonth.Click += delegate
            {
                dtFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtTo.Value = DateTime.Today;
                GenerateReport();
            };

            Button btnYear = MakeButton("This year", 85);
            btnYear.Click += delegate
            {
                dtFrom.Value = new DateTime(DateTime.Today.Year, 1, 1);
                dtTo.Value = DateTime.Today;
                GenerateReport();
            };

            Button btnExport = MakeButton("Export CSV", 95);
            btnExport.Click += delegate { ExportReport(); };

            row.Controls.AddRange(new Control[] { from, dtFrom, to, dtTo, btnGenerate, btnMonth, btnYear, btnExport });
            return row;
        }

        private static Button MakeButton(string text, int width)
        {
            Button b = new Button();
            b.Text = text;
            b.Size = new Size(width, 30);
            b.Margin = new Padding(10, 2, 0, 0);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Green;
            b.ForeColor = Color.White;
            b.UseVisualStyleBackColor = false;
            return b;
        }

        private Control BuildCards()
        {
            TableLayoutPanel cards = new TableLayoutPanel();
            cards.Dock = DockStyle.Fill;
            cards.ColumnCount = 3;
            cards.RowCount = 1;
            for (int i = 0; i < 3; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

            cards.Controls.Add(MakeCard("Total Income", lblIncome, Green, null), 0, 0);
            cards.Controls.Add(MakeCard("Total Expenses", lblExpense, Red, null), 1, 0);
            cards.Controls.Add(MakeCard("Net Balance", lblNet, Blue, cardNet), 2, 0);
            return cards;
        }

        private static Panel MakeCard(string caption, Label value, Color color, Panel reuse)
        {
            Panel card = reuse ?? new Panel();
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(0, 5, 10, 5);
            card.BackColor = color;

            Label cap = new Label();
            cap.Text = caption;
            cap.ForeColor = Color.White;
            cap.Font = new Font("Microsoft Sans Serif", 10F);
            cap.AutoSize = true;
            cap.Location = new Point(12, 8);

            value.Text = "$0.00";
            value.ForeColor = Color.White;
            value.Font = new Font("Times New Roman", 20F, FontStyle.Bold);
            value.AutoSize = true;
            value.Location = new Point(10, 30);

            card.Controls.Add(cap);
            card.Controls.Add(value);
            return card;
        }

        private Control BuildGrids()
        {
            TableLayoutPanel grids = new TableLayoutPanel();
            grids.Dock = DockStyle.Fill;
            grids.ColumnCount = 2;
            grids.RowCount = 1;
            grids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            grids.Controls.Add(MakeGridBox("Income by category", gridIncome), 0, 0);
            grids.Controls.Add(MakeGridBox("Expenses by category", gridExpense), 1, 0);
            return grids;
        }

        private static GroupBox MakeGridBox(string title, DataGridView grid)
        {
            GroupBox box = new GroupBox();
            box.Text = title;
            box.Dock = DockStyle.Fill;
            box.Margin = new Padding(0, 0, 10, 0);

            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Green;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            box.Controls.Add(grid);
            return box;
        }

        // MainForm calls this every time the Reports page is opened
        public void refreshData()
        {
            if (Db.InDesigner(this)) return;
            GenerateReport();
        }

        private void GenerateReport()
        {
            if (dtFrom.Value.Date > dtTo.Value.Date)
            {
                MessageBox.Show("The 'From' date cannot be later than the 'To' date.", "Error Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime from = dtFrom.Value.Date;
            DateTime toExclusive = dtTo.Value.Date.AddDays(1);

            try
            {
                using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
                {
                    connect.Open();

                    incomeTable = BuildCategoryTable(connect, "income", "income", "date_income", from, toExclusive, out totalIncome);
                    expenseTable = BuildCategoryTable(connect, "expenses", "cost", "date_expense", from, toExclusive, out totalExpense);

                    // chart: the 6 months that end with the month of the "To" date
                    DateTime lastMonth = new DateTime(dtTo.Value.Year, dtTo.Value.Month, 1);
                    chart.Labels = new List<string>();
                    chart.Income = new List<decimal>();
                    chart.Expenses = new List<decimal>();
                    for (int i = 5; i >= 0; i--)
                    {
                        DateTime start = lastMonth.AddMonths(-i);
                        DateTime next = start.AddMonths(1);
                        chart.Labels.Add(start.ToString("MMM yy", CultureInfo.InvariantCulture));
                        chart.Income.Add(Db.SumAmount(connect, "income", "income", "date_income", start, next));
                        chart.Expenses.Add(Db.SumAmount(connect, "expenses", "cost", "date_expense", start, next));
                    }
                }

                gridIncome.DataSource = incomeTable;
                gridExpense.DataSource = expenseTable;
                FormatGrid(gridIncome);
                FormatGrid(gridExpense);

                decimal net = totalIncome - totalExpense;
                lblIncome.Text = Db.Money(totalIncome);
                lblExpense.Text = Db.Money(totalExpense);
                lblNet.Text = (net < 0 ? "-" : "") + Db.Money(Math.Abs(net));
                cardNet.BackColor = net < 0 ? Red : Blue;

                chart.Invalidate();
                periodText = from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " to " +
                             dtTo.Value.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Db.ShowError(ex);
            }
        }

        private static void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Contains("Total"))
            {
                grid.Columns["Total"].DefaultCellStyle.Format = "N2";
                grid.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (grid.Columns.Contains("Share"))
                grid.Columns["Share"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // Category | Entries | Total | Share   (biggest first)
        private static DataTable BuildCategoryTable(SqlConnection connect, string table, string amountColumn,
                                                    string dateColumn, DateTime from, DateTime toExclusive, out decimal grandTotal)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Category", typeof(string));
            result.Columns.Add("Entries", typeof(int));
            result.Columns.Add("Total", typeof(decimal));
            result.Columns.Add("Share", typeof(string));

            string sql = "SELECT category, COUNT(*) AS entries, SUM(TRY_CAST(" + amountColumn + " AS DECIMAL(18,2))) AS total " +
                         "FROM " + table + " WHERE " + dateColumn + " >= @from AND " + dateColumn + " < @to " +
                         "GROUP BY category ORDER BY total DESC";

            List<object[]> rows = new List<object[]>();
            grandTotal = 0m;

            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = toExclusive;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader["category"] == DBNull.Value ? "(none)" : reader["category"].ToString();
                        int entries = Convert.ToInt32(reader["entries"]);
                        decimal total = reader["total"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["total"]);
                        rows.Add(new object[] { category, entries, total });
                        grandTotal += total;
                    }
                }
            }

            foreach (object[] r in rows)
            {
                decimal total = (decimal)r[2];
                string share = grandTotal > 0m
                    ? (total / grandTotal * 100m).ToString("0.0", CultureInfo.InvariantCulture) + "%"
                    : "0.0%";
                result.Rows.Add(r[0], r[1], total, share);
            }
            return result;
        }

        private void ExportReport()
        {
            if (incomeTable == null || expenseTable == null)
            {
                MessageBox.Show("Please generate the report first.", "Information Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> lines = new List<string>();
            lines.Add(CsvHelper.Line("Expense Tracker report", periodText));
            lines.Add("");
            lines.Add(CsvHelper.Line("Summary"));
            lines.Add(CsvHelper.Line("Total income", Db.AmountText(totalIncome)));
            lines.Add(CsvHelper.Line("Total expenses", Db.AmountText(totalExpense)));
            lines.Add(CsvHelper.Line("Net balance", Db.AmountText(totalIncome - totalExpense)));
            lines.Add("");
            AddTable(lines, "Income by category", incomeTable);
            lines.Add("");
            AddTable(lines, "Expenses by category", expenseTable);
            lines.Add("");
            lines.Add(CsvHelper.Line("Monthly summary"));
            lines.Add(CsvHelper.Line("Month", "Income", "Expenses"));
            for (int i = 0; i < chart.Labels.Count; i++)
            {
                lines.Add(CsvHelper.Line(chart.Labels[i], Db.AmountText(chart.Income[i]), Db.AmountText(chart.Expenses[i])));
            }

            CsvHelper.SaveLines("report", lines);
        }

        private static void AddTable(List<string> lines, string title, DataTable table)
        {
            lines.Add(CsvHelper.Line(title));
            lines.Add(CsvHelper.Line("Category", "Entries", "Total", "Share"));
            foreach (DataRow r in table.Rows)
            {
                lines.Add(CsvHelper.Line(r["Category"], r["Entries"], Db.AmountText((decimal)r["Total"]), r["Share"]));
            }
        }
    }
}
