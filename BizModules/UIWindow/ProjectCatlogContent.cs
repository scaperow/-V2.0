using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BizComponents;
using Yqun.Client;
using Yqun.Services;
using System.Collections;
using Yqun.Bases;
using Yqun.Bases.ClassBases;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using BizModules.Properties;
using Yqun.Permissions.Runtime;
using Yqun.Permissions.Common;
using BizCommon;

namespace BizModules
{
    public partial class ProjectCatlogContent : DockContent
    {
        internal ModuleConfiguration ActiveModule = ModuleConfiguration.Empty;
        internal List<ModuleField> ActiveModuleFields = new List<ModuleField>();

        //Timer for scrolling
        private Timer timer = new Timer();

        public ProjectCatlogContent()
        {
            InitializeComponent();

            this.Icon = IconHelper.GetIcon(Resources.工程结构);

            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
        }

        public BizWindow BizWindow
        {
            get
            {
                return DockPanel.Parent as BizWindow;
            }
        }

        internal BizViewerContent Viewer
        {
            get
            {
                return BizWindow.BizViewerContent;
            }
        }

        internal ModelCatlogContent ResourceContent
        {
            get
            {
                return BizWindow.ModelCatlogContent;
            }
        }

        internal String ActiveNodeCode
        {
            get
            {
                return MainListTree.SelectedNode.Name;
            }
        }

        void ProjectContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem m = sender as ToolStripItem;
            if (m != null && m.Tag is string)
            {
                try
                {
                    switch (m.Tag.ToString().ToLower())
                    {
                        case "@module_newdata":
                            NewData();
                            break;
                        case "@exportdata":
                            ExportData();
                            break;
                        case "@batchprint":
                            BatchPrint();
                            break;
                        case "@module_modesetup":
                            ModeSetUp();
                            break;
                        case "@top_newproject":
                            NewProject();
                            break;
                        case "@eng_newdepartment":
                            NewCompany();
                            break;
                        case "@unit_newprjsct":
                            NewPrjsct();
                            break;
                        case "@tenders_newfolder":
                            NewFolder();
                            break;
                        case "@eng_editproject":
                            EditProject();
                            break;
                        case "@unit_editdepartment":
                            EditCompany();
                            break;
                        case "@tenders_editcontract":
                            EditPrjsct();
                            break;
                        case "@folder_editfolder":
                            EditFolder();
                            break;
                        case "@eng_deleproject":
                            DeleteProject();
                            break;
                        case "@unit_deledepartment":
                            DeleteCompany();
                            break;
                        case "@tenders_delecontract":
                            DeletePrjsct();
                            break;
                        case "@folder_delefolder":
                            DeleteFolder();
                            break;
                        case "@module_delemodule":
                            DeleteModel();
                            break;
                        case "@quaauth":
                            QuaAuthFunction();
                            break;
                        case "@teststadiumtemperature":
                            TestStadiumTemperature();                   //温度试验记录
                            break;
                        case "@pxrelationreport":
                            PXRelationReport();                   //平行关系对应查询
                            break;
                        case "@devicemanagement":
                            DeviceManagement();
                            break;

                        case "@createdevice":
                            CreateDevice();
                            break;

                        default:
                            break;
                    }
                }
                catch
                {
                }
            }
        }


        private void CreateDevice()
        {
            ModifyDevice form;
            var roomNode = MainListTree.SelectedNode;
            if (roomNode != null)
            {
                var unitNode = roomNode.Parent;

                if (unitNode != null)
                {
                    var sectionNode = unitNode.Parent;
                    var sectionSelection = sectionNode.Tag as Selection;
                    var unitSelection = unitNode.Tag as Selection;
                    var roomSelection = roomNode.Tag as Selection;
                    if (sectionSelection != null && unitSelection != null && roomSelection != null)
                    {
                        var section = new Prjsct()
                        {
                            PrjsctCode = sectionSelection.Code,
                            PrjsctName = sectionNode.Text
                        };

                        var unit = new Orginfo()
                        {
                            DepName = unitNode.Text,
                            DepCode = unitSelection.Code
                        };

                        var room = new PrjFolder()
                        {
                            FolderName = roomNode.Text,
                            FolderCode = roomSelection.Code
                        };

                        form = new ModifyDevice(section, unit, room);
                        form.Show();
                        return;
                    }
                }
            }

            form = new ModifyDevice();
            form.Show();
        }

        private void DeviceManagement()
        {
            var form = new DeviceManagement();
            form.Show();

        }

        /// <summary>
        /// 温度试验记录
        /// </summary>
        private void TestStadiumTemperature()
        {
            String TestRoomCode = MainListTree.SelectedNode.Name;
            TemperatureDialog temperatureDialog = new TemperatureDialog(TestRoomCode);
            temperatureDialog.ShowDialog();
        }
        /// <summary>
        /// 平行关系对应查询
        /// </summary>
        private void PXRelationReport()
        {
            Selection Selection = MainListTree.SelectedNode.Tag as Selection;
            //Guid ModuleID = new Guid("");
            if (Selection != null && Selection.Tag.ToString().StartsWith("@module"))
            {
                String ModuleID = Selection.ID;
                String NodeCode = MainListTree.SelectedNode.Name;
                if (NodeCode.Length > 16)
                {
                    String PXTestRoomCode = NodeCode.Substring(0, 16);
                    PXReportRelationDialog pxRelationDialog = new PXReportRelationDialog(PXTestRoomCode,new Guid( ModuleID));
                    pxRelationDialog.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 试验室资质授权
        /// </summary>
        private void QuaAuthFunction()
        {
            String FolderCode = MainListTree.SelectedNode.Name;
            QualificationAuthForm AQForm = new QualificationAuthForm(FolderCode);
            AQForm.ShowDialog();
        }

        /// <summary>
        /// 新建资料
        /// </summary>
        internal void NewData()
        {
            Selection Selection = MainListTree.SelectedNode.Tag as Selection;
            if (Selection.ID is String)
            {
                String ModuleID = Selection.ID;
                String NodeCode = MainListTree.SelectedNode.Name;
                Boolean isAdmin = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;
                if (!isAdmin)
                {//如果不是管理员
                    #region 判断是否可以新建资料 Added by Tan in 20140729
                    string strDeniedModuleIDs = Yqun.Common.ContextCache.ApplicationContext.Current.DeniedModuleIDs;
                    List<string> lstDeniedModuleIDs;
                    if (!string.IsNullOrEmpty(strDeniedModuleIDs))
                    {
                        lstDeniedModuleIDs = new List<string>(strDeniedModuleIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    else
                    {
                        lstDeniedModuleIDs = new List<string>();
                    }
                    bool isDenied = false;
                    if (lstDeniedModuleIDs.Count > 0)
                    {
                        foreach (string item in lstDeniedModuleIDs)
                        {
                            if (item == ModuleID)
                            {
                                isDenied = true;
                                break;
                            }
                        }
                    }
                    if (isDenied == true)
                    {
                        MessageBox.Show("对不起，您没有在此模板下新建资料的权限！");
                        return;
                    }
                    #endregion
                    if (Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code.Length == 16)
                    {
                        //如果不是领导
                        if (Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code != NodeCode.Substring(0, 16))
                        {
                            MessageBox.Show("请在自己试验室下新建资料");
                            return;
                        }
                    }
                }
                String DataID = Guid.Empty.ToString();
                Viewer.DataViewControl.ModuleID = new Guid(ModuleID);
                Viewer.DataViewControl.TestRoomCode = NodeCode.Substring(0, 16);
                Viewer.DataViewControl.OpenData(DataID, false, true, ModuleID);
            }
        }

        /// <summary>
        /// 批量打印
        /// </summary>
        private void BatchPrint()
        {
            BatchPrintClass batchPrintClass = new BatchPrintClass();
            batchPrintClass.Run(ActiveModule, ActiveNodeCode, null);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        private void ExportData()
        {
            Yqun.Bases.Selection sel = MainListTree.SelectedNode.Tag as Yqun.Bases.Selection;
            if (sel != null)
            {
                DataExportClass dataExportClass = new DataExportClass();
                dataExportClass.Run(new Guid(sel.ID), MainListTree.SelectedNode.Name.Substring(0, 16),
                    MainListTree.SelectedNode.Text);
            }
        }

        /// <summary>
        /// 模板台账设置
        /// </summary>
        internal void ModeSetUp()
        {
            if (MainListTree.SelectedNode != MainListTree.TopNode)
            {
                Selection Selection = MainListTree.SelectedNode.Tag as Selection;
                if (Selection != null && Selection.ID is String)
                {
                    Guid ModelIndex = new Guid(Selection.ID);
                    String testRoomCode = MainListTree.SelectedNode.Name.Substring(0, 16);

                    ModelUserFieldSettingDialog ModelUserFieldSettingDialog = new ModelUserFieldSettingDialog(ModelIndex, testRoomCode);
                    ModelUserFieldSettingDialog.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 新建工程
        /// </summary>
        internal void NewProject()
        {
            TreeNode ParentNode = MainListTree.SelectedNode;
            FormCreatProJect ProJectForm = new FormCreatProJect();
            ProJectForm.ProjectInfo = new Project();

            if (DialogResult.OK == ProJectForm.ShowDialog())
            {
                ProJectForm.ProjectInfo.Code = DepositoryProjectCatlog.GetNextCode(ParentNode.Name);
                Boolean Result = DepositoryProjectInfo.New(ProJectForm.ProjectInfo);
                if (Result)
                {
                    TreeNode Node = new TreeNode();
                    Selection Selection = new Selection();

                    Node.Text = ProJectForm.ProjectInfo.Description;
                    Node.Name = ProJectForm.ProjectInfo.Code;

                    Selection.ID = ProJectForm.ProjectInfo.Index;
                    Selection.Tag = "@eng";
                    Node.Tag = Selection;
                    Node.ImageIndex = 1;
                    Node.SelectedImageIndex = 1;
                    ParentNode.Nodes.Add(Node);
                    ParentNode.ExpandAll();
                }
            }
        }

        /// <summary>
        /// 新建单位
        /// </summary>
        internal void NewCompany()
        {
            TreeNode ParentNode = MainListTree.SelectedNode;
            FormSelectDep SelectDepForm = new FormSelectDep();

            if (DialogResult.OK == SelectDepForm.ShowDialog())
            {
                SelectDepForm.UnitInfo.DepCode = DepositoryProjectCatlog.GetNextCode(ParentNode.Name);
                if (DepositoryOrganInfo.ToUser(SelectDepForm.UnitInfo.Index, ParentNode.Name))
                {
                    MessageBox.Show("您选中的单位已在本标段下存在，请重新选择单位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Boolean Result = DepositoryOrganInfo.Select(SelectDepForm.UnitInfo);
                    if (Result)
                    {
                        TreeNode Node = new TreeNode();
                        Selection Selection = new Selection();
                        Selection.ID = SelectDepForm.UnitInfo.Index;
                        Selection.Tag = SelectDepForm.UnitInfo.DepType;
                        Node.Tag = Selection;
                        Node.Text = SelectDepForm.UnitInfo.DepName;
                        Node.Name = SelectDepForm.UnitInfo.DepCode;

                        Node.ImageIndex = 4;
                        Node.SelectedImageIndex = 4;
                        ParentNode.Nodes.Add(Node);
                        ParentNode.Expand();
                    }
                }
            }
        }

        /// <summary>
        /// 新建标段
        /// </summary>
        internal void NewPrjsct()
        {
            TreeNode ParentNode = MainListTree.SelectedNode;
            FormCreatPrjsc PrjscForm = new FormCreatPrjsc();
            PrjscForm.PrjsctInfo = new Prjsct();

            if (DialogResult.OK == PrjscForm.ShowDialog())
            {
                PrjscForm.PrjsctInfo.PrjsctCode = DepositoryProjectCatlog.GetNextCode(ParentNode.Name);
                if (PrjscForm.PrjsctInfo.PrjsctName.Trim() != string.Empty)
                {
                    Boolean Result = DepositoryPrjsctInfo.New(PrjscForm.PrjsctInfo);
                    if (Result)
                    {
                        TreeNode Node = new TreeNode();
                        Selection Selection = new Selection();

                        Node.Text = PrjscForm.PrjsctInfo.PrjsctName;
                        Node.Name = PrjscForm.PrjsctInfo.PrjsctCode;

                        Selection.ID = PrjscForm.PrjsctInfo.Index;
                        Selection.Tag = "@tenders";
                        Node.Tag = Selection;

                        Node.ImageIndex = 3;
                        Node.SelectedImageIndex = 3;
                        ParentNode.Nodes.Add(Node);
                        ParentNode.Expand();
                    }
                }
                else
                {
                    MessageBox.Show("标段信息不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        internal void NewFolder()
        {
            TreeNode ParentNode = MainListTree.SelectedNode;
            FormFolder FolderForm = new FormFolder();
            FolderForm.FolderInfo = new PrjFolder();

            if (DialogResult.OK == FolderForm.ShowDialog())
            {
                FolderForm.FolderInfo.FolderCode = DepositoryProjectCatlog.GetNextCode(ParentNode.Name);
                if (FolderForm.FolderInfo.FolderName.Trim() != string.Empty)
                {
                    Boolean Result = DepositoryFolderInfo.New(FolderForm.FolderInfo);
                    if (Result)
                    {
                        TreeNode Node = new TreeNode();
                        Selection sele = new Selection();

                        Node.Text = FolderForm.FolderInfo.FolderName;
                        Node.Name = FolderForm.FolderInfo.FolderCode;

                        sele.ID = FolderForm.FolderInfo.Index;
                        sele.Code = FolderForm.FolderInfo.FolderCode;
                        sele.Tag = "@folder";
                        Node.Tag = sele;

                        Node.ImageIndex = 5;
                        Node.SelectedImageIndex = 5;
                        ParentNode.Nodes.Add(Node);
                        ParentNode.Expand();
                    }
                }
            }

        }

        /// <summary>
        /// 编辑工程
        /// </summary>
        internal void EditProject()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection Selection = Node.Tag as Selection;

            FormCreatProJect EditProJectForm = new FormCreatProJect();
            EditProJectForm.Text = "编辑工程";
            EditProJectForm.ProjectInfo = new Project();
            EditProJectForm.ProjectInfo = DepositoryProjectInfo.Query(Node.Name);
            if (DialogResult.OK == EditProJectForm.ShowDialog())
            {
                Boolean Result = DepositoryProjectInfo.Update(EditProJectForm.ProjectInfo);
                if (Result)
                {
                    Node.Text = EditProJectForm.ProjectInfo.Description;
                    BizWindow.TraversingTreeNode(MainListTree.Nodes, Selection, Node.Text);
                }
            }
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        internal void EditCompany()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection Selection = Node.Tag as Selection;

            FormCreatDep EditDepartmentForm = new FormCreatDep();
            EditDepartmentForm.Text = "编辑单位";
            EditDepartmentForm.UnitInfo = DepositoryOrganInfo.Query(Selection.ID);

            if (DialogResult.OK == EditDepartmentForm.ShowDialog())
            {
                Boolean Result = DepositoryOrganInfo.Update(EditDepartmentForm.UnitInfo);
                if (Result)
                {
                    Node.Text = EditDepartmentForm.UnitInfo.DepName;
                    BizWindow.TraversingTreeNode(MainListTree.Nodes, Selection, Node.Text);
                }
            }
        }

        /// <summary>
        /// 编辑标段
        /// </summary>
        internal void EditPrjsct()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection Selection = Node.Tag as Selection;

            FormCreatPrjsc EditPrjscForm = new FormCreatPrjsc();
            EditPrjscForm.Text = "编辑标段";
            EditPrjscForm.PrjsctInfo = DepositoryPrjsctInfo.Query(Node.Name);
            if (DialogResult.OK == EditPrjscForm.ShowDialog())
            {
                Boolean Result = DepositoryPrjsctInfo.Update(EditPrjscForm.PrjsctInfo);
                if (Result)
                {
                    Node.Text = EditPrjscForm.PrjsctInfo.PrjsctName;
                    BizWindow.TraversingTreeNode(MainListTree.Nodes, Selection, Node.Text);
                }
            }
        }

        /// <summary>
        /// 编辑文件夹
        /// </summary>
        internal void EditFolder()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection sele = Node.Tag as Selection;
            FormFolder EditFolder = new FormFolder();

            EditFolder.Text = "编辑文件夹";
            EditFolder.FolderInfo = DepositoryFolderInfo.Query(Node.Name);
            //EditFolder.FolderInfo.FolderName = Node.Text;
            //EditFolder.FolderInfo.FolderCode = Node.Name;
            //EditFolder.FolderInfo.Index = sele.ID;

            if (DialogResult.OK == EditFolder.ShowDialog())
            {
                Boolean Result = DepositoryFolderInfo.Update(EditFolder.FolderInfo);
                if (Result)
                {
                    Node.Text = EditFolder.FolderInfo.FolderName;
                    //Node.Parent.Collapse();
                }
            }
        }

        internal void DeleteProject()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection Selection = Node.Tag as Selection;

            Boolean IsNodes = (Node.Nodes.Count > 0);
            if (!IsNodes)
            {
                if (MessageBox.Show("你确定要删除工程吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Boolean Result = DepositoryProjectInfo.Delete(Node.Name.ToString(), Selection.ID);
                    if (Result)
                    {
                        MainListTree.SelectedNode = Node.Parent;
                        MainListTree.Nodes.Remove(Node);
                        Viewer.DataViewControl.RefreshView(Node.Name);

                        MessageBox.Show("删除工程成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("工程正在使用，不能删除该工程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal void DeleteCompany()
        {
            if (MessageBox.Show("你确定要删除单位吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                TreeNode Node = MainListTree.SelectedNode;
                Selection Selection = Node.Tag as Selection;

                Boolean IsNodes = (Node.Nodes.Count > 0);
                if (IsNodes)
                {
                    MessageBox.Show("单位正在使用，不能删除该单位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Boolean Result = DepositoryOrganInfo.Delete(Node.Name.ToString());
                    if (Result)
                    {
                        MainListTree.SelectedNode = Node.Parent;
                        MainListTree.Nodes.Remove(Node);
                        Viewer.DataViewControl.RefreshView(Node.Name);

                        MessageBox.Show("删除单位成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        internal void DeletePrjsct()
        {
            if (MessageBox.Show("你确定要删除该标段吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                TreeNode Node = MainListTree.SelectedNode;
                Selection Selection = Node.Tag as Selection;

                Boolean IsNodes = (Node.Nodes.Count > 0);
                if (!IsNodes)
                {
                    Boolean Result = DepositoryPrjsctInfo.Delete(Node.Name.ToString(), Selection.ID);
                    if (Result)
                    {
                        MainListTree.SelectedNode = Node.Parent;
                        MainListTree.Nodes.Remove(Node);
                        Viewer.DataViewControl.RefreshView(Node.Name);

                        MessageBox.Show("删除标段成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("标段正在使用，不能删除该标段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        internal void DeleteFolder()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection Selection = Node.Tag as Selection;

            if (MessageBox.Show("你确定要删除试验室‘" + Node.Text + "’吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Boolean Result = DepositoryFolderInfo.Delete(Selection.Code, Selection.ID);
                if (Result)
                {
                    MainListTree.SelectedNode = Node.Parent;
                    MainListTree.Nodes.Remove(Node);
                    Viewer.DataViewControl.RefreshView(Node.Name);

                    MessageBox.Show("删除试验室‘" + Node.Text + "’成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        internal void DeleteModel()
        {
            if (MessageBox.Show("你确定要删除该模板吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                TreeNode Node = MainListTree.SelectedNode;
                Selection Selection = Node.Tag as Selection;
                if (ProjModule.HasDataByModuleIDAndTestRoomCode(Selection.ID, Node.Name.Substring(0, 16)))
                {
                    MessageBox.Show("试验‘" + Node.Text + "’已包含资料，删除操作被系统取消。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Boolean Result = ProjModule.RemoveModuleFromTestRoom(Selection.ID, Node.Name);
                if (Result)
                {
                    MainListTree.SelectedNode = Node.Parent;
                    MainListTree.Nodes.Remove(Node);
                    Viewer.DataViewControl.RefreshView(Node.Name);

                    MessageBox.Show("删除模板‘" + Node.Text + "’成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 重命名弹出菜单
        /// </summary>
        private void ModifyName()
        {
            TreeNode Node = MainListTree.SelectedNode;
            Boolean IsSheet = (Node.Tag is SheetConfiguration);
            if (Node != Node.TreeView.TopNode)
            {
                Node.TreeView.LabelEdit = true;
                Node.BeginEdit();
            }
        }

        private void ProjectCatlogContent_Load(object sender, EventArgs e)
        {
            DepositoryProjectCatlog.InitProjectCatlog(MainListTree);

            MainListTree.AllowDrop = true;
            MainListTree.DragEnter += new DragEventHandler(MainListTree_DragEnter);
            MainListTree.DragLeave += new EventHandler(MainListTree_DragLeave);
            MainListTree.DragOver += new DragEventHandler(MainListTree_DragOver);
            MainListTree.DragDrop += new DragEventHandler(MainListTree_DragDrop);
            MainListTree.MouseClick += new MouseEventHandler(MainListTree_MouseClick);
            ProjectContextMenu.Opening += new CancelEventHandler(ProjectContextMenu_Opening);
            ProjectContextMenu.Click += new EventHandler(ProjectContextMenu_Click);
        }

        #region 树节点拖动事件

        TreeNode tempDropNode = null;
        void MainListTree_DragEnter(object sender, DragEventArgs e)
        {
            Point point = MainListTree.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragEnter(MainListTree.Handle, point.X, point.Y);

            Cursor.Current = Cursors.Default;
            e.Effect = DragDropEffects.Move;
            timer.Enabled = true;
        }

        void MainListTree_DragLeave(object sender, EventArgs e)
        {
            DragHelper.ImageList_DragLeave(MainListTree.Handle);

            timer.Enabled = false;
        }

        void MainListTree_DragOver(object sender, DragEventArgs e)
        {
            if (!MainListTree.Focused) MainListTree.Focus();

            Point point = MainListTree.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(point.X, point.Y);

            e.Effect = DragDropEffects.Move;

            Point pt = MainListTree.PointToClient(new Point(e.X, e.Y));
            TreeNode dropNode = MainListTree.HitTest(pt).Node;
            if (dropNode != null && dropNode != tempDropNode)
            {
                DragHelper.ImageList_DragShowNolock(false);
                MainListTree.SelectedNode = dropNode;
                DragHelper.ImageList_DragShowNolock(true);
                tempDropNode = dropNode;
            }
        }

        void MainListTree_DragDrop(object sender, DragEventArgs e)
        {
            DragHelper.ImageList_DragLeave(MainListTree.Handle);

            Point pt = MainListTree.PointToClient(new Point(e.X, e.Y));
            TreeNode ParentNode = MainListTree.HitTest(pt).Node;
            if (ParentNode != null)
            {
                Selection Selection = ParentNode.Tag as Selection;
                if (Selection.Tag.ToString() == "@module")
                {
                    ParentNode = ParentNode.Parent;
                    Selection = ParentNode.Tag as Selection;
                }
                if (Selection.Tag.ToString() == "@folder")
                {
                    String moduleID = ResourceContent.modelControl.dragNode.Name;
                    String description = ResourceContent.modelControl.dragNode.Text;
                    string moduleCode = DepositoryProjectCatlog.GetNextCode(ParentNode.Name);

                    if (ProjModule.HaveModuleInfo(ParentNode.Name, moduleID))
                    {
                        MessageBox.Show("模板已经存在！", "提示", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (ParentNode.Tag != null)
                        {
                            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||
                                moduleCode.StartsWith(Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code))
                            {
                                if (ProjModule.SaveTemlateResult(moduleID, moduleCode))
                                {
                                    TreeNode Node = new TreeNode();
                                    Node.Text = description;
                                    Node.Name = moduleCode;

                                    Selection = new Selection();
                                    Selection.ID = moduleID;
                                    Selection.Tag = "@module";
                                    Node.Tag = Selection;
                                    Node.ImageIndex = 6;
                                    Node.SelectedImageIndex = 6;
                                    ParentNode.Nodes.Add(Node);
                                    ParentNode.Expand();

                                    MessageBox.Show("模板保存成功！", "提示", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    MessageBox.Show("模板保存失败！", "提示", MessageBoxButtons.OK);
                                }
                            }
                            else
                            {
                                MessageBox.Show("只能将试验模板拖放到本试验室的下面！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }

            ResourceContent.modelControl.dragNode = null;
            timer.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Point pt = MainListTree.PointToClient(Control.MousePosition);
            TreeNode node = MainListTree.HitTest(pt).Node;

            if (node == null) return;

            if (pt.Y < 30)
            {
                if (node.PrevVisibleNode != null)
                {
                    node = node.PrevVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    MainListTree.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);

                }
            }
            else if (pt.Y > MainListTree.Size.Height - 30)
            {
                if (node.NextVisibleNode != null)
                {
                    node = node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    MainListTree.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        #endregion 树节点拖动事件

        void MainListTree_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = MainListTree.PointToClient(Cursor.Position);
                Selection Selection = MainListTree.SelectedNode.Tag as Selection;
                string NodeTag = Selection.Tag.ToString();
                ProjectContextMenu.Show(MainListTree, pt);
            }
        }

        private void ProjectContextMenu_Opening(object sender, CancelEventArgs e)
        {
            TreeNode Node = MainListTree.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            string NodeTag = Selection.Tag.ToString();

            Selection ParentSelection = null;
            if (Node.Parent != null && Node.Parent.Parent != null)
                ParentSelection = Node.Parent.Parent.Tag as Selection;

            NewDataMenuItem.Visible = (NodeTag.ToLower() == "@module");
            TaizhangSettingMenuItem.Visible = (NodeTag.ToLower() == "@module");

            ExportDataMenuItem.Visible = (NodeTag.ToLower() == "@module");
            BatchPrintMenuItem.Visible = (NodeTag.ToLower() == "@module");
            toolStripMenuItem1.Visible = (NodeTag.ToLower() == "@module");

            NewProjectMenuItem.Visible = (NodeTag.ToLower() == "@top");
            NewCompanyMenuItem.Visible = (NodeTag.ToLower() == "@tenders");
            NewSectionMenuItem.Visible = (NodeTag.ToLower() == "@eng");
            NewFolderMenuItem.Visible = (NodeTag.ToLower().Contains("@unit"));
            SystemNewMenuItem.Visible = (NodeTag.ToLower() != "@folder" && NodeTag.ToLower() != "@module");

            EditProjectMenuItem.Visible = (NodeTag.ToLower() == "@eng");
            EditDepMenuItem.Visible = (NodeTag.ToLower().Contains("@unit"));
            EditPrjsctMenuItem.Visible = (NodeTag.ToLower() == "@tenders");
            EditFolderMenuItem.Visible = (NodeTag.ToLower() == "@folder");
            EditMenuItem.Visible = (NodeTag.ToLower() != "@top" && NodeTag.ToLower() != "@module");

            DeleteProjectMenuItem.Visible = (NodeTag.ToLower() == "@eng");
            DeleteCompanyMenuItem.Visible = (NodeTag.ToLower().Contains("@unit"));
            DeleteSectionMenuItem.Visible = (NodeTag.ToLower() == "@tenders");
            DeleteFolderMenuItem.Visible = (NodeTag.ToLower() == "@folder");
            DeleteModelMenuItem.Visible = (NodeTag.ToLower() == "@module");
            SystemDeleteMenuItem.Visible = (NodeTag.ToLower() != "@top");
            QuaAuthMenuItem.Visible = (NodeTag.ToLower() == "@folder");
            DeviceMenuItem.Visible = (NodeTag.ToLower() == "@folder");
            TestTemperatureMenuItem.Visible = (NodeTag.ToLower() == "@folder");

            #region 平行关系对应查询显示设置
            //PXRelationReportMenuItem.Visible=(NodeTag.ToLower() == "@module");
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                PXRelationReportMenuItem.Visible = (NodeTag.ToLower() == "@module") && true && (ParentSelection.Tag.ToString().ToLower() == "@unit_监理单位");
            }
            else
            {
                PXRelationReportMenuItem.Visible = (NodeTag.ToLower() == "@module") && (ParentSelection.Tag.ToString().ToLower() == "@unit_监理单位") && BizWindow.AuthDictionary.ContainsKey(PXRelationReportMenuItem.Tag.ToString());
            }
            #endregion

            toolStripSeparator1.Visible = (NodeTag.ToLower() == "@folder");

            if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                bool visible = (NodeTag.ToLower() == "@top" && BizWindow.AuthDictionary.ContainsKey(NewProjectMenuItem.Tag.ToString()));
                bool parentvisible = visible;
                NewProjectMenuItem.Visible = visible;
                toolStripMenuItem1.Visible = visible;

                visible = (NodeTag.ToLower() == "@tenders" && BizWindow.AuthDictionary.ContainsKey(NewCompanyMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                NewCompanyMenuItem.Visible = visible;

                visible = (NodeTag.ToLower() == "@eng" && BizWindow.AuthDictionary.ContainsKey(NewSectionMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                NewSectionMenuItem.Visible = visible;

                visible = (NodeTag.ToLower().Contains("@unit") && BizWindow.AuthDictionary.ContainsKey(NewFolderMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                NewFolderMenuItem.Visible = visible;

                SystemNewMenuItem.Visible = (NodeTag.ToLower() != "@folder" && NodeTag.ToLower() != "@module" && parentvisible);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                visible = (NodeTag.ToLower() == "@eng" && BizWindow.AuthDictionary.ContainsKey(EditProjectMenuItem.Tag.ToString()));
                parentvisible = visible;
                EditProjectMenuItem.Visible = visible;

                visible = (NodeTag.ToLower().Contains("@unit") && BizWindow.AuthDictionary.ContainsKey(EditDepMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                EditDepMenuItem.Visible = visible;

                visible = (NodeTag.ToLower() == "@tenders" && BizWindow.AuthDictionary.ContainsKey(EditPrjsctMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                EditPrjsctMenuItem.Visible = visible;

                visible = (NodeTag.ToLower() == "@folder" && BizWindow.AuthDictionary.ContainsKey(EditFolderMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                EditFolderMenuItem.Visible = visible;

                EditMenuItem.Visible = (NodeTag.ToLower() != "@top" && NodeTag.ToLower() != "@module" && parentvisible);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                visible = (NodeTag.ToLower() == "@eng" && BizWindow.AuthDictionary.ContainsKey(DeleteProjectMenuItem.Tag.ToString()));
                parentvisible = visible;
                DeleteProjectMenuItem.Visible = visible;

                visible = (NodeTag.ToLower().Contains("@unit") && BizWindow.AuthDictionary.ContainsKey(DeleteCompanyMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                DeleteCompanyMenuItem.Visible = visible;

                visible = (NodeTag.ToLower() == "@tenders" && BizWindow.AuthDictionary.ContainsKey(DeleteSectionMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                DeleteSectionMenuItem.Visible = visible;

                visible = (NodeTag.ToLower() == "@folder" && BizWindow.AuthDictionary.ContainsKey(DeleteFolderMenuItem.Tag.ToString()));
                parentvisible = parentvisible || visible;
                DeleteFolderMenuItem.Visible = visible;

                visible = (NodeTag.ToLower() == "@module" && BizWindow.AuthDictionary.ContainsKey(DeleteModelMenuItem.Tag.ToString()));
                visible = visible && (ParentSelection.Tag.ToString().ToLower() == Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type.ToLower());
                parentvisible = parentvisible || visible;
                DeleteModelMenuItem.Visible = visible;

                SystemDeleteMenuItem.Visible = (NodeTag.ToLower() != "@top" && parentvisible);

                QuaAuthMenuItem.Visible = QuaAuthMenuItem.Visible && BizWindow.AuthDictionary.ContainsKey(QuaAuthMenuItem.Tag.ToString());
                TestTemperatureMenuItem.Visible = TestTemperatureMenuItem.Visible && BizWindow.AuthDictionary.ContainsKey(TestTemperatureMenuItem.Tag.ToString());
                DeviceMenuItem.Visible = DeviceMenuItem.Visible && BizWindow.AuthDictionary.ContainsKey(DeviceMenuItem.Tag.ToString());
                toolStripSeparator1.Visible = QuaAuthMenuItem.Visible || TestTemperatureMenuItem.Visible;


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //done 刘晓明，调整Visible属性，目前是登录用户的单位编码等于工程结构中单位节点的编码
                visible = (NodeTag.ToLower() == "@module" && BizWindow.AuthDictionary.ContainsKey(NewDataMenuItem.Tag.ToString()));
                visible = visible && (MainListTree.SelectedNode.Name.Substring(0, 16) ==
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);
                NewDataMenuItem.Visible = (visible &&
                                          (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||
                                           ParentSelection != null && Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type.ToLower() == ParentSelection.Tag.ToString().ToLower()));

                visible = (NodeTag.ToLower() == "@module" && BizWindow.AuthDictionary.ContainsKey(TaizhangSettingMenuItem.Tag.ToString()));
                visible = visible && (MainListTree.SelectedNode.Name.Substring(0, 16) ==
                    Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);
                TaizhangSettingMenuItem.Visible = (visible &&
                                          (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator || ParentSelection != null));


            }

            LineMenuItemForDevice.Visible = TestTemperatureMenuItem.Visible == true && DeviceMenuItem.Visible == true;
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MainListTree.Update();
        }

        private void MainListTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode Node = e.Node;
            Selection Selection = e.Node.Tag as Selection;
            string MainTreeNode = Selection.Tag.ToString();

            ToolStrip MainTool = (ToolStrip)Cache.CustomCache[SystemString.工具栏];
            bool IsLine = false;

            for (int i = 0; i < MainTool.Items.Count; i++)
            {
                if (MainTool.Items[i].Tag != null)
                {
                    Boolean IsAuth = true;
                    if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                    {
                        try
                        {
                            IsAuth = BizWindow.AuthDictionary[MainTool.Items[i].Tag.ToString()];
                        }
                        catch
                        {
                            IsAuth = false;
                        }
                    }

                    MainTool.Items[i].Visible = IsAuth && (
                                                MainTool.Items[i].Tag.ToString().ToLower().StartsWith(MainTreeNode) ||
                                                MainTool.Items[i].Tag.ToString().ToLower().StartsWith("@layout") ||
                                                MainTool.Items[i].Tag.ToString().ToLower().StartsWith("@projectcatlogcontent") ||
                                                MainTool.Items[i].Tag.ToString().ToLower().StartsWith("@bizviewercontent") ||
                                                MainTool.Items[i].Tag.ToString().ToLower().StartsWith("@modulecatlogcontent"));

                    IsLine = IsLine | MainTool.Items[i].Visible;
                }
                else
                {
                    MainTool.Items[i].Visible = IsLine;
                    IsLine = false;
                }
            }

            ToolStrip MainMenu = (ToolStrip)Cache.CustomCache[SystemString.菜单栏];
            for (int i = 0; i < MainMenu.Items.Count; i++)
            {
                ToolStripMenuItem MenuItem = (ToolStripMenuItem)MainMenu.Items[i];
                if (MenuItem.Tag != null && MenuItem.Tag.ToString().ToLower() == "@prject")
                {
                    MenuItem.Enabled = true;
                    for (int j = 0; j < MenuItem.DropDownItems.Count; j++)
                    {
                        if (MenuItem.DropDownItems[j].Tag != null && MenuItem.DropDownItems[j].Tag.ToString().ToLower() == "@prject")
                        {
                            ToolStripMenuItem SubMenuItem = (ToolStripMenuItem)MenuItem.DropDownItems[j];
                            SubMenuItem.Enabled = true;
                            for (int k = 0; k < SubMenuItem.DropDownItems.Count; k++)
                            {
                                SubMenuItem.DropDownItems[k].Enabled = SubMenuItem.DropDownItems[k].Tag.ToString().ToLower().StartsWith(MainTreeNode.ToLower());
                            }
                        }
                    }
                }
                else if (MenuItem.Tag != null && MenuItem.Tag.ToString().ToLower() == "@data")
                {
                    MenuItem.Enabled = true;
                    for (int j = 0; j < MenuItem.DropDownItems.Count; j++)
                    {
                        if (MenuItem.DropDownItems[j].Tag != null)
                        {
                            MenuItem.DropDownItems[j].Enabled = MenuItem.DropDownItems[j].Tag.ToString().ToLower().StartsWith(MainTreeNode.ToLower());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 由于与NodeMouseClick冲突，暂时不支持双击节点新建资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainListTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //String DepType = Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type;
            //Boolean IsAdmin = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;

            //if (e.Button == MouseButtons.Left)
            //{
            //    Selection Selection = MainListTree.SelectedNode.Tag as Selection;
            //    if (Selection.ID is String && Selection.Tag.ToString() == "@module")
            //    {
            //        if (IsAdmin ||
            //            DepType == "@unit_施工单位" ||
            //            (DepType == "@unit_监理单位" && (Selection.ID == TestRoomBasicInformation.试验人员技术档案 ||
            //                                             Selection.ID == TestRoomBasicInformation.试验室仪器设备汇总表 ||
            //                                             Selection.ID == TestRoomBasicInformation.拌和站基本情况登记表 ||
            //                                             Selection.ID == TestRoomBasicInformation.试验室综合情况登记表)))
            //        {
            //            String ModuleID = Selection.ID;
            //            String NodeCode = MainListTree.SelectedNode.Name;

            //            if (ActiveModule == null || ActiveModule == ModuleConfiguration.Empty || ActiveModule.Index != ModuleID)
            //            {
            //                ActiveModule = CacheModuleConfiguration.InitModuleConfiguration(ModuleID);
            //            }

            //            if (ActiveModuleFields.Count > 0)
            //            {
            //                if (ActiveModuleFields[0].ModuleCode != NodeCode || ActiveModuleFields[0].ModuleIndex != ModuleID)
            //                {
            //                    ActiveModuleFields = DepositoryModuleUserFields.GetModuleFields(ModuleID, NodeCode);
            //                }

            //                String DataID = Guid.NewGuid().ToString();
            //                Viewer.DataViewControl.Module = ActiveModule;
            //                Viewer.DataViewControl.ModelFields = ActiveModuleFields;
            //                Viewer.DataViewControl.ModelCode = NodeCode;
            //                Viewer.DataViewControl.OpenData(DataID, false, true);
            //            }
            //            else
            //            {
            //                MessageBox.Show("请先设置台账，再双击新建资料。");
            //            }
            //        }
            //    }
            //}
        }

        private void MainListTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                JZApplicationCatch.CurrentModule = null;
                e.Node.TreeView.SelectedNode = e.Node;
                Selection Selection = e.Node.Tag as Selection;
                if (Selection != null && Selection.Tag.ToString().StartsWith("@module"))
                {
                    String ModuleID = Selection.ID;
                    String NodeCode = e.Node.Name;

                    ProgressScreen.Current.ShowSplashScreen();
                    AddOwnedForm(ProgressScreen.Current);


                    ProgressScreen.Current.SetStatus = "正在显示资料......";
                    Viewer.DataViewControl.Node = e.Node;
                    Viewer.DataViewControl.ModuleID = new Guid(ModuleID);
                    Viewer.DataViewControl.TestRoomCode = NodeCode.Substring(0, 16);
                    Viewer.DataViewControl.ShowData();

                    RemoveOwnedForm(ProgressScreen.Current);
                    ProgressScreen.Current.CloseSplashScreen();
                }
            }
        }

        private void MenuTemperatureSet_Click(object sender, EventArgs e)
        {

        }
    }
}