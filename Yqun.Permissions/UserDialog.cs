using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Permissions.Common;
using Yqun.Common.Encoder;
using Yqun.Bases;

namespace Yqun.Permissions
{
    /// <summary>
    /// 创建和编辑用户
    /// </summary>
    public partial class UserDialog : Form
    {
        RoleListDialog roleList = new RoleListDialog();

        public UserDialog()
        {
            InitializeComponent();
        }

        User m_EditUser;
        public User EditUser
        {
            get
            {
                return m_EditUser;
            }
            set
            {
                m_EditUser = value;

                if (m_EditUser != null)
                {
                    TextBox_Name.Text = m_EditUser.Name;
                    TextBox_Password1.Text = EncryptSerivce.Dencrypt(m_EditUser.Password);
                    TextBox_Password2.Text = TextBox_Password1.Text;

                    RolesView.Rows.Clear();
                    foreach (Role role in m_EditUser.Roles)
                    {
                        int RowIndex = RolesView.Rows.Add(1);
                        RolesView.Rows[RowIndex].Cells[0].Value = role.Name;
                        RolesView.Rows[RowIndex].Tag = role;
                    }
                }
            }
        }

        #region 角色管理

        private void Button_New_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == roleList.ShowDialog(this))
            {
                foreach (Role role in roleList.GetCheckedNode())
                {
                    bool HaveRole = false;
                    foreach (DataGridViewRow row in RolesView.Rows)
                    {
                        Role r = row.Tag as Role;
                        if (r.Index == role.Index)
                        {
                            HaveRole = true;
                            break;
                        }
                    }

                    if (!HaveRole)
                    {
                        int RowIndex = RolesView.Rows.Add(1);
                        RolesView.Rows[RowIndex].Cells[0].Value = role.Name;
                        RolesView.Rows[RowIndex].Tag = role;
                    }
                }
            }
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in RolesView.SelectedCells)
            {
                RolesView.Rows.RemoveAt(cell.RowIndex);
            }
        }

        #endregion 角色管理

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Name.Text))
                return;

            if (TextBox_Password1.Text != TextBox_Password2.Text)
            {
                MessageBox.Show("两次输入的密码不一致。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CheckBox_PasswordChar_CheckedChanged(object sender, EventArgs e)
        {
            this.TextBox_Password1.PasswordChar = (CheckBox_PasswordChar.Checked? '\0':'*');
            this.TextBox_Password2.PasswordChar = this.TextBox_Password1.PasswordChar;
        }
    }
}
