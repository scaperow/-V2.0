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
    public partial class ShowTestRoomInfoDlg : Form
    {
        string TestRoomCode = string.Empty;

        public ShowTestRoomInfoDlg(string testRoomCode)
        {
            InitializeComponent();
            TestRoomCode = testRoomCode;
        }

        private void ShowTestRoomInfoDlg_Load(object sender, EventArgs e)
        {
            DataTable dt = ProjectHelperClient.GetTestRoomInfoByCode(TestRoomCode);
            txtTestRoomCode.Text = TestRoomCode;
            if (dt != null && dt.Rows.Count > 0)
            {
                lblProjectName.Text = dt.Rows[0]["工程名称"].ToString();
                lblSegentName.Text = dt.Rows[0]["标段名称"].ToString();
                lblCompanyName.Text = dt.Rows[0]["单位名称"].ToString();
                lblTestRoomName.Text = dt.Rows[0]["试验室名称"].ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
