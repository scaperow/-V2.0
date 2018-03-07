using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Yqun.Bases;
using ReportCommon;

namespace ReportComponents
{
    public partial class ReportCatlogForm : DockContent
    {
        public ReportCatlogForm()
        {
            InitializeComponent();
        }

        public ReportWindow ReportWindow
        {
            get
            {
                return DockPanel.Parent as ReportWindow;
            }
        }

        private void ReportCatlogForm_Load(object sender, EventArgs e)
        {
            DepositoryReportCatlog.InitReportCatlog(treeView1);
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == NewFolderButton)
            {
                NewFolder();
            }
            else if (sender == ModifyFolderButton)
            {
                ModifyFolder();
            }
            else if (sender == DeleteFolderButton)
            {
                DeleteFolder();
            }
            else if (sender == NewReportButton)
            {
                NewReport();
            }
            else if (sender == ModifyReportButton)
            {
                ModifyReport();
            }
            else if (sender == DeleteReportButton)
            {
                DeleteReport();
            }
        }

        private void NewFolder()
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);
            if (!IsReport)
            {
                string Code = DepositoryReportCatlog.GetNextCode(Node.Name);
                string FolderName = DepositoryReportCatlog.GetFolderName(Node.Name, "新建文件夹");

                Boolean Result = DepositoryReportCatlog.New(Code, FolderName);
                if (Result)
                {
                    TreeNode SubNode = new TreeNode();
                    SubNode.Name = Code;
                    SubNode.Text = FolderName;
                    SubNode.SelectedImageIndex = 1;
                    SubNode.ImageIndex = 0;

                    Selection = new Selection();
                    Selection.Value = "false";
                    SubNode.Tag = Selection;

                    Node.Nodes.Add(SubNode);
                    Node.Expand();
                    Node.TreeView.SelectedNode = SubNode;
                }
            }
        }

        private void ModifyFolder()
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);
            if (Node != Node.TreeView.TopNode && !IsReport)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void DeleteFolder()
        {
            TreeNode Node = treeView1.SelectedNode;
            String Msg = "文件夹不为空，删除失败！";
            if (Node.Nodes.Count > 0)
            {
                MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);
            Msg = "你确定要删除文件夹 ‘" + Node.Text + "’ 吗？";
            if (!IsReport && DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                if (ReportWindow.report != null && ReportWindow.report.Configuration.Code.StartsWith(Node.Name))
                {
                    ReportWindow.RemoveReport();
                }

                DepositoryReportCatlog.Delete(Node.Name);
                Node.Remove();
            }
        }

        private void NewReport()
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);

            if (!IsReport)
            {
                string Code = DepositoryReportCatlog.GetNextCode(Node.Name);
                string ReportName = DepositoryReportCatlog.GetReportName(Node.Name, "新建表单");

                ReportConfiguration Report = new ReportConfiguration();
                Report.Code = Code;
                Report.Description = ReportName;
                Boolean Result = DepositoryReportConfiguration.New(Report);
                if (Result)
                {
                    TreeNode SubNode = new TreeNode();
                    SubNode.Name = Code;

                    Selection = new Selection();
                    Selection.ID = Report.Index;
                    Selection.Value = "true";
                    SubNode.Tag = Selection;
                    SubNode.Text = ReportName;
                    SubNode.SelectedImageIndex = 2;
                    SubNode.ImageIndex = 2;

                    Node.Nodes.Add(SubNode);
                    Node.Expand();
                    Node.TreeView.SelectedNode = SubNode;
                }
            }
        }

        private void ModifyReport()
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);

            if (Node != Node.TreeView.TopNode && IsReport)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void DeleteReport()
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);

            string Msg = "你确定要删除报表 ‘" + Node.Text + "’ 吗？";
            if (IsReport && DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                DepositoryReportConfiguration.Delete(Selection.ID);

                ReportWindow.RemoveReport();

                Node.Remove();
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        e.Node.EndEdit(false);
                        Selection Selection = (e.Node.Tag as Selection);
                        Boolean IsReport = Convert.ToBoolean(Selection.Value);
                        if (!IsReport)
                        {
                            DepositoryReportCatlog.Update(e.Node.Name, e.Label);
                        }
                        else
                        {
                            Boolean result = DepositoryReportConfiguration.UpdateReportName(Selection.ID, e.Label);
                        }
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageBox.Show("无效的树节点文本.\n" + "无效的字符是: '@','.', ',', '!'", "编辑节点");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show("无效的树节点文本.\n文本不能为空", "编辑节点");
                    e.Node.BeginEdit();
                }

                e.Node.TreeView.LabelEdit = false;
            }
        }

        /// <summary>
        /// 双击打开表单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Selection Selection = e.Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value); 
            if (IsReport)
            {
                ReportWindow.OpenReport(Selection.ID);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);
            DeleteFolderButton.Enabled = (treeView1.SelectedNode != treeView1.TopNode && !IsReport);
            DeleteReportButton.Enabled = (treeView1.SelectedNode != treeView1.TopNode && IsReport);
            NewReportButton.Enabled = (treeView1.SelectedNode != treeView1.TopNode && !IsReport);
            NewFolderButton.Enabled = !IsReport;
            ModifyFolderButton.Enabled = (treeView1.SelectedNode != treeView1.TopNode && !IsReport);
            ModifyReportButton.Enabled = (treeView1.SelectedNode != treeView1.TopNode && IsReport);
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = treeView1.PointToClient(Cursor.Position);
                TreeNode Node = treeView1.HitTest(pt).Node;
                treeView1.SelectedNode = Node;

                contextMenuStrip1.Show(treeView1, pt);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            TreeNode Node = treeView1.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            Boolean IsReport = Convert.ToBoolean(Selection.Value);
            MenuItem_DeleteFolder.Enabled = (treeView1.SelectedNode != treeView1.TopNode && !IsReport);
            MenuItem_DeleteReport.Enabled = (treeView1.SelectedNode != treeView1.TopNode && IsReport);
            MenuItem_NewReport.Enabled = (treeView1.SelectedNode != treeView1.TopNode && !IsReport);
            MenuItem_NewFolder.Enabled = !IsReport;
            MenuItem_ModifyFolder.Enabled = !IsReport;
            MenuItem_ModifyReport.Enabled = IsReport;
        }  

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (sender == MenuItem_NewFolder)
            {
                NewFolder();
            }
            else if (sender == MenuItem_ModifyFolder)
            {
                ModifyFolder();
            }
            else if (sender == MenuItem_DeleteFolder)
            {
                DeleteFolder();
            }
            else if (sender == MenuItem_NewReport)
            {
                NewReport();
            }
            else if (sender == MenuItem_ModifyReport)
            {
                ModifyReport();
            }
            else if (sender == MenuItem_DeleteReport)
            {
                DeleteReport();
            }
        }
    }
}
