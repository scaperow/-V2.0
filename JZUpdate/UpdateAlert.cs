using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JZUpgrade
{
    public partial class UpdateAlert : Form
    {
                //1 管理系统文件+不执行，
                //2 采集系统文件+不执行，
        int UpdateFlag = 1;
        public UpdateAlert(int _updateflag)
        {
            UpdateFlag = _updateflag;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateAlert_Load(object sender, EventArgs e)
        {
            if (UpdateFlag == 1)
            {
                web.Url = new Uri("http://www.kingrocket.com/updatedesc/glupdate.html");
            }
            else if (UpdateFlag ==2)
            {
                web.Url = new Uri("http://www.kingrocket.com/updatedesc/cjupdate.html");
            }
        }
    }
}
