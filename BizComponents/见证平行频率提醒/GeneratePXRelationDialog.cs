using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;

namespace BizComponents
{
    public partial class GeneratePXRelationDialog : Form
    {
        Guid SGDataID;
        Guid ModuleID;
        public GeneratePXRelationDialog(Guid _sgDataID, Guid _moduleID)
        {
            InitializeComponent();
            SGDataID = _sgDataID;
            ModuleID = _moduleID;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtBGBH.Text.Trim() != "")
            {
                BindPXBGData();
            }
            else
            {
                MessageBox.Show("请输入报告/委托编号");
            }
        }

        private void BindPXBGData()
        {

            DataTable Data = PXJZReportDataList.GetEnPXedReport(ModuleID, txtBGBH.Text.Trim());

            if (Data != null && Data.Rows.Count > 0)
            {
                spread_stadium.ShowRow(spread_stadium.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                spread_stadium_Sheet1.Rows.Count = Data.Rows.Count;

                int HiddenColumnCount = 2;
                spread_stadium_Sheet1.Columns.Count = Data.Columns.Count - HiddenColumnCount;
                if (spread_stadium_Sheet1.Columns.Count > 0)
                {
                    spread_stadium_Sheet1.Columns[0].Width = 60;
                    spread_stadium_Sheet1.Columns[1].Width = 100;
                    spread_stadium_Sheet1.Columns[2].Width = 80;
                    spread_stadium_Sheet1.Columns[3].Width = 80;
                    spread_stadium_Sheet1.Columns[4].Width = 60;
                    spread_stadium_Sheet1.Columns[5].Width = 60;
                    spread_stadium_Sheet1.Columns[6].Width = 80;
                    spread_stadium_Sheet1.Columns[7].Width = 80;
                    //spread_stadium_Sheet1.Columns[8].Width = 80;
                    //spread_stadium_Sheet1.Columns[9].Width = 80;
                }

                DateTimeCellType datetime = new DateTimeCellType();
                datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
                FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                text.Multiline = true;
                text.WordWrap = true;

                spread_stadium_Sheet1.Columns[7].CellType = datetime;
                //spread_stadium_Sheet1.Columns[0].Visible = false;
                //spread_stadium_Sheet1.Columns[1].Visible = false;

                spread_stadium_Sheet1.Rows.Count = Data.Rows.Count;

                int i, j;
                foreach (System.Data.DataColumn Column in Data.Columns)
                {
                    if (Column.ColumnName == "ID" || Column.ColumnName == "ModuleID")
                        continue;

                    i = Data.Columns.IndexOf(Column);
                    spread_stadium_Sheet1.Columns[i - HiddenColumnCount].VerticalAlignment = CellVerticalAlignment.Center;
                    spread_stadium_Sheet1.Columns[i - HiddenColumnCount].Label = Column.ColumnName;

                    foreach (DataRow Row in Data.Rows)
                    {
                        j = Data.Rows.IndexOf(Row);
                        spread_stadium_Sheet1.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        spread_stadium_Sheet1.Cells[j, i - HiddenColumnCount].Value = Row[Column.ColumnName].ToString();
                    }
                }

                foreach (DataRow Row in Data.Rows)
                {
                    j = Data.Rows.IndexOf(Row);
                    spread_stadium_Sheet1.Rows[j].Tag = Row["ID"].ToString() + "," + Row["ModuleID"].ToString();
                }

                spread_stadium.CellDoubleClick -= new CellClickEventHandler(spread_stadium_CellDoubleClick);
                spread_stadium.CellDoubleClick += new CellClickEventHandler(spread_stadium_CellDoubleClick);
            }
            else
            {
                MessageBox.Show("未找到数据，请检查委托/报告编号是否正确；或打开重新保存资料！");
            }
        }

        static void spread_stadium_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FpSpread fpSpread = sender as FpSpread;
            Row Row = fpSpread.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String)
            {
                String[] Tokens = Row.Tag.ToString().Split(',');

                DataDialog Dialog = new DataDialog(new Guid(Tokens[0]), new Guid(Tokens[1]));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Dialog.Owner = Owner;
                Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Dialog.Size = Owner.ClientRectangle.Size;
                Dialog.ReadOnly = true;
                Dialog.ShowDialog();
            }
        }

        private void tsmiResetPXRelation_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您是否确定生成对应的平行关系？", "确认操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Row Row = spread_stadium_Sheet1.ActiveRow;

                if (Row != null && Row.Tag is String)
                {
                    String[] Tokens = Row.Tag.ToString().Split(',');
                    Guid PXDataID = new Guid(Tokens[0]);
                    int flag = PXJZReportDataList.GeneratePXRelation(SGDataID, PXDataID);
                    if (flag == 1)
                    {
                        MessageBox.Show("平行对应关系生成成功,请到平行关系对应查询中查看！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("平行对应关系生成失败，请稍后再试！");
                    }
                }
            }
        }
    }
}
