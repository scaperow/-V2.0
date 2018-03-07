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
    public partial class ResetStadiumDialog : Form
    {
        public ResetStadiumDialog()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Trim() != "")
            {
                BindStadiumData();
            }
            else
            {
                MessageBox.Show("请输入委托编号");
            }
        }

        private void BindStadiumData()
        {
            
            DataTable Data = DocumentHelperClient.SearchStadiumByWTBH(toolStripTextBox1.Text.Trim());

            if (Data != null)
            {
                spread_stadium.ShowRow(spread_stadium.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                spread_stadium_sheet.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count == 0)
                {
                    return;
                }
                int HiddenColumnCount = 4;
                spread_stadium_sheet.Columns.Count = Data.Columns.Count - HiddenColumnCount;
                if (spread_stadium_sheet.Columns.Count > 0)
                {
                    spread_stadium_sheet.Columns[0].Width = 90;
                    spread_stadium_sheet.Columns[1].Width = 120;
                    spread_stadium_sheet.Columns[2].Width = 140;
                    spread_stadium_sheet.Columns[3].Width = 120;
                    spread_stadium_sheet.Columns[4].Width = 110;
                    spread_stadium_sheet.Columns[5].Width = 80;
                    spread_stadium_sheet.Columns[6].Width = 90;
                    spread_stadium_sheet.Columns[7].Width = 80;
                    spread_stadium_sheet.Columns[8].Width = 120;
                    spread_stadium_sheet.Columns[9].Width = 120;
                    spread_stadium_sheet.Columns[10].Width = 120;
                    spread_stadium_sheet.Columns[0, spread_stadium_sheet.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                    spread_stadium_sheet.Columns[0, spread_stadium_sheet.Columns.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                }

                DateTimeCellType datetime = new DateTimeCellType();
                datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
                FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                text.Multiline = true;
                text.WordWrap = true;

                spread_stadium_sheet.Columns[5].CellType = datetime;

                spread_stadium_sheet.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {
                    spread_stadium_sheet.Rows[0, spread_stadium_sheet.Rows.Count - 1].Height = 35;
                    spread_stadium_sheet.Rows[0, spread_stadium_sheet.Rows.Count - 1].Locked = true;
                    spread_stadium_sheet.Rows[0, spread_stadium_sheet.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                    spread_stadium_sheet.Rows[0, spread_stadium_sheet.Rows.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                }

                int i, j;
                foreach (System.Data.DataColumn Column in Data.Columns)
                {
                    if (Column.ColumnName == "ID" || Column.ColumnName == "DataID" || Column.ColumnName == "ModuleID" || Column.ColumnName == "DateSpan")
                        continue;

                    i = Data.Columns.IndexOf(Column);
                    spread_stadium_sheet.Columns[i - HiddenColumnCount].VerticalAlignment = CellVerticalAlignment.Center;
                    spread_stadium_sheet.Columns[i - HiddenColumnCount].Label = Column.ColumnName;

                    foreach (DataRow Row in Data.Rows)
                    {
                        j = Data.Rows.IndexOf(Row);
                        spread_stadium_sheet.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        spread_stadium_sheet.Cells[j, i - HiddenColumnCount].Value = Row[Column.ColumnName].ToString();
                    }
                }

                foreach (DataRow Row in Data.Rows)
                {
                    j = Data.Rows.IndexOf(Row);
                    spread_stadium_sheet.Rows[j].Tag = Row["DataID"].ToString() + "," + Row["ModuleID"].ToString() + "," + Row["ID"].ToString();
                }

                spread_stadium.CellDoubleClick -= new CellClickEventHandler(spread_stadium_CellDoubleClick);
                spread_stadium.CellDoubleClick += new CellClickEventHandler(spread_stadium_CellDoubleClick);
            }

            if (Data == null || Data.Rows.Count == 0)
            {
                MessageBox.Show("未找到数据，请检查委托编号是否正确；或重新保存资料！");
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您是否确定将所选记录的龄期设置到今天提醒？", "确认操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Row Row = spread_stadium_sheet.ActiveRow;

                if (Row != null && Row.Tag is String)
                {
                    String[] Tokens = Row.Tag.ToString().Split(',');
                    Guid stadiumID = new Guid(Tokens[2]);
                    Boolean flag = DocumentHelperClient.ResetStadiumToToday(stadiumID);
                    if (flag)
                    {
                        MessageBox.Show("设置成功！");
                    }
                    else
                    {
                        MessageBox.Show("设置失败，请稍后再试！");
                    }
                }
            }
        }
    }
}
