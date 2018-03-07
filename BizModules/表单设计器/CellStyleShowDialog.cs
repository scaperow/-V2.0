using System;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizComponents;
using BizCommon;
using FarPoint.Win;
using FarPoint.Win.Spread.Model;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using FarPoint.Win.Spread.CellType;
using System.Data;

namespace BizModules
{
    public partial class CellStyleShowDialog : Form
    {
        private Guid SheetID;
        private DataTable DTCellStyle;
        public CellStyleShowDialog(Guid _SheetID, DataTable dt)
        {
            InitializeComponent();
            SheetID = _SheetID;
            DTCellStyle = dt;
        }

        private void DetailDataItems_Load(object sender, EventArgs e)
        {
            DataItems_Sheet1.Columns.Count = 4;
            DataItems_Sheet1.DataSource = DTCellStyle;
            DataItems_Sheet1.ColumnHeader.Columns[0].Visible = false;
            DataItems_Sheet1.ColumnHeader.Columns[1].Visible = false;
            DataItems_Sheet1.ColumnHeader.Columns[2].Label = "位置";
            DataItems_Sheet1.ColumnHeader.Columns[3].Label = "样式";
            DataItems_Sheet1.ColumnHeader.Columns[2].Width =60;
            DataItems_Sheet1.ColumnHeader.Columns[3].Width = 680;
        }

        private void Button_deletetable_Click(object sender, EventArgs e)
        {
            Int32 activeRowIndex = DataItems_Sheet1.ActiveRowIndex;
            //string strSheetID = DataItems_Sheet1.Cells[activeRowIndex, 1].Value.ToString();
            string strCellName = DataItems_Sheet1.Cells[activeRowIndex, 2].Value.ToString();
            DataRow[] drArr = DTCellStyle.Select("SheetID ='" + SheetID + "' and CellName='" + strCellName + "' ");
            if (drArr != null && drArr.Length > 0)
            {
                BizComponents.ModuleHelperClient.DeleteCellStyle(SheetID, strCellName);
                DataRow row = drArr[0];
                DTCellStyle.Rows.Remove(row);
                //DataItems_Sheet1.Rows.Remove(activeRowIndex, 1);
            }
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            SheetDesinger sheetDesinger = (SheetDesinger)this.Owner;
            sheetDesinger.dtCellStyle = DTCellStyle;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
