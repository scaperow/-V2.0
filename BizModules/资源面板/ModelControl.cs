using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;
using BizCommon;
using BizComponents;
using Yqun.Interfaces;
using Yqun.Common.ContextCache;
using Yqun.Bases;
using System.IO;

namespace BizModules
{
    public partial class ModelControl : UserControl
    {
        public ModelControl()
        {
            InitializeComponent();
        }

        //用来存放拖动的节点
        TreeNode _dragNode;
        internal TreeNode dragNode
        {
            get
            {
                return _dragNode;
            }
            set
            {
                _dragNode = value;
            }
        }

        public void InitModuleCatlog()
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                DepositoryResourceCatlog.InitModuleCatlog(TemplateTree);
            }
            else
            {
                DepositoryResourceCatlog.InitModuleCatlog(TemplateTree);
            }

            TemplateTree.SelectedNode = TemplateTree.TopNode;
        }

        private void ModelControl_Load(object sender, EventArgs e)
        {
            TemplateTree.ItemDrag += new ItemDragEventHandler(TemplateTree_ItemDrag);
            TemplateTree.DragEnter += new DragEventHandler(TemplateTree_DragEnter);
            TemplateTree.DragLeave += new EventHandler(TemplateTree_DragLeave);
            TemplateTree.DragOver += new DragEventHandler(TemplateTree_DragOver);
            TemplateTree.GiveFeedback += new GiveFeedbackEventHandler(TemplateTree_GiveFeedback);
        }

        void TemplateTree_DragOver(object sender, DragEventArgs e)
        {
            Point point = TemplateTree.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(point.X, point.Y);
            e.Effect = DragDropEffects.Move;
        }

        void TemplateTree_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if ((e.Effect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;
                TemplateTree.Cursor = Cursors.Default;
            }
            else
                e.UseDefaultCursors = true;
        }

        private void TemplateTree_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point point = TemplateTree.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragEnter(TemplateTree.Handle, point.X, point.Y);
            e.Effect = DragDropEffects.Move;
        }

        private void TemplateTree_DragLeave(object sender, System.EventArgs e)
        {
            DragHelper.ImageList_DragLeave(TemplateTree.Handle);
        }

        void TemplateTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode Node = e.Item as TreeNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);

            if (IsModule)
            {
                dragNode = e.Item as TreeNode;
                TemplateTree.SelectedNode = dragNode;

                int imageWidth = dragNode.Bounds.Size.Width + TemplateTree.Indent;

                imageListDrag.Images.Clear();
                imageListDrag.ImageSize = new Size(imageWidth > 255 ? 255 : imageWidth, dragNode.Bounds.Height);

                Bitmap bmp = new Bitmap(dragNode.Bounds.Width + TemplateTree.Indent, dragNode.Bounds.Height);
                Graphics gfx = Graphics.FromImage(bmp);
                gfx.DrawImage(imageList1.Images[dragNode.ImageIndex], 0, 0);
                gfx.DrawString(dragNode.Text, TemplateTree.Font,
                               new SolidBrush(TemplateTree.ForeColor),
                               (float)TemplateTree.Indent, 1.0f);

                imageListDrag.Images.Add(bmp);

                Point p = TemplateTree.PointToClient(Control.MousePosition);

                int dx = p.X + TemplateTree.Indent - dragNode.Bounds.Left;
                int dy = p.Y - dragNode.Bounds.Top;

                if (DragHelper.ImageList_BeginDrag(imageListDrag.Handle, 0, dx, dy))
                {
                    TemplateTree.DoDragDrop(bmp, DragDropEffects.Move);
                    DragHelper.ImageList_EndDrag();
                }
            }
            else
            {
                return;
            }
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            String userCode = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode;
            if (userCode == "-2")
            {
                ModelDesignerMenuItem.Visible = IsModule;
                toolStripMenuItem4.Visible = IsModule;
                QueryModelMenuItem.Visible = !IsModule;
                toolStripMenuItem5.Visible = !IsModule;
                NewModelMenuItem.Visible = !IsModule;
                EditModelMenuItem.Visible = IsModule;
                DeleteModelMenuItem.Visible = IsModule;
                toolStripMenuItem2.Visible = !IsModule;
                NewFolderMenuItem.Visible = !IsModule;
                EditFolderMenuItem.Visible = !IsModule;
                DeleteFolderMenuItem.Visible = !IsModule;
                toolStripMenuItem3.Visible = IsModule;
                TaizhangSettingMenuItem.Visible = IsModule;
                ToolStripMenuItemExport.Visible = IsModule;
                ToolStripMenuItemImport.Visible = !IsModule;
                TestToolStripMenuItem.Visible = !IsModule;
                LineToolStripMenuItem.Visible = !IsModule;
                LineModuleToolStripMenuItem.Visible = !IsModule;
                UploadToolStripMenuItem.Visible = IsModule;
                GGCDocUploadToolStripMenuItem.Visible = IsModule;
                GGCUploadToolStripMenuItem.Visible = IsModule;
                StasticsToolStripMenuItem.Visible = IsModule;
            }
            else
            {
                ModelDesignerMenuItem.Visible = false;
                toolStripMenuItem4.Visible = false;
                QueryModelMenuItem.Visible = false;
                toolStripMenuItem5.Visible = false;
                NewModelMenuItem.Visible = false;
                EditModelMenuItem.Visible = false;
                DeleteModelMenuItem.Visible = false;
                toolStripMenuItem2.Visible = false;
                NewFolderMenuItem.Visible = false;
                EditFolderMenuItem.Visible = false;
                DeleteFolderMenuItem.Visible = false;
                toolStripMenuItem3.Visible = false;
                TaizhangSettingMenuItem.Visible = false;
                ToolStripMenuItemExport.Visible = false;
                ToolStripMenuItemImport.Visible = false;
                TestToolStripMenuItem.Visible = false;
                LineToolStripMenuItem.Visible = false;
                LineModuleToolStripMenuItem.Visible = false;
                UploadToolStripMenuItem.Visible = false;
                GGCDocUploadToolStripMenuItem.Visible = false;
                GGCUploadToolStripMenuItem.Visible = false;
                StasticsToolStripMenuItem.Visible = false;
            }
            if (userCode == "-1" || userCode == "-2")
            {
                LineFormulaToolStripMenuItem.Visible = IsModule;
                SpecialUpdateToolStripMenuItem.Visible = IsModule;
            }
            else
            {
                LineFormulaToolStripMenuItem.Visible = false;
                SpecialUpdateToolStripMenuItem.Visible = false;
            }
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            TemplateTree.Update();
        }

        //模板上下文菜单
        void ModelMenuStrip_Click(object sender, EventArgs e)
        {
            ToolStripItem m = sender as ToolStripItem;
            if (m != null && m.Tag is string)
            {
                switch (m.Tag.ToString().ToLower())
                {
                    case "@modeldesigner"://模板设计器
                        OpenModelDesigner();
                        break;
                    case "@searchmodel"://查找模板
                        SearchModel();
                        break;
                    case "@newmodel"://新建模板
                        NewModule();
                        break;
                    case "@renamemodel"://重命名模板
                        ModifyModule();
                        break;
                    case "@deletemodel"://删除模板
                        DeleteModule();
                        break;
                    case "@newmodelfolder"://新建文件夹
                        NewFolder();
                        break;
                    case "@editmodelfolder"://编辑文件夹
                        ModifyFolder();
                        break;
                    case "@deletemodelfolder"://删除文件夹
                        DeleteFolder();
                        break;
                    case "@taizhangsetting"://台账设置
                        TaizhangSetting();
                        break;
                    case "@stadium"://龄期设置
                        StadiumSet();
                        break;
                    case "@export"://检验设置
                        QualifySetting();
                        break;
                    case "@test"://采集设置
                        TestSetting();
                        break;
                    case "@line"://线路维护
                        ManageLine();
                        break;
                    case "@linemodule"://线路模板设置
                        ManageLineModule();
                        break;
                    case "@lineformula"://线路公式维护
                        ManageLineFormula();
                        break;

                    case "@upload"://上传设置
                        UploadSetting();
                        break;
                    case "@ggcupload"://工管中心上传设置
                        GGCUploadSetting();
                        break;
                    case "@ggcdocupload"://工管中心文档上传设置
                        GGCDocUploadSetting();
                        break;

                    case "@statisticsmap":
                        StatisticsMap();
                        break;
                    case "@specialupdate"://自定义更新模板
                        SpecialUpdate();
                        break;
                    default:
                        break;
                }
            }
        }

        private void StatisticsMap()
        {
            var node = TemplateTree.SelectedNode;
            var isModule = Convert.ToBoolean(node.Tag);

            if (isModule)
            {
                var moduleID = new Guid(node.Name);
                var form = new StatisticsManagement(moduleID, node.Text);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 自定义更新模板
        /// </summary>
        private void SpecialUpdate()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                Guid moduleID = new Guid(Node.Name);
                SpecialUploadDlg sud = new SpecialUploadDlg(moduleID, Node.Text);
                sud.ShowDialog();
            }
        }

        /// <summary>
        /// 上传设置
        /// </summary>
        private void UploadSetting()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                Guid moduleID = new Guid(Node.Name);
                UploadSettingDialog qsd = new UploadSettingDialog(moduleID, Node.Text);
                qsd.ShowDialog();
            }
        }

        /// <summary>
        /// 工管中心上传设置
        /// </summary>
        private void GGCUploadSetting()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                Guid moduleID = new Guid(Node.Name);
                GGCUploadSettingDialog qsd = new GGCUploadSettingDialog(moduleID, Node.Text);
                qsd.ShowDialog();
            }
        }
        /// <summary>
        /// 工管中心文档上传设置
        /// </summary>
        private void GGCDocUploadSetting()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                Guid moduleID = new Guid(Node.Name);
                GGCDocUploadSettingDialog qsd = new GGCDocUploadSettingDialog(moduleID, Node.Text);
                qsd.ShowDialog();
            }
        }
        private void ManageLineFormula()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                ModelDesigner moduleDesigner = new ModelDesigner(new Guid(Node.Name));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                moduleDesigner.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                moduleDesigner.Size = Owner.ClientRectangle.Size;
                moduleDesigner.ForLine = true;
                moduleDesigner.ShowDialog(Owner);
            }
        }

        private void ManageLine()
        {
            ModuleLineDialog dlg = new ModuleLineDialog();
            dlg.ShowDialog();
        }
        private void ManageLineModule()
        {
            LineModuleSetting ldsDialog = new LineModuleSetting();
            ldsDialog.ShowDialog();
        }

        private void TestSetting()
        {
            CaiJiConfigDialog dlg = new CaiJiConfigDialog();
            dlg.ShowDialog();
        }

        private void QualifySetting()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                Guid moduleID = new Guid(Node.Name);
                QualifySettingDialog qsd = new QualifySettingDialog(moduleID);
                qsd.ShowDialog();
            }
        }

        /// <summary>
        /// 导出所选择的模板到XML文件
        /// </summary>
        private void ExportModel()
        {
            try
            {
                TreeNode Node = TemplateTree.SelectedNode;

                Boolean IsModule = Convert.ToBoolean(Node.Tag);
                if (IsModule)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XML文件|*.xml";
                    saveFileDialog.RestoreDirectory = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        String xml = DepositoryModuleConfiguration.GetModuleRelationData(Node.Name);
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                        {
                            sw.Write(xml);
                        }
                        MessageBox.Show("导出成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 龄期设置
        /// </summary>
        private void StadiumSet()
        {
            StadiumConfigDialog dlg = new StadiumConfigDialog();
            dlg.ShowDialog();
        }

        private void OpenModelDesigner()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                ModelDesigner moduleDesigner = new ModelDesigner(new Guid(Node.Name));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                moduleDesigner.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                moduleDesigner.Size = Owner.ClientRectangle.Size;
                moduleDesigner.ShowDialog(Owner);
            }
        }

        private void SearchModel()
        {
            SearchDialog Dialog = new SearchDialog(TemplateTree);
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Show(Owner);
        }

        private void NewFolder()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (!IsModule)
            {
                string Code = DepositoryResourceCatlog.GetNextCode(Node.Name);
                string FolderName = "新建文件夹";

                Boolean Result = DepositoryResourceCatlog.New(Code, FolderName);
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
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (Node != Node.TreeView.TopNode && !IsModule)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void DeleteFolder()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            string Msg = "你确定要删除文件夹 ‘" + Node.Text + "’ 和文件里面的所有模板吗？";
            if (!IsModule && DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                if (Node.Nodes.Count > 0)
                {
                    String Info = "文件夹‘" + Node.Text + "’里面有其他的文件夹或模板，删除失败。";
                    MessageBox.Show(Info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DepositoryResourceCatlog.Delete(Node.Name);
                    Node.Remove();
                }
            }
        }

        private void NewModule()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (!IsModule)
            {
                string Code = DepositoryResourceCatlog.GetNextCode(Node.Name);
                string ModuleName = "新建模板";

                Sys_Module module = new Sys_Module();
                module.ID = Guid.NewGuid();
                module.ModuleSettings = new List<ModuleSetting>();
                module.Name = ModuleName;
                module.Description = ModuleName;
                module.CatlogCode = Code;

                Boolean Result = ModuleHelperClient.NewModule(module);
                if (Result)
                {
                    TreeNode SubNode = new TreeNode();
                    SubNode.Name = module.ID.ToString();
                    SubNode.Text = ModuleName;
                    SubNode.SelectedImageIndex = 2;
                    SubNode.ImageIndex = 2;
                    SubNode.Tag = true;


                    Node.Nodes.Add(SubNode);
                    Node.Expand();
                    Node.TreeView.SelectedNode = SubNode;
                    Node.TreeView.SelectedNode.TreeView.LabelEdit = true;
                    Node.TreeView.SelectedNode.BeginEdit();
                }
            }
        }

        private void ModifyModule()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (Node != Node.TreeView.TopNode && IsModule)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void DeleteModule()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            string Msg = "你确定要删除模板 ‘" + Node.Text + "’ 吗？";
            if (IsModule && DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                if (!ModuleHelperClient.IsModuleUsing(new Guid(Node.Name)))
                {
                    ModuleHelperClient.DeleteModule(new Guid(Node.Name));
                    Node.Remove();
                }
                else
                {
                    String Info = "删除失败，因为工程结构使用了该模板。";
                    MessageBox.Show(Info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //台账设置
        private void TaizhangSetting()
        {
            TreeNode Node = TemplateTree.SelectedNode;
            Boolean IsModule = Convert.ToBoolean(Node.Tag);
            if (IsModule)
            {
                ModelSystemFieldSettingDialog ModelSystemFieldSettingDialog = new ModelSystemFieldSettingDialog(new Guid(Node.Name));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                ModelSystemFieldSettingDialog.ShowDialog(Owner);
            }
        }

        private void TemplateTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        e.Node.EndEdit(false);
                        Boolean IsModule = Convert.ToBoolean(e.Node.Tag);
                        if (!IsModule)
                        {
                            DepositoryResourceCatlog.Update(e.Node.Name, e.Label);
                        }
                        else
                        {
                            ModuleHelperClient.UpdateModuleName(new Guid(e.Node.Name), e.Label);
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

        private void TemplateTree_MouseUp(object sender, MouseEventArgs e)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Point pt = TemplateTree.PointToClient(Cursor.Position);
                    TreeNode Node = TemplateTree.HitTest(pt).Node;
                    TemplateTree.SelectedNode = Node;

                    ModelContextMenu.Show(TemplateTree, pt);
                }
            }
        }

    }
}
