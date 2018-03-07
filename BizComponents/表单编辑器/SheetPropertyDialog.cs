using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public partial class SheetPropertyDialog : Form
    {
        SheetView ActiveSheet;

        public SheetPropertyDialog(SheetView ActiveSheet)
        {
            InitializeComponent();

            RowCount.Maximum = int.MaxValue;
            ColumnCount.Maximum = int.MaxValue;

            this.ActiveSheet = ActiveSheet;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ActiveSheet.RowCount = System.Convert.ToInt32(RowCount.Value);
            ActiveSheet.ColumnCount = System.Convert.ToInt32(ColumnCount.Value);

            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void DisplayInformation(string row, string col)
        {
            this.label3.Text = string.Format("参考值：表单共 {0} 行 {1} 列。", row, col);
        }
    }
}
