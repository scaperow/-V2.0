using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using System.Threading;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public partial class DeviceManagement : Form
    {
        private int PageIndex
        {
            set;
            get;
        }

        private int PageSize
        {
            get
            {
                return 30;
            }
        }

        private int PageTotal = int.MaxValue;

        private int DataTotal = int.MaxValue;

        private List<SheetDevice> Source;

        public string FilterString
        {
            get
            {
                var where = " 1 = 1";

                if (string.IsNullOrEmpty(Filter.Text) == false)
                {
                    if (Filter.Text.Contains('%'))
                    {
                        where = string.Format(" d.CreateBy LIKE '{0}' OR d.LastEditBy LIKE '{0}' OR d.MachineCode LIKE '{0}' OR d.DeviceCompany LIKE '{0}' OR d.RemoteCode1 LIKE = '{0}' OR d.RemoteCode2 LIKE = '{0}'", Filter.Text);
                    }
                    else
                    {
                        where = string.Format(" d.CreateBy = '{0}' OR d.LastEditBy = '{0}' OR d.MachineCode = '{0}' OR d.DeviceCompany = '{0}' OR d.RemoteCode1 = '{0}' OR d.RemoteCode2 = '{0}'", Filter.Text);
                    }
                }

                return where;
            }
        }

        public string ReginString
        {
            get
            {
                var builder = new StringBuilder(" 1 = 1 ");
                if (Universal.Checked && !Pressure.Checked)
                {
                    builder.AppendFormat(" AND d.DeviceType = 2 ");
                }

                if (!Universal.Checked && Pressure.Checked)
                {
                    builder.AppendFormat(" AND d.DeviceType = 1 ");
                }

                if (Deleted.Checked)
                {
                    builder.AppendFormat(" AND d.IsActive = 1 ");
                }
                else
                {
                    builder.AppendFormat(" AND d.IsActive = 0 ");
                }

                return builder.ToString();
            }
        }

        public string QueryString
        {
            get
            {
                var result = string.Format("AND ({0}) AND ({1}) AND ({2})", FilterString, TreeString, ReginString);

                return result;
            }
        }


        public string TreeString
        {
            get
            {
                var builder = new StringBuilder(" 1 = 1");
                var section = Section.SelectedValue as string;
                var unit = Unit.SelectedValue as string;
                var room = TestRoom.SelectedValue as string;
                if (!string.IsNullOrEmpty(section) && section != "#")
                {
                    builder.AppendFormat(" AND c.标段编码 = '{0}'", section);
                }

                if (!string.IsNullOrEmpty(unit) && unit != "#")
                {
                    builder.AppendFormat(" AND c.单位编码 = '{0}'", unit);
                }

                if (!string.IsNullOrEmpty(room) && room != "#")
                {
                    builder.AppendFormat(" AND c.试验室编码 = '{0}'", room);
                }

                return builder.ToString();
            }
        }



        public DeviceManagement()
        {
            InitializeComponent();
        }

        private void Cells_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            ModifyDevice add = new ModifyDevice();
            add.FormClosed += new FormClosedEventHandler(ModifyDevice_FormClosed);
            add.Show();
        }

        void ModifyDevice_FormClosed(object sender, FormClosedEventArgs e)
        {
            Go(0);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (check_all.Checked)
            {
                ShowAllTestRoom();
            }
            else
            {
                Go(0);
            }
        }

        /// <summary>
        /// 显示所有试验室（包括设备信息不全的）
        /// </summary>
        private void ShowAllTestRoom()
        {
            DataTable Data = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitProjectCatlog", new object[] { }) as DataTable;

        }

        private void Cells_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {


        }

        private void EditDevice()
        {
            var selections = Cells.Selections;
            var sheet = Cells.Sheets[0];

            if (selections.Count == 0)
            {
                MessageBox.Show("请选择要修改的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var selection in selections)
            {
                for (int index = -1; index < selection.RowCount; index++)
                {
                    //至少执行一次下方的代码，即使 selection.RowCount = 0 的情况下
                    if (index == -1)
                    {
                        index = 0;
                    }

                    var i = selection.Row + index;
                    if (Source.Count > i)
                    {
                        var form = new ModifyDevice(Source[i]);
                        form.Show();
                        form.FormClosed += new FormClosedEventHandler(ModifyDevice_FormClosed);
                    }
                }
            }
        }


        private void ViewDevice()
        {
            var selections = Cells.Selections;
            var sheet = Cells.Sheets[0];

            if (selections.Count == 0)
            {
                MessageBox.Show("请选择要修改的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var selection in selections)
            {
                for (int index = -1; index < selection.RowCount; index++)
                {
                    //至少执行一次下方的代码，即使 selection.RowCount = 0 的情况下
                    if (index == -1)
                    {
                        index = 0;
                    }

                    var i = selection.Row + index;
                    if (Source.Count > i)
                    {
                        var form = new DeviceDetail(Source[i]);
                        form.Show();
                    }
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            EditDevice();
        }

        private void Delete_Click(object sender, EventArgs e)
        {

            var selections = Cells.Selections;
            var sheet = Cells.Sheets[0];
            var ids = new List<string>();

            if (selections.Count == 0)
            {
                MessageBox.Show("请选择要修改的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            if (MessageBox.Show("确定要删除所选项吗?", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            foreach (var selection in selections)
            {
                for (int index = -1; index < selection.RowCount; index++)
                {
                    //至少执行一次下方的代码，即使 selection.RowCount = 0 的情况下
                    if (index == -1)
                    {
                        index = 0;
                    }


                    var i = selection.Row + index;

                    if (Source.Count > i)
                    {
                        ids.Add(Source[i].Device.ID.ToString());
                    }

                }
            }

            DeviceHelperClient.DeleteDevice(ids);
            MessageBox.Show("已删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Go(0);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeviceManager_Load(object sender, EventArgs e)
        {
            Sheet.Columns.Count = 12;
            Sheet.Columns[0].Label = "标段名称";
            Sheet.Columns[0].Width = 100;

            Sheet.Columns[1].Label = "单位名称";
            Sheet.Columns[1].Width = 100;

            Sheet.Columns[2].Label = "试验室名称";
            Sheet.Columns[2].Width = 100;

            Sheet.Columns[3].Label = "类型";
            Sheet.Columns[3].Width = 80;

            Sheet.Columns[4].Label = "是否为电液伺服";
            Sheet.Columns[4].Width = 100;

            Sheet.Columns[5].Label = "设备编码";
            Sheet.Columns[5].Width = 200;

            Sheet.Columns[6].Label = "公管中心编码";
            Sheet.Columns[6].Width = 150;

            Sheet.Columns[7].Label = "信息中心编码";
            Sheet.Columns[7].Width = 150;

            Sheet.Columns[8].Label = "试验室编码";
            Sheet.Columns[8].Width = 150;

            Sheet.Columns[9].Label = "设备厂家";
            Sheet.Columns[9].Width = 100;

            Sheet.Columns[10].Label = "创建人";
            Sheet.Columns[10].Width = 80;

            Sheet.Columns[11].Label = "创建时间";
            Sheet.Columns[11].Width = 100;

            Sheet.Columns[0, Sheet.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
            SetTreeSource();
        }

        private void SetTreeSource()
        {
            List<Prjsct> projects = new List<Prjsct>();

            projects.AddRange(DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code));
            projects.Insert(0, new Prjsct()
            {
                PrjsctName = "全部",
                PrjsctCode = "#"
            });

            Section.DataSource = projects;
        }

        private void BindRequest(int index, int size)
        {
            var result = DeviceHelperClient.GetDeviceList(index, size, QueryString);

            if (result == null || result.TotalCount == 0 || result.Source == null || result.Source.Rows.Count == 0)
            {
                Source = null;
                PageTotal = 0;
                DataTotal = 0;
            }
            else
            {
                Source = ConvertSource(result.Source);
                PageTotal = result.TotalCount % PageSize == 0 ? result.TotalCount / PageSize : result.TotalCount / PageSize + 1;
                DataTotal = result.TotalCount;
            }

            BindToGrid(Source);

            Numbers.Text = string.Format("/ {0}", PageTotal);
            DataTotalLabel.Text = string.Format("共有数据 " + DataTotal + " 条");
            First.Enabled = Previous.Enabled = PageIndex > 1;
            Next.Enabled = Last.Enabled = PageIndex < PageTotal;

            Search.Enabled = true;

            if (PageIndex > PageTotal)
            {
                PageIndex = PageTotal;
            }

            Index.Text = PageIndex + "";

        }

        private void BindToGrid(List<SheetDevice> sheets)
        {

            Cells.ShowRow(Cells.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            Sheet.Rows.Count = 0;

            if (sheets == null)
            {
                return;
            }

            Sheet.Rows.Count = sheets.Count;

            for (int i = 0; i < sheets.Count; i++)
            {
                var sheet = sheets[i];

                Sheet.Cells[i, 0].Value = sheet.SectionName;
                Sheet.Cells[i, 1].Value = sheet.UnitName;
                Sheet.Cells[i, 2].Value = sheet.TestRoomName;
                Sheet.Cells[i, 3].Value = sheet.Device.DeviceType == DeviceTypeEnum.Universal ? "万能机" : "压力机";
                Sheet.Cells[i, 4].Value = sheet.Device.IsDYSF ? "是" : "否";
                Sheet.Cells[i, 5].Value = sheet.Device.MachineCode;
                Sheet.Cells[i, 6].Value = sheet.Device.RemoteCode1;
                Sheet.Cells[i, 7].Value = sheet.Device.RemoteCode2;
                Sheet.Cells[i, 8].Value = sheet.TestRoomCode;
                Sheet.Cells[i, 9].Value = sheet.Device.DeviceCompany;
                Sheet.Cells[i, 10].Value = sheet.Device.CreateBy;
                Sheet.Cells[i, 11].Value = sheet.Device.CreateTime;

            }
        }

        private List<SheetDevice> ConvertSource(DataTable table)
        {
            var result = new List<SheetDevice>();
            foreach (DataRow row in table.Rows)
            {
                var device = new Sys_Device()
                {
                    ID = new Guid(row["ID"].ToString()),
                    ClientConfig = row["ClientConfig"] as string,
                    DeviceType = (DeviceTypeEnum)Convert.ToInt32(row["DeviceType"]),
                    IsDYSF = Convert.ToBoolean(row["IsDYSF"]),
                    Comment = row["Comment"] as string,
                    ConfigStatus = Convert.IsDBNull(row["ConfigStatus"]) ? 0 : Convert.ToInt32(row["ConfigStatus"].ToString()),
                    ConfigUpdateTime = Convert.IsDBNull(row["ConfigUpdateTime"]) ? DateTime.MinValue : Convert.ToDateTime(row["ConfigUpdateTime"]),
                    CreateBy = row["CreateBy"] as string,
                    CreateTime = Convert.ToDateTime(row["CreateTime"]),
                    DeviceCompany = row["DeviceCompany"] as string,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    LastEditBy = row["LastEditBy"] as string,
                    LastEditTime = Convert.ToDateTime(row["LastEditTime"]),
                    MachineCode = row["MachineCode"] as string,
                    RemoteCode1 = row["RemoteCode1"] as string,
                    RemoteCode2 = row["RemoteCode2"] as string,
                    TestRoomCode = row["TestRoomCode"] as string

                };

                var sheet = new SheetDevice()
                {
                    Device = device,
                    SectionName = row["标段名称"] as string,
                    UnitName = row["单位名称"] as string,
                    TestRoomName = row["试验室名称"] as string,
                    SectionCode = row["标段编码"] as string,
                    TestRoomCode = row["试验室编码"] as string,
                    UnitCode = row["单位编码"] as string
                };

                result.Add(sheet);
            }

            return result;
        }

        private void Go(int offset)
        {
            Search.Enabled = false;
            PageIndex += offset;
            if (PageIndex <= 0)
            {
                PageIndex = 1;
            }

            BindRequest(PageIndex, PageSize);
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

        private void Next_Click(object sender, EventArgs e)
        {
            Go(1);
        }

        private void Detail_Click(object sender, EventArgs e)
        {
            ViewDevice();
        }

        private void Cells_CellClick_1(object sender, CellClickEventArgs e)
        {

        }

        private void sheetDeviceBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }

        private void Sheet_Changed(object sender, SheetViewEventArgs e)
        {

        }

        private void Sheet_RowChanged(object sender, SheetViewEventArgs e)
        {
            this.Text = Guid.NewGuid().ToString();
        }

        private void NavigatorBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Filter_Enter(object sender, EventArgs e)
        {
            OpenSearchRegion();
        }

        private void FoldSearchRegion()
        {

            SearchContainer.BorderStyle = BorderStyle.None;
            SearchContainer.Height -= 260;
        }

        private void OpenSearchRegion()
        {

            SearchContainer.BorderStyle = BorderStyle.FixedSingle;
            SearchContainer.Height += 260;

        }

        private void Filter_Leave(object sender, EventArgs e)
        {

        }

        private void Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search.Enabled = false;
            TestRoom.DataSource = null;

            var org = Unit.SelectedItem as Orginfo;
            var roomes = new List<PrjFolder>();

            if (org != null && !string.IsNullOrEmpty(org.DepCode))
            {
                roomes.AddRange(DepositoryFolderInfo.QueryPrjFolders(org.DepCode, ""));
                roomes.Add(new PrjFolder()
                {
                    FolderName = "全部",
                    FolderCode = "#"
                });

                TestRoom.DataSource = roomes;
            }

            Search.Enabled = true;
        }

        private void Section_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search.Enabled = false;
            Unit.DataSource = null;

            var section = Section.SelectedItem as Prjsct;
            var units = new List<Orginfo>();

            if (section != null && !string.IsNullOrEmpty(section.PrjsctCode))
            {
                units.AddRange(DepositoryOrganInfo.QueryOrgans(section.PrjsctCode, ""));
                units.Add(new Orginfo()
                {
                    DepName = "全部",
                    DepCode = "#"
                });

                Unit.DataSource = units;
            }
            Search.Enabled = true;
        }

        private void OpenSearchRegion_LinkClicked(object sender, EventArgs e)
        {
            OpenSearchRegion();
        }

        private void FoldSearchRegion_Click(object sender, EventArgs e)
        {
            FoldSearchRegion();
        }

        private void Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Go(0);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDevice();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete_Click(sender, e);
        }

        private void 详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDevice();
        }

        private void Index_MouseDown(object sender, MouseEventArgs e)
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

        private void TestRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void check_all_CheckedChanged(object sender, EventArgs e)
        {
            if (check_all.Checked)
            {
                this.Pressure.Checked = false;
                this.Universal.Checked = false;
                this.Deleted.Checked = false;
            }
        }


    }

    public class SheetDevice
    {
        public Sys_Device Device { set; get; }
        public string TestRoomName { set; get; }
        public string UnitName { set; get; }
        public string SectionName { set; get; }
        public string TestRoomCode { set; get; }
        public string UnitCode { set; get; }
        public string SectionCode { set; get; }
    }

}
