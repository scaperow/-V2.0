using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using FarPoint.Win.Spread;
using Yqun.Interfaces;
using Yqun.Common.ContextCache;
using System.Threading;
using BizCommon;

namespace BizComponents
{
    public delegate void AddViewDelegate(FpSpread spread, SheetView view);
    public delegate void ChangeTabTitleDelegate(TabPage tp, String title);

    public partial class TaskView : Form
    {
        String TestRoomCode = "";
        string Dataid = string.Empty;
        string userType = string.Empty;
        private DataTable OverTimeTable = null;

        public TaskView()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(TabParallel);
            tabControl1.TabPages.Remove(TabWitness);
        }

        private void AddView(FpSpread spread, SheetView view)
        {
            spread.Sheets.Clear();
            spread.Sheets.Add(view);
        }
        private void ChangeTabTitle(TabPage tp, String title)
        {
            tp.Text = title;
        }

        private void TaskView_Load(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在显示待做事项列表...";

            try
            {
                TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;

                string UserNodeCode = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode;
                if (UserNodeCode.Length > 12)
                {
                    UserNodeCode = UserNodeCode.Substring(0, 12);
                }

                userType = DepositoryLabStadiumList.GetUserType(UserNodeCode);
                this.Text = "待做事项列表-【" + Yqun.Common.ContextCache.ApplicationContext.Current.InSegment.Description
                    + "-" + Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Description + "-" +
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Description + "】-" + DateTime.Now.ToString("yyyy-MM-dd");
                spread_invalide.MouseDown += new MouseEventHandler(FpSpread_MouseDown);

                BindStadium();

                Thread t = new Thread(new ParameterizedThreadStart(BindInvalid));
                t.Start(Yqun.Common.ContextCache.ApplicationContext.Current);

                Thread t2 = new Thread(new ParameterizedThreadStart(BindRequest));
                t2.Start(Yqun.Common.ContextCache.ApplicationContext.Current);

                SetOverTimeSheet();
                Thread t3 = new Thread(new ParameterizedThreadStart(BindTestOverTime));
                t3.Start(Yqun.Common.ContextCache.ApplicationContext.Current);

                tabPage1.Text = "今日到期待做试验【" + spread_stadium_sheet.RowCount + "】";
                this.删除ToolStripMenuItem.Visible = false;

                if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator
                    || Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
                {
                    this.删除ToolStripMenuItem.Visible = true;
                }

                if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||
                    Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
                {
                    填写原因ToolStripMenuItem.Visible = false;
                    处理ToolStripMenuItem.Visible = true;

                }
                else if (userType.IndexOf("监理") > 0)
                {
                    填写原因ToolStripMenuItem.Visible = false;
                    处理ToolStripMenuItem.Visible = true;
                }
                else
                {
                    填写原因ToolStripMenuItem.Visible = true;
                    处理ToolStripMenuItem.Visible = false;
                }

                //ShowPXJZ();
            }
            catch
            {
            }

            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();
        }

        private void SetOverTimeSheet()
        {
            sheet_test_overtime.OperationMode = OperationMode.ReadOnly;
            sheet_test_overtime.Columns.Count = 7;
            sheet_test_overtime.Columns[0].Visible = false;
            sheet_test_overtime.Columns[0].Label = "DataID";
            sheet_test_overtime.Columns[1].Width = 70;
            sheet_test_overtime.Columns[1].Label = "标段";
            sheet_test_overtime.Columns[2].Width = 80;
            sheet_test_overtime.Columns[2].Label = "单位";
            sheet_test_overtime.Columns[3].Width = 80;
            sheet_test_overtime.Columns[3].Label = "试验室";
            sheet_test_overtime.Columns[4].Width = 160;
            sheet_test_overtime.Columns[4].Label = "模板名称";
            sheet_test_overtime.Columns[5].Width = 120;
            sheet_test_overtime.Columns[5].Label = "委托编号";
            sheet_test_overtime.Columns[6].Width = 800;
            sheet_test_overtime.Columns[6].Label = "试验数据";
            sheet_test_overtime.Columns[0, sheet_test_overtime.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
        }

        private void BindStadium()
        {

            DataTable Data = null;

            Data = ModuleHelperClient.GetStadiumList("", "", TestRoomCode, -1);

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
                    spread_stadium_sheet.Columns[0].Width = 60;
                    spread_stadium_sheet.Columns[1].Width = 80;
                    spread_stadium_sheet.Columns[2].Width = 100;
                    spread_stadium_sheet.Columns[3].Width = 120;
                    spread_stadium_sheet.Columns[4].Width = 60;
                    spread_stadium_sheet.Columns[5].Width = 70;
                    spread_stadium_sheet.Columns[6].Width = 90;
                    spread_stadium_sheet.Columns[7].Width = 60;
                    spread_stadium_sheet.Columns[8].Width = 120;
                    spread_stadium_sheet.Columns[9].Width = 120;
                    spread_stadium_sheet.Columns[10].Width = 60;
                    spread_stadium_sheet.Columns[11].Width = 120;
                    spread_stadium_sheet.Columns[12].Width = 120;
                    //spread_stadium_sheet.Columns[0, spread_stadium_sheet.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                    //spread_stadium_sheet.Columns[0, spread_stadium_sheet.Columns.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
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
                        if ((i - HiddenColumnCount) == 10)
                        {
                            spread_stadium_sheet.Cells[j, i - HiddenColumnCount].Value = Row[Column.ColumnName].ToString().TrimEnd('0').TrimEnd('.');
                        }
                        else
                        {
                            spread_stadium_sheet.Cells[j, i - HiddenColumnCount].Value = Row[Column.ColumnName].ToString();
                        }
                    }
                }

                foreach (DataRow Row in Data.Rows)
                {
                    j = Data.Rows.IndexOf(Row);
                    spread_stadium_sheet.Rows[j].Tag = Row["DataID"].ToString() + "," + Row["ModuleID"].ToString();
                }

                spread_stadium.CellDoubleClick -= new CellClickEventHandler(spread_stadium_CellDoubleClick);
                spread_stadium.CellDoubleClick += new CellClickEventHandler(spread_stadium_CellDoubleClick);
            }

        }

        private void BindInvalid(Object c)
        {
            try
            {
                Yqun.Common.ContextCache.ApplicationContext.Current = c as Yqun.Common.ContextCache.ApplicationContext;
                SheetView spread_invalide_sheet = new SheetView();
                spread_invalide_sheet.Rows.Count = 0;
                spread_invalide_sheet.Columns.Count = 12;
                spread_invalide_sheet.ColumnHeader.Cells[0, 0].Text = "标段";
                spread_invalide_sheet.ColumnHeader.Cells[0, 1].Text = "单位";
                spread_invalide_sheet.ColumnHeader.Cells[0, 2].Text = "试验室";
                spread_invalide_sheet.ColumnHeader.Cells[0, 3].Text = "试验报告";
                spread_invalide_sheet.ColumnHeader.Cells[0, 4].Text = "报告编号";
                spread_invalide_sheet.ColumnHeader.Cells[0, 5].Text = "报告日期";
                spread_invalide_sheet.ColumnHeader.Cells[0, 6].Text = "试验项目";
                spread_invalide_sheet.ColumnHeader.Cells[0, 7].Text = "标准值";
                spread_invalide_sheet.ColumnHeader.Cells[0, 8].Text = "实测值";
                spread_invalide_sheet.ColumnHeader.Cells[0, 9].Text = "原因分析";
                spread_invalide_sheet.ColumnHeader.Cells[0, 10].Text = "监理意见";
                spread_invalide_sheet.ColumnHeader.Cells[0, 11].Text = "处理结果";

                spread_invalide_sheet.Columns[0].Width = 80;
                spread_invalide_sheet.Columns[1].Width = 100;
                spread_invalide_sheet.Columns[2].Width = 100;
                spread_invalide_sheet.Columns[3].Width = 190;
                spread_invalide_sheet.Columns[4].Width = 200;
                spread_invalide_sheet.Columns[5].Width = 90;
                spread_invalide_sheet.Columns[6].Width = 110;
                spread_invalide_sheet.Columns[7, 8].Width = 90;
                spread_invalide_sheet.Columns[9].Width = 200;
                spread_invalide_sheet.Columns[10].Width = 200;
                spread_invalide_sheet.Columns[11].Width = 200;

                FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
                datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
                spread_invalide_sheet.Columns[5].CellType = datetime;
                Boolean isSG = false;
                if (userType.IndexOf("施工") > 0)
                {
                    isSG = true;
                }
                DataTable Data = DocumentHelperClient.GetUndoInvalidDocumentList(isSG);
                if (Data != null)
                {
                    //spread_invalide.ShowRow(spread_invalide.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                    spread_invalide_sheet.Rows.Count = Data.Rows.Count;
                    if (Data.Rows.Count > 0)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                        text.Multiline = true;
                        text.WordWrap = true;

                        spread_invalide_sheet.Columns[0, 2].CellType = text;

                        spread_invalide_sheet.Rows[0, spread_invalide_sheet.Rows.Count - 1].Height = 25;
                        spread_invalide_sheet.Rows[0, spread_invalide_sheet.Rows.Count - 1].Locked = true;

                        spread_invalide_sheet.Columns[0, spread_invalide_sheet.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                        spread_invalide_sheet.Rows[0, spread_invalide_sheet.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;

                        int i, j;
                        foreach (System.Data.DataColumn Column in Data.Columns)
                        {
                            if (Column.ColumnName == "ID" || Column.ColumnName == "ModuleID")
                                continue;

                            i = Data.Columns.IndexOf(Column);
                            spread_invalide_sheet.Columns[i - 2].VerticalAlignment = CellVerticalAlignment.Center;
                            spread_invalide_sheet.Columns[i - 2].Label = Column.ColumnName;

                            foreach (DataRow Row in Data.Rows)
                            {
                                j = Data.Rows.IndexOf(Row);
                                spread_invalide_sheet.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                                spread_invalide_sheet.Cells[j, i - 2].Value = Row[Column.ColumnName].ToString();
                            }
                        }

                        foreach (DataRow Row in Data.Rows)
                        {
                            j = Data.Rows.IndexOf(Row);
                            spread_invalide_sheet.Rows[j].Tag = Row["ID"].ToString() + "," + Row["ModuleID"].ToString();
                        }

                    }
                }
                AddViewDelegate avd = new AddViewDelegate(AddView);
                spread_invalide.Invoke(avd, spread_invalide, spread_invalide_sheet);
                ChangeTabTitleDelegate cttd = new ChangeTabTitleDelegate(ChangeTabTitle);
                tabPage2.Invoke(cttd, tabPage2, "待处理不合格报告【" + spread_invalide.Sheets[0].RowCount + "】");
            }
            catch
            {
            }
        }

        private void BindRequest(Object c)
        {
            try
            {
                Yqun.Common.ContextCache.ApplicationContext.Current = c as Yqun.Common.ContextCache.ApplicationContext;
                SheetView spread_request_sheet = new SheetView();
                spread_request_sheet.Columns.Count = 11;
                spread_request_sheet.Columns[0].Width = 80;
                spread_request_sheet.Columns[1].Width = 100;
                spread_request_sheet.Columns[2].Width = 200;
                spread_request_sheet.Columns[3].Width = 150;
                spread_request_sheet.Columns[4].Width = 240;
                spread_request_sheet.Columns[5].Width = 80;
                spread_request_sheet.Columns[6].Width = 130;
                spread_request_sheet.Columns[7].Width = 120;
                spread_request_sheet.Columns[8].Width = 300;
                spread_request_sheet.Columns[9].Width = 300;

                spread_request_sheet.Columns[0, spread_request_sheet.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;

                TextCellType text = new TextCellType();
                text.Multiline = true;
                text.WordWrap = true;

                spread_request_sheet.Columns[0, 2].CellType = text;

                DataTable Data = DocumentHelperClient.GetUnPreocessedRequestList();
                spread_request_sheet.Rows.Count = 0;
                if (Data != null)
                {
                    //spread_request.ShowRow(spread_request.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);

                    spread_request_sheet.Rows.Count = Data.Rows.Count;
                    if (Data.Rows.Count > 0)
                    {
                        spread_request_sheet.Rows[0, spread_request_sheet.Rows.Count - 1].Height = 20;
                        spread_request_sheet.Rows[0, spread_request_sheet.Rows.Count - 1].Locked = true;
                        spread_request_sheet.Rows[0, spread_request_sheet.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                    }

                    int i, j;
                    foreach (System.Data.DataColumn Column in Data.Columns)
                    {
                        if (Column.ColumnName == "ID" || Column.ColumnName == "DataID" || Column.ColumnName == "ModuleID")
                            continue;

                        i = Data.Columns.IndexOf(Column);
                        spread_request_sheet.Columns[i - 3].VerticalAlignment = CellVerticalAlignment.Center;
                        spread_request_sheet.Columns[i - 3].Label = Column.ColumnName;

                        foreach (DataRow Row in Data.Rows)
                        {
                            j = Data.Rows.IndexOf(Row);
                            spread_request_sheet.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                            spread_request_sheet.Cells[j, i - 3].Value = Row[Column.ColumnName].ToString();
                        }
                    }

                    foreach (DataRow Row in Data.Rows)
                    {
                        j = Data.Rows.IndexOf(Row);
                        spread_request_sheet.Rows[j].Tag = Row["ID"].ToString() + "," + Row["DataID"].ToString() + "," + Row["ModuleID"].ToString();
                    }

                }
                AddViewDelegate avd = new AddViewDelegate(AddView);
                spread_request.Invoke(avd, spread_request, spread_request_sheet);
                ChangeTabTitleDelegate cttd = new ChangeTabTitleDelegate(ChangeTabTitle);
                tabPage3.Invoke(cttd, tabPage3, "待审批用户申请【" + spread_request.Sheets[0].RowCount + "】");
            }
            catch
            {
            }
        }

        private void BindTestOverTime(Object c)
        {
            try
            {
                Yqun.Common.ContextCache.ApplicationContext.Current = c as Yqun.Common.ContextCache.ApplicationContext;

                this.OverTimeTable = DocumentHelperClient.GetTestOverTimeData();
                var table = GroupOverTime(OverTimeTable);

                this.Invoke(new MethodInvoker(delegate
                {
                    sheet_test_overtime.Rows.Count = 0;
                    if (table != null)
                    {
                        sheet_test_overtime.Rows.Count = table.Rows.Count;
                        if (table.Rows.Count > 0)
                        {
                            sheet_test_overtime.Rows[0, sheet_test_overtime.Rows.Count - 1].Height = 20;
                            sheet_test_overtime.Rows[0, sheet_test_overtime.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                        }

                        int j;

                        foreach (DataRow Row in table.Rows)
                        {
                            j = table.Rows.IndexOf(Row);
                            sheet_test_overtime.Rows[j].Tag = Row;
                            sheet_test_overtime.Cells[j, 0].Value = Row["DataID"].ToString();
                            sheet_test_overtime.Cells[j, 1].Value = Row["标段名称"].ToString();
                            sheet_test_overtime.Cells[j, 2].Value = Row["单位名称"].ToString();
                            sheet_test_overtime.Cells[j, 3].Value = Row["试验室名称"].ToString();
                            sheet_test_overtime.Cells[j, 4].Value = Row["模板名称"].ToString();
                            sheet_test_overtime.Cells[j, 5].Value = Row["WTBH"].ToString();
                            sheet_test_overtime.Cells[j, 6].Value = Row["TestData"].ToString();

                            //spread_test_sheet.Cells[j, 5].Value = DateTime.Parse(Row["龄期到期日期"].ToString()).ToString("yyyy-MM-dd");
                            //spread_test_sheet.Cells[j, 6].Value = Row["试验员"].ToString();
                            //spread_test_sheet.Cells[j, 7].Value = DateTime.Parse(Row["实际试验日期"].ToString()).ToString("yyyy-MM-dd");
                            //spread_test_sheet.Cells[j, 8].Value = Row["SerialNumber"].ToString();
                        }

                        AddView(spread_test_overtime, sheet_test_overtime);
                        ChangeTabTitle(tabPage4, "待审批过期试验【" + spread_test_overtime.Sheets[0].RowCount + "】");
                        //AddViewDelegate avd = new AddViewDelegate(AddView);
                        //spread_test_overtime.Invoke(avd, spread_test_overtime, sheet_test_overtime);
                        //ChangeTabTitleDelegate cttd = new ChangeTabTitleDelegate(ChangeTabTitle);
                        //tabPage4.Invoke(cttd, tabPage4, "待审批过期试验【" + spread_test_overtime.Sheets[0].RowCount + "】");
                    }
                }));

            }
            catch
            {
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

        private void spread_request_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Row Row = spread_request.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String && Row.Tag.ToString() != "")
            {
                string tag = spread_request.ActiveSheet.ActiveRow.Tag.ToString();
                ModificationDetailView detalis = new ModificationDetailView(tag);
                detalis.ShowDialog();
            }
        }

        private void spread_invalide_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Row Row = spread_invalide.ActiveSheet.ActiveRow;
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

        private void FpSpread_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    contextMenuStrip1.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        private void 原因分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Row Row = spread_invalide.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String)
            {
                String[] Tokens = Row.Tag.ToString().Split(',');
                Int32 t = 0;
                if (userType.IndexOf("监理") > 0)
                {
                    t = 1;
                }
                InvalidProcess ip = new InvalidProcess(Tokens[0], t);
                ip.ShowDialog();
            }
        }

        private void 填写原因ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = spread_test_overtime.ActiveSheet;
            var rows = FarPointExtensions.GetSelectionRows(sheet);
            var ids = new List<string>();

            if (rows.Length > 0)
            {
                var form = new TestOverTimeReason(new Guid(sheet.Cells[rows[0].Index, 0].Value.ToString()), OverTimeTable);
                form.Show();
            }
        }

        private void 处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = spread_test_overtime.ActiveSheet;
            var rows = FarPointExtensions.GetSelectionRows(sheet);
            var ids = new List<string>();

            foreach (var row in rows)
            {
                var form = new TestOverTimeProcess(new Guid(sheet.Cells[row.Index, 0].Value.ToString()), OverTimeTable);
                form.FormClosed += new FormClosedEventHandler(TestOverTimeProcess_FormClosed);
                form.Show();
            }
        }

        void TestOverTimeProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            BindTestOverTime(Yqun.Common.ContextCache.ApplicationContext.Current);
        }
        //void TestOverTimeReason_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    BindTestOverTime(Yqun.Common.ContextCache.ApplicationContext.Current);
        //}

        private DataTable GroupOverTime(DataTable table)
        {
            var t = new DataTable();
            t.Columns.Add("DataID");
            t.Columns.Add("标段名称");
            t.Columns.Add("单位名称");
            t.Columns.Add("试验室名称");
            t.Columns.Add("模板名称");
            t.Columns.Add("WTBH");
            t.Columns.Add("TestData");
            t.Columns.Add("TestRoomCode");

            IEnumerable<IGrouping<string, DataRow>> result = table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(row => row["DataID"].ToString());

            foreach (IGrouping<string, DataRow> item in result)
            {
                var row = item.First();
                var dataID = row["DataID"].ToString();
                var builder = new StringBuilder();
                var subs = table.Select(" DataID = '" + dataID + "'");
                for (var i = 0; i < subs.Length; i++)
                {
                    var sub = subs[i];
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(sub["TestData"].ToString());
                    var value = string.Format("[{0}] ", sub["SerialNumber"]);

                    foreach (var d in data)
                    {
                        switch (d.Name)
                        {
                            case JZTestEnum.DHBJ:
                                value += "断后标距：" + (d.Value ?? "").ToString() + ";";
                                break;
                            case JZTestEnum.LDZDL:
                                value += "拉断最大力：" + (d.Value ?? "").ToString() + ";";
                                break;
                            case JZTestEnum.PHHZ:
                                value += "破坏荷载：" + (d.Value ?? "").ToString() + ";";
                                break;
                            case JZTestEnum.QFL:
                                value += "屈服力：" + (d.Value ?? "").ToString() + ";";
                                break;
                            default:
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(value))
                    {
                        builder.Append(value + " ");
                    }
                }

                var r = t.NewRow();

                r["TestData"] = builder.ToString();
                r["标段名称"] = row["标段名称"];
                r["单位名称"] = row["单位名称"];
                r["DataID"] = row["DataID"];
                r["试验室名称"] = row["试验室名称"];
                r["模板名称"] = row["模板名称"];
                r["WTBH"] = row["WTBH"];
                r["TestRoomCode"] = row["TestRoomCode"];

                t.Rows.Add(r);
            }

            return t;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = spread_test_overtime.ActiveSheet;
            var rows = FarPointExtensions.GetSelectionRows(sheet);
            if (rows == null || rows.Length == 0)
            {
                MessageBox.Show("没有选中要删除的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ids = new List<string>();
            foreach (var row in rows)
            {
                ids.Add(sheet.Cells[row.Index, 0].Value.ToString());
            }

            DocumentHelperClient.DeleteTestOverTime(ids);
            BindTestOverTime(Yqun.Common.ContextCache.ApplicationContext.Current);
        }

        private void OverTimeMenu_Opening(object sender, CancelEventArgs e)
        {
            var rows = FarPointExtensions.GetSelectionRows(spread_test_overtime.ActiveSheet);
            if (rows == null || rows.Length == 0)
            {
                return;
            }

            var row = rows[0].Tag as DataRow;
            if (row == null)
            {
                return;
            }

            if (userType.IndexOf("监理") > 0)
            {
                var code = Convert.ToString(row["TestRoomCode"]);
                填写原因ToolStripMenuItem.Visible = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code == code;
            }
        }
    }
}
