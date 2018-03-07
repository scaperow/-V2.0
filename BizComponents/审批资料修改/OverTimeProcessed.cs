using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FarPoint.Win.Spread;
using BizCommon;

namespace BizComponents.审批资料修改
{
    public partial class OverTimeProcessed : Form
    {
        private DataTable OverTimeTable = null;
        private int PageIndex
        {
            set;
            get;
        }

        private int PageSize
        {
            get
            {
                int size = 20;
                if (!int.TryParse(Size.Text, out size))
                {
                    Size.Text = 20 + "";
                }
                return size;
            }
        }

        private int PageTotal = int.MaxValue;

        private int PageNumbers = int.MaxValue;

        public OverTimeProcessed()
        {
            InitializeComponent();
        }

        private void FpSpread_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(this, new Point(e.X, e.Y));

            }
        }
        private void OverTimeProcessed_Load(object sender, EventArgs e)
        {
            Datas.MouseDown += new MouseEventHandler(FpSpread_MouseDown);
            Go(0);
            string UserNodeCode = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode;
            if (UserNodeCode.Length > 12)
            {
                UserNodeCode = UserNodeCode.Substring(0, 12);
            }
            string userType = DepositoryLabStadiumList.GetUserType(UserNodeCode);
            this.删除ToolStripMenuItem.Visible = false;
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
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
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                填写原因ToolStripMenuItem.Visible = true;
                处理ToolStripMenuItem.Visible = true;
            }
        }

        private void RequestSetDataSource(int index, int size)
        {
            Datas.GroupBarText = "请稍后，正在加载数据";
            Datas_SheetInfo.GroupBarVisible = true;

            SheetView sheet = new SheetView();
            sheet.OperationMode = OperationMode.ReadOnly;
            sheet.Columns.Count = 11;
            sheet.Columns[0].Width = 70;
            sheet.Columns[0].Label = "标段";
            sheet.Columns[1].Width = 80;
            sheet.Columns[1].Label = "单位";
            sheet.Columns[2].Width = 80;
            sheet.Columns[2].Label = "试验室";
            sheet.Columns[3].Width = 160;
            sheet.Columns[3].Label = "模板名称";
            sheet.Columns[4].Width = 120;
            sheet.Columns[4].Label = "委托编号";
            sheet.Columns[5].Width = 90;
            sheet.Columns[5].Label = "龄期到期日期";
            sheet.Columns[6].Width = 70;
            sheet.Columns[6].Label = "试验员";
            sheet.Columns[7].Width = 90;
            sheet.Columns[7].Label = "实际试验日期";
            //sheet.Columns[8].Width = 80;
            //sheet.Columns[8].Label = "试件序号";
            sheet.Columns[8].Width = 300;
            sheet.Columns[8].Label = "试验数据";
            sheet.Columns[9].Label = "ID";
            sheet.Columns[9].Visible = false;
            sheet.Columns[10].Label = "DataID";
            sheet.Columns[10].Visible = false;
            sheet.Columns[0, sheet.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
            sheet.Rows.Count = 0;

            ThreadPool.QueueUserWorkItem(delegate
            {
                var result = DocumentHelperClient.GetTestOverTimeProcessed(PageIndex, PageSize);
                var table = result.Source;

                this.Invoke(new MethodInvoker(delegate
                {
                    if (result == null || result.TotalCount == 0 || result.Source == null || result.Source.Rows.Count == 0)
                    {
                        PageTotal = 0;
                        PageNumbers = 0;

                        Datas.GroupBarText = "当前没有数据";
                    }
                    else
                    {
                        PageTotal = result.TotalCount % PageSize == 0 ? result.TotalCount / PageSize : result.TotalCount / PageSize + 1;
                        PageNumbers = result.TotalCount;
                        OverTimeTable = GroupOverTime(result.Source);
                        sheet.Rows.Count = OverTimeTable.Rows.Count;
                        this.BindDataSource(sheet, OverTimeTable);
                    }
                    Numbers.Text = string.Format("/ {0}", PageTotal);
                    TotalCount.Text = string.Format("共有数据 " + PageNumbers + " 条");
                    First.Enabled = Previous.Enabled = PageIndex > 1;
                    Next.Enabled = Last.Enabled = PageIndex < PageTotal;
                    Index.Text = PageIndex + "";
                }));
            });
        }

        private DataTable GroupOverTime(DataTable table)
        {
            var t = new DataTable();
            t.Columns.Add("ID");
            t.Columns.Add("ModuleID");
            t.Columns.Add("DataID");
            t.Columns.Add("TestData");
            t.Columns.Add("标段名称");
            t.Columns.Add("单位名称");
            t.Columns.Add("试验室名称");
            t.Columns.Add("试验员");
            t.Columns.Add("实际试验日期");
            t.Columns.Add("延时原因");
            t.Columns.Add("模板名称");
            t.Columns.Add("WTBH");
            t.Columns.Add("龄期到期日期");
            t.Columns.Add("Status");

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

                r["ID"] = row["ID"];
                r["ModuleID"] = row["ModuleID"];
                r["DataID"] = row["DataID"];
                r["TestData"] = builder.ToString();
                r["标段名称"] = row["标段名称"];
                r["单位名称"] = row["单位名称"];
                r["试验室名称"] = row["试验室名称"];
                r["试验员"] = row["试验员"];
                r["实际试验日期"] = row["实际试验日期"];
                r["延时原因"] = row["延时原因"];
                r["模板名称"] = row["模板名称"];
                r["WTBH"] = row["WTBH"];
                r["龄期到期日期"] = row["龄期到期日期"];
                r["WTBH"] = row["WTBH"];
                r["Status"] = row["Status"];


                t.Rows.Add(r);
            }

            return t;
        }
        private void BindDataSource(SheetView sheet, DataTable table)
        {
            int index = 0;
            foreach (DataRow Row in table.Rows)
            {
                index = table.Rows.IndexOf(Row);
                sheet.Rows[index].Tag = Row;
                sheet.Cells[index, 0].Value = Row["标段名称"].ToString();
                sheet.Cells[index, 1].Value = Row["单位名称"].ToString();
                sheet.Cells[index, 2].Value = Row["试验室名称"].ToString();
                sheet.Cells[index, 3].Value = Row["模板名称"].ToString();
                sheet.Cells[index, 4].Value = Row["WTBH"].ToString();
                sheet.Cells[index, 5].Value = DateTime.Parse(Row["龄期到期日期"].ToString()).ToString("yyyy-MM-dd");
                sheet.Cells[index, 6].Value = Row["试验员"].ToString();
                sheet.Cells[index, 7].Value = DateTime.Parse(Row["实际试验日期"].ToString()).ToString("yyyy-MM-dd");
                //sheet.Cells[index, 8].Value = Row["SerialNumber"].ToString();
                //List<JZTestCell> lstJZTC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(Row["TestData"].ToString());
                String express = Row["TestData"].ToString();
                //foreach (JZTestCell item in lstJZTC)
                //{
                //    switch (item.Name)
                //    {
                //        case JZTestEnum.DHBJ:
                //            express += "断后标距：" + (item.Value ?? "").ToString() + "；";
                //            break;
                //        case JZTestEnum.LDZDL:
                //            express += "拉断最大力：" + (item.Value ?? "").ToString() + "；";
                //            break;
                //        case JZTestEnum.PHHZ:
                //            express += "破坏荷载：" + (item.Value ?? "").ToString() + "；";
                //            break;
                //        case JZTestEnum.QFL:
                //            express += "屈服力：" + (item.Value ?? "").ToString() + "；";
                //            break;
                //        default:
                //            break;
                //    }
                //}
                sheet.Cells[index, 8].Value = express;
                sheet.Cells[index, 9].Value = Row["ID"].ToString();
                sheet.Cells[index, 10].Value = Row["DataID"].ToString();
                int Status = int.Parse(Row["Status"].ToString());
                if (Status == 1)
                {//通过背景色
                    //sheet.Rows[index].BackColor = Color.FromArgb(113,193,113);
                }
                else if (Status == 2)
                {//拒绝背景色
                    sheet.Rows[index].BackColor = Color.FromArgb(250,170,170);
                }
            }

            if (sheet.Rows.Count > 0)
            {
                sheet.Rows[0, sheet.Rows.Count - 1].Height = 25;
                sheet.Rows[0, sheet.Rows.Count - 1].Locked = true;
            }

            Datas.Sheets.Clear();
            Datas.Sheets.Add(sheet);
            Datas_SheetInfo.GroupBarVisible = false;
            Datas.GroupBarText = "";
        }
        #region 分页
        private void Next_Click(object sender, EventArgs e)
        {
            Go(1);
        }

        private void Go(int offset)
        {
            PageIndex += offset;
            if (PageIndex <= 0)
            {
                PageIndex = 1;
            }

            RequestSetDataSource(PageIndex, PageSize);
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            Go(-1);
        }

        private void First_Click(object sender, EventArgs e)
        {
            Go(-PageIndex);
        }

        private void Last_Click(object sender, EventArgs e)
        {
            Go(PageTotal - PageIndex);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Index_Click(object sender, EventArgs e)
        {

        }

        private void Index_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var index = PageIndex;
                int.TryParse(Index.Text, out index);

                if (index <= 1 || index > PageTotal)
                {
                    Index.Text = index.ToString();
                }
                else
                {
                    PageIndex = index;
                    Go(0);
                }
            }
        }
        #endregion
        private void 填写原因ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = Datas.ActiveSheet;
            var rows = FarPointExtensions.GetSelectionRows(sheet);
            var ids = new List<string>();

            if (rows.Length > 0)
            {
                var form = new TestOverTimeReason(new Guid(sheet.Cells[rows[0].Index, 10].Value.ToString()), OverTimeTable);
                //form.FormClosed += new FormClosedEventHandler(TestOverTimeProcess_FormClosed);
                form.Show();
            }
            //Row Row = spread_test_overtime.ActiveSheet.ActiveRow;
            //if (Row != null)
            //{
            //    DataRow r = Row.Tag as DataRow;
            //    if (r == null)
            //    {
            //        return;
            //    }
            //    Guid id = new Guid(r["ID"].ToString());
            //    TestOverTimeReason totr = new TestOverTimeReason(id);
            //    totr.ShowDialog();
            //}
        }

        private void 处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = Datas.ActiveSheet;
            var rows = FarPointExtensions.GetSelectionRows(sheet);
            var ids = new List<string>();

            foreach (var row in rows)
            {
                var form = new TestOverTimeProcess(new Guid(sheet.Cells[row.Index, 10].Value.ToString()), OverTimeTable);
                form.FormClosed += new FormClosedEventHandler(TestOverTimeProcess_FormClosed);
                form.Show();
            }
        }
        void TestOverTimeProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            RequestSetDataSource(PageIndex, PageSize);
        }
    }
}
