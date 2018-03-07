using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Permissions.Common;
using Yqun.Bases;

namespace Yqun.Permissions
{
    public partial class RoleDialog : Form
    {
        public RoleDialog()
        {
            InitializeComponent();
        }

        Role m_EditRole;
        public Role EditRole
        {
            get
            {
                return m_EditRole;
            }
            set
            {
                m_EditRole = value;

                if (m_EditRole != null)
                {
                    TextBox_Name.Text = m_EditRole.Name;
                    CheckBox_IsAdmin.Checked = m_EditRole.IsAdministrator;
                }
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Name.Text))
                return;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
