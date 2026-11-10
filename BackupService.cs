using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Personal_Expense_Tracker
{
    /// <summary>
    /// Database backup and restore.
    /// Backup  = every table is saved into ONE xml file (with its structure).
    /// Restore = inside ONE transaction the tables are emptied and refilled from that file,
    ///           so a failed restore changes nothing.
    /// </summary>
    internal static class BackupService
    {
        public static readonly string[] Tables = { "users", "Categories", "income", "expenses" };

        // columns a backup file must contain to be accepted
        private static readonly Dictionary<string, string[]> RequiredColumns = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            { "users", new[] { "id", "username", "password" } },
            { "Categories", new[] { "id", "category", "type", "status" } },
            { "income", new[] { "id", "category", "item", "income", "date_income" } },
            { "expenses", new[] { "id", "category", "item", "cost", "date_expense" } }
        };

        public static string AutoBackupFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups"); }
        }

        /// <summary>Saves all tables to the file and returns a short summary (rows per table).</summary>
        public static string Backup(string path)
        {
            DataSet ds = new DataSet("ExpenseBackup");
            using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
            {
                connect.Open();
                foreach (string table in Tables)
                {
                    DataTable dt = new DataTable(table);
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + table, connect))
                    {
                        adapter.Fill(dt);
                    }
                    ds.Tables.Add(dt);
                }
            }
            WriteFile(ds, path);
            return Summary(ds);
        }

        public static void WriteFile(DataSet ds, string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder)) Directory.CreateDirectory(folder);
            ds.WriteXml(path, XmlWriteMode.WriteSchema);
        }

        /// <summary>Reads a backup file and checks that it really is one. Throws InvalidDataException if not.</summary>
        public static DataSet ReadAndValidate(string path)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(path, XmlReadMode.ReadSchema);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("This is not a valid backup file.\n" + ex.Message);
            }

            foreach (string table in Tables)
            {
                if (!ds.Tables.Contains(table))
                    throw new InvalidDataException("The backup file has no '" + table + "' table.");

                foreach (string col in RequiredColumns[table])
                {
                    if (!ds.Tables[table].Columns.Contains(col))
                        throw new InvalidDataException("The table '" + table + "' in the backup file has no '" + col + "' column.");
                }
            }
            return ds;
        }

        public static string Summary(DataSet ds)
        {
            return string.Join(", ", Tables.Select(t => t + ": " + ds.Tables[t].Rows.Count));
        }

        /// <summary>
        /// Replaces ALL data with the content of the backup file (all-or-nothing).
        /// A safety copy of the current data is written to the Backups folder first.
        /// </summary>
        public static string Restore(string path)
        {
            DataSet backup = ReadAndValidate(path);

            string safety = Path.Combine(AutoBackupFolder, "auto_before_restore_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
            Backup(safety);

            using (SqlConnection connect = new SqlConnection(Db.ConnectionString))
            {
                connect.Open();
                using (SqlTransaction tx = connect.BeginTransaction())
                {
                    try
                    {
                        foreach (string table in Tables)
                        {
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM " + table, connect, tx))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (string table in Tables)
                        {
                            DataTable dt = backup.Tables[table];
                            using (SqlBulkCopy bulk = new SqlBulkCopy(connect, SqlBulkCopyOptions.KeepIdentity, tx))
                            {
                                bulk.DestinationTableName = table;
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }
                                bulk.WriteToServer(dt);
                            }
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }

            // a backup from an older version may lack roles or contain plain-text passwords
            Db.Migrate();

            return Summary(backup) + "\nSafety copy of the previous data:\n" + safety;
        }
    }
}
