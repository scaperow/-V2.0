using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Permissions.Common;
using System.Windows.Forms;
using System.Data;
using Yqun.Bases;
using Yqun.Services;

namespace Yqun.Permissions
{
    public class DepositoryOrganization
    {
        private DepositoryOrganization()
        {
        }

        public static void Init(TreeView View)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "组织结构";

            Selection TopSelection = new Selection();
            TopSelection.Value = "true";
            TopNode.Tag = TopSelection;
            TopNode.ImageIndex = 0;
            TopNode.SelectedImageIndex = 0;
            
            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.PermissionManager.dll", "InitOrganization", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();
                    string IsNode = row["IsNode"].ToString();

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

                        Selection Selection = new Selection();
                        Selection.ID = RalationID;
                        Selection.Value = IsNode;
                        Selection.Tag = Tag;
                        Selection.Code = Code;
                        Node.Tag = Selection;

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
                            case "@user":
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

            View.SelectedNode = TopNode;
            View.ExpandAll();
        }

        public static void Init(TreeView View, String[] UserNames)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "组织结构";
            TopNode.SelectedImageIndex = 0;
            TopNode.ImageIndex = 0;

            Selection TopSelection = new Selection();
            TopSelection.Value = "true";
            TopNode.Tag = TopSelection;
            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.PermissionManager.dll", "InitOrganization", new object[] { UserNames }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();
                    string IsNode = row["IsNode"].ToString();

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

                        Selection Selection = new Selection();
                        Selection.ID = RalationID;
                        Selection.Value = IsNode;
                        Selection.Tag = Tag;
                        Node.Tag = Selection;

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
                            case "@user":
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

            TopNode.Expand();
            View.SelectedNode = TopNode;
        }

        public static void InitWithoutUserName(TreeView View)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "组织结构";
            TopNode.SelectedImageIndex = 0;
            TopNode.ImageIndex = 0;

            Selection TopSelection = new Selection();
            TopSelection.Value = "true";
            TopNode.Tag = TopSelection;
            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.PermissionManager.dll", "InitOrganizationWithoutUserName", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();
                    string IsNode = row["IsNode"].ToString();

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

                        Selection Selection = new Selection();
                        Selection.ID = RalationID;
                        Selection.Value = IsNode;
                        Selection.Tag = Tag;
                        Node.Tag = Selection;

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
                            case "@user":
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

            TopNode.Expand();
            View.SelectedNode = TopNode;
        }

        public static String GetNextCode(String ParentCode)
        {
            return Agent.CallService("Yqun.BO.PermissionManager.dll", "GetNextOrganizationCode", new object[] { ParentCode }).ToString();
        }

        public static String Getusername(String TrueName)
        {
            return (Agent.CallService("Yqun.BO.PermissionManager.dll", "GetUserLoginName", new object[] { TrueName })).ToString();
        }
    }
}
