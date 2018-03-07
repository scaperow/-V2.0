using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.ContextCache;

namespace Yqun.MainUI
{
    public partial class AppHelp : Form
    {
        public AppHelp()
        {
            InitializeComponent();

            this.Text = "关于 " + Application.ProductName;
            this.Icon = Yqun.MainUI.Properties.Resources.logo16;
        }

        private void AppHelp_Load(object sender, EventArgs e)
        {
            this.labelProductName.Text = "产品名称：" + Application.ProductName;
            this.labelVersion.Text = "版本：" + Application.ProductVersion;
            this.labelCompanyName.Text = "公司名称：" + Application.CompanyName;
            this.labelHomePage.Text = "公司主页：http://www.kingrocket.com";
            this.textBoxDescription.Text = "";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.kingrocket.com");   
        }
    }
}
