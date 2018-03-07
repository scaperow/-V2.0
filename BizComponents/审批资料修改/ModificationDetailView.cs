using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yqun.Interfaces;
using FarPoint.Win.Spread;
using Yqun.Common.ContextCache;
using BizModules;

namespace BizComponents
{
    public partial class ModificationDetailView : Form
    {
        Int64 logID = 0;

        String DataID = "";
        String ID = "";
        String ModuleID = "";

        public ModificationDetailView(String tag)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(tag) == false)
            {
                var ids = tag.Split(',');

                if (ids.Length >= 1)
                {
                    ID = ids[0];
                }

                if (ids.Length >= 2)
                {
                    DataID = ids[1];
                }

                if (ids.Length >= 3)
                {
                    ModuleID = ids[2];
                }
            }
        }

        private void ModificationDetailView_Load(object sender, EventArgs e)
        {
            InitModificationDetail();
            logID = DepositoryDataModificationInfo.GetOperateLogIDByModifyID(ID);

            if (logID == 0)
            {
                bt_editProcess.Enabled = false;
                bt_editProcess.Text = "申请人还未对此资料进行修改";
            }
            else
            {
                bt_editProcess.Enabled = true;
                bt_editProcess.Text = "查看修改过程";
            }
        }

        private void InitModificationDetail()
        {
            DataTable dt = DepositoryDataModificationInfo.HaveDataModificationInfoByID(ID);
            if (dt != null)
            {
                lb_user.Text = dt.Rows[0]["申请者"].ToString();
                lb_time.Text = dt.Rows[0]["申请日期"].ToString();


                tb_content.Text = dt.Rows[0]["Caption"].ToString();
                tb_reason.Text = dt.Rows[0]["Reason"].ToString();


                lb_segment.Text = dt.Rows[0]["标段名称"].ToString();
                lb_company.Text = dt.Rows[0]["单位名称"].ToString();
                lb_testroom.Text = dt.Rows[0]["试验室名称"].ToString();
                lb_modelName.Text = dt.Rows[0]["模板名称"].ToString();

                lb_approvePerson.Text = dt.Rows[0]["ApprovePerson"].ToString();
                lb_approveTime.Text = dt.Rows[0]["ApproveTime"].ToString();
                lb_result.Text = dt.Rows[0]["State"].ToString();
                tb_processReason.Text = dt.Rows[0]["ProcessReason"].ToString();

                int IsRequestStadium = int.Parse(dt.Rows[0]["IsRequestStadium"].ToString());
                chkIsRequestStadium.Checked = IsRequestStadium == 1 ? true : false;

                bt_yes.Visible = false;
                bt_no.Visible = false;
                tb_processReason.Enabled = false;

                if (dt.Rows[0]["State"].ToString() == "已提交")
                {
                    if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位" ||
                        Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                    {
                        bt_yes.Visible = true;
                        bt_no.Visible = true;
                        tb_processReason.Enabled = true;
                    }
                }

            }
        }

        private void bt_yes_Click(object sender, EventArgs e)
        {

            if (tb_processReason.Text.Trim() == "")
            {
                MessageBox.Show("处理意见不能为空!");
                return;
            }
            Button b = sender as Button;
            if (DialogResult.OK == MessageBox.Show("确认将此的报告设置为 ‘" + b.Text + "’？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                Boolean Result = DepositoryDataModificationInfo.UpdateDataModificationInfoAndStadium(new string[] { ID }, Yqun.Common.ContextCache.ApplicationContext.Current.UserName, b.Text, tb_processReason.Text.Trim(), chkIsRequestStadium.Checked ? 1 : 0);
                String Message = (Result ? "设置成功。" : "设置失败");
                MessageBoxIcon Icon = (Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);

                this.Close();
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_editProcess_Click(object sender, EventArgs e)
        {
            LogDialog Dialog = new LogDialog(logID);
            Form Owner = Yqun.Common.ContextCache.Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.ReadOnly = true;
            Dialog.ShowDialog(Owner);
        }

        private void buttonOpenDocument_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DataID) || string.IsNullOrEmpty(ModuleID))
            {
                MessageBox.Show("没有获取到足够的依据来打开文档, 请稍后再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                ModuleViewer Viewer = new ModuleViewer(new Guid(DataID), new Guid(ModuleID), Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);

                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Viewer.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Viewer.Size = Owner.ClientRectangle.Size;
                Viewer.ReadOnly = false;
                Viewer.isNewData = false;
                Viewer.viewControl = null;

                Viewer.ShowDialog();
            }
            else
            {
                DataDialog Dialog = new DataDialog(new Guid(DataID), new Guid(ModuleID));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Dialog.Owner = Owner;
                Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Dialog.Size = Owner.ClientRectangle.Size;
                Dialog.ReadOnly = true;
                Dialog.ShowDialog();
            }
        }

    }
}
