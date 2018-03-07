using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Bases;
using System.Windows.Forms;
using System.Data;
using Yqun.Services;
using Yqun.Data.DataBase;
using Yqun.Client;
using Yqun.Permissions.Common;
using BizCommon;
using System.Threading;
using System.Collections;

namespace BizComponents
{
    public delegate void LoadViewDelegate(TreeView view, TreeNode node, OperateEnum operate, TreeNode subnode);

    /// <summary>
    /// 工程结构目录
    /// </summary>
    public class DepositoryProjectCatlog
    {
        public static String TreeID = "6ED9D9CB-117E-4d8c-A63B-0157BD1F9DFD";

        public static void InitProjectCatlog(TreeView View)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Run));
            Hashtable ht = new Hashtable();
            ht.Add(1, Yqun.Common.ContextCache.ApplicationContext.Current);
            ht.Add(2, View);
            t.Start(ht);
        }

        private static void Run(Object obj)
        {
            Hashtable ht = obj as Hashtable;
            Yqun.Common.ContextCache.ApplicationContext.Current = ht[1] as Yqun.Common.ContextCache.ApplicationContext;

            TreeView View = ht[2] as TreeView;
            LoadViewDelegate operate = new LoadViewDelegate(TreeViewOperate);
            View.Invoke(operate, View, null, OperateEnum.Clear, null);

            //View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Text = "工程列表";
            TopNode.SelectedImageIndex = 0;
            TopNode.ImageIndex = 0;

            Selection Selection = new Selection();
            Selection.ID = TreeID;//唯一标记
            Selection.Tag = "@top";
            TopNode.Tag = Selection;

            View.Invoke(operate, View, null, OperateEnum.Add, TopNode);
            //View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitProjectCatlog", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow[] drParent = Data.Select(" len(NodeCode)=4");
                foreach (DataRow row in drParent)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();

                    int ImageIndex = 0;
                    int SelectedImageIndex = 0;

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
                    View.Invoke(operate, View, TopNode, OperateEnum.NodeAdd, Node);
                    //TopNode.Nodes.Add(Node);
                    DataRow[] drSub = Data.Select("NodeCode like '" + Code + "%'");
                    if (drSub.Length > 0)
                    {
                        BindSubNode(Data, Code, Node, operate, View);
                    }
                }
            }
            View.Invoke(operate, View, null, OperateEnum.Set, View.TopNode);
            //View.SelectedNode = View.TopNode;
            if (View.TopNode != null)
            {
                View.Invoke(operate, View, null, OperateEnum.Expand, null);
                //View.TopNode.Expand();
            }
        }

        private static void BindSubNode(DataTable dt, string ParentCode, TreeNode ParentNode, LoadViewDelegate operate, TreeView view)
        {
            DataRow[] drParent = dt.Select(string.Format("NodeCode like '{0}%' and len(NodeCode)>{1} and len(NodeCode)={1}+4 ", ParentCode, ParentCode.Length), "OrderID");
            Selection Selection = new Selection();
            if (drParent.Length > 0)
            {
                foreach (DataRow row in drParent)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();

                    int ImageIndex = 0;
                    int SelectedImageIndex = 0;

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
                    view.Invoke(operate, view, ParentNode, OperateEnum.NodeAdd, Node);
                    //ParentNode.Nodes.Add(Node);
                    //DataRow[] drSub = dt.Select("NodeCode like '" + Code + "%'");
                    //if (drSub.Length > 1)
                    //{
                        BindSubNode(dt, Code, Node, operate, view);
                    //}
                }
            }
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

            DataTable Data = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitProjectCatlog", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {

                DataRow[] drParent = Data.Select(" len(NodeCode)=4");
                foreach (DataRow row in drParent)
                {
                    //foreach (DataRow row in Data.Rows)
                    //{
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();

                    int ImageIndex = 0;
                    int SelectedImageIndex = 0;
                    string ParentCode = "";

                    //try
                    //{
                    //    ParentCode = Code.Substring(0, Code.Length - 4);
                    //}
                    //catch
                    //{ }

                    //TreeNode[] Nodes = View.Nodes.Find(ParentCode, true);
                    //if (Nodes.Length > 0)
                    //{
                    //TreeNode ParentNode = Nodes[0];

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
                        TopNode.Nodes.Add(Node);
                        DataRow[] drSub = Data.Select("NodeCode like '" + Code + "%'");
                        if (drSub.Length > 0)
                        {
                            BindSubNodeWithoutModel(Data, Code, Node);
                        }
                    }
                    else
                    {
                        Selection selection = TopNode.Tag as Selection;
                        selection.Value = string.Format("{0},{1}", selection.Value, Code);
                        selection.Value1 = string.Format("{0},{1}", selection.Value1, RalationID);
                    }
                    //}
                }

                Data.Dispose();
                Data = null;
            }

            View.SelectedNode = View.TopNode;
            if (View.TopNode != null)
                View.TopNode.Expand();
        }

        private static void BindSubNodeWithoutModel(DataTable dt, string ParentCode, TreeNode ParentNode)
        {
            DataRow[] drParent = dt.Select(string.Format("NodeCode like '{0}%' and len(NodeCode)>{1} and len(NodeCode)={1}+4 ", ParentCode, ParentCode.Length), "OrderID");
            Selection Selection = new Selection();
            if (drParent.Length > 0)
            {
                foreach (DataRow row in drParent)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();

                    int ImageIndex = 0;
                    int SelectedImageIndex = 0;


                    if (Tag.ToLower() != "@module")
                    {
                        #region
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

                        DataRow[] drSub = dt.Select("NodeCode like '" + Code + "%'");
                        if (drSub.Length > 0)
                        {
                            BindSubNodeWithoutModel(dt, Code, Node);
                        }
                        #endregion
                    }
                    else
                    {
                        Selection selection = ParentNode.Tag as Selection;
                        selection.Value = string.Format("{0},{1}", selection.Value, Code);
                        selection.Value1 = string.Format("{0},{1}", selection.Value1, RalationID);
                    }


                }
            }
        }

        public static string GetNextCode(string ParentCode)
        {
            string Code = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetProjectCatlogNextCode", new object[] { ParentCode }) as string;
            return Code;
        }

        public static String GetProjectTestModuleCode(String TestRoomCode, String ModelIndex)
        {
            return Convert.ToString(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetProjectTestModelCode", new object[] { TestRoomCode, ModelIndex }));
        }

        private static void TreeViewOperate(TreeView view, TreeNode node, OperateEnum operate, TreeNode subnode)
        {
            if (view == null)
            {
                return;
            }
            switch (operate)
            {
                case OperateEnum.Clear:
                    view.Nodes.Clear();
                    break;
                case OperateEnum.Add:
                    view.Nodes.Add(subnode);
                    break;
                case OperateEnum.Set:
                    view.SelectedNode = subnode;
                    break;
                case OperateEnum.Expand:
                    view.TopNode.Expand();
                    break;
                case OperateEnum.NodeAdd:
                    node.Nodes.Add(subnode);
                    break;
                default:
                    break;
            }
        }
    }

    public enum OperateEnum
    {
        Clear,
        Add,
        Set,
        Expand,
        NodeAdd
    }
}
