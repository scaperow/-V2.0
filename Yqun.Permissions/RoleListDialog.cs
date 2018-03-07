using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Permissions.Common;
using Yqun.Permissions.Properties;
using Yqun.Bases;

namespace Yqun.Permissions
{
    public partial class RoleListDialog : Form
    {
        public RoleListDialog()
        {
            InitializeComponent();
        }

        private void RoleList_Load(object sender, EventArgs e)
        {
            DepositoryRole.Init(treeView1);
        }

        public List<Role> GetCheckedNode()
        {
            List<Role> roles = new List<Role>();

            TreeNode NextNode = treeView1.TopNode;
            while (NextNode != null)
            {
                if (NextNode.FirstNode != null)
                {
                    NextNode = NextNode.FirstNode;
                }
                else if (NextNode.NextNode != null)
                {
                    NextNode = NextNode.NextNode;
                }
                else
                {
                    TreeNode tempNode = NextNode.Parent;
                    while (tempNode.NextNode == null)
                    {
                        if (tempNode.Parent == null)
                            break;
                        tempNode = tempNode.Parent;
                    }

                    NextNode = tempNode.NextNode;
                }

                if (NextNode != null && NextNode.Checked)
                {
                    Role role = NextNode.Tag as Role;
                    roles.Add(role);
                }
            }

            return roles;
        }

        //只能选中一个角色
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode NextNode = e.Node.TreeView.TopNode;
            while (NextNode != null)
            {
                if (NextNode.FirstNode != null)
                {
                    NextNode = NextNode.FirstNode;
                }
                else if (NextNode.NextNode != null)
                {
                    NextNode = NextNode.NextNode;
                }
                else
                {
                    TreeNode tempNode = NextNode.Parent;
                    while (tempNode.NextNode == null)
                    {
                        if (tempNode.Parent == null)
                            break;
                        tempNode = tempNode.Parent;
                    }

                    NextNode = tempNode.NextNode;
                }

                if (NextNode != null)
                    NextNode.Checked = false;
            }

            e.Node.Checked = true;
        }
    }
}
