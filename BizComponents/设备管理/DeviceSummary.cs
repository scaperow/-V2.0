using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using FarPoint.Win.Spread;
using BizModules;

namespace BizComponents
{
    public partial class DeviceSummary : Form
    {
        public Guid SummaryModuleID = new Guid("A0C51954-302D-43C6-931E-0BAE2B8B10DB");
        public int UserType;
        public DeviceSummary()
        {
            InitializeComponent();
            Tabs.TabPages.Remove(tabPage1);
        }

        private void DeviceSummary_Load(object sender, EventArgs e)
        {
            LoadSegments();
        }

        private void LoadSegments()
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位")
            {
                UserType = 0;
            }
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
            {
                UserType = 1;
            }
            else
            {
                UserType = 1;
            }

            ComboBox_Segments.Items.Clear();
            List<Prjsct> PrjsctList = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.Items.AddRange(PrjsctList.ToArray());
            ComboBox_Segments.SelectedIndex = 0;
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
            {
                ComboBox_Company.SelectedIndex = 0;
            }
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
            LabelTotalAll.Text = LabelTotalNoOvertime.Text = LabelTotalOvertime.Text = "0";
            TextIndex_All.Text = TextIndex_Overdue.Text = TextIndex_NoOverdue.Text = "1";
            TotalOfAll = TotalOfOvertime = TotalNoOvertime = 1;
            Tabs_SelectedIndexChanged(Tabs, EventArgs.Empty);
        }

        private void ParseCycle(string cycle, out int? unit, out bool? year)
        {
            var result = 0;
            cycle = cycle.Replace("一", "1")
                .Replace("二", "2")
                .Replace("三", "3")
                .Replace("四", "4")
                .Replace("五", "5")
                .Replace("六", "6")
                .Replace("七", "7")
                .Replace("八", "8")
                .Replace("九", "9")
                .Replace("十", "10")
                .Replace(" ", "")
                .Replace("个", "");

            if (cycle.Contains("年"))
            {
                cycle = cycle.Replace("年", "");
                if (int.TryParse(cycle, out result))
                {
                    unit = result;
                    year = true;
                    return;
                }
            }

            if (cycle.Contains("月"))
            {
                cycle = cycle.Replace("月", "");
                if (int.TryParse(cycle, out result))
                {
                    unit = result;
                    year = false;
                    return;
                }
            }

            unit = null;
            year = null;
            return;
        }

        private DataSet QuerySet(SheetView sheet, int pageIndex, int pageSize, int meet)
        {
            var testRoomCode = "";
            if (ComboBox_TestRooms.SelectedItem is PrjFolder)
            {
                testRoomCode = (ComboBox_TestRooms.SelectedItem as PrjFolder).FolderCode;
            }
            else
            {
                testRoomCode = "";
            }


            var dataset = DeviceHelperClient.GetDeviceSummary(testRoomCode, meet, pageIndex, pageSize);
            if (dataset != null)
            {
                var total = int.Parse(dataset.Tables[1].Rows[0][0].ToString());
                var summary = dataset.Tables[0];
                var rowIndex = -1;
                var now = DateTime.Now;

                if (summary != null)
                {
                    sheet.Rows.Count = pageSize > summary.Rows.Count ? summary.Rows.Count : pageSize;

                    foreach (DataRow row in summary.Rows)
                    {
                        rowIndex++;
                        sheet.Cells[rowIndex, 0].Value = row["标段名称"];
                        sheet.Cells[rowIndex, 1].Value = row["单位名称"];
                        sheet.Cells[rowIndex, 2].Value = row["试验室名称"];
                        sheet.Cells[rowIndex, 3].Value = row["管理编号"];
                        sheet.Cells[rowIndex, 4].Value = row["设备名称"];
                        sheet.Cells[rowIndex, 5].Value = row["生产厂家"];
                        sheet.Cells[rowIndex, 6].Value = row["规格型号"];
                        sheet.Cells[rowIndex, 7].Value = row["数量"];
                        sheet.Cells[rowIndex, 8].Value = row["检定情况"];
                        sheet.Cells[rowIndex, 9].Value = row["检定证书编号"];
                        sheet.Cells[rowIndex, 12].Value = row["检定周期"];
                        sheet.Rows[rowIndex].Tag = row["id"];
                        sheet.Cells[rowIndex, 2].Tag = row["试验室编码"];

                        DateTime start;
                        DateTime end;
                        bool equal = true;
                        if (DateTime.TryParse(row["上次校验日期"].ToString(), out start))
                        {
                            sheet.Cells[rowIndex, 10].Value = start.ToString("yyyy年MM月dd日");
                        }
                        else
                        {
                            sheet.Cells[rowIndex, 10].BackColor = Color.FromArgb(110, 110, 110);
                            equal = false;
                        }

                        if (DateTime.TryParse(row["预计下次校验日期"].ToString(), out end))
                        {
                            sheet.Cells[rowIndex, 11].Value = end.ToString("yyyy年MM月dd日");
                        }
                        else
                        {
                            sheet.Cells[rowIndex, 11].BackColor = Color.FromArgb(110, 110, 110);
                            equal = false;
                        }

                        if (equal)
                        {
                            if (now > start && now < end)
                            {
                                sheet.Cells[rowIndex, 13].Value = "否";
                            }
                            else
                            {
                                sheet.Rows[rowIndex].BackColor = Color.FromArgb(255, 86, 64);
                                sheet.Cells[rowIndex, 13].Value = "是";
                            }

                            int? unit;
                            bool? year;
                            var c = row["检定周期"] == DBNull.Value ? "" : row["检定周期"].ToString();
                            ParseCycle(c, out unit, out year);
                            if (year.HasValue && unit.HasValue)
                            {
                                var time = default(DateTime);
                                if (year.Value == true)
                                {
                                    time = start.AddYears(unit.Value);
                                }
                                else
                                {
                                    time = end.AddMonths(unit.Value);
                                }

                                var dif = Math.Abs((end - time).TotalDays);
                                if (dif >= 2)
                                {
                                    sheet.Cells[rowIndex, 12].BackColor = Color.FromArgb(56, 178, 206);
                                }
                            }
                            else
                            {
                                sheet.Cells[rowIndex, 12].BackColor = Color.FromArgb(110, 110, 110);
                            }
                        }
                    }

                    return dataset;
                }
            }

            return dataset;
        }

        public int TotalOfAll, TotalOfOvertime, TotalNoOvertime;

        private void LoadSummaryAll()
        {
            Spread_All.ShowRow(Spread_All.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            Sheet_All.Rows.Count = 0;

            var index = GetIndex(TextIndex_All);
            if (index < 1)
            {
                index = 1;
            }

            if (index > TotalOfAll)
            {
                index = TotalOfAll;
            }

            TextIndex_All.Text = index + "";

            var size = GetSize(TextSize_All);
            var set = QuerySet(Sheet_All, index, size, 0);
            if (set != null)
            {
                var total = int.Parse(set.Tables[1].Rows[0][0].ToString());
                var pages = (total + 20 - 1) / 20;
                if (pages == 0)
                {
                    pages = 1;
                }

                TotalOfAll = pages;
                CurrentIndex_All.Text = string.Format("{0}/{1}", index, pages);
                LabelTotalAll.Text = total + "";
            }
        }

        private void LoadSummaryOvertime()
        {
            myCell1.ShowRow(myCell1.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            Sheet_Overtime.Rows.Count = 0;

            var index = GetIndex(TextIndex_Overdue);
            if (index < 1)
            {
                index = 1;
            }

            if (index > TotalOfOvertime)
            {
                index = TotalOfOvertime;
            }

            TextIndex_Overdue.Text = index + "";

            var size = GetSize(TextSize_Overtime);
            var set = QuerySet(Sheet_Overtime, index, size, 1);
            if (set != null)
            {
                var total = int.Parse(set.Tables[1].Rows[0][0].ToString());
                var pages = (total + 20 - 1) / 20;
                if (pages == 0)
                {
                    pages = 1;
                }

                TotalOfOvertime = pages;
                LabelCurrent_Overdue.Text = string.Format("{0}/{1}", index, pages);
                LabelTotalOvertime.Text = total + "";
            }
        }

        private void LoadSummaryNoOvertime()
        {
            myCell2.ShowRow(myCell2.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            SheetNoOvertime.Rows.Count = 0;

            var index = GetIndex(TextIndex_NoOverdue);
            if (index < 1)
            {
                index = 1;
            }

            if (index > TotalNoOvertime)
            {
                index = TotalNoOvertime;
            }

            TextIndex_NoOverdue.Text = index + "";

            var size = GetSize(TextSize_NoOvertime);
            var set = QuerySet(SheetNoOvertime, index, size, 2);

            if (set != null)
            {
                var total = int.Parse(set.Tables[1].Rows[0][0].ToString());
                var pages = (total + 20 - 1) / 20;
                if (pages == 0)
                {
                    pages = 1;
                }

                TotalNoOvertime = pages;
                LabelIndex_NoOverdue.Text = string.Format("{0}/{1}", index, pages);
                LabelTotalNoOvertime.Text = total + "";
            }
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Tabs.SelectedIndex)
            {
                case 0:
                    LoadSummaryAll();
                    break;

                case 1:
                    LoadSummaryOvertime();
                    break;

                case 2:
                    LoadSummaryNoOvertime();
                    break;
            }
        }

        private void ButtonPrevious_Overdue_Click(object sender, EventArgs e)
        {
            var index = GetIndex(TextIndex_Overdue);

            TextIndex_Overdue.Text = --index + "";
            LoadSummaryOvertime();
        }

        private void ButtonNext_Overdue_Click(object sender, EventArgs e)
        {
            var index = GetIndex(TextIndex_Overdue);

            TextIndex_Overdue.Text = ++index + "";
            LoadSummaryOvertime();
        }

        private int GetIndex(ToolStripTextBox control)
        {
            var text = control.Text;
            var index = 1;
            if (int.TryParse(text, out index))
            {
                return index;
            }

            return 1;
        }

        private int GetSize(ToolStripTextBox control)
        {
            var text = control.Text;
            var size = 50;
            if (int.TryParse(text, out size))
            {
                if (size > 200)
                {
                    size = 200;
                    control.Text = size + "";
                }

                return size;
            }

            control.Text = size + "";
            return size;
        }

        private void ButtonPrevious_All_Click(object sender, EventArgs e)
        {
            var index = GetIndex(TextIndex_All);

            TextIndex_All.Text = --index + "";
            LoadSummaryAll();
        }

        private void ButtonNext_All_Click(object sender, EventArgs e)
        {
            var index = GetIndex(TextIndex_All);

            TextIndex_All.Text = ++index + "";
            LoadSummaryAll();
        }

        private void ButtonPrevious_NoOverdue_Click(object sender, EventArgs e)
        {
            var index = GetIndex(TextIndex_NoOverdue);

            TextIndex_NoOverdue.Text = --index + "";
            LoadSummaryNoOvertime();
        }

        private void ButtonNext_NoOverdue_Click(object sender, EventArgs e)
        {
            var index = GetIndex(TextIndex_NoOverdue);

            TextIndex_NoOverdue.Text = ++index + "";
            LoadSummaryNoOvertime();
        }

        private void myCell1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            OpenDocument(Sheet_Overtime, e.Row);
        }

        private void Spread_All_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            OpenDocument(Sheet_All, e.Row);
        }

        private void OpenDocument(SheetView sheet, int row)
        {
            var tagID = sheet.Rows[row].Tag == null ? "" : sheet.Rows[row].Tag.ToString();
            if (string.IsNullOrEmpty(tagID))
            {
                return;
            }

            var testRoomCode = sheet.Cells[row, 2].Tag as string;
            if (string.IsNullOrEmpty(testRoomCode))
            {
                return;
            }

            var documentID = new Guid(tagID);
            var form = new ModuleViewer(documentID, SummaryModuleID, testRoomCode);
            form.ShowDialog();

        }
    }
}
