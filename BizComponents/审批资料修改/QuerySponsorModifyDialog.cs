using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizCommon;
using Yqun.Interfaces;
using BizComponents;
using Yqun.Bases;
using System.IO;
using FarPoint.Win.Spread.Model;

namespace BizComponents
{
    public partial class QuerySponsorModifyDialog : Form
    {
        public QuerySponsorModifyDialog()
        {
            InitializeComponent();
        }

        private void QuerySponsorModifyDialog_Load(object sender, EventArgs e)
        {
            ComboBox_Segments.Items.Clear();
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.SelectedIndex = 0;

            if (cb_state.Items.Count > 0)
                cb_state.SelectedIndex = 0;
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在获取数据...";

            String segment = "";
            String company = "";
            String testroom = "";
            String SelectedState = cb_state.SelectedItem.ToString();
            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                segment = (ComboBox_Segments.SelectedItem as Prjsct).PrjsctCode;
            }
            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                company = (ComboBox_Company.SelectedItem as Orginfo).DepCode;
            }
            if (ComboBox_TestRooms.SelectedItem is PrjFolder)
            {
                testroom = (ComboBox_TestRooms.SelectedItem as PrjFolder).FolderCode;
            }

            DateTime start = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", StartDateTimePicker.Value.Year, StartDateTimePicker.Value.Month, StartDateTimePicker.Value.Day));
            DateTime end = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", EndDateTimePicker.Value.Year, EndDateTimePicker.Value.Month, EndDateTimePicker.Value.Day));

            DataTable Data = DocumentHelperClient.GetRequestChangeList(segment, company, testroom, start, end, SelectedState, tb_content.Text.Trim(), tb_user.Text.Trim());
            if (Data != null)
            {
                FpSpread FpSpread = fpSpread1;
                SheetView FpSpread_Info = fpSpread1_Sheet;

                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);

                FpSpread_Info.Columns.Count = 12;
                FpSpread_Info.Columns[0].Width = 60;
                FpSpread_Info.Columns[1].Width = 60;
                FpSpread_Info.Columns[2].Width = 80;
                FpSpread_Info.Columns[3].Width = 80;
                FpSpread_Info.Columns[4].Width = 240;
                FpSpread_Info.Columns[5].Width = 160;
                FpSpread_Info.Columns[6].Width = 80;
                FpSpread_Info.Columns[7].Width = 50;
                FpSpread_Info.Columns[8].Width = 120;
                FpSpread_Info.Columns[9].Width = 300;
                FpSpread_Info.Columns[10].Width = 300;

                FpSpread_Info.Columns[0, FpSpread_Info.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;

                TextCellType text = new TextCellType();
                text.Multiline = true;
                text.WordWrap = true;

                FpSpread_Info.Columns[0, 2].CellType = text;

                FpSpread_Info.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {
                    FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Height = 20;
                    FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Locked = true;
                    FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                }

                int i, j;
                foreach (System.Data.DataColumn Column in Data.Columns)
                {
                    if (Column.ColumnName == "ID" || Column.ColumnName == "DataID" || Column.ColumnName == "ModuleID" )
                        continue;

                    i = Data.Columns.IndexOf(Column);
                    FpSpread_Info.Columns[i - 3].VerticalAlignment = CellVerticalAlignment.Center;
                    FpSpread_Info.Columns[i - 3].Label = Column.ColumnName;

                    foreach (DataRow Row in Data.Rows)
                    {
                        j = Data.Rows.IndexOf(Row);
                        FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        FpSpread_Info.Cells[j, i - 3].Value = Row[Column.ColumnName].ToString();
                    }
                }

                foreach (DataRow Row in Data.Rows)
                {
                    j = Data.Rows.IndexOf(Row);
                    FpSpread_Info.Rows[j].Tag = Row["ID"].ToString() + "," + Row["DataID"].ToString() + "," + Row["ModuleID"].ToString();// Row["ID"].ToString();
                }
                if (Data.Rows.Count == 0)
                {
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                    MessageBox.Show("无数据，请重新选择条件!");
                }
            }
            else
            {
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
                MessageBox.Show("无数据，请重新选择条件!");
            }
            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();
        }

        private void ComboBox_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedState = cb_state.SelectedItem.ToString();
            foreach (Row Row in fpSpread1_Sheet.Rows)
            {
                String state = fpSpread1_Sheet.Cells[Row.Index, 0].Text;
                Row.Visible = (SelectedState == "全部" || SelectedState == state);
            }
        }

        private void ComboBox_Segments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Company.Items.Clear();
            ComboBox_Company.Items.Add("全部单位");

            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                Prjsct prjsct = ComboBox_Segments.SelectedItem as Prjsct;
                List<Orginfo> OrgInfos = DepositoryOrganInfo.QueryOrgans(prjsct.PrjsctCode, "");
                ComboBox_Company.Items.AddRange(OrgInfos.ToArray());
            }

            if (ComboBox_Company.Items.Count > 1)
                ComboBox_Company.SelectedIndex = 1;
            else
                ComboBox_Company.SelectedIndex = 0;
        }

        private void ComboBox_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_TestRooms.Items.Clear();
            ComboBox_TestRooms.Items.Add("全部试验室");

            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                Orginfo Orginfo = ComboBox_Company.SelectedItem as Orginfo;
                List<PrjFolder> PrjFolders = DepositoryFolderInfo.QueryPrjFolders(Orginfo.DepCode, "");
                ComboBox_TestRooms.Items.AddRange(PrjFolders.ToArray());
            }

            if (ComboBox_TestRooms.Items.Count > 1)
                ComboBox_TestRooms.SelectedIndex = 1;
            else
                ComboBox_TestRooms.SelectedIndex = 0;
        }

        private void ComboBox_Segments_DropDown(object sender, EventArgs e)
        {
            ComboBox_Segments.Items.Clear();
            List<Prjsct> PrjsctList = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.Items.AddRange(PrjsctList.ToArray());
        }

        private void fpSpread1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Row Row = fpSpread1.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String && Row.Tag.ToString() != "")
            {
                string tag = fpSpread1.ActiveSheet.ActiveRow.Tag.ToString();
                ModificationDetailView detalis = new ModificationDetailView(tag);
                detalis.ShowDialog();
            }
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                String fullPath = folderBrowserDialog1.SelectedPath;
                String fileName = Path.Combine(fullPath, string.Format("资料修改查询({0}-{1}-{2}).xls", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                fpSpread1.SaveExcel(fileName, IncludeHeaders.ColumnHeadersCustomOnly);
                MessageBox.Show(string.Format("资料修改查询“{0}”导出完成", fileName));
            }
        }
    }
}
