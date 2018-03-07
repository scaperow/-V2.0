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
    public partial class UserAuthDialog : Form
    {
        public UserAuthDialog()
        {
            InitializeComponent();
        }

        private void UserAuthForm_Load(object sender, EventArgs e)
        {
            InitRoles();
            InitUsers();

            TreeNode Node = RoleView.SelectedNode;
            if (Node != null)
            {
                Role role = Node.Tag as Role;
                ShowUserInformation(role);
            }
        }

        private void InitRoles()
        {
            DepositoryRole.Init(RoleView);
            if (RoleView.Nodes.Count > 0)
                RoleView.Nodes[0].EnsureVisible();
        }

        private void InitUsers()
        {
            UsersView.Nodes.Clear();

            List<User> Users = DepositoryUser.Init();
            foreach (User user in Users)
            {
                TreeNode Node = new TreeNode();
                Node.Name = user.Index;
                Node.Text = user.Name;
                Node.Tag = user;

                UsersView.Nodes.Add(Node);
            }

            if (UsersView.Nodes.Count > 0)
            {
                //为了解决最后一个根节点不显示的问题，加几行代码
                UsersView.Nodes[UsersView.Nodes.Count - 1].Nodes.Add("");
                UsersView.Nodes[UsersView.Nodes.Count - 1].Expand();
                UsersView.Nodes[UsersView.Nodes.Count - 1].Nodes.Clear();

                UsersView.SelectedNode = UsersView.Nodes[0];
            } 
        }

        private void ShowUserInformation(Role role)
        {
            if (role == null)
                return;

            List<User> Users = DepositoryUser.Init(new string[] { role.Index });
            foreach (TreeNode Node in UsersView.Nodes)
            {
                User user = Node.Tag as User;
                Node.Checked = (Users.IndexOf(user) != -1);
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            //保存用户的信息
            List<User> users = new List<User>();
            foreach (TreeNode Node in UsersView.Nodes)
            {
                User user = Node.Tag as User;
                if (user != null)
                    users.Add(user);
            }

            if (users.Count > 0)
                DepositoryUser.Update(users.ToArray());
            Close();
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UsersView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Node = RoleView.SelectedNode;
            if (Node != null)
            {
                Role role = Node.Tag as Role;
                User user = e.Node.Tag as User;

                if (e.Node.Checked)
                {
                    user.Roles.Clear();
                    user.Roles.Add(role);
                }
                else
                {
                    user.Roles.Remove(role);
                }
            }
        }

        private void UsersView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                TreeNode Node = RoleView.SelectedNode;
                if (Node != null)
                {
                    Role role = Node.Tag as Role;
                    User user = e.Node.Tag as User;

                    if (e.Node.Checked)
                    {
                        user.Roles.Clear();
                        user.Roles.Add(role);
                    }
                    else
                    {
                        user.Roles.Remove(role);
                    }
                }
            }
        }

        private void RolesView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Role role = e.Node.Tag as Role;
            UsersView.Visible = (role != null);
            ShowUserInformation(role);
        }
    }
}
