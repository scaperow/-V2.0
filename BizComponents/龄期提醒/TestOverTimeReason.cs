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

namespace BizComponents
{
    public partial class TestOverTimeReason : Form
    {

        private Guid DataID;
        private DataTable Datas;

        public TestOverTimeReason(Guid dataID, DataTable datas)
        {
            InitializeComponent();
            this.DataID = dataID;
            this.Datas = datas;
        }

        private void TestOverTimeProcess_Load(object sender, EventArgs e)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||
                    Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
            {
                bt_save.Visible = false;
            }

            if (Datas != null && Datas.Rows.Count > 0)
            {
                var type = DepositoryLabStadiumList.GetUserType(Yqun.Common.ContextCache.ApplicationContext.Current.UserCode);
                if (type == "监理")
                {
                    var row = Datas.Rows[0];
                    var code = Convert.ToString(row["TestRoomCode"]);
                    tb_reason.ReadOnly = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code != code;
                }
            }

            SetDataSource();
        }
        private void SetDataSource()
        {
            Sheet.Rows.Count = 0;
            var result = CaiJiHelperClient.GetTestOverTimeByDataID(DataID);
            Sheet.Rows.Count = result.Rows.Count;
            #region
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
                Sheet.Rows[i].Tag = row["ID"];
            }
            #endregion

            if (result.Rows.Count > 0)
            {
                ShowLabel(new Guid(result.Rows[0]["ID"].ToString()));

                //DataTable dt = CaiJiHelperClient.GetTestOverTimeByID(new Guid(result.Rows[0]["ID"].ToString()));
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    tb_reason.Text = (dt.Rows[0]["SGComment"] ?? "").ToString();
                //    int Status = int.Parse(dt.Rows[0]["Status"].ToString());
                //    if (Status == 0)
                //    {
                //        tb_reason.Enabled = true;
                //        bt_save.Enabled = true;
                //    }
                //    else
                //    {
                //        tb_reason.Enabled = false;
                //        bt_save.Enabled = false;
                //    }
                //}
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
            {
                tb_reason.Enabled = true;
                bt_save.Enabled = true;
            }
            else
            {
                tb_reason.Enabled = false;
                bt_save.Enabled = false;
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
        private void Table_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (e.Range.Row > 0)
            {
                string ID = "";
                var row = Sheet.Rows[e.Range.Row];
                if (row == null)
                {
                    return;
                }
                ID = row.Tag.ToString();
                DataTable dt = CaiJiHelperClient.GetTestOverTimeByID(new Guid(ID));
                if (dt != null && dt.Rows.Count > 0)
                {
                    tb_reason.Text = (dt.Rows[0]["SGComment"] ?? "").ToString();
                }
            }
            else
            {
                tb_reason.Text = "";
            }
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_reason.Text.Trim()))
            {
                MessageBox.Show("延时原因不能为空！");
                return;
            }
            var ids = new List<Guid>();
            if (chkAll.Checked)
            {
                for (int i = 0; i < Sheet.Rows.Count; i++)
                {
                    var t = Sheet.Rows[i].Tag.ToString();
                    if (string.IsNullOrEmpty(t))
                    {
                        continue;
                    }

                    ids.Add(new Guid(t));
                }
            }
            else
            {
                var rows = FarPointExtensions.GetSelectionRows(Sheet);
                foreach (var row in rows)
                {
                    var t = row.Tag.ToString();
                    if (string.IsNullOrEmpty(t))
                    {
                        continue;
                    }

                    ids.Add(new Guid(t));
                }
            }

            if (ids.Count <= 0)
            {
                MessageBox.Show("请选择要处理的数据！");
                return;
            }

            try
            {
                CaiJiHelperClient.SubmitCommentMulti(ids.ToArray(), tb_reason.Text.Trim(), 2);

                SetDataSource();
                MessageBox.Show("保存成功，请等待监理审批！");
                if (chkAll.Checked)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("保存失败，请联系管理员");
            }
            //try
            //{
            //    CaiJiHelperClient.SubmitComment(id, tb_reason.Text.Trim());
            //    MessageBox.Show("保存成功");
            //}
            //catch
            //{
            //    MessageBox.Show("保存失败，请联系管理员");
            //}
            //this.Close();
        }
    }
}
