using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>The matching rules (no controls here, so they are easy to test).</summary>
    internal static class FilterLogic
    {
        /// <param name="searchText">empty = no text filter</param>
        /// <param name="category">empty = all categories</param>
        /// <param name="dateText">the grid date text (MM-dd-yyyy)</param>
        public static bool Matches(string searchText, string category, bool dateFilterOn, DateTime from, DateTime to,
                                   string rowCategory, string rowDateText, params string[] rowTexts)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                bool found = false;
                foreach (string t in rowTexts)
                {
                    if (t != null && t.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) { found = true; break; }
                }
                if (!found) return false;
            }

            if (!string.IsNullOrEmpty(category) &&
                !string.Equals(category, rowCategory, StringComparison.OrdinalIgnoreCase))
                return false;

            if (dateFilterOn)
            {
                DateTime d;
                if (!DateTime.TryParseExact(rowDateText, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                    return false;
                if (d.Date < from.Date || d.Date > to.Date) return false;
            }
            return true;
        }
    }

    /// <summary>Search box + category filter + date range + Reset + Export CSV, placed above a grid.</summary>
    internal class FilterBar : Panel
    {
        private readonly TextBox txtSearch = new TextBox();
        private readonly ComboBox cboCategory = new ComboBox();
        private readonly CheckBox chkDate = new CheckBox();
        private readonly DateTimePicker dtFrom = new DateTimePicker();
        private readonly DateTimePicker dtTo = new DateTimePicker();
        private readonly Button btnReset = new Button();
        private readonly Button btnExport = new Button();
        private bool suppress;

        public event EventHandler FilterChanged;
        public event EventHandler ExportClicked;

        public FilterBar()
        {
            this.Size = new Size(792, 36);
            this.BackColor = Color.White;
            Font small = new Font("Microsoft Sans Serif", 9F);
            this.Font = small;

            AddLabel("Search:", 0, 9, 50);
            txtSearch.SetBounds(52, 5, 120, 22);
            txtSearch.TextChanged += delegate { RaiseChanged(); };

            AddLabel("Category:", 180, 9, 62);
            cboCategory.SetBounds(244, 5, 110, 22);
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Items.Add("All");
            cboCategory.SelectedIndex = 0;
            cboCategory.SelectedIndexChanged += delegate { RaiseChanged(); };

            chkDate.Text = "Date:";
            chkDate.SetBounds(362, 7, 54, 22);
            chkDate.CheckedChanged += delegate
            {
                dtFrom.Enabled = chkDate.Checked;
                dtTo.Enabled = chkDate.Checked;
                RaiseChanged();
            };

            dtFrom.SetBounds(418, 5, 100, 22);
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Enabled = false;
            dtFrom.ValueChanged += delegate { RaiseChanged(); };

            AddLabel("to", 522, 9, 16);

            dtTo.SetBounds(540, 5, 100, 22);
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Enabled = false;
            dtTo.ValueChanged += delegate { RaiseChanged(); };

            StyleButton(btnReset, "Reset", 650, 56);
            btnReset.Click += delegate { Reset(); };

            StyleButton(btnExport, "Export CSV", 708, 84);
            btnExport.Click += delegate { if (ExportClicked != null) ExportClicked(this, EventArgs.Empty); };

            this.Controls.AddRange(new Control[] { txtSearch, cboCategory, chkDate, dtFrom, dtTo, btnReset, btnExport });
        }

        private void AddLabel(string text, int x, int y, int width)
        {
            Label l = new Label();
            l.Text = text;
            l.SetBounds(x, y, width, 18);
            l.BackColor = Color.White;
            this.Controls.Add(l);
        }

        private static void StyleButton(Button b, string text, int x, int width)
        {
            b.Text = text;
            b.SetBounds(x, 3, width, 27);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Color.FromArgb(49, 121, 84);
            b.ForeColor = Color.White;
            b.UseVisualStyleBackColor = false;
        }

        private void RaiseChanged()
        {
            if (!suppress && FilterChanged != null) FilterChanged(this, EventArgs.Empty);
        }

        public void Reset()
        {
            suppress = true;
            txtSearch.Text = "";
            cboCategory.SelectedIndex = 0;
            chkDate.Checked = false;
            suppress = false;
            RaiseChanged();
        }

        /// <summary>Refills the category list and keeps the current choice when it still exists.</summary>
        public void SetCategories(IEnumerable<string> categories)
        {
            string current = SelectedCategory;
            suppress = true;
            cboCategory.Items.Clear();
            cboCategory.Items.Add("All");
            foreach (string c in categories.Where(x => !string.IsNullOrWhiteSpace(x))
                                           .Distinct(StringComparer.OrdinalIgnoreCase)
                                           .OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
            {
                cboCategory.Items.Add(c);
            }
            int index = string.IsNullOrEmpty(current) ? 0 : cboCategory.FindStringExact(current);
            cboCategory.SelectedIndex = index < 0 ? 0 : index;
            suppress = false;
        }

        public string SearchText { get { return txtSearch.Text.Trim(); } }

        public string SelectedCategory
        {
            get { return cboCategory.SelectedIndex <= 0 ? "" : cboCategory.SelectedItem.ToString(); }
        }

        public bool Matches(string rowCategory, string rowDateText, params string[] rowTexts)
        {
            return FilterLogic.Matches(SearchText, SelectedCategory, chkDate.Checked,
                                       dtFrom.Value, dtTo.Value, rowCategory, rowDateText, rowTexts);
        }
    }
}
