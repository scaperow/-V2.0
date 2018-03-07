using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;

namespace Yqun.MainUI
{
    public partial class ChangeDefaultPasswordForm : Form
    {
        public ChangeDefaultPasswordForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改默认密码
        /// </summary>
        /// <param name="defefaultPwd">默认密码值</param>
        /// <returns>true修改成功,false修改失败</returns>
        internal bool Changpwd(string defefaultPwd)
        {
            try
            {
                if (txtTwoPwd.Text.Length <= 4 || txtOnePwd.Text.Length <= 4 || txtOnePwd.Text == "111111" || txtOnePwd.Text == "888888")
                {
                    MessageBox.Show("新密码长度要求大于4且不能为初始密码！", "提示", MessageBoxButtons.OK);
                    txtOnePwd.Text = "";
                    txtTwoPwd.Text = "";
                    return false;
                }

                if (txtOnePwd.Text == txtTwoPwd.Text)
                {
                    string Password = txtOnePwd.Text;
                    int r = Agent.SetUserPassword(Yqun.Common.ContextCache.ApplicationContext.Current.UserName, Password);
                    if (r == 1)
                    {
                        Yqun.Common.ContextCache.ApplicationContext.Current.Password = Password;
                        MessageBox.Show("密码修改成功！", "提示", MessageBoxButtons.OK);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("密码修改失败！", "提示", MessageBoxButtons.OK);
                        txtOnePwd.Text = "";
                        txtTwoPwd.Text = "";
                        txtOnePwd.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("第二次输入密码和第一次输入密码\r\n 不同，请重新输入！", "提示", MessageBoxButtons.OK);
                    txtOnePwd.Text = "";
                    txtTwoPwd.Text = "";
                    txtOnePwd.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        private void ChangeDefaultPasswordForm_Activated(object sender, EventArgs e)
        {
            txtOnePwd.Focus();
        }
    }
}
