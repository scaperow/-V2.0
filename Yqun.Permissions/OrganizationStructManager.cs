using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using Yqun.Services;
using System.Security.Cryptography;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using Yqun.Permissions.Common;
using Yqun.Permissions.Properties;

namespace Yqun.Permissions
{
    /// <summary>
    /// 用来对组织结构进行管理
    /// </summary>
    public partial class OrganizationStructManager : ControlStandard
    {

        public Selection SelectionUser
        {
            get
            {
                var userNode = OrganizationView.SelectedNode;
                if (userNode == null)
                {
                    return null;
                }

                if (userNode.Level != 5)
                {
                    return null;
                }

                return userNode.Tag as Selection;
            }
        }

        public Selection TestRoomSelection
        {
            get
            {
                var selectedNode = OrganizationView.SelectedNode;

                if (selectedNode.Level == 5)
                {
                    return selectedNode.Parent.Tag as Selection;
                }

                return null;
            }
        }

        public OrganizationStructManager()
        {
            InitializeComponent();
        }

        public override object InvokeMessage(Yqun.Interfaces.YqunMessage Message)
        {
            object Result = base.InvokeMessage(Message);

            switch (Message.TypeFlag.ToLower())
            {
                case "@open":
                    OpenModel();
                    break;
            }

            return Result;
        }

        private void OpenModel()
        {
            InitOrganizationStruct();
            LoadDeviceRoles();
        }

        /// <summary>
        /// 加载组织结构
        /// </summary>
        private void InitOrganizationStruct()
        {
            DepositoryOrganization.Init(OrganizationView);

            if (OrganizationView.Nodes.Count > 0)
                OrganizationView.Nodes[0].EnsureVisible();

            if (OrganizationView.TopNode != null)
                OrganizationView.TopNode.Text = "组织结构";
        }

        private void LoadDeviceRoles()
        {
            var table = Agent.CallService("Yqun.BO.BusinessManager.dll", "LoadDeviceRoles", new object[] { }) as DataTable;
            if (table == null || table.Rows.Count == 0)
            {
                return;
            }

            TreeNode node = null;
            foreach (DataRow row in table.Rows)
            {
                if (string.IsNullOrEmpty(row["设备编码"] as string))
                {
                    continue;
                }

                node = LoadDeviceRoles("标段编码", "标段名称", 1, 1, row, null);
                if (node == null)
                {
                    continue;
                }

                node = LoadDeviceRoles("单位编码", "单位名称", 2, 1, row, node);
                if (node == null)
                {
                    continue;
                }

                node = LoadDeviceRoles("试验室编码", "试验室名称", 3, 1, row, node);
                if (node == null)
                {
                    continue;
                }

                node = LoadDeviceRoles("设备编码", "设备编码", 4, 3, row, node);
            }

            foreach (TreeNode n in TreeDevices.Nodes)
            {
                n.ExpandAll();
            }
        }

        private TreeNode LoadDeviceRoles(string code, string name, int level, int image, DataRow row, TreeNode parent)
        {
            var codeValue = row[code] as string;
            var nameValue = row[name] as string;
            var testRoomCode = row["试验室编码"] as string;

            TreeNode[] nodes = parent == null ? TreeDevices.Nodes.Find(codeValue, true) : parent.Nodes.Find(codeValue, true);
            if (nodes != null && nodes.Length > 0)
            {
                return nodes[0];
            }


            if (string.IsNullOrEmpty(codeValue))
            {
                return null;
            }

            var node = new TreeNode()
            {
                Text = nameValue,
                Name = codeValue,
                StateImageIndex = image,
                ImageIndex = image,
                Tag = new DeviceNodeTag()
                {
                    ID = codeValue,
                    TestRoomCode = testRoomCode,
                    Level = level
                }
            };

            if (parent == null)
            {
                TreeDevices.Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node);
            }

            return node;
        }

        /// <summary>
        /// 工具栏响应过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == ToolStripButton_NewUser || sender == ContextMenu_NewUser)
            {
                NewUser();
            }
            else if (sender == ToolStripButton_EditUser || sender == ContextMenu_EditUser)
            {
                EditUser();
            }
            else if (sender == ToolStripButton_DeleteUser || sender == ContextMenu_DeleteUser)
            {
                DeleteUser();
            }
            else if (sender == ToolStripButton_UserAuth)
            {
                UserAuthDialog userauthForm = new UserAuthDialog();
                userauthForm.ShowDialog(this);
            }
        }

        #region 管理用户

        private void NewUser()
        {
            TreeNode Node = OrganizationView.SelectedNode;
            Selection selection = Node.Tag as Selection;
            Boolean IsNode = System.Convert.ToBoolean(selection.Value);
            if (IsNode)
            {
                UserDialog userForm = new UserDialog();
                userForm.Text = "新建用户";
                if (DialogResult.OK == userForm.ShowDialog(this))
                {
                    User user = new User();
                    user.Index = Guid.NewGuid().ToString();
                    user.Code = DepositoryUser.GetNextCode(Node.Name);
                    user.Name = userForm.TextBox_Name.Text;
                    user.Password = EncryptSerivce.Encrypt(userForm.TextBox_Password1.Text);
                    user.IsSys = false;

                    foreach (DataGridViewRow row in userForm.RolesView.Rows)
                    {
                        Role role = row.Tag as Role;
                        if (role != null)
                            user.Roles.Add(role);
                    }

                    bool r = DepositoryUser.New(user);
                    if (r)
                    {
                        TreeNode uNode = new TreeNode();
                        uNode.Text = user.Name;
                        uNode.Name = user.Code;
                        uNode.ImageIndex = 2;
                        uNode.SelectedImageIndex = 2;
                        selection = new Selection();
                        selection.ID = user.Index;
                        selection.Value = false.ToString();
                        uNode.Tag = selection;
                        Node.Nodes.Add(uNode);

                        Node.Expand();
                        OrganizationView.SelectedNode = uNode;
                    }

                    string Msg = r ? "新建用户成功。" : "新建用户失败。";
                    MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void EditUser()
        {
            TreeNode Node = OrganizationView.SelectedNode;
            Selection selection = Node.Tag as Selection;
            Boolean IsUser = !System.Convert.ToBoolean(selection.Value);
            if (IsUser)
            {
                UserDialog userForm = new UserDialog();
                User user = DepositoryUser.Init(selection.ID);
                userForm.EditUser = user;
                userForm.Text = "编辑用户";

                if (DialogResult.OK == userForm.ShowDialog(this))
                {
                    user.Name = userForm.TextBox_Name.Text;
                    user.Password = EncryptSerivce.Encrypt(userForm.TextBox_Password1.Text);
                    user.IsSys = false;

                    user.Roles.Clear();
                    foreach (DataGridViewRow row in userForm.RolesView.Rows)
                    {
                        Role role = row.Tag as Role;
                        if (role != null)
                            user.Roles.Add(role);
                    }

                    bool r = DepositoryUser.Update(user);
                    if (r)
                    {
                        Node.Text = userForm.TextBox_Name.Text;
                    }

                    string Msg = r ? "更新用户成功。" : "更新用户失败。";
                    MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DeleteUser()
        {
            TreeNode Node = OrganizationView.SelectedNode;
            if (Node.Nodes.Count == 0)
            {
                Selection selection = Node.Tag as Selection;
                Boolean IsUser = !System.Convert.ToBoolean(selection.Value);
                if (IsUser)
                {
                    if (DialogResult.Yes == MessageBox.Show(string.Format("是否要删除用户‘{0}’吗？", Node.Text), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        bool r = DepositoryUser.Delete(selection.ID);
                        if (r)
                        {
                            Node.Remove();
                        }

                        string Msg = r ? "删除用户成功。" : "删除用户失败。";
                        MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion 管理用户

        #region 菜单状态管理

        private void OrganizationView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode Node = OrganizationView.SelectedNode;
            Selection selection = Node.Tag as Selection;
            String Type = selection.TypeFlag;
            Boolean IsNode = System.Convert.ToBoolean(selection.Value);
            Boolean IsTopNode = (Node == OrganizationView.TopNode);

            ToolStripButton_NewUser.Enabled = IsNode && !IsTopNode;
            ToolStripButton_EditUser.Enabled = !IsNode;
            ToolStripButton_DeleteUser.Enabled = !IsNode;

            SetDeviceRoles(selection, TestRoomSelection);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            TreeNode Node = OrganizationView.SelectedNode;
            Selection selection = Node.Tag as Selection;
            String Type = selection.TypeFlag;
            Boolean IsNode = System.Convert.ToBoolean(selection.Value);
            Boolean IsTopNode = (Node == OrganizationView.TopNode);

            ContextMenu_NewUser.Enabled = IsNode && !IsTopNode;
            ContextMenu_EditUser.Enabled = !IsNode;
            ContextMenu_DeleteUser.Enabled = !IsNode;
        }

        #endregion 菜单状态管理

        private void SetDeviceRoles(Selection userSelection, Selection testRoomSelection)
        {
            TreeDevices.AfterCheck -= new TreeViewEventHandler(TreeDevices_AfterCheck);
            Cursor = Cursors.WaitCursor;

            foreach (TreeNode node in TreeDevices.Nodes)
            {
                var tag = node.Tag as DeviceNodeTag;

                if (tag.Level == 4)
                {
                    node.Checked = tag.TestRoomCode == (testRoomSelection == null ? "" : testRoomSelection.Code);
                }
                else
                {
                    node.Checked = false;
                }

                CheckSubDeviceNodes(node, testRoomSelection);
            }

            var roles = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUserDeviceRole", new object[] { userSelection.ID }) as string[];
            if (roles == null)
            {
                return;
            }

            foreach (var role in roles)
            {
                var node = TreeDevices.Nodes.Find(role, true);
                if (node != null)
                {
                    foreach (var n in node)
                    {
                        n.Checked = true;
                    }
                }
            }

            Cursor = Cursors.Default;
            TreeDevices.AfterCheck += new TreeViewEventHandler(TreeDevices_AfterCheck);
        }

        private void OrganizationView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode Node = OrganizationView.GetNodeAt(e.X, e.Y);
                if (Node == null)
                    return;

                OrganizationView.SelectedNode = Node;
                if (OrganizationView.SelectedNode != OrganizationView.TopNode)
                    contextMenuStrip1.Show(OrganizationView, new Point(e.X, e.Y));
            }
        }

        private void TreeDevices_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeDevices.AfterCheck -= new TreeViewEventHandler(TreeDevices_AfterCheck);

            var selection = TestRoomSelection;

            if (e.Node.Checked == false && IsInTestroomNode(e.Node, selection))
            {
                MessageBox.Show("不能取消所隶属试验室的权限", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Node.Checked = true;
            }

            foreach (TreeNode node in e.Node.Nodes)
            {
                if (node.Checked == false && IsInTestroomNode(node, selection))
                {
                    continue;
                }

                node.Checked = e.Node.Checked;

                CheckSubNodes(node, selection);
            }

            TreeDevices.AfterCheck += new TreeViewEventHandler(TreeDevices_AfterCheck);
        }

        private bool IsInTestroomNode(TreeNode node, Selection testRoomCode)
        {
            if (testRoomCode == null)
            {
                return false;
            }

            var tag = node.Tag as DeviceNodeTag;

            if (tag == null)
            {
                return false;
            }

            return testRoomCode.Code == tag.TestRoomCode;
        }

        private void CheckSubDeviceNodes(TreeNode node, Selection testRoomSelection)
        {
            foreach (TreeNode sub in node.Nodes)
            {
                var tag = sub.Tag as DeviceNodeTag;
                if (tag.Level == 4)
                {
                    sub.Checked = tag.TestRoomCode == (testRoomSelection == null ? "" : testRoomSelection.Code);
                }
                else
                {
                    sub.Checked = false;
                }

                CheckSubDeviceNodes(sub, testRoomSelection);
            }
        }

        private void CheckSubNodes(TreeNode node, Selection testRoomSelection)
        {
            foreach (TreeNode sub in node.Nodes)
            {
                if (node.Checked == false && IsInTestroomNode(sub, testRoomSelection))
                {
                    continue;
                }
                else
                {
                    sub.Checked = node.Checked;
                }

                CheckSubNodes(sub, testRoomSelection);
            }
        }

        private List<string> Roles = new List<string>();

        private void ButtonSaveDeviceRole_Click(object sender, EventArgs e)
        {
            Roles.Clear();

            if (SelectionUser == null || string.IsNullOrEmpty(SelectionUser.ID))
            {
                MessageBox.Show("当前选择的是非试验用户的节点, 不能保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;

            foreach (TreeNode node in TreeDevices.Nodes)
            {
                var tag = node.Tag as DeviceNodeTag;
                if (tag.Level == 4 && node.Checked)
                {
                    Roles.Add(tag.ID);
                }

                SaveDeviceRole(node);
            }

            var result = Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveDeviceRoles", new object[] { SelectionUser.ID, Roles.ToArray() });

            Cursor = Cursors.Default;
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveDeviceRole(TreeNode node)
        {
            var testRoomSelection = TestRoomSelection;

            foreach (TreeNode n in node.Nodes)
            {
                var tag = n.Tag as DeviceNodeTag;


                if (tag.Level == 4 && n.Checked)
                {

                    if (testRoomSelection == null || testRoomSelection.Code != tag.TestRoomCode)
                    {
                        Roles.Add(tag.ID);
                    }
                }

                SaveDeviceRole(n);
            }
        }
    }

    public class DeviceNodeTag
    {
        public string ID;
        public string Name;
        /// <summary>
        /// 1. 标段
        /// 2. 单位
        /// 3. 试验室
        /// 4. 设备
        /// </summary>
        public int Level;
        public string TestRoomCode;

    }
}
