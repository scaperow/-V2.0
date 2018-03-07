using System;
using System.Windows.Forms;
using Yqun.Services;
using System.Data;
using Yqun.Bases;

namespace ReportComponents
{
    public class DepositoryReportCatlog
    {
        public static void InitReportCatlog(TreeView View)
        {
            View.Nodes.Clear();

            Selection TopSelection = new Selection();
            TopSelection.Value = "false";
            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "报表列表";
            TopNode.SelectedImageIndex = 1;
            TopNode.ImageIndex = 0;
            TopNode.Tag = TopSelection;
            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportCatlog", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Index = row["ID"].ToString();
                    string Code = row["CatlogCode"].ToString();
                    string Name = row["Description"].ToString();
                    Boolean IsReport = Convert.ToBoolean(row["IsReport"]);

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
                        Selection.ID = Index;
                        Selection.Value = IsReport.ToString();
                        Node.Tag = Selection;

                        Node.SelectedImageIndex = IsReport ? 2 : 1;
                        Node.ImageIndex = IsReport ? 2 : 0;
                        ParentNode.Nodes.Add(Node);
                    }
                }
            }

            TopNode.Expand();
        }

        public static string GetReportIndex(string ReportCode)
        {
            return Convert.ToString(Agent.CallService("Yqun.BO.ReportManager.dll", "GetReportIndex", new object[] { ReportCode }));
        }        

        public static string GetReportName(string FolderCode, string ReportName)
        {
            return Convert.ToString(Agent.CallService("Yqun.BO.ReportManager.dll", "GetNewReportName", new object[] { FolderCode, ReportName }));
        }

        public static string GetFolderName(string FolderCode, string FolderName)
        {
            return Convert.ToString(Agent.CallService("Yqun.BO.ReportManager.dll", "GetNewFolderName", new object[] { FolderCode, FolderName }));
        }

        public static string GetNextCode(string ParentCode)
        {
            return Convert.ToString(Agent.CallService("Yqun.BO.ReportManager.dll", "GetNextReportCatlogCode", new object[] { ParentCode }));
        }

        public static bool New(string Code, string Name)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "NewReportCatlog", new object[] { Code, Name }));
        }

        public static bool Delete(string Code)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "DeleteReportCatlog", new object[] { Code }));
        }

        public static bool Update(string Code, string Name)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReportManager.dll", "UpdateReportCatlog", new object[] { Code, Name }));
        }
    }
}
