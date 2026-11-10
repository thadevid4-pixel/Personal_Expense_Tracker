using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Personal_Expense_Tracker
{
    /// <summary>Bar chart (Income vs Expenses per month) drawn with GDI+, so no chart library is needed.</summary>
    internal class MonthlyChart : Control
    {
        public string Title = "";
        public List<string> Labels = new List<string>();
        public List<decimal> Income = new List<decimal>();
        public List<decimal> Expenses = new List<decimal>();

        private static readonly Color IncomeColor = Color.FromArgb(49, 121, 84);
        private static readonly Color ExpenseColor = Color.FromArgb(214, 80, 60);

        public MonthlyChart()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            using (Font titleFont = new Font("Times New Roman", 12F, FontStyle.Bold))
            {
                g.DrawString(Title, titleFont, Brushes.Black, 10, 8);
            }
            DrawLegend(g);

            int n = Labels.Count;
            decimal max = 0m;
            if (Income.Count > 0) max = Math.Max(max, Income.Max());
            if (Expenses.Count > 0) max = Math.Max(max, Expenses.Max());
            if (n == 0 || max <= 0m)
            {
                g.DrawString("No data for this period.", Font, Brushes.Gray, 12, 48);
                return;
            }

            Rectangle area = new Rectangle(15, 50, Math.Max(20, Width - 30), Math.Max(20, Height - 50 - 32));
            float slot = area.Width / (float)n;
            float barWidth = slot * 0.32f;

            using (SolidBrush incomeBrush = new SolidBrush(IncomeColor))
            using (SolidBrush expenseBrush = new SolidBrush(ExpenseColor))
            using (Font small = new Font("Microsoft Sans Serif", 8.5F))
            using (StringFormat center = new StringFormat())
            {
                center.Alignment = StringAlignment.Center;
                for (int i = 0; i < n; i++)
                {
                    float x0 = area.Left + i * slot + (slot - 2 * barWidth) / 2;
                    DrawBar(g, incomeBrush, small, center, x0, barWidth, area, Value(Income, i), max);
                    DrawBar(g, expenseBrush, small, center, x0 + barWidth, barWidth, area, Value(Expenses, i), max);
                    g.DrawString(Labels[i], small, Brushes.Black,
                                 new RectangleF(area.Left + i * slot, area.Bottom + 4, slot, 18), center);
                }
                g.DrawLine(Pens.Gray, area.Left, area.Bottom, area.Right, area.Bottom);
            }
        }

        private static decimal Value(List<decimal> list, int i)
        {
            return i < list.Count ? list[i] : 0m;
        }

        private static void DrawBar(Graphics g, Brush brush, Font font, StringFormat center, float x, float width,
                                    Rectangle area, decimal value, decimal max)
        {
            float h = (float)(value / max) * (area.Height - 20);
            float y = area.Bottom - h;
            if (h > 0.5f) g.FillRectangle(brush, x, y, width, h);
            if (value > 0m)
                g.DrawString(value.ToString("N0"), font, Brushes.Black, new RectangleF(x - 14, y - 16, width + 28, 16), center);
        }

        private void DrawLegend(Graphics g)
        {
            int x = Math.Max(150, Width - 190);
            using (SolidBrush a = new SolidBrush(IncomeColor))
            using (SolidBrush b = new SolidBrush(ExpenseColor))
            {
                g.FillRectangle(a, x, 12, 12, 12);
                g.DrawString("Income", Font, Brushes.Black, x + 16, 9);
                g.FillRectangle(b, x + 80, 12, 12, 12);
                g.DrawString("Expenses", Font, Brushes.Black, x + 96, 9);
            }
        }
    }
}
