using System;
using System.Drawing;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>A tiny "type something" dialog (WinForms has no built-in InputBox).</summary>
    internal static class Prompt
    {
        /// <summary>Returns the typed text, or null when the user cancels.</summary>
        public static string Show(string title, string label, bool password)
        {
            using (Form form = new Form())
            {
                form.Text = title;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ShowInTaskbar = false;
                form.ClientSize = new Size(380, 130);

                Label lbl = new Label();
                lbl.Text = label;
                lbl.AutoSize = false;
                lbl.SetBounds(15, 15, 350, 24);

                TextBox txt = new TextBox();
                txt.SetBounds(15, 45, 350, 24);
                txt.UseSystemPasswordChar = password;

                Button ok = new Button();
                ok.Text = "OK";
                ok.DialogResult = DialogResult.OK;
                ok.SetBounds(195, 85, 80, 30);

                Button cancel = new Button();
                cancel.Text = "Cancel";
                cancel.DialogResult = DialogResult.Cancel;
                cancel.SetBounds(285, 85, 80, 30);

                form.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
                form.AcceptButton = ok;
                form.CancelButton = cancel;

                return form.ShowDialog() == DialogResult.OK ? txt.Text : null;
            }
        }
    }
}
