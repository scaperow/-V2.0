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

namespace BizComponents
{
    public partial class TestOverTimeProcess : Form
    {
        private Guid DataID;
        private DataTable Datas;

        public TestOverTimeProcess(Guid dataID, DataTable datas)
        {
            InitializeComponent();
            this.DataID = dataID;
            this.Datas = datas;
        }

        private void SetDataSource()
        {
           Sheet.Rows.Count = 0;
            var result = CaiJiHelperClient.GetTestOverTimeByDataID(DataID);

            if (result == null || result.Rows.Count == 0)
            {
                return;
            }

            Sheet.Rows.Count = result.Rows.Count;
            for (var i = 0; i < result.Rows.Count; i++)
            {
                var row = result.Rows[i];
                Sheet.Cells[i, 0].Value = row["标段名称"];
                Sheet.Cells[i, 1].Value = row["单位名称"];
                Sheet.Cells[i, 2].Value = row["试验室名称"];
                Sheet.Cells[i, 3].Value = row["模板名称"];
                Sheet.Cells[i, 4].Value = row["WTBH"];
                Sheet.Cells[i, 6].Value = (row["实际试验日期"] == DBNull.Value || row["实际试验日期"] == null) ? "" : ((DateTime)(row["实际试验日期"])).ToString("yyyy-MM-dd");
                Sheet.Cells[i, 7].Value = (row["龄期到期日期"] == DBNull.Value || row["龄期到期日期"] == null) ? "" : ((DateTime)(row["龄期到期日期"])).ToString("yyyy-MM-dd");

                var builder = new StringBuilder();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(row["TestData"].ToString());
                var value = string.Format("[{0}] ", row["SerialNumber"]);

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

                Sheet.Cells[i, 5].Value = builder.ToString();
                Sheet.Rows[i].Tag = new Guid(row["ID"].ToString());

            }

            ShowLabel(new Guid(result.Rows[0]["ID"].ToString()));
        }

        private void TestOverTimeProcess_Load(object sender, EventArgs e)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||
                    Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
            {
                bt_approval.Visible = false;
                bt_disapproval.Visible = false;
            }
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                btnReset.Visible = true;
            }
            else
            {
                btnReset.Visible = false;
            }
            SetDataSource();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_disapproval_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_process.Text))
            {
                MessageBox.Show("请填写处理结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rows = FarPointExtensions.GetSelectionRows(Sheet);
            var ids = new List<Guid>();

            if (ApplyAll.Checked)
            {
                foreach (Row row in Sheet.Rows)
                {
                    if (row.Tag == null)
                    {
                        continue;
                    }

                    var id = new Guid(row.Tag.ToString());

                    ids.Add(id);
                }
            }
            else
            {

                foreach (var row in rows)
                {
                    var id = new Guid(row.Tag.ToString());
                    if (id == null)
                    {
                        continue;
                    }

                    ids.Add(id);
                }
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("请选择要处理的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ProcessOverTime(2, ids.ToArray());
            }
        }

        private void bt_approval_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_process.Text))
            {
                MessageBox.Show("请填写处理结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rows = FarPointExtensions.GetSelectionRows(Sheet);
            var ids = new List<Guid>();

            if (ApplyAll.Checked)
            {
                foreach (Row row in Sheet.Rows)
                {
                    if (row.Tag == null)
                    {
                        continue;
                    }

                    var id = new Guid(row.Tag.ToString());

                    ids.Add(id);
                }
            }
            else
            {

                foreach (var row in rows)
                {
                    var id = new Guid(row.Tag.ToString());
                    if (id == null)
                    {
                        continue;
                    }

                    ids.Add(id);
                }
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("请选择要处理的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ProcessOverTime(1, ids.ToArray());
            }
        }


        private void ProcessOverTime(Int32 status, Guid[] ids)
        {
            try
            {
                CaiJiHelperClient.ProcessTestOverTime(ids, tb_process.Text.Trim(), status);
                MessageBox.Show("处理成功，查看处理过的试验请到\"审批\"-->\"查看已审批的过期试验中查看\"");
                if (ApplyAll.Checked == true)
                {
                    this.Close();
                }
                SetDataSource();
            }
            catch
            {
                MessageBox.Show("保存失败，请联系管理员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Table_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (e.Range.RowCount == 1)
            {
                var row = Sheet.Rows[e.Range.Row];
                var id = new Guid(row.Tag.ToString());
                if (id == null)
                {
                    return;
                }

                ShowLabel(id);
            }
            else
            {
                tb_process.Text = tb_reason.Text = lb_process.Text = lb_reason.Text = "";
            }
        }

        private void ShowLabel(Guid id)
        {

            var table = CaiJiHelperClient.GetTestOverTimeByID(id);

            if (table == null || table.Rows.Count == 0)
            {
                return;
            }

            var row = table.Rows[0];

            tb_reason.Text = (row["SGComment"] ?? "").ToString();
            tb_process.Text = (row["JLComment"] ?? "").ToString();

            int Status = int.Parse(row["Status"].ToString());
            if (Status == 0)
            {//未处理
                tb_process.Enabled = true;
                bt_approval.Enabled = true;
                bt_disapproval.Enabled = true;
            }
            else if (Status == 2)
            {//拒绝
                tb_process.Enabled = true;
                bt_approval.Enabled = true;
                bt_disapproval.Enabled = false;
            }
            else
            {//已通过
                tb_process.Enabled = false;
                bt_approval.Enabled = false;
                bt_disapproval.Enabled = false;
            }
            #region 显示填写与处理
            if (row["LastSGUser"] != DBNull.Value)
            {
                lb_reason.Text = row["LastSGUser"].ToString() + " 于 " +
                   row["LastSGTime"].ToString() + " 填写";
            }
            else
            {
                lb_reason.Text = "未填写";
            }

            if (row["ApprovedJLUser"] != DBNull.Value)
            {
                int status = int.Parse(row["Status"].ToString());
                String result = " 未处理";
                if (status == 1)
                {
                    result = " 通过";
                }
                else if (status == 2)
                {
                    result = " 拒绝";
                }
                lb_process.Text = row["ApprovedJLUser"].ToString() + " 于 " +
                   row["ApprovedTime"].ToString() + result;
            }
            else
            {
                lb_process.Text = "未处理";
            }
            #endregion
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var rows = FarPointExtensions.GetSelectionRows(Sheet);
            var ids = new List<Guid>();

            if (ApplyAll.Checked)
            {
                foreach (Row row in Sheet.Rows)
                {
                    if (row.Tag == null)
                    {
                        continue;
                    }

                    var id = new Guid(row.Tag.ToString());

                    ids.Add(id);
                }
            }
            else
            {

                foreach (var row in rows)
                {
                    var id = new Guid(row.Tag.ToString());
                    if (id == null)
                    {
                        continue;
                    }

                    ids.Add(id);
                }
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("请选择要处理的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                CaiJiHelperClient.UpdateTestOverTimeStatus(ids.ToArray(), 0);
                SetDataSource();
                this.Close();
                //ProcessOverTime(1, ids.ToArray());
            }
        }

    }
}
