using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Client.BizUI;
using BizCommon;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using BizModules;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using System.IO;
using BizComponents.查询统计;
using System.Collections;
using Yqun.Bases;
using BizComponents;
using System.Diagnostics;
using System.Reflection;
using BizModules.Properties;
using FarPoint.Win;
using Yqun.Permissions.Runtime;
using FarPoint.Win.Spread.CellType;
using BizModules.UIWindow;

namespace BizModules
{
    public partial class DataViewControl : UserControl
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ModelDataManager DataManager = new ModelDataManager();
        BizViewerContent BizViewerContent;
        FlowLayoutPanel _Panel_Filter;
        List<String> CopyDataID = new List<String>();
        String Filter;
        List<ModuleField> GroupInfo = new List<ModuleField>();

        ISheetDataModel BlankGroupDataModel;
        ISheetDataModel SaveGroupDataModel;
        DataIDShowForm _DataIDShowForm = null;
        //int[] GroupCount = null;

        List<FieldDefineInfo> Fields = new List<FieldDefineInfo>();

        public DataViewControl(BizViewerContent BizViewerContent)
        {
            InitializeComponent();
            //GroupCount = 0;
            this.BizViewerContent = BizViewerContent;

            FpSpread.SelectionBlockOptions = SelectionBlockOptions.Rows | SelectionBlockOptions.Columns;
            FpSpread.CellDoubleClick += new CellClickEventHandler(FpSpread_CellDoubleClick);
            FpSpread.MouseUp += new MouseEventHandler(FpSpread_MouseUp);
            FpSpread.EditModeOff += new EventHandler(FpSpread_EditModeOff);
            FpSpread.Grouped += new EventHandler(FpSpread_Grouped);
            FpSpread.ColumnDragMoveCompleted += new DragMoveCompletedEventHandler(FpSpread_ColumnDragMoveCompleted);
            FpSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(FpSpread_ColumnWidthChanged);

            FpSpread.EnterCell += new EnterCellEventHandler(FpSpread_EnterCell);
            FpSpread.LeaveCell += new LeaveCellEventHandler(FpSpread_LeaveCell);
            FpSpread.AutoSortingColumn += new AutoSortingColumnEventHandler(FpSpread_AutoSortingColumn);
            FpSpread.AllowColumnMove = true;
            FpSpread.GroupBarText = "拖放列会按照该列进行分组";
            ActiveSheet.AllowGroup = true;
            ActiveSheet.GroupMaximumLevel = 1;

            ActiveSheet.PrintInfo = PrintInfomation.DefaultPrintInfomation.getDefaultPrintInfomation();
            ActiveSheet.OperationMode = OperationMode.Normal;
            ActiveSheet.RowHeader.AutoText = HeaderAutoText.Blank;

            BlankGroupDataModel = ActiveSheet.Models.Data;
            SaveGroupDataModel = ActiveSheet.Models.Data;

            TextBox.LostFocus += new EventHandler(ToolStripTextBox_LostFocus);
            AppendFilterMenuItem.Click += new EventHandler(AppendFilterMenuItem_Click);

            this.ExtentDataItemChanged += new EventHandler<ExtentDataItemChangedEventArgs>(DataViewControl_ExtentDataItemChanged);
        }

        private String _sortField = "CreatedTime";
        public String SortField
        {
            get
            {
                return _sortField;
            }
            set
            {
                _sortField = value;
            }
        }

        private Int32 _sortDirection = 1;
        public Int32 SortDirection
        {
            get
            {
                return _sortDirection;
            }
            set
            {
                _sortDirection = value;
            }
        }

        void FpSpread_AutoSortingColumn(object sender, AutoSortingColumnEventArgs e)
        {
            String columnKey = "";
            if (e.Sheet.Columns[e.Column].Tag is JZCustomView)
            {
                columnKey = (e.Sheet.Columns[e.Column].Tag as JZCustomView).DocColumn;
            }
            if (String.IsNullOrEmpty(columnKey))
            {
                return;
            }
            SortField = columnKey;
            if (e.Ascending)
            {
                SortDirection = 0;
            }
            else
            {
                SortDirection = 1;
            }
            Current = 1;
            BindDataSource();
            RefreshNavigatorBar();
        }

        public FlowLayoutPanel Panel_Filter
        {
            get
            {
                return _Panel_Filter;
            }
            set
            {
                _Panel_Filter = value;
            }
        }

        private void FpSpread_EnterCell(object sender, EnterCellEventArgs e)
        {
            if (FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType is BizComponents.ComboBoxCellType)
            {
                BizComponents.ComboBoxCellType CellType = FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType as BizComponents.ComboBoxCellType;
                CellType.DropDownButton = true;
            }
        }

        private void FpSpread_LeaveCell(object sender, LeaveCellEventArgs e)
        {
            if (FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType is BizComponents.ComboBoxCellType)
            {
                BizComponents.ComboBoxCellType CellType = FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType as BizComponents.ComboBoxCellType;
                CellType.DropDownButton = false;
            }
        }

        void FpSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            Dictionary<JZCustomView, float> fields = new Dictionary<JZCustomView, float>();
            foreach (FarPoint.Win.Spread.ColumnWidthChangeExtents c in e.ColumnList)
            {
                for (int i = c.FirstColumn; i <= c.LastColumn; i++)
                {
                    Column column = FpSpread.ActiveSheet.Columns[i];
                    if (column.Tag != null && column.Tag is JZCustomView)
                    {
                        fields.Add(column.Tag as JZCustomView, column.Width);
                    }
                }
            }
            if (fields.Count > 0)
            {
                DocumentHelperClient.UpdateCustomViewWidth(ModuleID, TestRoomCode, fields);
            }
        }

        /// <summary>
        /// 将修改后的表外字段的值保存到数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataViewControl_ExtentDataItemChanged(object sender, ExtentDataItemChangedEventArgs e)
        {
            Sys_Document doc = new Sys_Document();
            doc.ID = e.DataID;
            if (e.FieldName == "DataName")
            {
                doc.DataName = (e.Value ?? "").ToString().Trim(' ', '\r', '\n');
            }
            else if (e.FieldName == "TryType")
            {
                doc.TryType = (e.Value ?? "").ToString().Trim(' ', '\r', '\n');
            }
            else
            {
                return;
            }
            DocumentHelperClient.UpdateDocumentBaseInfo(doc, e.FieldName);
        }

        void FpSpread_EditModeOff(object sender, EventArgs e)
        {
            int ActiveColumn = FpSpread.ActiveSheet.ActiveColumnIndex;
            if (ActiveColumn >= 0)
            {
                Column Column = FpSpread.ActiveSheet.ColumnHeader.Columns[ActiveColumn];
                JZCustomView cv = Column.Tag as JZCustomView;
                if (cv != null)
                {
                    if (cv.DocColumn == "DataName" || cv.DocColumn == "TryType")
                    {
                        ExtentDataItemChangedEventArgs ex = new ExtentDataItemChangedEventArgs();
                        ex.DataID = new Guid(FpSpread.ActiveSheet.Cells[FpSpread.ActiveSheet.ActiveRowIndex, 0].Value.ToString());
                        ex.FieldName = cv.DocColumn;
                        ex.Value = FpSpread.ActiveSheet.ActiveCell.Value;
                        OnExtentDataItemChanged(ex);
                    }
                }
            }
        }

        #region 事件

        static readonly object ExtentDataItemChangedEvent = new object();
        public event EventHandler<ExtentDataItemChangedEventArgs> ExtentDataItemChanged
        {
            add
            {
                Events.AddHandler(ExtentDataItemChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(ExtentDataItemChangedEvent, value);
            }
        }

        protected virtual void OnExtentDataItemChanged(ExtentDataItemChangedEventArgs e)
        {
            EventHandler<ExtentDataItemChangedEventArgs> handler = (EventHandler<ExtentDataItemChangedEventArgs>)Events[ExtentDataItemChangedEvent];
            if (handler != null)
                handler(FpSpread, e);
        }

        #endregion 事件

        private Guid _moduleID = Guid.Empty;
        internal Guid ModuleID
        {
            get
            {
                return _moduleID;
            }
            set
            {
                _moduleID = value;
            }
        }

        private String _testRoomCode = "";
        internal String TestRoomCode
        {
            get
            {
                return _testRoomCode;
            }
            set
            {
                _testRoomCode = value;
            }
        }

        public TreeNode Node { get; set; }

        FpSpread FpSpread
        {
            get
            {
                return this.myCell1;
            }
        }

        SheetView ActiveSheet
        {
            get
            {
                return this.myCell1_Sheet1;
            }
        }

        void AppendFilterMenuItem_Click(object sender, EventArgs e)
        {
            int ActiveColumn = FpSpread.ActiveSheet.ActiveColumnIndex;
            Column Column = FpSpread.ActiveSheet.ColumnHeader.Columns[ActiveColumn];
            JZCustomView cv = Column.Tag as JZCustomView;
            if (cv != null && cv.DocColumn != "")
            {
                AppendDataFilter(Panel_Filter, cv);
            }
            BizViewerContent.BizWindow.QueryDataContent.Activate();
        }

        private Boolean ContainFilter(Panel Container, JZCustomView cv)
        {
            Boolean Result = false;
            foreach (Control control in Container.Controls)
            {
                if (control is DateTimeQueryControl)
                {
                    DateTimeQueryControl DateTimeQueryControl = control as DateTimeQueryControl;
                    Result = (DateTimeQueryControl.FieldInfo == cv);
                    break;
                }
                else if (control is NumberQueryControl)
                {
                    NumberQueryControl NumberQueryControl = control as NumberQueryControl;
                    Result = (NumberQueryControl.FieldInfo == cv);
                    break;
                }
                else if (control is StringQueryControl)
                {
                    StringQueryControl StringQueryControl = control as StringQueryControl;
                    Result = (StringQueryControl.FieldInfo == cv);
                    break;
                }
            }

            return Result;
        }

        private void AppendDataFilter(Panel Container, JZCustomView cv)
        {
            if (!ContainFilter(Container, cv))
            {
                if (cv.DocColumn == "BGRQ") //日期型搜索条件
                {
                    DateTimeQueryControl control = new DateTimeQueryControl();

                    if (Container.Controls.Count > 0)
                    {
                        control.Left = Container.Controls[Container.Controls.Count - 1].Left + Container.Controls[Container.Controls.Count - 1].Width;
                    }
                    else
                    {
                        control.Left = 2;
                    }

                    control.BackColor = SystemColors.ControlLight;
                    control.FieldInfo = cv;
                    control.DeleteButton.Click += new EventHandler(DeleteButton_Click);
                    Container.Controls.Add(control);
                }
                else if (cv.DocColumn == "ShuLiang")
                {
                    NumberQueryControl control = new NumberQueryControl();

                    if (Container.Controls.Count > 0)
                    {
                        control.Left = Container.Controls[Container.Controls.Count - 1].Left + Container.Controls[Container.Controls.Count - 1].Width;
                    }
                    else
                    {
                        control.Left = 2;
                    }

                    control.BackColor = SystemColors.ControlLight;
                    control.FieldInfo = cv;
                    control.DeleteButton.Click += new EventHandler(DeleteButton_Click);
                    Container.Controls.Add(control);
                }
                else //文本型搜索条件
                {
                    StringQueryControl control = new StringQueryControl();

                    if (Container.Controls.Count > 0)
                    {
                        control.Left = Container.Controls[Container.Controls.Count - 1].Left + Container.Controls[Container.Controls.Count - 1].Width;
                    }
                    else
                    {
                        control.Left = 2;
                    }

                    control.BackColor = SystemColors.ControlLight;
                    control.FieldInfo = cv;
                    control.DeleteButton.Click += new EventHandler(DeleteButton_Click);
                    Container.Controls.Add(control);
                }
            }
        }

        /// <summary>
        /// 筛选资料
        /// </summary>
        internal void QueryData()
        {
            Filter = GetFilters(Panel_Filter);
            Current = 1;
            BindDataSource();
            RefreshNavigatorBar();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Panel_Filter.Controls.Remove(button.Parent);
            Filter = GetFilters(Panel_Filter);
            Current = 1;
            BindDataSource();
            RefreshNavigatorBar();
        }

        private String GetFilters(Panel ConditionContainer)
        {
            List<string> Filters = new List<string>();
            foreach (Control control in ConditionContainer.Controls)
            {
                if (control is DateTimeQueryControl)
                {
                    DateTimeQueryControl querycontrol = control as DateTimeQueryControl;
                    Filters.Add(querycontrol.Filter);
                }
                else if (control is NumberQueryControl)
                {
                    NumberQueryControl querycontrol = control as NumberQueryControl;
                    Filters.Add(querycontrol.Filter);
                }
                else if (control is StringQueryControl)
                {
                    StringQueryControl querycontrol = control as StringQueryControl;
                    Filters.Add(querycontrol.Filter);
                }
                else if (control is CheckQueryControl)
                {
                    CheckQueryControl querycontrol = control as CheckQueryControl;
                    Filters.Add(querycontrol.Filter);
                }
            }

            for (int i = 0; i < Filters.Count; i++)
            {
                if (Filters[i] == string.Empty)
                    Filters.Remove(Filters[i]);
            }

            String result = string.Join(" And ", Filters.ToArray());
            if (!String.IsNullOrEmpty(result))
            {
                result = " and " + result;
            }
            return result;
        }

        internal void BindDataSource()
        {
            ActiveSheet.Models.Data = BlankGroupDataModel;

            Sys_TaiZhang tz = DocumentHelperClient.GetDocumentList(ModuleID, TestRoomCode, SortField, SortDirection, Current, ItemCount, Filter);
            DataCount = tz.TotalCount;
            BindSheetView(FpSpread, tz);
            ActiveSheet.Models.Data = SaveGroupDataModel;

            if (ActiveSheet.Models.Data is GroupDataModel)
            {
                GroupDataModel DataModel = ActiveSheet.Models.Data as GroupDataModel;
                #region 判断是否需要锁定
                SheetView SheetView = FpSpread.ActiveSheet;
                TreeNode CompanyNode = Node.Parent.Parent;
                Selection ParentSelection = CompanyNode.Tag as Selection;
                EditBaseCellType BaseCellType = null;
                Boolean IsLockedTryType = false;
                Boolean IsLocakedDataName = false;

                BizComponents.ComboBoxCellType comboBox = new BizComponents.ComboBoxCellType();
                comboBox.DropDownButton = false;

                BizComponents.TextCellType text = new BizComponents.TextCellType();
                text.ReadOnly = true;

                IsLocakedDataName = (ParentSelection.Tag.ToString().ToLower() != Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type.ToLower()) && !Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;
                if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                {
                    if (Node.Name.Substring(0, 16) != Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code)
                    {
                        IsLocakedDataName = true;
                    }
                }

                if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
                {
                    if (ParentSelection.Tag.ToString().ToLower() == "@unit_施工单位")
                    {
                        IsLockedTryType = false;
                        comboBox.Items = new List<string>(new string[] { "自检", "见证" });
                        BaseCellType = comboBox;
                    }
                    else
                    {
                        BaseCellType = text;
                        IsLockedTryType = true;
                    }
                }
                else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位")
                {
                    BaseCellType = text;
                    IsLockedTryType = true;
                }
                else
                {
                    comboBox.Items = new List<string>(new string[] { "自检", "见证", "抽检", "平行" });
                    BaseCellType = comboBox;
                    IsLockedTryType = false;
                }
                #endregion
                for (int i = 0; i < DataModel.RowCount; i++)
                {
                    if (DataModel.IsGroup(i))
                    {
                        Group g = DataModel.GetGroup(i);
                        String Text = ActiveSheet.Models.Data.GetValue(i, g.Column).ToString();
                        g.Text = ActiveSheet.Columns[g.Column].Label + Text.Substring(Text.IndexOf(":"));
                    }
                    else
                    {
                        for (int j = 0; j < SheetView.ColumnCount; j++)
                        {
                            String columnKey = "";
                            if (SheetView.Columns[j].Tag is JZCustomView)
                            {
                                columnKey = (SheetView.Columns[j].Tag as JZCustomView).DocColumn;
                                if (columnKey == "")
                                {
                                    columnKey = (SheetView.Columns[j].Tag as JZCustomView).Description;
                                }
                            }
                            else
                            {
                                columnKey = SheetView.Columns[j].Tag.ToString();
                            }
                            Cell cell = SheetView.Cells[i, j];

                            if (columnKey == "DataName")
                            {
                                cell.Locked = IsLocakedDataName;
                            }
                            else if (columnKey == "TryType")
                            {
                                cell.CellType = BaseCellType.Clone() as EditBaseCellType;
                                cell.Locked = IsLockedTryType;
                            }
                            else
                            {
                                cell.CellType = text;
                                cell.Locked = true;
                            }
                        }
                    }
                }
            }
        }

        private List<ModuleField> GetGroupInfo(SheetView ActiveSheet, GroupDataModel DataModel)
        {
            List<ModuleField> FieldList = new List<ModuleField>();
            SortInfo[] sortInfos = DataModel.SortInfo;
            foreach (SortInfo sortInfo in sortInfos)
            {
                ModuleField field = ActiveSheet.ColumnHeader.Columns[sortInfo.Index].Tag as ModuleField;
                if (field != null)
                {
                    FieldList.Add(field);
                }
            }

            return FieldList;
        }

        void FpSpread_Grouped(object sender, EventArgs e)
        {
            SaveGroupDataModel = ActiveSheet.Models.Data;
            GroupDataModel DataModel = ActiveSheet.Models.Data as GroupDataModel;
            //GroupCount = new int[DataModel.Groups.Count];
            //for (int i = 0; i < DataModel.Groups.Count; i++)
            //{
            //    GroupCount[i] = ((FarPoint.Win.Spread.Model.Group)(DataModel.Groups[i])).Rows.Count;
            //}
            GroupInfo = GetGroupInfo(ActiveSheet, DataModel);
            BindDataSource();
        }

        void FpSpread_ColumnDragMoveCompleted(object sender, DragMoveCompletedEventArgs e)
        {
            ActiveSheet.MoveColumn(e.ToIndex, e.FromIndex, true);
        }

        void FpSpread_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Rectangle rect = FpSpread.GetColumnHeaderRectangle(FpSpread.GetActiveColumnViewportIndex());
                HitTestInformation Info = FpSpread.HitTest(e.X, e.Y);
                if (rect.Contains(e.X, e.Y))
                {
                    if (FpSpread.ActiveSheet.Rows.Count > 0)
                    {
                        FpSpread.ActiveSheet.ClearSelection();
                        FpSpread.ActiveSheet.AddSelection(0, Info.HeaderInfo.Column, FpSpread.ActiveSheet.Rows.Count, 1);
                    }
                    FpSpread.ActiveSheet.ActiveColumnIndex = Info.HeaderInfo.Column;
                    contextMenuStrip2.Show(FpSpread, new Point(e.X, e.Y));
                }
                else
                {
                    TreeNode Node = BizViewerContent.BizWindow.ProjectCatlogContent.MainListTree.SelectedNode;
                    Selection Selection = Node.Tag as Selection;
                    if (Selection.Tag.ToString().ToLower() == "@module")
                    {
                        if (Info.Type == HitTestType.RowHeader && Info.HeaderInfo.Row >= 0)
                        {
                            FpSpread.ActiveSheet.ActiveRowIndex = Info.HeaderInfo.Row;
                        }
                        else if (Info.Type == HitTestType.Viewport && Info.ViewportInfo.Row >= 0)
                        {
                            FpSpread.ActiveSheet.ActiveRowIndex = Info.ViewportInfo.Row;
                        }
                        contextMenuStrip1.Show(FpSpread, new Point(e.X, e.Y));
                    }
                }
            }
        }

        void FpSpread_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Boolean isGroupRow = false;
            GroupDataModel DataModel = SaveGroupDataModel as GroupDataModel;
            if (DataModel != null)
                isGroupRow = DataModel.IsGroup(e.Row);

            if (!e.ColumnHeader && !isGroupRow)
            {
                FpSpread fpSpread = sender as FpSpread;
                object obj = fpSpread.ActiveSheet.Cells[fpSpread.ActiveSheet.ActiveRowIndex, 0].Value;
                HitTestInformation htf = fpSpread.HitTest(e.X, e.Y);
                if ((htf.Type == HitTestType.RowHeader && obj != null && obj is Guid) ||
                    (htf.Type == HitTestType.Viewport && obj != null && obj is Guid &&
                    fpSpread.ActiveSheet.Cells[e.Row, e.Column].Locked))
                {
                    String DataID = obj.ToString();
                    Selection Selection = Node.Tag as Selection;
                    TreeNode CompanyNode = Node.Parent.Parent;
                    Selection ParentSelection = CompanyNode.Tag as Selection;

                    //done 刘晓明，调整ReadOnly的值，目前是登录用户的单位编码等于工程结构中单位节点的编码
                    Boolean ReadOnly = (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type.ToLower() != ParentSelection.Tag.ToString().ToLower()) && !Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;
                    if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                    {
                        if (Node.Name.Substring(0, 16) != Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code)
                        {
                            ReadOnly = true;
                        }
                    }
                    OpenData(DataID, ReadOnly, Selection.ID);
                }
            }
        }

        public void OpenData(String DataID, Boolean ReadOnly, String moduleID)
        {
            OpenData(DataID, ReadOnly, false, moduleID);
        }

        public void OpenData(String DataID, Boolean ReadOnly, Boolean IsNewData, String moduleID)
        {
            //Selection Selection = Node.Tag as Selection;
            TreeNode CompanyNode = Node.Parent.Parent;
            Selection ParentSelection = CompanyNode.Tag as Selection;
            ModuleViewer Viewer = new ModuleViewer(new Guid(DataID), new Guid(moduleID), Node.Name.Substring(0, 16));
            Viewer.Text = string.Concat("资料编辑器[", Node.Text, "]");


            if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位" && ParentSelection.Tag.ToString().ToLower() == "@unit_监理单位")
                Viewer.TryType = "抽检";
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位" && ParentSelection.Tag.ToString().ToLower() == "@unit_施工单位")
                Viewer.TryType = "自检";
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator && ParentSelection.Tag.ToString().ToLower() == "@unit_监理单位")
                Viewer.TryType = "抽检";
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator && ParentSelection.Tag.ToString().ToLower() == "@unit_施工单位")
                Viewer.TryType = "自检";

            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Viewer.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Viewer.Size = Owner.ClientRectangle.Size;
            Viewer.ReadOnly = ReadOnly;
            Viewer.isNewData = IsNewData;
            Viewer.viewControl = this;
            //Viewer.FormClosed += new FormClosedEventHandler(Viewer_FormClosed);

            Viewer.ShowDialog(Owner);
        }

        void Viewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            BindDataSource();

        }

        public void SyncData(String tryType, JZDocument doc, Boolean isNewData)
        {
            SheetView view = FpSpread.ActiveSheet;
            int rowIndex = -1;
            if (isNewData)
            {
                if (Current == 1)
                {
                    view.Rows.Add(0, 1);
                }
            }
            else
            {
                for (int i = 0; i < view.Rows.Count; i++)
                {
                    object obj = view.Cells[i, 0].Value;
                    if (doc.ID == new Guid(obj.ToString()))
                    {
                        rowIndex = i;
                        break;
                    }
                }
            }
            if (rowIndex >= 0)
            {
                for (int j = 0; j < view.ColumnCount; j++)
                {
                    if (view.Columns[j].Tag is String)
                    {
                        if (view.Columns[j].Tag.ToString() == "ID")
                        {
                            view.Cells[rowIndex, j].Value = doc.ID;
                        }
                    }
                    else if (view.Columns[j].Tag is JZCustomView)
                    {
                        JZCustomView cv = view.Columns[j].Tag as JZCustomView;
                        if (cv.DocColumn == "TryType" && isNewData == true)
                        {
                            view.Cells[rowIndex, j].Value = tryType;
                        }
                        else if (cv.DocColumn.ToUpper() == "BGRQ")
                        {
                            DateTime dtBGRQ = new DateTime();
                            object oBGRQ = JZCommonHelper.GetCellValue(doc, cv.SheetID, cv.CellName);
                            bool bBGRQ = false;

                            if (oBGRQ is decimal || oBGRQ is float || oBGRQ is double)
                            {
                                //oBGRQ = int.Parse(item.Value.ToString());
                                double dRQ = double.Parse(oBGRQ.ToString());
                                try
                                {
                                    dtBGRQ = DateTime.FromOADate(dRQ);
                                    bBGRQ = true;

                                }
                                catch (Exception)
                                {
                                    bBGRQ = false;
                                }
                            }
                            else
                            {
                                bBGRQ = DateTime.TryParse(oBGRQ == null ? "" : oBGRQ.ToString(), out dtBGRQ);
                            }
                            if (bBGRQ)
                            {
                                oBGRQ = dtBGRQ.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                logger.Error("SyncData 报告日期转换错误,原值:" + oBGRQ);
                                oBGRQ = DBNull.Value;
                            }
                            view.Cells[rowIndex, j].Value = oBGRQ;

                            //if (bBGRQ)
                            //{
                            //    view.Cells[rowIndex, j].Value = dtBGRQ.ToString("yyyy-MM-dd");
                            //}
                            //else
                            //{
                            //    view.Cells[rowIndex, j].Value = oBGRQ;
                            //}
                        }
                        else if (cv.SheetID != null && cv.SheetID != Guid.Empty && cv.CellName != "" && cv.CellName != null)
                        {
                            view.Cells[rowIndex, j].Value = JZCommonHelper.GetCellValue(doc, cv.SheetID, cv.CellName);
                        }
                    }
                }
                view.SetActiveCell(rowIndex, 0);
            }
            else
            {
                BindDataSource();
            }
        }

        public void RefreshView(String NodeCode)
        {
            if (TestRoomCode.StartsWith(NodeCode))
            {
                ActiveSheet.Rows.Count = 0;
                ActiveSheet.Columns.Count = 0;
            }
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Selection Selection = Node.Tag as Selection;
            TreeNode CompanyNode = Node.Parent.Parent;
            Selection ParentSelection = CompanyNode.Tag as Selection;

            Boolean NewPingxingVisible = (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位" &&
                                 ParentSelection.Tag.ToString().ToLower() == "@unit_施工单位" && (Selection.ID != TestRoomBasicInformation.试验人员技术档案 &&
                                                                                                  Selection.ID != TestRoomBasicInformation.试验室仪器设备汇总表 &&
                                                                                                  Selection.ID != TestRoomBasicInformation.拌和站基本情况登记表 &&
                                                                                                  Selection.ID != TestRoomBasicInformation.试验室综合情况登记表));

            //done 刘晓明，调整变量NewDataVisible的值，目前是登录用户的单位编码等于工程结构中单位节点的编码
            Boolean NewDataVisible = (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type.ToLower() == ParentSelection.Tag.ToString().ToLower());
            if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                if (Node.Name.Substring(0, 16) != Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code)
                {
                    NewDataVisible = false;
                }
                SelectDataIDMenuItem.Visible = false;
            }
            NewPingxingMenuItem.Visible = NewPingxingVisible;
            NewMenuItem.Visible = NewDataVisible;
            CopyDataMenuItem.Visible = NewDataVisible || Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;

            NewPingxingMenuItem.Enabled = (Selection.Tag != null ? Selection.Tag.ToString().ToLower() == "@module" : false);
            NewMenuItem.Enabled = (Selection.Tag != null ? Selection.Tag.ToString().ToLower() == "@module" : false);

            if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                BizWindow BizWindow = this.BizViewerContent.BizWindow;

                bool visible = NewPingxingVisible && ((Selection.Tag != null ? Selection.Tag.ToString().ToLower() == "@module" : false) && AuthManager.GetFunctionAuth(BizWindow.BizID, NewPingxingMenuItem.Tag.ToString()));
                NewPingxingMenuItem.Visible = visible;

                visible = NewDataVisible && ((Selection.Tag != null ? Selection.Tag.ToString().ToLower() == "@module" : false) && BizWindow.AuthDictionary.ContainsKey(NewMenuItem.Tag.ToString()));
                NewMenuItem.Visible = visible;

                visible = BizWindow.AuthDictionary.ContainsKey(EditMenuItem.Tag.ToString());
                EditMenuItem.Visible = visible;

                visible = BizWindow.AuthDictionary.ContainsKey(DeleteMenuItem.Tag.ToString());
                DeleteMenuItem.Visible = visible;

                visible = NewDataVisible && BizWindow.AuthDictionary.ContainsKey(CopyDataMenuItem.Tag.ToString());
                CopyDataMenuItem.Visible = visible;
                toolStripMenuItem1.Visible = visible;

                visible = BizWindow.AuthDictionary.ContainsKey(PasteDataMenuItem.Tag.ToString());
                PasteDataMenuItem.Visible = visible;
                toolStripMenuItem3.Visible = visible;

                visible = AuthManager.GetFunctionAuth(BizWindow.BizID, ExportDataMenuItem.Tag.ToString());
                ExportDataMenuItem.Visible = visible;

                visible = BizWindow.AuthDictionary.ContainsKey(BatchPrintMenuItem.Tag.ToString());
                BatchPrintMenuItem.Visible = visible;
            }
            #region 生成平行对应关系 删除平行关系
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                GeneratePXRelationMenuItem.Visible = true && ParentSelection.Tag.ToString().ToLower() == "@unit_施工单位" && (Selection.ID != TestRoomBasicInformation.试验人员技术档案 && Selection.ID != TestRoomBasicInformation.试验室仪器设备汇总表 && Selection.ID != TestRoomBasicInformation.拌和站基本情况登记表 && Selection.ID != TestRoomBasicInformation.试验室综合情况登记表);
                DeletePXRelationMenuItem.Visible = true && ParentSelection.Tag.ToString().ToLower() == "@unit_监理单位" && (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2") && (Selection.ID != TestRoomBasicInformation.试验人员技术档案 && Selection.ID != TestRoomBasicInformation.试验室仪器设备汇总表 && Selection.ID != TestRoomBasicInformation.拌和站基本情况登记表 && Selection.ID != TestRoomBasicInformation.试验室综合情况登记表);
            }
            else
            {
                GeneratePXRelationMenuItem.Visible = false;
                DeletePXRelationMenuItem.Visible = false;
            }
            #endregion
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                DeleteMenuItem.Visible = true;
                //DeletePXRelationMenuItem.Visible = true;
            }
            else
            {
                DeleteMenuItem.Visible = false;
                //DeletePXRelationMenuItem.Visible = false;
            }
        }

        void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == NewPingxingMenuItem)
            {
                NewPingxingData();
            }
            else if (sender == GeneratePXRelationMenuItem)
            {
                GeneratePXRelation();
            }
            else if (sender == DeletePXRelationMenuItem)
            {
                DeletePXRelation();
            }
            else if (sender == NewMenuItem)
            {
                NewData();
            }
            else if (sender == EditMenuItem)
            {
                EditData();
            }
            else if (sender == DeleteMenuItem)
            {
                DeleteData();
            }
            else if (sender == TemperatureListToolStripMenuItem)
            {
                TemperatureList();
            }
            else if (sender == SelectDataIDMenuItem)
            {
                SelectDataID();
            }
            else if (sender == CopyDataMenuItem)
            {
                CopyData();
            }
            else if (sender == PasteDataMenuItem)
            {
                PasteData();
            }
            else if (sender == ExportDataMenuItem)
            {
                ExportData();
            }
            else if (sender == BatchPrintMenuItem)
            {
                BatchPrint();
            }
        }

        /// <summary>
        /// 新建平行记录
        /// </summary>
        private void NewPingxingData()
        {
            Boolean result = true;

            Object obj = ActiveSheet.Cells[ActiveSheet.ActiveRowIndex, 0].Value;
            if (obj != null)
            {
                Guid sgDataID = new Guid(obj.ToString());
                Selection Selection = Node.Tag as Selection;
                if (Selection.ID is String)
                {
                    String ModelCode = DepositoryProjectCatlog.GetProjectTestModuleCode(Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code, Selection.ID);
                    if (ModelCode == "")
                    {
                        MessageBox.Show("当前试验室下没有找到‘" + Node.Text + "’试验模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (DocumentHelperClient.HasPXAlready(sgDataID))
                    {
                        MessageBox.Show("选中的的资料已平检过！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    result = DocumentHelperClient.NewPXDocument(sgDataID);
                }
            }

            String Message = (result ? "新建平行资料成功。" : "新建平行资料失败！");
            MessageBoxIcon Icon = (result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
        }
        private void GeneratePXRelation()
        {
            Boolean result = true;

            Object obj = ActiveSheet.Cells[ActiveSheet.ActiveRowIndex, 0].Value;
            if (obj != null)
            {
                Guid sgDataID = new Guid(obj.ToString());
                Selection Selection = Node.Tag as Selection;
                if (Selection.ID is String)
                {
                    //String ModelCode = DepositoryProjectCatlog.GetProjectTestModuleCode(Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code, Selection.ID);
                    //if (ModelCode == "")
                    //{
                    //    MessageBox.Show("当前试验室下没有找到‘" + Node.Text + "’试验模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    if (DocumentHelperClient.HasPXRelation(sgDataID))
                    {
                        MessageBox.Show("选中的的资料已平检过！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    GeneratePXRelationDialog gpx = new GeneratePXRelationDialog(sgDataID, new Guid(Selection.ID));
                    gpx.ShowDialog();
                }
            }
        }

        private void DeletePXRelation()
        {
            if (MessageBox.Show("确定删除该份资料的平行对应关系？", "确认操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int result = 0;
                Object obj = ActiveSheet.Cells[ActiveSheet.ActiveRowIndex, 0].Value;
                if (obj != null)
                {
                    Guid PXDataID = new Guid(obj.ToString());
                    result = PXJZReportDataList.DeletePXRelation(PXDataID);
                    String Message = (result == 1 ? "删除平行对应关系成功!" : "删除平行对应关系失败！");
                    MessageBoxIcon Icon = (result == 1 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);

                    if (result == 1)
                    {
                        for (int j = 0; j < ActiveSheet.ColumnCount; j++)
                        {
                            String columnKey = "";
                            if (ActiveSheet.Columns[j].Tag is JZCustomView)
                            {
                                columnKey = (ActiveSheet.Columns[j].Tag as JZCustomView).DocColumn;
                                if (columnKey == "")
                                {
                                    columnKey = (ActiveSheet.Columns[j].Tag as JZCustomView).Description;
                                }
                            }
                            else
                            {
                                columnKey = ActiveSheet.Columns[j].Tag.ToString();
                            }

                            if (columnKey == "TryType")
                            {
                                ActiveSheet.Cells[ActiveSheet.ActiveRowIndex, j].Value = "抽检";
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 新建资料
        /// </summary>
        private void NewData()
        {
            BizViewerContent.BizWindow.ProjectCatlogContent.NewData();
        }

        /// <summary>
        /// 编辑资料
        /// </summary>
        public void EditData()
        {
            if (ActiveSheet.ActiveRow == null)
            {
                return;
            }
            if (ActiveSheet.ActiveRowIndex >= 0)
            {
                Object obj = ActiveSheet.Cells[ActiveSheet.ActiveRowIndex, 0].Value;
                if (obj != null)
                {
                    String DataID = obj.ToString();
                    //done 刘晓明，调整参数ReadOnly的值，目前默认是false，可编辑，参照方法ContextMenuStrip_Opening
                    Boolean readOnly = false;
                    if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                    {
                        if (Node.Name.Substring(0, 16) != Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code)
                        {
                            readOnly = true;
                        }
                    }
                    Selection Selection = Node.Tag as Selection;
                    OpenData(DataID, readOnly, Selection.ID);
                }
            }
        }

        /// <summary>
        /// 删除资料
        /// </summary>
        public void DeleteData()
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2")
            {
                if (ActiveSheet.ActiveRow == null)
                {
                    return;
                }
                int RowIndex = ActiveSheet.ActiveRowIndex;
                if (RowIndex != -1)
                {
                    String Message = "确定要删除选中的资料吗？";
                    if (DialogResult.OK == MessageBox.Show(Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        List<string> DataIDs = new List<string>();
                        Object obj = ActiveSheet.Cells[RowIndex, 0].Value;
                        if (obj != null)
                            DataIDs.Add(obj.ToString());

                        DeleteData(DataIDs.ToArray());
                    }
                }
            }
            else
            {
                MessageBox.Show("对不起，您没有删除资料的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///  查看资料id
        /// </summary>
        public void SelectDataID()
        {
            if (ActiveSheet.ActiveRow == null)
            {
                return;
            }
            int RowIndex = ActiveSheet.ActiveRowIndex;
            if (RowIndex != -1)
            {
                if (_DataIDShowForm == null)
                {
                    _DataIDShowForm = new DataIDShowForm(ActiveSheet.Cells[RowIndex, 0].Value.ToString().Trim());
                    _DataIDShowForm.ShowDialog();
                }
                else
                {
                    _DataIDShowForm.DataID = ActiveSheet.Cells[RowIndex, 0].Value.ToString().Trim();
                    _DataIDShowForm.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 一次仅能删除一条资料
        /// </summary>
        /// <param name="DataID"></param>
        private void DeleteData(String[] DataID)
        {
            if (DataID != null && DataID.Length > 0)
            {
                Guid dataID = new Guid(DataID[0]);
                if (DocumentHelperClient.HasPXAlready(dataID))
                {
                    MessageBox.Show("该条资料已做过平检，删除操作取消。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (DocumentHelperClient.DeleteDocument(dataID))
                {
                    BindDataSource();
                }
                else
                {
                    MessageBox.Show("删除资料失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 复制资料
        /// </summary>
        public void CopyData()
        {
            if (ActiveSheet.ActiveRow == null)
            {
                return;
            }
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
                    if (item == ModuleID.ToString())
                    {
                        isDenied = true;
                        break;
                    }
                }
            }
            if (isDenied == true)
            {
                MessageBox.Show("对不起，您没有在此模板下复制资料的权限！");
                return;
            }
            #endregion

            int RowIndex = ActiveSheet.ActiveRowIndex;
            if (RowIndex != -1)
            {
                this.CopyDataID.Clear();
                Object obj = ActiveSheet.Cells[RowIndex, 0].Value;
                if (obj != null && obj.ToString() != "")
                {
                    this.CopyDataID.Add(obj.ToString());
                }
            }
            this.PasteDataMenuItem.Visible = true;
        }

        /// <summary>
        /// 粘贴资料
        /// </summary>
        public void PasteData()
        {
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
                    if (item == ModuleID.ToString())
                    {
                        isDenied = true;
                        break;
                    }
                }
            }
            if (isDenied == true)
            {
                MessageBox.Show("对不起，您没有在此模板下粘贴资料的权限！");
                return;
            }
            #endregion
            Boolean flag = true;
            foreach (String copyID in this.CopyDataID)
            {
                Guid copyDataID = new Guid(copyID);
                flag = flag && DocumentHelperClient.CopyDocument(copyDataID);
            }
            if (flag)
            {
                BindDataSource();
                this.CopyDataID.Clear();
                this.PasteDataMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// 导出台账
        /// </summary>
        private void ExportData()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                String fullPath = folderBrowserDialog1.SelectedPath;
                String fileName = Path.Combine(fullPath, Node.Text + ".xls");
                FpSpread.SaveExcel(fileName, IncludeHeaders.ColumnHeadersCustomOnly);
                MessageBox.Show(string.Format("台账“{0}”导出完成", fileName));
            }
        }

        /// <summary>
        /// 批量打印
        /// </summary>
        private void BatchPrint()
        {
            //BatchPrintClass batchPrintClass = new BatchPrintClass();
            //CellRange[] ranges = ActiveSheet.GetSelections();

            //if (ranges != null && ranges.Length > 0)
            //{
            //    List<string> DataIDs = new List<string>();
            //    for (int i = 0; i < ranges.Length; i++)
            //    {
            //        int StartRow = ranges[i].Row;
            //        int CountRow = ranges[i].RowCount;
            //        int EndRow = StartRow + CountRow;

            //        for (int j = StartRow; j < EndRow; j++)
            //        {
            //            Object obj = ActiveSheet.Cells[ActiveSheet.ActiveRowIndex, 0].Value;
            //            if (obj != null && obj is String)
            //            {
            //                DataIDs.Add(obj.ToString());
            //            }
            //        }
            //    }
            //    batchPrintClass.Run(ActiveModule, ModelCode, DataIDs.ToArray());
            //}
        }

        void ToolStripTextBox_LostFocus(object sender, EventArgs e)
        {
            int Result;
            if (int.TryParse(TextBox.Text, out Result) && Result >= 1)
            {
                _PageIndex = Result;
                BindDataSource();
            }
        }

        void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == FirstButton)
            {
                MoveFirst();
            }
            else if (sender == LastButton)
            {
                MoveLast();
            }
            else if (sender == PrevButton)
            {
                MovePrev();
            }
            else if (sender == NextButton)
            {
                MoveNext();
            }
        }

        private void BindSheetView(FpSpread fpSpread, Sys_TaiZhang tz)
        {
            if (Node == null)
                return;

            TreeNode CompanyNode = Node.Parent.Parent;
            Selection ParentSelection = CompanyNode.Tag as Selection;

            SheetView SheetView = fpSpread.ActiveSheet;

            //绑定数据时保证显示第一行，以免报错“System.ArgumentOutOfRangeException: Invalid low bound argument”
            if (SheetView.Rows.Count > 0)
            {
                fpSpread.ShowRow(fpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            }

            SheetView.Rows.Count = 0;
            SheetView.Columns.Count = 0;

            SheetView.Protect = true;
            SheetView.OperationMode = OperationMode.RowMode;

            EditBaseCellType BaseCellType = null;
            Boolean IsLockedTryType = false;
            Boolean IsLocakedDataName = false;

            BizComponents.ComboBoxCellType comboBox = new BizComponents.ComboBoxCellType();
            comboBox.DropDownButton = false;

            BizComponents.TextCellType text = new BizComponents.TextCellType();
            text.ReadOnly = true;

            IsLocakedDataName = (ParentSelection.Tag.ToString().ToLower() != Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type.ToLower())
                    && !Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;
            if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                if (Node.Name.Substring(0, 16) != Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code)
                {
                    IsLocakedDataName = true;
                }
            }

            if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
            {
                if (ParentSelection.Tag.ToString().ToLower() == "@unit_施工单位")
                {
                    comboBox.Items = new List<string>(new string[] { "自检", "见证" });
                    BaseCellType = comboBox;
                    IsLockedTryType = false;
                }
                else
                {
                    BaseCellType = text;
                    IsLockedTryType = true;
                }
            }
            else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位")
            {
                BaseCellType = text;
                IsLockedTryType = true;
            }
            else
            {
                comboBox.Items = new List<string>(new string[] { "自检", "见证", "抽检", "平行" });
                BaseCellType = comboBox;
                IsLockedTryType = false;
            }
            SheetView.Columns.Count = tz.CustomViews.Count + 2;//ID和IsPXed是否被平行
            SheetView.Columns[0].Label = "ID";
            SheetView.Columns[0].Tag = "ID";
            SheetView.Columns[0].Visible = false;
            for (int i = 0; i < tz.CustomViews.Count; i++)
            {
                JZCustomView cv = tz.CustomViews[i];
                SheetView.Columns[i + 1].Tag = cv;
                SheetView.Columns[i + 1].Visible = true;
                SheetView.Columns[i + 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                SheetView.Columns[i + 1].VerticalAlignment = CellVerticalAlignment.Center;
                SheetView.Columns[i + 1].AllowAutoSort = true;
                SheetView.Columns[i + 1].Locked = false;
                SheetView.Columns[i + 1].Label = cv.Description;
                SheetView.Columns[i + 1].Width = cv.ColumnWidth;
            }

            SheetView.Columns[tz.CustomViews.Count + 1].Label = "IsPXed";
            SheetView.Columns[tz.CustomViews.Count + 1].Tag = "IsPXed";
            SheetView.Columns[tz.CustomViews.Count + 1].Visible = false;

            if (tz.Data == null || tz.Data.Count == 0)
                return;

            Dictionary<string, int> RowIndex = new Dictionary<string, int>();

            SheetView.Rows.Count = tz.Data.Count;
            SheetView.Rows[0, SheetView.RowCount - 1].Resizable = false;
            int GroupIndex = 0;
            for (int i = 0; i < tz.Data.Count; i++)
            {
                for (int j = 0; j < SheetView.ColumnCount; j++)
                {
                    String columnKey = "";
                    if (SheetView.Columns[j].Tag is JZCustomView)
                    {
                        columnKey = (SheetView.Columns[j].Tag as JZCustomView).DocColumn;
                        if (columnKey == "")
                        {
                            columnKey = (SheetView.Columns[j].Tag as JZCustomView).Description;
                        }
                    }
                    else
                    {
                        columnKey = SheetView.Columns[j].Tag.ToString();
                    }
                    Cell cell = SheetView.Cells[i, j];

                    if (cell.Value != null && (cell.Value is DateTime))
                    {
                        cell.Value = DateTime.Parse(cell.Value.ToString()).ToString("yyyy-MM-dd");
                    }
                    if (columnKey == "DataName")
                    {
                        cell.Locked = IsLocakedDataName;
                    }
                    else if (columnKey == "TryType")
                    {
                        cell.CellType = BaseCellType.Clone() as EditBaseCellType;
                        cell.Locked = IsLockedTryType;
                    }
                    else
                    {
                        cell.CellType = text;
                        cell.Locked = true;
                    }
                    SetCellValue(cell, tz.Data[i - GroupIndex], columnKey);
                    #region 被平行的资料加上标识
                    if (columnKey.ToLower() == "ispxed")
                    {
                        int iIsPxed = 0;
                        try
                        {
                            iIsPxed = int.Parse(cell.Value.ToString());
                        }
                        catch
                        {
                            iIsPxed = 0;
                        }
                        if (iIsPxed == 1)
                        {
                            SheetView.SetRowLabel(i, 0, "平");
                        }
                    }
                    #endregion
                }
            }
        }

        private void SetCellValue(Cell cell, List<JZCell> data, String key)
        {
            foreach (JZCell item in data)
            {
                if (item.Name == key)
                {
                    if (item.Value != null)
                    {
                        if (key.IndexOf("日期") > -1 || key.IndexOf("时间") > -1)
                        {
                            object oBGRQ = item.Value;
                            DateTime dtBGRQ = new DateTime();
                            bool bBGRQ = false; ;
                            if (item.Value is decimal || item.Value is float || item.Value is double)
                            {
                                //oBGRQ = int.Parse(item.Value.ToString());
                                double dRQ = double.Parse(oBGRQ.ToString());
                                try
                                {
                                    dtBGRQ = DateTime.FromOADate(dRQ);
                                    bBGRQ = true;

                                }
                                catch (Exception)
                                {
                                    bBGRQ = false;
                                }
                            }
                            else
                            {
                                bBGRQ = DateTime.TryParse(oBGRQ == null ? "" : oBGRQ.ToString(), out dtBGRQ);
                            }
                            if (bBGRQ)
                            {
                                cell.Value = dtBGRQ.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                cell.Value = oBGRQ;
                            }
                        }
                        else
                        {
                            if (item.Value is decimal || item.Value is float || item.Value is double)
                            {
                                double v = Convert.ToDouble(item.Value);
                                cell.Value = v.ToString("F3");
                            }
                            else
                            {
                                cell.Value = item.Value;
                            }

                        }
                    }
                    break;
                }
            }
        }

        private void RefreshNavigatorBar()
        {
            TextBox.Text = Current.ToString();
            StripLabel.Text = string.Format("/ {0}", "共" + PageCount.ToString() + "页");
            TotalCount.Text = string.Format("共 {0} 条资料", DataCount);
        }

        public void ShowData()
        {
            Filter = string.Empty;
            GroupInfo.Clear();
            ActiveSheet.Models.Data = BlankGroupDataModel;
            SaveGroupDataModel = BlankGroupDataModel;
            //GroupCount = null;
            MoveFirst();
        }

        #region IPage 成员

        int _PageIndex = 1;
        private int Current
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }

        int _ItemCount = 20;
        public int ItemCount
        {
            get
            {
                return _ItemCount;
            }
            set
            {
                _ItemCount = value;
            }
        }

        private void MoveFirst()
        {
            FpSpread.StopCellEditing();
            Current = 1;
            BindDataSource();
            RefreshNavigatorBar();
        }

        private void MoveLast()
        {
            FpSpread.StopCellEditing();
            Current = PageCount;
            BindDataSource();
            RefreshNavigatorBar();
        }

        private void MoveNext()
        {
            FpSpread.StopCellEditing();
            if (Current < PageCount)
                Current++;

            BindDataSource();
            RefreshNavigatorBar();
        }

        public void MovePrev()
        {
            FpSpread.StopCellEditing();
            if (Current > 1)
                Current--;

            BindDataSource();
            RefreshNavigatorBar();
        }

        private int PageCount
        {
            get
            {
                int TotalCount = DataCount;
                double value = TotalCount * 1.0 / ItemCount;
                int Count = Convert.ToInt32(value);
                if (value - Count > 0)
                    Count = Count + 1;
                return (Count != 0 ? Count : 1);
            }
        }

        private int DataCount
        {
            get;
            set;
        }


        #endregion

        private void txtPageCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            else if (e.KeyChar == 13)
            {
                ItemCount = Convert.ToInt32(txtPageCount.Text);
                Current = 1;
                BindDataSource();
                RefreshNavigatorBar();
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }

        internal void TemperatureList()
        {
            if (ActiveSheet.ActiveRow == null)
            {
                return;
            }

            int RowIndex = ActiveSheet.ActiveRowIndex;
            if (RowIndex != -1)
            {
                var form = new TemperatureDialog(Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);
                form.ShowWithDocument(Convert.ToString(ActiveSheet.Cells[RowIndex, 0].Value).Trim());
            }
        }
    }

    public partial class ExtentDataItemChangedEventArgs : EventArgs
    {
        String _FieldName;
        public String FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }

        public Guid DataID
        {
            get;
            set;
        }

        public Object Value
        {
            get;
            set;
        }
    }
}
