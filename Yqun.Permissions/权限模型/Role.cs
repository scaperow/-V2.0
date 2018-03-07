using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Permissions.Common;
using System.Windows.Forms;
using Yqun.Bases;
using System.Data;
using Yqun.Services;

namespace Yqun.Permissions
{
    public class DepositoryRole
    {
        private DepositoryRole()
        {
        }

        public static void Init(TreeView View)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "角色列表";

            TopNode.ImageIndex = 0;
            TopNode.SelectedImageIndex = 0;
            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.PermissionManager.dll", "InitRoleInformation", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();
                    Boolean IsNode = Convert.ToBoolean(row["IsNode"]);
                    Boolean IsAdministrator = (row["IsAdministrator"].ToString() == Boolean.TrueString);

                    int ImageIndex = 0;
                    int SelectedImageIndex = 0;
                    string ParentCode = "";

                    try
                    {
                        ParentCode = Code.Substring(0, Code.Length - 4);
                    }
                    catch
                    { }

                    TreeNode[] Nodes = View.Nodes.Find(ParentCode, true);
                    if (Nodes.Length > 0)
                    {
                        TreeNode ParentNode = Nodes[0];

                        TreeNode Node = new TreeNode();
                        Node.Name = Code;
                        Node.Text = Name;

                        if (!IsNode)
                        {
                            Role role = new Role();
                            role.Index = RalationID;
                            role.Name = Name;
                            role.Code = Code;
                            role.IsAdministrator = IsAdministrator;
                            Node.Tag = role;
                        }
                        else
                        {
                            Node.Tag = Tag.ToLower();
                        }

                        switch (Tag.ToLower())
                        {
                            case "@eng":
                            case "@tenders":
                            case "@unit_施工单位":
                            case "@unit_监理单位":
                            case "@folder":
                                ImageIndex = 1;
                                SelectedImageIndex = ImageIndex;
                                break;
                            case "@role":
                                ImageIndex = 2;
                                SelectedImageIndex = ImageIndex;
                                break;
                        }

                        Node.SelectedImageIndex = SelectedImageIndex;
                        Node.ImageIndex = ImageIndex;
                        ParentNode.Nodes.Add(Node);
                    }
                }
            }

            if (View.Nodes.Count > 0)
                View.SelectedNode = View.Nodes[0];
            View.ExpandAll();
        }

        public static RoleCollection Init(String UserIndex)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitRoleInformation", new object[] { UserIndex }) as RoleCollection;
        }

        public static RoleCollection Init(String[] RoleIndex)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "InitRoleInformation", new object[] { RoleIndex }) as RoleCollection;
        }

        public static String GetNextCode(String ParentCode)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "GetNextRoleCode", new object[] { ParentCode }).ToString();
        }

        public static bool New(Role role)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "NewRole", new object[] { role }));
        }

        public static bool Delete(String Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "DeleteRole", new object[] { Index }));
        }

        public static bool Update(Role role)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "UpdateRole", new object[] { role }));
        }
    }
}
