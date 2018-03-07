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
using BizCommon;
using Yqun.Bases;
using System.IO;
using FarPoint.Win.Spread.Model;

namespace BizComponents
{
    public partial class QueryReportEvaluateDialog : Form
    {
        int userType;
        public QueryReportEvaluateDialog()
        {
            InitializeComponent();
        }

        void FpSpread_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (userType == 0 || userType == 1)
                {
                    contextMenuStrip1.Show(FpSpread, new Point(e.X, e.Y));
                }
            }
        }

        private void QueryReportEvaluateDialog_Load(object sender, EventArgs e)
        {

            if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位")
            {
                userType = 0;
            }
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
            {
                userType = 1;
            }
            else
            {
                userType = 1;
            }

            FpSpread_Info.Rows.Count = 0;
            FpSpread_Info.Columns.Count = 19;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "是否合格";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "标段";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "单位";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "试验室";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "试验报告";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "委托编号";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "报告编号";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "报告日期";
            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "试验项目";
            FpSpread_Info.ColumnHeader.Cells[0, 9].Text = "标准值";
            FpSpread_Info.ColumnHeader.Cells[0, 10].Text = "实测值";
            FpSpread_Info.ColumnHeader.Cells[0, 11].Text = "原因分析";
            FpSpread_Info.ColumnHeader.Cells[0, 12].Text = "监理意见";
            FpSpread_Info.ColumnHeader.Cells[0, 13].Text = "处理结果";
            FpSpread_Info.ColumnHeader.Cells[0, 14].Text = "合格时间";
            FpSpread_Info.ColumnHeader.Cells[0, 15].Text = "申请人";
            FpSpread_Info.ColumnHeader.Cells[0, 16].Text = "申请时间";
            FpSpread_Info.ColumnHeader.Cells[0, 17].Text = "批准人";
            FpSpread_Info.ColumnHeader.Cells[0, 18].Text = "批准时间";

            FpSpread_Info.Columns[0].Width = 60;
            FpSpread_Info.Columns[1].Width = 60;
            FpSpread_Info.Columns[2].Width = 100;
            FpSpread_Info.Columns[3].Width = 120;
            FpSpread_Info.Columns[4].Width = 200;
            FpSpread_Info.Columns[5].Width = 100;
            FpSpread_Info.Columns[6].Width = 100;
            FpSpread_Info.Columns[7].Width = 70;
            FpSpread_Info.Columns[8].Width = 180;
            FpSpread_Info.Columns[9].Width = 80;
            FpSpread_Info.Columns[10].Width = 60;
            FpSpread_Info.Columns[11].Width = 200;
            FpSpread_Info.Columns[12].Width = 200;
            FpSpread_Info.Columns[13].Width = 80;
            FpSpread_Info.Columns[14].Width = 80;
            FpSpread_Info.Columns[15].Width = 80;
            FpSpread_Info.Columns[16].Width = 80;
            FpSpread_Info.Columns[17].Width = 80;
            FpSpread_Info.Columns[18].Width = 80;

            FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            FpSpread_Info.Columns[7].CellType = datetime;
            FpSpread_Info.Columns[14].CellType = datetime;
            FpSpread_Info.Columns[16].CellType = datetime;
            FpSpread_Info.Columns[18].CellType = datetime;
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                FpSpread_Info.Columns[14].Visible = true;
                FpSpread_Info.Columns[15].Visible = true;
                FpSpread_Info.Columns[16].Visible = true;
                FpSpread_Info.Columns[17].Visible = true;
                FpSpread_Info.Columns[18].Visible = true;
            }
            else
            {
                FpSpread_Info.Columns[14].Visible = false;
                FpSpread_Info.Columns[15].Visible = false;
                FpSpread_Info.Columns[16].Visible = false;
                FpSpread_Info.Columns[17].Visible = false;
                FpSpread_Info.Columns[18].Visible = false;
            }
            ComboBox_Segments.Items.Clear();
            List<Prjsct> PrjsctList = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.Items.AddRange(PrjsctList.ToArray());

            if (ComboBox_Segments.Items.Count > 0)
                ComboBox_Segments.SelectedIndex = 0;

            Button_Export.Visible = true;// Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8;
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                btnTestUpload.Visible = true;
                btnTestSMSSend.Visible = true;
            }
            else
            {
                btnTestUpload.Visible = false;
                btnTestSMSSend.Visible = false;
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

            if (ComboBox_Company.Items.Count > 0)
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

            if (ComboBox_TestRooms.Items.Count > 0)
                ComboBox_TestRooms.SelectedIndex = 0;
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在获取数据...";
            String segment = "";
            String company = "";
            String testroom = "";
            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                segment = (ComboBox_Segments.SelectedItem as Prjsct).PrjsctCode;
            }
            else
            {
                segment = "";
            }
            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                company = (ComboBox_Company.SelectedItem as Orginfo).DepCode;
            }
            else
            {
                company = "";
            }
            if (ComboBox_TestRooms.SelectedItem is PrjFolder)
            {
                testroom = (ComboBox_TestRooms.SelectedItem as PrjFolder).FolderCode;
            }
            else
            {
                testroom = "";
            }
            DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_Start.Value.Year, DateTimePicker_Start.Value.Month, DateTimePicker_Start.Value.Day));
            DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_End.Value.Year, DateTimePicker_End.Value.Month, DateTimePicker_End.Value.Day));



            String sReportName = TextBox_ReportName.Text;
            String sReportNumber = TextBox_ReportNumber.Text;

            String sTestItems = TextBox_TestItems.Text;
            int SameCountSum = 0;

            DataTable Data = DocumentHelperClient.GetInvalidDocumentList(segment, company, testroom, sReportName, sReportNumber, StartDate, EndDate, sTestItems);
            if (Data != null)
            {
                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                FpSpread_Info.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {

                    FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                    text.Multiline = true;
                    text.WordWrap = true;

                    FpSpread_Info.Columns[0, 2].CellType = text;
                    FpSpread_Info.Columns[11].CellType = text;
                    FpSpread_Info.Columns[12].CellType = text;
                    FpSpread_Info.Columns[13].CellType = text;


                    FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Height = 25;
                    FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Locked = true;

                    FpSpread_Info.Columns[0, FpSpread_Info.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                    FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;

                    int i, j;
                    foreach (System.Data.DataColumn Column in Data.Columns)
                    {
                        if (Column.ColumnName == "ID" || Column.ColumnName == "ModuleID")
                            continue;

                        i = Data.Columns.IndexOf(Column);
                        FpSpread_Info.Columns[i - 2].VerticalAlignment = CellVerticalAlignment.Center;
                        FpSpread_Info.Columns[i - 2].Label = Column.ColumnName;

                        foreach (DataRow Row in Data.Rows)
                        {
                            j = Data.Rows.IndexOf(Row);
                            FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                            FpSpread_Info.Cells[j, i - 2].Value = Row[Column.ColumnName].ToString();
                        }
                    }
                    string strIsHeGe,  strID;
                    //foreach (DataRow Row in Data.Rows)
                    //{
                    //    j = Data.Rows.IndexOf(Row);
                    //    FpSpread_Info.Rows[j].Tag = Row["ID"].ToString() + "," + Row["ModuleID"].ToString();
                    //    strIsHeGe = Row["是否合格"].ToString();
                    //    if (strIsHeGe == "已合格")
                    //    {
                    //        FpSpread_Info.Rows[j].BackColor = Color.FromArgb(240, 180, 120);
                    //    }
                    //    else
                    //    {
                    //        FpSpread_Info.Rows[j].BackColor = Color.White;
                    //    }
                    //    strInvalidItem = Row["不合格项目"].ToString();
                    //    FpSpread_Info.Rows[j].Height = strInvalidItem.Split('\r').Length * 24;
                    //    strID = Row["ID"].ToString();
                    //}
                    DataRow drRow;
                    int SameCount;
                    for (int m = 0; m < Data.Rows.Count; m++)
                    {
                        FpSpread_Info.Rows[m].Tag = Data.Rows[m]["ID"].ToString() + "," + Data.Rows[m]["ModuleID"].ToString();
                        strIsHeGe = Data.Rows[m]["是否合格"].ToString();
                        if (strIsHeGe == "已合格")
                        {
                            FpSpread_Info.Rows[m].BackColor = Color.FromArgb(240, 180, 120);
                        }
                        else
                        {
                            FpSpread_Info.Rows[m].BackColor = Color.White;
                        }
                        drRow = Data.Rows[m];
                        strID = drRow["ID"].ToString();
                        SameCount = -1;
                        foreach (DataRow drItem in Data.Rows)
                        {
                            if (strID == drItem["ID"].ToString())
                            {
                                SameCount++;
                            }
                        }
                        if (SameCount > 0)
                        {
                            foreach (System.Data.DataColumn Column in Data.Columns)
                            {
                                if (Column.ColumnName == "ID" || Column.ColumnName == "ModuleID" || Column.ColumnName == "不合格项目" || Column.ColumnName == "标准规定值" || Column.ColumnName == "实测值")
                                    continue;

                                i = Data.Columns.IndexOf(Column);
                                FpSpread_Info.Cells[m, i - 2].RowSpan = SameCount + 1;

                            }
                            SameCountSum += SameCount;
                            m += SameCount;
                        }
                    }
                    FpSpread.MouseUp += new MouseEventHandler(FpSpread_MouseUp);
                }
                else
                {
                    FpSpread.MouseUp -= new MouseEventHandler(FpSpread_MouseUp);
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                    MessageBox.Show("无数据，请重新选择条件!");
                }
            }
            else
            {
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
                FpSpread.MouseUp -= new MouseEventHandler(FpSpread_MouseUp);
                MessageBox.Show("无数据，请重新选择条件!");
            }

            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();
            if (Data == null)
                TotalCount.Text = string.Format("本次查询共 {0} 条记录", 0);
            else
                TotalCount.Text = string.Format("本次查询共 {0} 条记录", Data.Rows.Count - SameCountSum);
        }

        private void FpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Row Row = FpSpread.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String)
            {
                String[] Tokens = Row.Tag.ToString().Split(',');

                DataDialog Dialog = new DataDialog(new Guid(Tokens[0]), new Guid(Tokens[1]));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Dialog.Size = Owner.ClientRectangle.Size;
                Dialog.ReadOnly = true;
                Dialog.ShowDialog(Owner);
            }
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                String fullPath = folderBrowserDialog1.SelectedPath;
                String fileName = Path.Combine(fullPath, string.Format("不合格报告({0}-{1}-{2}).xls", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                FpSpread.SaveExcel(fileName, IncludeHeaders.ColumnHeadersCustomOnly);
                MessageBox.Show(string.Format("不合格报告“{0}”导出完成", fileName));
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //模拟上传试验数据
            TestDemoGGC td = new TestDemoGGC();
            td.Show();
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Row Row = FpSpread.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String)
            {
                String[] Tokens = Row.Tag.ToString().Split(',');
                InvalidProcess note = new InvalidProcess(Tokens[0], userType);
                note.ShowDialog();
            }
        }

        private void btnTestSMSSend_Click(object sender, EventArgs e)
        {
            TestDemo td = new TestDemo();
            td.Show();
        }
    }
}
