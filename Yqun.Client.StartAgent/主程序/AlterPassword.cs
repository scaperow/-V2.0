using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using Yqun.Services;

namespace Yqun.MainUI
{
    public partial class AlterPassword : Form
    {
        public AlterPassword()
        {
            InitializeComponent();

            this.Icon = Yqun.MainUI.Properties.Resources.logo16;
        }

        private void AlterPassword_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }

        private void OkPassword_Click(object sender, EventArgs e)
        {
            OldPassword.Focus();
            if (OldPassword.Text == Yqun.Common.ContextCache.ApplicationContext.Current.Password)
            {
                if (OnePassword.Text == TwoPassword.Text)
                {
                    if (OnePassword.Text.Length <= 4 || TwoPassword.Text.Length <= 4 || OnePassword.Text == "111111" || OnePassword.Text == "888888")
                    {
                        MessageBox.Show("新密码长度要求大于4且不能为初始密码！", "提示");
                        OldPassword.Text = "";
                        OnePassword.Text = "";
                        TwoPassword.Text = "";
                        OldPassword.Focus();
                    }
                    else
                    {
                        string Password = OnePassword.Text;
                        int r = Agent.SetUserPassword(Yqun.Common.ContextCache.ApplicationContext.Current.UserName, Password);
                        if (r == 1)
                        {
                            Yqun.Common.ContextCache.ApplicationContext.Current.Password = Password;
                            MessageBox.Show("密码修改成功！", "提示", MessageBoxButtons.OK);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("密码修改失败！", "提示", MessageBoxButtons.OK);
                            AlterPassword PasswordForm = new AlterPassword();
                            PasswordForm.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("第二次输入密码和第一次输入密码\r\n 不同，请重新输入！", "提示");

                    OldPassword.Text = "";
                    OnePassword.Text = "";
                    TwoPassword.Text = "";
                    OldPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("密码错误请重新输入！", "提示");

                OldPassword.Text = "";
                OnePassword.Text = "";
                TwoPassword.Text = "";
                OldPassword.Focus();
            }
        }

        private void CancelPassword_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
