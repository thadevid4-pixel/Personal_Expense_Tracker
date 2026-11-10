using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>Export to CSV (opens fine in Excel).</summary>
    internal static class CsvHelper
    {
        public static string Escape(string s)
        {
            if (s == null) s = "";
            if (s.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0)
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        public static string Line(params object[] values)
        {
            return string.Join(",", values.Select(v => Escape(Convert.ToString(v, CultureInfo.InvariantCulture))));
        }

        /// <summary>Exports the visible columns/rows of a DataGridView (so filters are respected).</summary>
        public static void ExportGrid(DataGridView grid, string defaultName)
        {
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<DataGridViewColumn> cols = grid.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToList();

            List<string> lines = new List<string>();
            lines.Add(Line(cols.Select(c => (object)c.HeaderText).ToArray()));
            foreach (DataGridViewRow row in grid.Rows)
            {
                lines.Add(Line(cols.Select(c => row.Cells[c.Index].Value).ToArray()));
            }
            SaveLines(defaultName, lines);
        }

        /// <summary>Asks where to save, then writes the lines (UTF-8 with BOM so Excel shows all characters).</summary>
        public static void SaveLines(string defaultName, IList<string> lines)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "CSV file (*.csv)|*.csv";
                dlg.FileName = defaultName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm", CultureInfo.InvariantCulture) + ".csv";
                dlg.Title = "Export to CSV";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    File.WriteAllLines(dlg.FileName, lines, new UTF8Encoding(true));
                    MessageBox.Show("Exported successfully:\n" + dlg.FileName, "Information Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not save the file.\n\n" + ex.Message, "Error Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
