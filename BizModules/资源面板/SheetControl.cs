using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using BizComponents;
using Yqun.Services;
using Yqun.Interfaces;
using Yqun.Common.ContextCache;
using Yqun.Bases;

namespace BizModules
{
    public partial class SheetControl : UserControl
    {
        public SheetControl()
        {
            InitializeComponent();
        }

        public void InitSheetCatlog()
        {
            DepositorySheetCatlog.InitSheetCatlog(FpSheetView);
            FpSheetView.SelectedNode = FpSheetView.TopNode;
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            TreeNode Node = FpSheetView.SelectedNode;
            if (Node != null)
            {
                Boolean IsSheet = Convert.ToBoolean(Node.Tag);
                String userCode = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode;
                if (userCode == "-2")
                {
                    SheetDesignerMenuItem.Visible = IsSheet;
                    toolStripMenuItem5.Visible = IsSheet;
                    CopySheetMenuItem.Visible = IsSheet;
                    toolStripMenuItem4.Visible = IsSheet;
                    QuerySheetMenuItem.Visible = !IsSheet;
                    toolStripMenuItem1.Visible = !IsSheet;
                    NewSheetMenuItem.Visible = !IsSheet;
                    EditSheetMenuItem.Visible = IsSheet;
                    DeleteSheetMenuItem.Visible = IsSheet;
                    toolStripMenuItem2.Visible = !IsSheet;
                    NewFolderMenuItem.Visible = !IsSheet;
                    EditFolderMenuItem.Visible = !IsSheet;
                    DeleteFolderMenuItem.Visible = !IsSheet;
                    toolStripMenuItem3.Visible = !IsSheet;
                    CheckDataZoneMenuItem.Visible = !IsSheet;
                }
                else
                {
                    SheetDesignerMenuItem.Visible = false;
                    toolStripMenuItem5.Visible = false;
                    CopySheetMenuItem.Visible = false;
                    toolStripMenuItem4.Visible = false;
                    QuerySheetMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    NewSheetMenuItem.Visible = false;
                    EditSheetMenuItem.Visible = false;
                    DeleteSheetMenuItem.Visible = false;
                    toolStripMenuItem2.Visible = false;
                    NewFolderMenuItem.Visible = false;
                    EditFolderMenuItem.Visible = false;
                    DeleteFolderMenuItem.Visible = false;
                    toolStripMenuItem3.Visible = false;
                    CheckDataZoneMenuItem.Visible = false;
                }
            }
            else
            {
                FpSheetView.SelectedNode = FpSheetView.TopNode;
                e.Cancel = true;
            }
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            FpSheetView.Update();
        }

        //表单上下文菜单
        void SheetMenuStrip_Click(object sender, EventArgs e)
        {
            ToolStripItem m = sender as ToolStripItem;
            if (m != null && m.Tag is string)
            {
                switch (m.Tag.ToString().ToLower())
                {
                    case "@sheetdesigner"://表单设计器
                        OpenSheetDesigner();
                        break;
                    case "@searchsheet"://查找表单
                        SearchSheet();
                        break;
                    case "@copysheet"://复制表单
                        CopySheet();
                        break;
                    case "@newsheet"://新建表单
                        NewSheet();
                        break;
                    case "@renamesheet"://重命名表单
                        ModifySheet();
                        break;
                    case "@deletesheet"://删除表单
                        DeleteSheet();
                        break;
                    case "@newsheetfolder"://新建文件夹
                        NewFolder();
                        break;
                    case "@editsheetfolder"://编辑文件夹
                        ModifyFolder();
                        break;
                    case "@deletesheetfolder"://删除文件夹
                        DeleteFolder();
                        break;
                    case "@checkdatazone"://一致性校验
                        CheckDataZone();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OpenSheetDesigner()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            if (IsSheet)
            {
                SheetDesinger SheetDesigner = new SheetDesinger(new Guid(Node.Name));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                SheetDesigner.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                SheetDesigner.Size = Owner.ClientRectangle.Size;
                SheetDesigner.ShowDialog(Owner);
            }
        }

        private void SearchSheet()
        {
            SearchDialog Dialog = new SearchDialog(FpSheetView);
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Show(Owner);
        }

        private void CopySheet()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            if (IsSheet)
            {
                Guid newSheetID = Guid.NewGuid();
                if (DialogResult.OK == MessageBox.Show("确定要复制表单‘" + Node.Text + "’吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    Boolean Result = ModuleHelperClient.CopySheet(new Guid(Node.Name), newSheetID, Node.Text + "-副本");
                    if (Result)
                    {
                        TreeNode SubNode = new TreeNode();
                        SubNode.Name = newSheetID.ToString();
                        SubNode.Text = Node.Text + "-副本";
                        SubNode.SelectedImageIndex = 2;
                        SubNode.ImageIndex = 2;
                        SubNode.Tag = true;

                        Node.Parent.Nodes.Add(SubNode);
                        Node.TreeView.SelectedNode = SubNode;
                        SubNode.EnsureVisible();
                    }

                    String Message = (Result ? "复制表单成功。" : "复制表单失败！");
                    MessageBoxIcon Icon = (Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
                }
            }
        }

        private void NewSheet()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            if (!IsSheet)
            {
                string Code = DepositorySheetCatlog.GetNextCode(Node.Name);
                string SheetName = DepositorySheetConfiguration.GetSheetName(Node.Name, "新建表单");
                Sys_Sheet sheet = new Sys_Sheet();
                sheet.ID = Guid.NewGuid();
                sheet.CatlogCode = Code;
                sheet.SheetData = "";
                sheet.Name = SheetName;
                sheet.SheetXML =JZCommonHelper.GZipCompressString( SheetConfiguration.BlankSheet);
                
                Boolean Result = ModuleHelperClient.SaveSheet(sheet);
                if (Result)
                {
                    TreeNode SubNode = new TreeNode();
                    SubNode.Name = sheet.ID.ToString();
                    SubNode.Text = SheetName;
                    SubNode.SelectedImageIndex = 2;
                    SubNode.ImageIndex = 2;
                    SubNode.Tag = true;

                    Node.Nodes.Add(SubNode);
                    Node.Expand();
                    Node.TreeView.SelectedNode = SubNode;
                }
            }
        }

        private void ModifySheet()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            if (Node != Node.TreeView.TopNode && IsSheet)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void DeleteSheet()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            string Msg = "你确定要删除表单 ‘" + Node.Text + "’ 吗？";
            if (IsSheet && DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                if (!ModuleHelperClient.IsSheetUsing(new Guid(Node.Name)))
                {
                    if (ModuleHelperClient.DeleteSheet(new Guid(Node.Name)))
                    {
                        Node.Remove();
                    }
                }
                else
                {
                    String Info = "删除失败，因为有模板使用了该表单。";
                    MessageBox.Show(Info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void NewFolder()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            if (!IsSheet)
            {
                string Code = DepositorySheetCatlog.GetNextCode(Node.Name);
                string FolderName = DepositorySheetConfiguration.GetFolderName(Node.Name, "新建文件夹");

                Boolean Result = DepositorySheetCatlog.New(Code, FolderName);
                if (Result)
                {
                    TreeNode SubNode = new TreeNode();
                    SubNode.Name = Code;
                    SubNode.Text = FolderName;
                    SubNode.SelectedImageIndex = 1;
                    SubNode.ImageIndex = 0;

                    Node.Nodes.Add(SubNode);
                    Node.Expand();
                    Node.TreeView.SelectedNode = SubNode;
                }
            }
        }

        private void ModifyFolder()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            if (Node != Node.TreeView.TopNode && !IsSheet)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void DeleteFolder()
        {
            TreeNode Node = FpSheetView.SelectedNode;
            Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            string Msg = "你确定要删除文件夹 ‘" + Node.Text + "’ 吗？";
            if (!IsSheet && DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                if (Node.Nodes.Count > 0)
                {
                    String Info = "文件夹‘" + Node.Text + "’里面有其他的文件夹或表单，删除失败。";
                    MessageBox.Show(Info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DepositorySheetCatlog.Delete(Node.Name);
                    Node.Remove();
                }
            }
        }

        private void CheckDataZone()
        {
            //TreeNode Node = FpSheetView.SelectedNode;
            //Boolean IsSheet = Convert.ToBoolean(Node.Tag);
            //if (!IsSheet)
            //{
            //    DataTable Data = DepositorySheetCatlog.GetSheetResource(Node.Name);

            //    ProgressForm form = new ProgressForm();
            //    form.DefaultStatusText = "正在校验单元格类型与字段类型的一致性...";
            //    form.DoWork += new ProgressForm.DoWorkEventHandler(form_DoWork);
            //    form.ProgressBar.Style = ProgressBarStyle.Blocks;
            //    form.Argument = Data;
            //    DialogResult result = form.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        Dictionary<String, List<String>> ErrorList = form.Result.Result as Dictionary<String, List<String>>;
            //        List<String> SheetIndexs = new List<string>(ErrorList.Keys);

            //        TreeNode NextNode = Node;
            //        while (NextNode != null && NextNode.FullPath.StartsWith(Node.FullPath))
            //        {
            //            if (Convert.ToBoolean(NextNode.Tag) && SheetIndexs.Contains(NextNode.Name))
            //            {
            //                NextNode.ForeColor = Color.Blue;
            //            }
            //            else
            //            {
            //                NextNode.ForeColor = Color.Empty;
            //            }

            //            if (NextNode.FirstNode != null)
            //            {
            //                NextNode = NextNode.FirstNode;
            //            }
            //            else if (NextNode.NextNode != null)
            //            {
            //                NextNode = NextNode.NextNode;
            //            }
            //            else
            //            {
            //                if (NextNode.Parent != null)
            //                {
            //                    TreeNode tempNode = NextNode.Parent;
            //                    while (tempNode.NextNode == null)
            //                    {
            //                        if (tempNode.Parent == null)
            //                            break;
            //                        tempNode = tempNode.Parent;
            //                    }

            //                    NextNode = tempNode.NextNode;
            //                }
            //                else
            //                {
            //                    NextNode = NextNode.Parent;
            //                }
            //            }
            //        }

            //        String Message = (ErrorList.Keys.Count > 0 ? "检查完毕，已将不一致的表单标记为蓝色！" : "检查完毕，未找到不一致的表单！");
            //        MessageBox.Show(Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            MessageBox.Show("完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void form_DoWork(ProgressForm sender, DoWorkEventArgs e)
        {
            DataTable myArgument = e.Argument as DataTable;
            Dictionary<String, List<String>> ErrorList = new Dictionary<string, List<string>>();

            if (myArgument != null && myArgument.Rows.Count > 0)
            {
                foreach (DataRow Row in myArgument.Rows)
                {
                    String SheetIndex = Row["ID"].ToString();
                    String Description = Row["Description"].ToString();
                    String DataTable = Row["DataTable"].ToString();

                    int index = myArgument.Rows.IndexOf(Row);
                    sender.SetProgress(index * 100 / myArgument.Rows.Count);
                    sender.DefaultStatusText = "正在校验‘" + Description + "’...";

                    if (SheetIndex != "" && DataTable != "")
                    {
                        List<String> ErrorFieldList = DataTableConsistencyManager.GetErrorFieldList(SheetIndex, DataTable);
                        if (ErrorFieldList.Count > 0)
                            ErrorList.Add(SheetIndex, ErrorFieldList);
                    }

                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            e.Result = ErrorList;
        }

        private void FpSheetView_MouseUp(object sender, MouseEventArgs e)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Point pt = FpSheetView.PointToClient(Cursor.Position);
                    TreeNode Node = FpSheetView.HitTest(pt).Node;
                    FpSheetView.SelectedNode = Node;

                    SheetContextMenu.Show(FpSheetView, pt);
                }
            }
        }

        private void FpSheetView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        e.Node.EndEdit(false);
                        Boolean IsSheet = Convert.ToBoolean(e.Node.Tag);
                        if (!IsSheet)
                        {
                            ModuleHelperClient.UpdateSheetCatelogName(new Guid(e.Node.Name), e.Label);
                        }
                        else
                        {
                            ModuleHelperClient.UpdateSheetName(new Guid(e.Node.Name), e.Label);
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
    }
}
