using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;
using System.Data;
using Yqun.Bases;

namespace BizComponents
{
    public class CacheProjectCatlog
    {
        public static String TreeID = "6ED9D9CB-117E-4d8c-A63B-0157BD1F9DFD";
        public static void InitProjectCatlog(TreeView View)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Text = "工程列表";
            TopNode.SelectedImageIndex = 0;
            TopNode.ImageIndex = 0;

            Selection Selection = new Selection();
            Selection.ID = TreeID;//唯一标记
            Selection.Tag = "@top";
            TopNode.Tag = Selection;

            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "InitProjectCatlog", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();

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

                        Selection = new Selection();
                        Selection.ID = RalationID;
                        Selection.Code = Code;
                        Selection.Tag = Tag;
                        Node.Tag = Selection;

                        switch (Tag.ToLower())
                        {
                            case "@eng":
                                ImageIndex = 1;
                                SelectedImageIndex = 2;
                                break;
                            case "@tenders":
                                ImageIndex = 3;
                                SelectedImageIndex = ImageIndex;
                                break;
                            case "@unit_施工单位":
                            case "@unit_监理单位":
                                ImageIndex = 4;
                                SelectedImageIndex = ImageIndex;
                                break;
                            case "@folder":
                                ImageIndex = 5;
                                SelectedImageIndex = ImageIndex;
                                break;
                            case "@module":
                                ImageIndex = 6;
                                SelectedImageIndex = ImageIndex;
                                break;
                        }

                        Node.SelectedImageIndex = SelectedImageIndex;
                        Node.ImageIndex = ImageIndex;
                        ParentNode.Nodes.Add(Node);
                    }
                }

                Data.Dispose();
                Data = null;
            }

            View.SelectedNode = View.TopNode;
            if (View.TopNode != null)
                View.TopNode.Expand();
        }

        public static void InitProjectCatlogWithoutModel(TreeView View)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Text = "工程列表";
            TopNode.SelectedImageIndex = 0;
            TopNode.ImageIndex = 0;

            Selection Selection = new Selection();
            Selection.ID = TreeID;//唯一标记
            Selection.Tag = "@top";
            TopNode.Tag = Selection;

            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "InitProjectCatlog", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();

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

                        if (Tag.ToLower() != "@module")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Name = Code;
                            Node.Text = Name;

                            Selection = new Selection();
                            Selection.ID = RalationID;
                            Selection.Tag = Tag;
                            Node.Tag = Selection;

                            switch (Tag.ToLower())
                            {
                                case "@eng":
                                    ImageIndex = 1;
                                    SelectedImageIndex = 2;
                                    break;
                                case "@tenders":
                                    ImageIndex = 3;
                                    SelectedImageIndex = ImageIndex;
                                    break;
                                case "@unit_施工单位":
                                case "@unit_监理单位":
                                    ImageIndex = 4;
                                    SelectedImageIndex = ImageIndex;
                                    break;
                                case "@folder":
                                    ImageIndex = 5;
                                    SelectedImageIndex = ImageIndex;
                                    break;
                            }

                            Node.SelectedImageIndex = SelectedImageIndex;
                            Node.ImageIndex = ImageIndex;
                            ParentNode.Nodes.Add(Node);
                        }
                        else
                        {
                            Selection selection = ParentNode.Tag as Selection;
                            selection.Value = string.Format("{0},{1}", selection.Value, Code);
                            selection.Value1 = string.Format("{0},{1}", selection.Value1, RalationID);
                        }
                    }
                }

                Data.Dispose();
                Data = null;
            }

            View.SelectedNode = View.TopNode;
            if (View.TopNode != null)
                View.TopNode.Expand();
        }
    }
}
