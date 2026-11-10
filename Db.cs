using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>
    /// ONE place for the database connection string and a few shared helpers.
    /// Expense.mdf is found automatically (it is searched upwards from the .exe folder),
    /// so the project works from any folder or PC - no hard-coded D:\ path needed.
    /// </summary>
    internal static class Db
    {
        public const string RoleAdmin = "Admin";
        public const string RoleStaff = "Staff";

        private const string DatabaseFileName = "Expense.mdf";

        // Last-resort path (the one the project originally used)
        private const string LegacyPath =
            @"D:\Safe Folder\Learning code\c #\PROJECT_C#\Personal Expense Tracker\Expense.mdf";

        private static string _databasePath;
        private static string _connectionString;

        public static string DatabasePath
        {
            get
            {
                if (_databasePath == null) _databasePath = FindDatabaseFile();
                return _databasePath;
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
                    builder.AttachDBFilename = DatabasePath;
                    builder.IntegratedSecurity = true;
                    builder.ConnectTimeout = 30;
                    builder.Encrypt = false;
                    builder.TrustServerCertificate = true;
                    builder.Pooling = false;
                    _connectionString = builder.ConnectionString;
                }
                return _connectionString;
            }
        }

        private static string FindDatabaseFile()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                for (int i = 0; i < 8 && dir != null; i++, dir = dir.Parent)
                {
                    string candidate = Path.Combine(dir.FullName, DatabaseFileName);
                    if (File.Exists(candidate)) return candidate;
                }
            }
            catch
            {
                // ignore and use the fallback below
            }
            return LegacyPath;
        }

        /// <summary>Returns null when the database is OK, otherwise a friendly error message.</summary>
        public static string CheckDatabase()
        {
            if (!File.Exists(DatabasePath))
            {
                return "The database file '" + DatabaseFileName + "' was not found.\n\n" +
                       "Keep it in the solution folder (the folder that contains the 'Personal Expense Tracker' project folder).\n\n" +
                       "Looked for:\n" + DatabasePath;
            }
            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {
                    connect.Open();
                }
                return null;
            }
            catch (Exception ex)
            {
                return "Cannot open the database.\n\n" +
                       "Make sure SQL Server LocalDB is installed (it comes with Visual Studio).\n\n" +
                       "Details: " + ex.Message;
            }
        }

        /// <summary>True when a control is being shown inside the Visual Studio designer.</summary>
        public static bool InDesigner(Control control)
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (control != null && control.Site != null && control.Site.DesignMode);
        }

        public static void ShowError(Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- amounts (income / cost are stored as text in the database) ----------

        /// <summary>Accepts 25, 25.5, 1,250.75, $30 ... and rejects anything else (letters, 0, negatives).</summary>
        public static bool TryParseAmount(string text, out decimal amount)
        {
            amount = 0m;
            if (string.IsNullOrWhiteSpace(text)) return false;

            string cleaned = text.Replace("$", "").Trim();
            decimal value;
            if (!decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return false;
            if (value <= 0m || value > 99999999.99m) return false;

            amount = Math.Round(value, 2);
            return true;
        }

        /// <summary>The text saved in the database, always like 25.00 (never depends on PC culture).</summary>
        public static string AmountText(decimal amount)
        {
            return amount.ToString("0.00", CultureInfo.InvariantCulture);
        }

        public static string Money(decimal value)
        {
            return "$" + value.ToString("N2", CultureInfo.InvariantCulture);
        }

        // ---------- dates ----------

        /// <summary>The data classes show dates as MM-dd-yyyy; this turns that text back into a date on ANY PC culture.</summary>
        public static DateTime ParseDisplayDate(string text)
        {
            DateTime date;
            if (DateTime.TryParseExact(text, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return date;
            return DateTime.Today;
        }

        // ---------- one-time upgrades (safe to run on every start) ----------

        /// <summary>
        /// 1) adds the user_role column (first account = Admin, the others = Staff)
        /// 2) replaces old plain-text passwords with PBKDF2 hashes
        /// Running it again changes nothing.
        /// </summary>
        public static void Migrate()
        {
            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();

                RunSql(connect, "IF COL_LENGTH('users', 'user_role') IS NULL ALTER TABLE users ADD user_role VARCHAR(20) NULL");
                RunSql(connect, "UPDATE users SET user_role = '" + RoleAdmin + "' WHERE user_role IS NULL AND id = (SELECT MIN(id) FROM users)");
                RunSql(connect, "UPDATE users SET user_role = '" + RoleStaff + "' WHERE user_role IS NULL");
                // never leave the system without an administrator
                RunSql(connect, "UPDATE users SET user_role = '" + RoleAdmin + "' WHERE id = (SELECT MIN(id) FROM users) " +
                                "AND NOT EXISTS (SELECT 1 FROM users WHERE user_role = '" + RoleAdmin + "')");

                // hash any plain-text password
                List<KeyValuePair<int, string>> plain = new List<KeyValuePair<int, string>>();
                using (SqlCommand cmd = new SqlCommand("SELECT id, password FROM users WHERE password IS NOT NULL AND password NOT LIKE 'PBKDF2$%'", connect))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        plain.Add(new KeyValuePair<int, string>(Convert.ToInt32(reader["id"]), reader["password"].ToString()));
                    }
                }
                foreach (KeyValuePair<int, string> row in plain)
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE users SET password = @pass WHERE id = @id", connect))
                    {
                        cmd.Parameters.AddWithValue("@pass", PasswordHasher.Hash(row.Value));
                        cmd.Parameters.AddWithValue("@id", row.Key);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void RunSql(SqlConnection connect, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // ---------- shared report math ----------

        /// <summary>
        /// Sums an amount column for the dates  fromDate &lt;= date &lt; toDate  (either side may be null = no limit).
        /// Amounts are stored as text, so TRY_CAST is used: a badly typed old value is skipped instead of crashing.
        /// Table/column names are constants from our own code only.
        /// </summary>
        public static decimal SumAmount(SqlConnection connect, string table, string amountColumn,
                                        string dateColumn, DateTime? fromDate, DateTime? toDate)
        {
            string sql = "SELECT SUM(TRY_CAST(" + amountColumn + " AS DECIMAL(18,2))) FROM " + table;
            string joiner = " WHERE ";
            if (fromDate.HasValue) { sql += joiner + dateColumn + " >= @from"; joiner = " AND "; }
            if (toDate.HasValue) { sql += joiner + dateColumn + " < @to"; }

            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                if (fromDate.HasValue) cmd.Parameters.Add("@from", SqlDbType.Date).Value = fromDate.Value.Date;
                if (toDate.HasValue) cmd.Parameters.Add("@to", SqlDbType.Date).Value = toDate.Value.Date;

                object result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0m : Convert.ToDecimal(result);
            }
        }
    }
}
