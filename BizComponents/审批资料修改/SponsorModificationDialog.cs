using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class SponsorModificationDialog : Form
    {
        Guid DataID;
        Sys_Module module;
        Sys_RequestChange Info = new Sys_RequestChange();

        public SponsorModificationDialog(Guid DataID, Sys_Module _module)
        {
            InitializeComponent();

            this.DataID = DataID;
            this.module = _module;
        }

        private void SponsorModificationDialog_Load(object sender, EventArgs e)
        {
            using (DataTable dt = DocumentHelperClient.GetRequestChangeUnOP(DataID))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    Info.ID = new Guid(dt.Rows[0]["ID"].ToString());
                    Info.Caption = dt.Rows[0]["Caption"].ToString();
                    Info.Reason = dt.Rows[0]["Reason"].ToString();
                    Info.IsRequestStadium = int.Parse(dt.Rows[0]["IsRequestStadium"].ToString());
                }
                else
                {
                    Info.ID = Guid.Empty;
                    Info.Caption = "";
                    Info.Reason = "";
                    Info.IsRequestStadium = 0;
                }
                Info.DocumentID = DataID;
                txtContent.Text = Info.Caption;
                txtReason.Text = Info.Reason;
                chkIsRequestStadium.Checked = Info.IsRequestStadium == 1 ? true : false;

                //SponsorModifyInfo.Visible =  != Guid.Empty;
                //ButtonOk.Enabled = !SponsorModifyInfo.Visible;
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContent.Text.Trim()))
            {
                txtContent.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtReason.Text.Trim()))
            {
                txtReason.Focus();
                return;
            }


            Info.DocumentID = DataID;
            Info.Caption = txtContent.Text;
            Info.Reason = txtReason.Text;
            Info.IsRequestStadium = chkIsRequestStadium.Checked == true ? 1 : 0;

            Boolean Result = DocumentHelperClient.NewRequestChange(Info);
            String Message = (Result ? "申请修改成功。" : "申请修改失败！");
            MessageBoxIcon Icon = (Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);

            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
