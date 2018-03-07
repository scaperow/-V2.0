using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using Yqun.Services;
using Yqun.Bases;
using System.Threading;
using FarPoint.Win.Spread;
using Yqun.Permissions.Common;
using FarPoint.Win;
using Yqun.Permissions.Properties;
using BizCommon;
using BizComponents;

namespace Yqun.Permissions
{
    public partial class AuthorizationManager : ControlStandard
    {
        Dictionary<string, Dictionary<string, List<TreeNode>>> cacheNodes = new Dictionary<string, Dictionary<string, List<TreeNode>>>();

        DataTable ModuleCategory;
        DataTable ModuleData;

        Dictionary<string, ISystemAuth> AuthModules = new Dictionary<string, ISystemAuth>();

        bool m_AuthFunctionEnabled = true;
        public bool AuthFunctionEnabled
        {
            get
            {
                return m_AuthFunctionEnabled;
            }
            set
            {
                m_AuthFunctionEnabled = value;
            }
        }

        bool m_AuthRowEnabled = true;
        public bool AuthRowEnabled
        {
            get
            {
                return m_AuthRowEnabled;
            }
            set
            {
                m_AuthRowEnabled = value;
            }
        }

        bool m_AuthColumnEnabled = false;
        public bool AuthColumnEnabled
        {
            get
            {
                return m_AuthColumnEnabled;
            }
            set
            {
                m_AuthColumnEnabled = value;
            }
        }

        bool m_AuthDataEnabled = false;
        public bool AuthDataEnabled
        {
            get
            {
                return m_AuthDataEnabled;
            }
            set
            {
                m_AuthDataEnabled = value;
            }
        }

        public AuthorizationManager()
        {
            InitializeComponent();

            cacheNodes.Clear();

            TabPage tabPage;
            if (AuthFunctionEnabled)
            {
                tabPage = new TabPage("功能授权");
                tabPage.Controls.Add(AuthFunction);
                AuthFunction.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tabPage);
            }

            if (AuthRowEnabled)
            {
                tabPage = new TabPage("行授权");
                tabPage.Controls.Add(AuthRow);
                AuthRow.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tabPage);
            }

            if (AuthColumnEnabled)
            {
                tabPage = new TabPage("列授权");
                tabPage.Controls.Add(AuthColumn);
                AuthColumn.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tabPage);
            }

            if (AuthDataEnabled)
            {
                tabPage = new TabPage("数据授权");
                tabPage.Controls.Add(AuthData);
                AuthData.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tabPage);
            }

            InitComponent();
        }

        void InitComponent()
        {
            #region 初始化字段控件

            DataColumnsSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            DataColumnsSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;

            DataColumnsSpread_Sheet1.RowHeaderVisible = false;

            DataColumnsSpread_Sheet1.Rows.Count = 1;
            DataColumnsSpread_Sheet1.Columns.Count = 6;

            DataColumnsSpread_Sheet1.Columns[0].Label = "模块分类";
            DataColumnsSpread_Sheet1.Columns[1].Label = "模块名称";
            DataColumnsSpread_Sheet1.Columns[2].Label = "表单名称";
            DataColumnsSpread_Sheet1.Columns[3].Label = "列名称";
            DataColumnsSpread_Sheet1.Columns[4].Label = "查看";
            DataColumnsSpread_Sheet1.Columns[5].Label = "编辑";

            DataColumnsSpread_Sheet1.Columns[0].Width = 100;
            DataColumnsSpread_Sheet1.Columns[1].Width = 130;
            DataColumnsSpread_Sheet1.Columns[2].Width = 120;
            DataColumnsSpread_Sheet1.Columns[3].Width = 120;
            DataColumnsSpread_Sheet1.Columns[4].Width = 50;
            DataColumnsSpread_Sheet1.Columns[5].Width = 50;

            DataColumnsSpread_Sheet1.Columns[0].Tag = "@ModuleCatlog";
            DataColumnsSpread_Sheet1.Columns[1].Tag = "@Module";
            DataColumnsSpread_Sheet1.Columns[2].Tag = "@Table";
            DataColumnsSpread_Sheet1.Columns[3].Tag = "@Table_Column";
            DataColumnsSpread_Sheet1.Columns[4].Tag = "@Browse";
            DataColumnsSpread_Sheet1.Columns[5].Tag = "@Edit";

            DataColumnsSpread_Sheet1.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            DataColumnsSpread_Sheet1.Columns[5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            DataColumnsSpread_Sheet1.Protect = true;
            DataColumnsSpread_Sheet1.Columns[0].Locked = true;
            DataColumnsSpread_Sheet1.Columns[1].Locked = true;
            DataColumnsSpread_Sheet1.Columns[2].Locked = true;
            DataColumnsSpread_Sheet1.Columns[3].Locked = true;
            DataColumnsSpread_Sheet1.Columns[4].Locked = true;
            DataColumnsSpread_Sheet1.Columns[5].Locked = true;

            DataColumnsSpread_Sheet1.Columns[4].Font = new System.Drawing.Font("Wingdings", 12.0f);
            DataColumnsSpread_Sheet1.Columns[5].Font = new System.Drawing.Font("Wingdings", 12.0f);

            DataColumnsSpread_Sheet1.Columns[0].Visible = false;

            #endregion 初始化字段控件

            #region 初始化功能控件

            FunctionsSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            FunctionsSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;

            FunctionsSpread_Sheet1.RowHeaderVisible = false;

            FunctionsSpread_Sheet1.Rows.Count = 1;
            FunctionsSpread_Sheet1.Columns.Count = 4;

            FunctionsSpread_Sheet1.Columns[0].Label = "模块名称";
            FunctionsSpread_Sheet1.Columns[1].Label = "功能分类";
            FunctionsSpread_Sheet1.Columns[2].Label = "功能名称";
            FunctionsSpread_Sheet1.Columns[3].Label = "选择";

            FunctionsSpread_Sheet1.Columns[0].Width = 150;
            FunctionsSpread_Sheet1.Columns[1].Width = 150;
            FunctionsSpread_Sheet1.Columns[2].Width = 230;
            FunctionsSpread_Sheet1.Columns[3].Width = 50;

            FunctionsSpread_Sheet1.Columns[0].Tag = "@Module";
            FunctionsSpread_Sheet1.Columns[1].Tag = "@SubModule";
            FunctionsSpread_Sheet1.Columns[2].Tag = "@Item";
            FunctionsSpread_Sheet1.Columns[3].Tag = "@Tag";

            FunctionsSpread_Sheet1.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            FunctionsSpread_Sheet1.Columns[2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            FunctionsSpread_Sheet1.Protect = true;
            FunctionsSpread_Sheet1.Columns[0].Locked = true;
            FunctionsSpread_Sheet1.Columns[1].Locked = true;
            FunctionsSpread_Sheet1.Columns[2].Locked = true;
            FunctionsSpread_Sheet1.Columns[3].Locked = true;

            FunctionsSpread_Sheet1.Columns[3].Font = new System.Drawing.Font("Wingdings", 12.0f);

            #endregion 初始化功能控件

            #region 初始化数据权限控件

            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseClick);
            treeView2.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView2_NodeMouseClick);

            #endregion 初始化数据权限控件
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "数据授权")
            {
                groupBox2.Left = 7;
                groupBox2.Top = label1.Top + label1.Height + 13;
                groupBox2.Width = (AuthData.Width - 20) / 2;
                groupBox2.Height = AuthData.Height - groupBox2.Top - 7;

                groupBox3.Top = groupBox2.Top;
                groupBox3.Left = groupBox2.Left + groupBox2.Width + 7;
                groupBox3.Width = AuthData.Width - groupBox2.Left - groupBox2.Width - 14;
                groupBox3.Height = groupBox2.Height;
            }
        }

        public override object InvokeMessage(Yqun.Interfaces.YqunMessage Message)
        {
            object Result = base.InvokeMessage(Message);

            switch (Message.TypeFlag.ToLower())
            {
                case "@open":
                    OpenModel();
                    break;
            }

            return Result;
        }

        private void OpenModel()
        {
            ModuleCategory = GetModuleCategory();
            ModuleData = GetModuleData();

            foreach (DataRow row in ModuleCategory.Rows)
            {
                string Code = row["Code"].ToString();
                DataRow[] ModuleDataRows = ModuleData.Select("offolder='" + Code + "'");
                foreach (DataRow row1 in ModuleDataRows)
                {
                    string Code1 = row1["Name"].ToString();

                    object o = null;
                    try
                    {
                        o = Agent.OpenRuntime(Code1, false);
                    }
                    catch (Exception /*ex*/)
                    {
                        //MessageBox.Show(ex.Message);
                    }

                    if (o is ISystemAuth)
                    {
                        ISystemAuth Data = o as ISystemAuth;
                        if (!AuthModules.ContainsKey(Code1))
                        {
                            AuthModules.Add(Code1, Data);
                        }
                    }
                }
            }

            InitAuthInfo();

            DepositoryRole.Init(RolesView);

            if (RolesView.Nodes.Count > 0)
                RolesView.Nodes[0].EnsureVisible();
        }

        private void InitAuthInfo()
        {
            if (AuthFunctionEnabled)
            {
                LoadFunctions();
            }

            if (AuthRowEnabled)
            {
                LoadRecords();
            }

            if (AuthColumnEnabled)
            {
                LoadFields();
            }

            if (AuthDataEnabled)
            {
                LoadTables();
            }
        }

        #region 权限管理

        void SpreadContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            FpSpread Spread = SpreadContextMenu.SourceControl as FpSpread;
            if (Spread == null) return;
            Cell[] cells = GetCells(Spread);
            if (e.ClickedItem == SelectedItems)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].BackColor != Color.LightGray)
                    {
                        CheckCell(cells[i], true);
                        if (this.tabControl1.SelectedIndex == 0)
                        {
                            AddFunctionPermission(Spread.ActiveSheet.Cells[cells[i].Row.Index, cells[i].Column.Index - 1]);
                        }
                        else if (this.tabControl1.SelectedIndex == 1)
                        {
                            AddFieldPermission(Spread.ActiveSheet.Cells[cells[i].Row.Index, cells[i].Column.Index - 1]);
                        }
                    }
                }
            }
            else if (e.ClickedItem == UnSelectedItems)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].BackColor != Color.LightGray)
                    {
                        CheckCell(cells[i], false);
                        if (this.tabControl1.SelectedIndex == 0)
                        {
                            RemoveFunctionPermission(Spread.ActiveSheet.Cells[cells[i].Row.Index, cells[i].Column.Index - 1]);
                        }
                        else if (this.tabControl1.SelectedIndex == 1)
                        {
                            RemoveFieldPermission(Spread.ActiveSheet.Cells[cells[i].Row.Index, cells[i].Column.Index - 1]);
                        }
                    }
                }
            }
        }

        void Spread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (sender == FunctionsSpread)
            {
                if (e.Column == 3 && FunctionsSpread_Sheet1.Cells[e.Row, e.Column].BackColor != Color.LightGray)
                {
                    CheckCell(FunctionsSpread_Sheet1.Cells[e.Row, e.Column]);

                    if (IsCellChecked(FunctionsSpread_Sheet1.Cells[e.Row, e.Column]))
                    {
                        AddFunctionPermission(FunctionsSpread_Sheet1.Cells[e.Row, 2]);
                    }
                    else
                    {
                        RemoveFunctionPermission(FunctionsSpread_Sheet1.Cells[e.Row, 2]);
                    }
                }
            }
            else if (sender == DataColumnsSpread)
            {
                if ((e.Column == 4 || e.Column == 5) && DataColumnsSpread_Sheet1.Cells[e.Row, e.Column].BackColor != Color.LightGray)
                {
                    CheckCell(DataColumnsSpread_Sheet1.Cells[e.Row, e.Column]);

                    if (IsCellChecked(DataColumnsSpread_Sheet1.Cells[e.Row, e.Column]))
                    {
                        AddFieldPermission(DataColumnsSpread_Sheet1.Cells[e.Row, 3]);
                    }
                    else
                    {
                        RemoveFieldPermission(DataColumnsSpread_Sheet1.Cells[e.Row, 3]);
                    }
                }
            }
        }

        void Spread_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FarPoint.Win.Spread.FpSpread Spread = sender as FarPoint.Win.Spread.FpSpread;
            if (e.Button == MouseButtons.Right)
            {
                Point pt = new Point(e.X, e.Y);
                SpreadContextMenu.Show(Spread, pt);
            }
        }

        FarPoint.Win.Spread.Model.CellRange MyCellRange = null;
        void Spread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyCellRange = e.Range;
        }

        Cell[] GetCells(FpSpread Spread)
        {
            List<Cell> CellList = new List<Cell>();
            if (MyCellRange == null) return CellList.ToArray();

            if (Spread == FunctionsSpread)
            {
                if (MyCellRange.Column <= 3 && MyCellRange.Column + MyCellRange.ColumnCount >= 3)
                {
                    for (int i = 0; i < MyCellRange.RowCount; i++)
                    {
                        CellList.Add(Spread.ActiveSheet.Cells[MyCellRange.Row + i, 3]);
                    }
                }
            }
            else if (Spread == DataColumnsSpread)
            {
                if (MyCellRange.Column <= 4 && MyCellRange.Column + MyCellRange.ColumnCount > 4)
                {
                    for (int i = 0; i < MyCellRange.RowCount; i++)
                    {
                        CellList.Add(Spread.ActiveSheet.Cells[MyCellRange.Row + i, 4]);
                    }
                }

                if (MyCellRange.Column <= 5 && MyCellRange.Column + MyCellRange.ColumnCount > 5)
                {
                    for (int i = 0; i < MyCellRange.RowCount; i++)
                    {
                        CellList.Add(Spread.ActiveSheet.Cells[MyCellRange.Row + i, 5]);
                    }
                }
            }

            return CellList.ToArray();
        }

        void CheckCell(Cell cell)
        {
            if (cell.Text == "")
            {
                cell.Text = Char.ToString('\x00fc');
                cell.ForeColor = Color.Blue;
            }
            else
            {
                cell.Text = "";
            }
        }

        public void CheckCell(Cell cell, bool bShow)
        {
            if (bShow)
            {
                cell.Text = Char.ToString('\x00fc');
                cell.ForeColor = Color.Blue;
            }
            else
            {
                cell.Text = "";
            }
        }

        public void CheckCellBackground(Cell cell, bool bShow)
        {
            cell.BackColor = bShow ? Color.LightGray : Color.White;
        }

        bool IsCellChecked(Cell Cell)
        {
            return (Cell.Text == Char.ToString('\x00fc'));
        }

        void AddFunctionPermission(Cell Cell)
        {
            if (RolesView.SelectedNode.Tag is Role)
            {
                string ModuleID = FunctionsSpread_Sheet1.Cells[Cell.Row.Index, 0].Tag.ToString();
                string Caption = FunctionsSpread_Sheet1.Cells[Cell.Row.Index, 0].Text;
                Role role = RolesView.SelectedNode.Tag as Role;
                FunctionsPermission Functions = null;
                foreach (Permission permission in role.Permissions)
                {
                    if (permission is FunctionsPermission && permission.ModuleID == ModuleID)
                    {
                        Functions = permission as FunctionsPermission;
                        break;
                    }
                }

                if (Functions == null)
                {
                    Functions = new FunctionsPermission();
                    Functions.ModuleID = ModuleID;
                    Functions.Caption = Caption;
                    Functions.Index = Guid.NewGuid().ToString();
                    role.Permissions.Add(Functions);
                }

                FunctionPermission function = null;
                foreach (FunctionPermission permission in Functions.Functions)
                {
                    if (permission.Index == Cell.Tag.ToString())
                    {
                        function = permission as FunctionPermission;
                        break;
                    }
                }

                if (function == null)
                {
                    function = new FunctionPermission();
                    function.Index = Cell.Tag.ToString();
                    function.Caption = Cell.Text;
                    Functions.Functions.Add(function);
                }
            }
        }

        void RemoveFunctionPermission(Cell Cell)
        {
            if (RolesView.SelectedNode.Tag is Role)
            {
                string ModuleID = FunctionsSpread_Sheet1.Cells[Cell.Row.Index, 0].Tag.ToString();
                Role role = RolesView.SelectedNode.Tag as Role;
                FunctionsPermission Functions = null;
                foreach (Permission permission in role.Permissions)
                {
                    if (permission is FunctionsPermission && permission.ModuleID == ModuleID)
                    {
                        Functions = permission as FunctionsPermission;
                        break;
                    }
                }

                if (Functions == null)
                {
                    Functions = new FunctionsPermission();
                    Functions.ModuleID = ModuleID;
                    Functions.Caption = "功能列表";
                    Functions.Index = Guid.NewGuid().ToString();
                    role.Permissions.Add(Functions);
                }

                FunctionPermission function = null;
                foreach (FunctionPermission permission in Functions.Functions)
                {
                    if (permission.Index == Cell.Tag.ToString())
                    {
                        function = permission as FunctionPermission;
                        break;
                    }
                }

                if (function != null)
                {
                    Functions.Functions.Remove(function);
                    if (Functions.Functions.Count == 0)
                        role.Permissions.Add(Functions);
                }
            }
        }

        void AddFieldPermission(Cell Cell)
        {
            if (RolesView.SelectedNode.Tag is Role)
            {
                string ModuleID = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 1].Tag.ToString();
                string Caption = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 1].Text;
                TableInfo tableInfo = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 2].Tag as TableInfo;
                Yqun.Permissions.Common.ColumnInfo columnInfo = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 3].Tag as Yqun.Permissions.Common.ColumnInfo;

                Role role = RolesView.SelectedNode.Tag as Role;

                if (tableInfo != null)
                {
                    FieldsPermission Fields = null;
                    foreach (Permission permission in role.Permissions)
                    {
                        FieldsPermission fieldspermission = permission as FieldsPermission;
                        if (fieldspermission != null && fieldspermission.FieldsName.ToLower() == tableInfo.TableName.ToLower())
                        {
                            Fields = permission as FieldsPermission;
                            break;
                        }
                    }

                    if (Fields == null)
                    {
                        Fields = new FieldsPermission();
                        Fields.ModuleID = ModuleID;
                        Fields.Caption = tableInfo.TableName;
                        Fields.FieldsName = tableInfo.TableName;
                        Fields.Index = Guid.NewGuid().ToString();
                        role.Permissions.Add(Fields);
                    }

                    if (columnInfo != null)
                    {
                        FieldPermission field = null;
                        foreach (FieldPermission permission in Fields.Fields)
                        {
                            if (permission.Index == columnInfo.Index)
                            {
                                field = permission as FieldPermission;
                                break;
                            }
                        }

                        if (field == null)
                        {
                            field = new FieldPermission();
                            field.Index = columnInfo.Index;
                            field.FieldName = columnInfo.Name;
                            Fields.Fields.Add(field);
                        }

                        Cell ViewableCell = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 4];
                        Cell EditableCell = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 5];
                        field.Editable = IsCellChecked(EditableCell);
                        field.Viewable = IsCellChecked(ViewableCell);
                    }
                }
            }
        }

        void RemoveFieldPermission(Cell Cell)
        {
            if (RolesView.SelectedNode.Tag is Role)
            {
                string ModuleID = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 1].Tag.ToString();
                string Caption = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 1].Text;
                TableInfo tableInfo = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 2].Tag as TableInfo;
                Yqun.Permissions.Common.ColumnInfo columnInfo = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 3].Tag as Yqun.Permissions.Common.ColumnInfo;

                Role role = RolesView.SelectedNode.Tag as Role;
                if (tableInfo != null)
                {
                    FieldsPermission Fields = null;
                    foreach (Permission permission in role.Permissions)
                    {
                        FieldsPermission fieldspermission = permission as FieldsPermission;
                        if (fieldspermission != null && fieldspermission.FieldsName.ToLower() == tableInfo.TableName.ToLower())
                        {
                            Fields = permission as FieldsPermission;
                            break;
                        }
                    }

                    if (Fields == null)
                    {
                        Fields = new FieldsPermission();
                        Fields.ModuleID = ModuleID;
                        Fields.Caption = tableInfo.TableName;
                        Fields.FieldsName = tableInfo.TableName;
                        Fields.Index = Guid.NewGuid().ToString();
                        role.Permissions.Add(Fields);
                    }

                    if (columnInfo != null)
                    {
                        FieldPermission field = null;
                        foreach (FieldPermission permission in Fields.Fields)
                        {
                            if (permission.Index == columnInfo.Index)
                            {
                                field = permission as FieldPermission;
                                break;
                            }
                        }

                        if (field != null)
                        {
                            Cell ViewableCell = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 4];
                            Cell EditableCell = DataColumnsSpread_Sheet1.Cells[Cell.Row.Index, 5];
                            field.Editable = IsCellChecked(EditableCell);
                            field.Viewable = IsCellChecked(ViewableCell);

                            if (!field.Editable && !field.Viewable)
                            {
                                Fields.Fields.Remove(field);
                                if (Fields.Fields.Count == 0)
                                    role.Permissions.Remove(Fields);
                            }
                        }
                    }
                }
            }
        }

        List<TreeNode> DDNode = new List<TreeNode>();
        void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            #region 图片点击

            if (e.Node.ImageIndex == 0 && e.Node.SelectedImageIndex == 0)
            {
                if (!DDNode.Contains(e.Node))
                {
                    DDNode.Add(e.Node);
                }
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }

            else if (e.Node.ImageIndex == 1 && e.Node.SelectedImageIndex == 1)
            {
                DDNode.Remove(e.Node);
                e.Node.ImageIndex = 0;
                e.Node.SelectedImageIndex = 0;
            }

            if (e.Node.SelectedImageIndex == 1)
            {
                AppendToRole(e.Node);
            }
            else
            {
                RemoveFromRole(e.Node);
            }
            #endregion

            #region 节点实现
            //           
            /*
            e.Node.Checked = !e.Node.Checked;

            if (e.Node.Checked)
            {
                AppendToRole(e.Node);
            }
            else
            {
                RemoveFromRole(e.Node);
            }
            // */
            #endregion
        }

        //闫海峰:添加条件到当前角色中
        /// <summary>
        /// 添加条件到当前角色中
        /// </summary>
        /// <param name="treeNode">条件值节点</param>
        private void AppendToRole(TreeNode treeNode)
        {
            TableInformation info = comboBox1.SelectedItem as TableInformation;
            ColumnInformation colInfo = treeView1.SelectedNode.Tag as ColumnInformation;
            DatasPermission datainfo = GetDatasPermission(info.TableName);
            if (datainfo == null)
            {
                datainfo = new DatasPermission();
                datainfo.Index = Guid.NewGuid().ToString();
                datainfo.Caption = info.TableName;
                datainfo.ModuleID = "E44DE985-04CF-4a50-9F92-E8AF47EC0AE1";

                Role role = GetSelectedRole();
                role.Permissions.Add(datainfo);
            }

            String SeleString = treeNode.Text;
            DataPermission dataTablePer = GetDataPermission(datainfo, colInfo.ColumName);
            if (dataTablePer == null)
            {
                dataTablePer = new DataPermission();
                dataTablePer.FieldName = colInfo.ColumName;

                datainfo.Conditions.Add(dataTablePer);
            }

            if (colInfo.ColumType.ToLower() == "datetime" ||
                colInfo.ColumType.ToLower() == "string")
                SeleString = "\"" + SeleString + "\"";

            if (!dataTablePer.Values.Contains(SeleString))
            {
                dataTablePer.Values.Add(SeleString);
            }
        }

        //闫海峰:删除条件从当前角色中
        /// <summary>
        /// 删除条件从当前角色中
        /// </summary>
        /// <param name="treeNode">条件值节点</param>
        private void RemoveFromRole(TreeNode treeNode)
        {
            TableInformation info = comboBox1.SelectedItem as TableInformation;
            ColumnInformation colInfo = treeView1.SelectedNode.Tag as ColumnInformation;
            DatasPermission datainfo = GetDatasPermission(info.TableName);
            if (datainfo != null)
            {
                String SeleString = treeNode.Text;
                DataPermission dataTablePer = new DataPermission();
                if (colInfo.ColumType.ToLower() == "datetime" ||
                    colInfo.ColumType.ToLower() == "string")
                    SeleString = "\"" + SeleString + "\"";

                DataPermission coltemp = null;
                foreach (DataPermission col in datainfo.Conditions)
                {
                    if (col.FieldName.ToLower() == colInfo.ColumName.ToLower())
                    {
                        coltemp = col;
                        break;
                    }
                }

                if (coltemp != null)
                {
                    coltemp.Values.Remove(SeleString);
                }

                if (coltemp.Values.Count == 0)
                    datainfo.Conditions.Remove(coltemp);

                if (datainfo.Conditions.Count == 0)
                {
                    Role role = GetSelectedRole();
                    role.Permissions.Remove(datainfo);
                }
            }
        }

        /// <summary>
        /// 获得表的权限对象
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private DatasPermission GetDatasPermission(string TableName)
        {
            DatasPermission Result = null;
            if (RolesView.SelectedNode == null)
                return Result;

            if (!string.IsNullOrEmpty(TableName))
            {                
                Role role = RolesView.SelectedNode.Tag as Role;
                foreach (Permission permission in role.Permissions)
                {
                    if (permission is DatasPermission)
                    {
                        DatasPermission Datas = permission as DatasPermission;
                        if (Datas.Caption.ToLower().Trim() == TableName.ToLower().Trim())
                        {
                            Result = Datas;
                            break;
                        }
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// 获得列的权限对象
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        private DataPermission GetDataPermission(DatasPermission Datas, string ColumnName)
        {
            DataPermission Result = null;
            if (!string.IsNullOrEmpty(ColumnName))
            {
                foreach (DataPermission data in Datas.Conditions)
                {
                    if (data.FieldName.ToLower() == ColumnName.ToLower())
                    {
                        Result = data;
                        break;
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// 获得当前选中的角色
        /// </summary>
        /// <returns></returns>
        private Role GetSelectedRole()
        {
            if (RolesView.SelectedNode != null && RolesView.SelectedNode.Tag is Role)
            {
                return RolesView.SelectedNode.Tag as Role;
            }

            return null;
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            #region 列出所选表中存在的数据

            TableInformation info = comboBox1.SelectedItem as TableInformation;
            ColumnInformation Info = e.Node.Tag as ColumnInformation;

            treeView2.Nodes.Clear();

            if (Info.Reference != null)
            {
                String ReferenceTable = "";
                List<string> Columns = new List<string>();
                foreach (ReferenceItem Item in Info.Reference.ReferenceItems)
                {
                    ReferenceTable = Item.ReferenceTableName;
                    Columns.Add(Item.ReferenceColumnName);
                }

                StringBuilder sql_Select = new StringBuilder();
                sql_Select.Append("select ID,");
                sql_Select.Append(string.Join(",", Columns.ToArray()));
                sql_Select.Append(" from ");
                sql_Select.Append(ReferenceTable);

                DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql_Select.ToString() }) as DataTable;

                if (Data != null && Data.Rows.Count > 0)
                {
                    foreach (DataRow row in Data.Rows)
                    {
                        TreeNode Node = new TreeNode();
                        Node.Text = row[Columns[0]].ToString();
                        Node.SelectedImageIndex = 0;
                        Node.ImageIndex = 0;
                        treeView2.Nodes.Add(Node);
                    }
                }        
            }

            else
            {
                String TableData = "select distinct " + Info.ColumName + " from " + info.TableName + " where " + Info.ColumName + " is not null";
                DataTable DataRow = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { TableData }) as DataTable;
                if (DataRow != null)
                {
                    foreach (DataRow r in DataRow.Rows)
                    {
                        TreeNode Node = new TreeNode();
                        String s = r[Info.ColumName].ToString();
                        if (!string.IsNullOrEmpty(s))
                        {
                            Node.Name = s;
                            Node.Text = s;
                            //闫海峰:图片显示
                            Node.ImageIndex = 0;
                            Node.SelectedImageIndex = 0;
                            treeView2.Nodes.Add(Node);
                        }
                    }
                }
            }

            treeView2.ExpandAll();

            #endregion

            ShowDataPermission(e.Node);
        }

        //闫海峰:显示选中字段的数据值
        /// <summary>
        /// 显示选中字段的数据值
        /// </summary>
        private void ShowDataPermission(TreeNode node)
        {
            TableInformation info = comboBox1.SelectedItem as TableInformation;
            ColumnInformation colInfo = node.Tag as ColumnInformation;
            DatasPermission datainfo = GetDatasPermission(info.TableName);
            if (datainfo != null)
            {
                DataPermission coltemp = null;
                foreach (DataPermission col in datainfo.Conditions)
                {
                    if (col.FieldName.ToLower() == colInfo.ColumName.ToLower())
                    {
                        coltemp = col;
                        break;
                    }
                }

                if (coltemp != null)
                {
                    foreach (TreeNode Node in treeView2.Nodes)
                    {
                        //闫海峰:图片节点实现
                        Node.SelectedImageIndex = 0;
                        Node.ImageIndex = 0;
                    }

                    foreach (String val in coltemp.Values)
                    {
                        TreeNode[] Nodes = new TreeNode[0];
                        Nodes = treeView2.Nodes.Find(val.Trim('\"', '\"'), false);

                        if (Nodes.Length > 0)
                        {
                            //闫海峰:图片节点实现
                            Nodes[0].ImageIndex = 1;
                            Nodes[0].SelectedImageIndex = 1;
                        }
                    }
                }
            }
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();

            TableInformation info = comboBox1.SelectedItem as TableInformation;
            SheetConfiguration Configuration = DepositorySheetConfiguration.InitConfiguration(GetSheetIndex(info.TableName));

            if (Configuration != null && Configuration.SheetStyle != "")
            {
                SheetView Sheet = Serializer.LoadObjectXml(typeof(SheetView), Configuration.SheetStyle, "SheetView") as SheetView;

                //增加查询条件Scdel=0     2013-10-19
                String TreeSelectInfo = "select COLNAME,DESCRIPTION,DISPTYPE from sys_Columns where Scdel=0 and TABLENAME='" + info.TableName + "' order by DESCRIPTION";
                DataTable ds = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { TreeSelectInfo }) as DataTable;
                if (ds != null)
                {
                    foreach (DataRow row in ds.Rows)
                    {
                        String ColumnName = row["COLNAME"].ToString();
                        String ColumnInfo = row["DESCRIPTION"].ToString();
                        String ColumnDispType = row["DISPTYPE"].ToString();

                        FieldDefineInfo FieldInfo = Configuration.DataTableSchema.GetFieldInfo(info.TableName, ColumnName);
                        if (FieldInfo != null)
                        {
                            DownListCellType downListCellType = Sheet.Cells[FieldInfo.RangeInfo].CellType as DownListCellType;
                            if (downListCellType != null)
                            {
                                if (downListCellType.ReferenceStyle == ReferenceStyle.Sheet)
                                {
                                    SheetReference SheetReference = downListCellType.ReferenceInfo as SheetReference;

                                    ColumnInformation columSelectInfo = new ColumnInformation();
                                    columSelectInfo.ColumName = ColumnName;
                                    columSelectInfo.ColumInfo = ColumnInfo;
                                    columSelectInfo.ColumType = ColumnDispType;
                                    columSelectInfo.Reference = SheetReference;

                                    TreeNode treeNewNode = new TreeNode();
                                    treeNewNode.Tag = columSelectInfo;
                                    treeNewNode.Text = ColumnInfo;
                                    this.treeView1.Nodes.Add(treeNewNode);
                                }
                                else
                                {
                                    ColumnInformation columSelectInfo = new ColumnInformation();
                                    columSelectInfo.ColumName = ColumnName;
                                    columSelectInfo.ColumInfo = ColumnInfo;
                                    columSelectInfo.ColumType = ColumnDispType;

                                    TreeNode treeNewNode = new TreeNode();
                                    treeNewNode.Tag = columSelectInfo;
                                    treeNewNode.Text = ColumnInfo;
                                    this.treeView1.Nodes.Add(treeNewNode);
                                }
                            }
                            else
                            {
                                ColumnInformation columSelectInfo = new ColumnInformation();
                                columSelectInfo.ColumName = ColumnName;
                                columSelectInfo.ColumInfo = ColumnInfo;
                                columSelectInfo.ColumType = ColumnDispType;

                                TreeNode treeNewNode = new TreeNode();
                                treeNewNode.Tag = columSelectInfo;
                                treeNewNode.Text = ColumnInfo;
                                this.treeView1.Nodes.Add(treeNewNode);
                            }
                        }
                        else
                        {
                            ColumnInformation columSelectInfo = new ColumnInformation();
                            columSelectInfo.ColumName = ColumnName;
                            columSelectInfo.ColumInfo = ColumnInfo;
                            columSelectInfo.ColumType = ColumnDispType;

                            TreeNode treeNewNode = new TreeNode();
                            treeNewNode.Tag = columSelectInfo;
                            treeNewNode.Text = ColumnInfo;
                            this.treeView1.Nodes.Add(treeNewNode);
                        }
                    }
                }
            }
        }

        string GetSheetIndex(string SheetName)
        {
            string SheetIndex = "";

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID from sys_biz_PrimaryDetailsSheet where PrimaryTable='");
            Sql_Select.Append(SheetName);
            Sql_Select.Append("'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                SheetIndex = Data.Rows[0]["ID"].ToString();
            }

            return SheetIndex;
        }

        #endregion 权限管理

        #region 数据授权

        /// <summary>
        /// 从数据库中记载所有的数据表
        /// </summary>
        private void LoadTables()
        {
            String TableSelectInfo = "SELECT [DESCRIPTION],[TABLENAME] FROM [SYS_TABLES] where [TABLENAME] in (select table_name from information_schema.tables ) order by [DESCRIPTION] asc";
            DataTable ds = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { TableSelectInfo }) as DataTable;
            if (ds != null)
            {
                comboBox1.Items.Clear();
                foreach (DataRow row in ds.Rows)
                {
                    String TableName = row["TABLENAME"].ToString();
                    String DESCRIPTION = row["DESCRIPTION"].ToString();

                    TableInformation tableInfo = new TableInformation();
                    tableInfo.TableInform = DESCRIPTION;
                    tableInfo.TableName = TableName;
                    comboBox1.Items.Add(tableInfo);
                }
            }
        }

        #endregion 数据授权

        #region 列授权

        private void LoadFields()
        {
            try
            {
                DataColumnsSpread_Sheet1.Rows.Count = 0;
                foreach (DataRow row in ModuleCategory.Rows)
                {
                    string Name = row["ID"].ToString();
                    string Code = row["Code"].ToString();
                    string Text = row["Name"].ToString();

                    int j = 0;
                    DataRow[] ModuleDataRows = ModuleData.Select("offolder='" + Code + "'");
                    if (ModuleDataRows.Length > 0)
                    {
                        foreach (DataRow row1 in ModuleDataRows)
                        {
                            string Name1 = row1["ID"].ToString();
                            string Code1 = row1["Name"].ToString();
                            string Text1 = row1["Description"].ToString();

                            int i = 0;
                            TableInfoCollection TableData = GetColumnsInfo(Code1);
                            if (TableData == null || TableData.Count == 0)
                            {
                                DataColumnsSpread_Sheet1.Rows.Add(DataColumnsSpread_Sheet1.Rows.Count, 1);
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 0].Text = Text;
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 0].Tag = Code;
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].Text = Text1;
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].Tag = Code1;
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 2].Tag = "";
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 3].Tag = "";
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 4].Tag = "";
                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 5].Tag = "";

                                j = j + 1;

                                continue;
                            }

                            foreach (TableInfo info in TableData)
                            {
                                if (info.ColumnsInfo.Count == 0)
                                {
                                    DataColumnsSpread_Sheet1.Rows.Add(DataColumnsSpread_Sheet1.Rows.Count, 1);
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 0].Text = Text;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 0].Tag = Code;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].Text = Text1;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].Tag = Code1;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 2].Text = info.TableText;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 2].Tag = info;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 3].Tag = "";
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 4].Tag = "";
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 5].Tag = "";

                                    i = i + 1;

                                    continue;
                                }

                                //声明一个TextCellType 单元格类型
                                FarPoint.Win.Spread.CellType.TextCellType TextType = new FarPoint.Win.Spread.CellType.TextCellType();
                                TextType.WordWrap = true;
                                TextType.Multiline = true;

                                foreach (Yqun.Permissions.Common.ColumnInfo finfo in info.ColumnsInfo)
                                {
                                    DataColumnsSpread_Sheet1.Rows.Add(DataColumnsSpread_Sheet1.Rows.Count, 1);

                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].CellType = TextType;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 2].CellType = TextType;

                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 0].Text = Text;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 0].Tag = Code;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].Text = Text1;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 1].Tag = Code1;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 2].Text = info.TableText;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 2].Tag = info;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 3].Text = finfo.Text;
                                    DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - 1, 3].Tag = finfo;
                                }

                                DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - info.ColumnsInfo.Count, 2].RowSpan = info.ColumnsInfo.Count;
                                i = i + info.ColumnsInfo.Count;
                            }

                            DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - i, 1].RowSpan = i;
                            j = j + i;
                        }

                        DataColumnsSpread_Sheet1.Cells[DataColumnsSpread_Sheet1.Rows.Count - j, 0].RowSpan = j;
                    }
                }
            }
            catch
            { }
        }

        private TableInfoCollection GetColumnsInfo(string BizID)
        {
            TableInfoCollection TableData = null;
            if (AuthModules.ContainsKey(BizID))
            {
                TableData = AuthModules[BizID].GetTablesInfo();
            }

            return TableData;
        }

        #endregion 列授权

        #region 行授权

        private void LoadRecords()
        {
            try
            {
                List<TreeInfo> Trees = new List<TreeInfo>();
                foreach (DataRow row in ModuleCategory.Rows)
                {
                    string Name = row["ID"].ToString();
                    string Code = row["Code"].ToString();
                    string Text = row["Name"].ToString();

                    DataRow[] ModuleDataRows = ModuleData.Select("offolder='" + Code + "'");
                    foreach (DataRow row1 in ModuleDataRows)
                    {
                        string Name1 = row1["ID"].ToString();
                        string Code1 = row1["Name"].ToString();
                        string Text1 = row1["Description"].ToString();

                        GetRecordsInfo(Code1, RecordsView);
                    }
                }
            }
            catch
            { }
        }

        private bool ContainTree(List<TreeInfo> Trees, TreeInfo Tree)
        {
            foreach (TreeInfo tree in Trees)
            {
                if (tree.TreeName == Tree.TreeName)
                    return true;
            }

            return false;
        }

        private void GetRecordsInfo(string BizID, TreeView RecordsView)
        {
            if (AuthModules.ContainsKey(BizID))
            {
                AuthModules[BizID].LoadTrees(RecordsView);
            }
        }

        #endregion 行授权

        #region 功能授权

        private void LoadFunctions()
        {
            try
            {
                FunctionsSpread_Sheet1.Rows.Count = 0;

                foreach (DataRow row in ModuleCategory.Rows)
                {
                    string Name = row["ID"].ToString();
                    string Code = row["Code"].ToString();
                    string Text = row["Name"].ToString();

                    DataRow[] ModuleDataRows = ModuleData.Select("offolder='" + Code + "'");
                    foreach (DataRow row1 in ModuleDataRows)
                    {
                        string Name1 = row1["ID"].ToString();
                        string Code1 = row1["Name"].ToString();
                        string Text1 = row1["Description"].ToString();

                        ModuleInfo FunctionData = GetFunctionData(Code1);
                        if (FunctionData == null || FunctionData.SubModuleInfos.Count == 0)
                            continue;

                        int i = 0;
                        foreach (SubModuleInfo info in FunctionData.SubModuleInfos)
                        {
                            foreach (FunctionInfo finfo in info.FunctionInfos)
                            {
                                FunctionsSpread_Sheet1.Rows.Add(FunctionsSpread_Sheet1.Rows.Count, 1);

                                FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - 1, 2].Text = finfo.Text;
                                FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - 1, 2].Tag = finfo.Index;

                                FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - 1, 1].Text = info.Text;
                                FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - 1, 1].Tag = info.Index;

                                FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - 1, 0].Text = Text1;
                                FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - 1, 0].Tag = Code1;
                            }

                            FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - info.FunctionInfos.Count, 1].RowSpan = info.FunctionInfos.Count;
                            i = i + info.FunctionInfos.Count;
                        }

                        FunctionsSpread_Sheet1.Cells[FunctionsSpread_Sheet1.Rows.Count - i, 0].RowSpan = i;
                    }
                }

            }
            catch
            { }

            if (FunctionsSpread_Sheet1.Rows.Count > 0)
                FunctionsSpread_Sheet1.SetActiveCell(0, 3);
        }

        /// <summary>
        /// 获得模块分类
        /// </summary>
        /// <returns></returns>
        DataTable GetModuleCategory()
        {
            try
            {
                StringBuilder Sql_ModuleCategory = new StringBuilder();
                Sql_ModuleCategory.Append("select ID,Name,Code from sys_solforldr order by OrderIndex");

                DataTable Result = Yqun.Services.Agent.CallRemoteService("Yqun.BO.LoginBO.dll",
                            "GetDataTable", new object[] { Sql_ModuleCategory.ToString() }) as DataTable;

                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得每一类中的模块
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="WhereCondition"></param>
        /// <returns></returns>
        DataTable GetModuleData()
        {
            try
            {
                StringBuilder Sql_ModuleData = new StringBuilder();
                Sql_ModuleData.Append("select ID,Name,Description,TypeFalg,offolder from sys_solcontent where ");
                Sql_ModuleData.Append("TypeFalg not in ('@NewNewAuthModule','@NewOrganization','@SETUP') ");
                Sql_ModuleData.Append(" order by offolder,orderIndex");
   
                DataTable Result = Yqun.Services.Agent.CallRemoteService("Yqun.BO.LoginBO.dll","GetDataTable", new object[] { Sql_ModuleData.ToString() }) as DataTable;

                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得模块中的所有的功能
        /// </summary>
        /// <param name="BizID"></param>
        ModuleInfo GetFunctionData(string BizID)
        {
            ModuleInfo ModuleData = null;
            if (AuthModules.ContainsKey(BizID))
            {
                ModuleData = AuthModules[BizID].GetFunctionInfos();
            }

            return ModuleData;
        }

        #endregion 功能授权

        #region 工具栏

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == NewRoleButton)
            {
                NewRole();
            }
            if (sender == EditRoleButton)
            {
                EditRole();
            }
            else if (sender == DeleteRoleButton)
            {
                DeleteRole();
            }
            else if (sender == SaveAuthButton)
            {
                SaveAuth();
            }
        }

        #endregion 工具栏

        #region 子功能

        private void NewRole()
        {
            TreeNode ParentNode = RolesView.SelectedNode;

            if (ParentNode == null)
                return;

            RoleDialog roleForm = new RoleDialog();
            String roleCode = ParentNode.Name;
            if (DialogResult.OK == roleForm.ShowDialog(this))
            {
                Role role = new Role();
                role.Index = Guid.NewGuid().ToString();
                role.Code = DepositoryRole.GetNextCode(roleCode);
                role.Name = roleForm.TextBox_Name.Text;
                role.IsAdministrator = roleForm.CheckBox_IsAdmin.Checked;

                Boolean r = DepositoryRole.New(role);
                if (r)
                {
                    TreeNode Node = new TreeNode();
                    Node.Name = role.Code;
                    Node.Text = role.Name;
                    Node.ImageIndex = 2;
                    Node.SelectedImageIndex = 2;
                    Node.Tag = role;

                    ParentNode.Nodes.Add(Node);
                    ParentNode.Expand();
                }

                string Msg = r ? "新建角色成功。" : "新建角色失败。";
                MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditRole()
        {
            TreeNode Node = RolesView.SelectedNode;

            if (Node == null)
                return;

            RoleDialog roleForm = new RoleDialog();
            roleForm.EditRole = Node.Tag as Role;
            if (DialogResult.OK == roleForm.ShowDialog(this))
            {
                Role role = Node.Tag as Role;
                role.Name = roleForm.TextBox_Name.Text;
                role.IsAdministrator = roleForm.CheckBox_IsAdmin.Checked;

                bool r = DepositoryRole.Update(role);
                if (r)
                    Node.Text = role.Name;

                string Msg = r ? "更新角色成功。" : "更新角色失败。";
                MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteRole()
        {
            TreeNode Node = RolesView.SelectedNode;

            if (Node == null)
                return;

            if (Node.Nodes.Count > 0)
            {
                MessageBox.Show("岗位下面不为空，请先删除附属的岗位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.OK == MessageBox.Show(string.Format("确认删除岗位‘{0}’吗？", Node.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                Role role = Node.Tag as Role;
                bool r = DepositoryRole.Delete(role.Index);
                if (r)
                {
                    Node.Remove();
                }

                string Msg = r ? "删除岗位成功。" : "删除岗位失败。";
                MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveAuth()
        {
            if (RolesView.SelectedNode.Tag is Role)
            {
                Role role = RolesView.SelectedNode.Tag as Role;
                Boolean Result = DepositoryRole.Update(role);
                Result = Result & DepositoryPermission.Update(role.Permissions);

                String Message = (Result? "保存选中岗位的权限成功。" : "保存选中岗位的权限失败！");
                MessageBoxIcon Icon = (Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
            }
        }

        #endregion 子功能

        #region 菜单状态管理

        private void RolesView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NewRoleButton.Enabled = (e.Node.Tag is String && (e.Node.Tag.ToString() == "@folder" ||
                                                              e.Node.Tag.ToString() == "@eng"));
            EditRoleButton.Enabled = (e.Node.Tag is Role);
            DeleteRoleButton.Enabled = (e.Node.Tag is Role);
        }

        #endregion 菜单状态管理

        #region 勾选树节点

        //勾选指定的节点
        void CheckRecord(Role role, TreeView View, string TreeName, RecordListElement record)
        {
            TreeNode TopNode = null;
            foreach (TreeNode Node in View.Nodes)
            {
                Selection Selection = Node.Tag as Selection;
                if (Selection.ID == TreeName)
                {
                    TopNode = Node;
                    break;
                }
            }

            if (TopNode != null)
            {
                TreeNode NextNode = TopNode;
                while (NextNode != null)
                {
                    Selection Selection = NextNode.Tag as Selection;
                    if (Selection.ID == record.Index && NextNode.Name == record.Code)
                    {
                        NextNode.EnsureVisible();
                        NextNode.Checked = true;
                        AppendTreeNode(role, NextNode);
                    }

                    if (NextNode.FirstNode != null)
                    {
                        NextNode = NextNode.FirstNode;
                    }
                    else if (NextNode.NextNode != null)
                    {
                        NextNode = NextNode.NextNode;
                    }
                    else
                    {
                        if (NextNode.Parent != null)
                        {
                            TreeNode tempNode = NextNode.Parent;
                            while (tempNode.NextNode == null)
                            {
                                if (tempNode.Parent == null)
                                    break;
                                tempNode = tempNode.Parent;
                            }

                            NextNode = tempNode.NextNode;
                        }
                        else
                        {
                            NextNode = NextNode.Parent;
                        }
                    }
                }
            }
        }

        void ResetRecord(TreeView View, string TreeName)
        {
            TreeNode TopNode = null;
            foreach (TreeNode Node in View.Nodes)
            {
                Selection Selection = Node.Tag as Selection;
                if (Selection.ID == TreeName)
                {
                    TopNode = Node;
                    break;
                }
            }

            if (TopNode != null)
            {
                TreeNode NextNode = TopNode;
                while (NextNode != null)
                {
                    NextNode.Checked = false;

                    if (NextNode.FirstNode != null)
                    {
                        NextNode = NextNode.FirstNode;
                    }
                    else if (NextNode.NextNode != null)
                    {
                        NextNode = NextNode.NextNode;
                    }
                    else
                    {
                        if (NextNode.Parent != null)
                        {
                            TreeNode tempNode = NextNode.Parent;
                            while (tempNode.NextNode == null)
                            {
                                if (tempNode.Parent == null)
                                    break;
                                tempNode = tempNode.Parent;
                            }

                            NextNode = tempNode.NextNode;
                        }
                        else
                        {
                            NextNode = NextNode.Parent;
                        }
                    }
                }
            }
        }

        void AppendTreeNode(Role role, TreeNode Node)
        {
            if (!cacheNodes.ContainsKey(role.Index))
                cacheNodes.Add(role.Index, new Dictionary<string, List<TreeNode>>());

            string TreeName = GetTreeName(Node);
            if (!cacheNodes[role.Index].ContainsKey(TreeName))
                cacheNodes[role.Index].Add(TreeName, new List<TreeNode>());

            if (!cacheNodes[role.Index][TreeName].Contains(Node))
                cacheNodes[role.Index][TreeName].Add(Node);
        }

        void RemoveTreeNode(Role role, TreeNode Node)
        {
            if (!cacheNodes.ContainsKey(role.Index))
                cacheNodes.Add(role.Index, new Dictionary<string, List<TreeNode>>());

            string TreeName = GetTreeName(Node);
            if (!cacheNodes[role.Index].ContainsKey(TreeName))
                cacheNodes[role.Index].Add(TreeName, new List<TreeNode>());

            if (cacheNodes[role.Index][TreeName].Contains(Node))
                cacheNodes[role.Index][TreeName].Remove(Node);
        }

        void FillRecordList(Role role)
        {
            if (cacheNodes.ContainsKey(role.Index) && cacheNodes[role.Index].Count > 0)
            {
                foreach (String TreeName in cacheNodes[role.Index].Keys)
                {
                    RecordsPermission RecordsPermission = null;
                    foreach (Permission permission in role.Permissions)
                    {
                        RecordsPermission recordsPermission = permission as RecordsPermission;
                        if (recordsPermission != null && recordsPermission.Caption == TreeName)
                        {
                            RecordsPermission = recordsPermission;
                            break;
                        }
                    }

                    if (RecordsPermission == null)
                    {
                        RecordsPermission = new RecordsPermission();
                        RecordsPermission.Index = Guid.NewGuid().ToString();
                        RecordsPermission.ModuleID = TreeName;
                        RecordsPermission.Caption = TreeName;
                        role.Permissions.Add(RecordsPermission);
                    }

                    RecordsPermission.RecordPermissionList.Clear();
                    List<TreeNode> Nodes = cacheNodes[role.Index][TreeName];
                    foreach (TreeNode Node in Nodes)
                    {
                        Selection Selection = Node.Tag as Selection;
                        RecordListElement Record = new RecordListElement();
                        Record.Index = Selection.ID;
                        Record.Caption = Node.Text;
                        Record.Code = Node.Name;

                        RecordsPermission.RecordPermissionList.Add(Record);
                    }
                }
            }
        }

        public string GetNodeCode(TreeNode Node)
        {
            List<string> Codes = new List<string>();
            TreeNode tempNode = Node;
            while (tempNode != null)
            {
                Selection Selection = tempNode.Tag as Selection;
                Codes.Add(Selection.Code);
                tempNode = tempNode.Parent;
            }

            Codes.Reverse();

            return string.Join("", Codes.ToArray());
        }

        #region 用来显示父级权限的提示

        //勾选指定的节点
        void CheckRecordTip(TreeView View, string TreeName, RecordListElement record)
        {
            TreeNode TopNode = null;
            foreach (TreeNode Node in View.Nodes)
            {
                Selection Selection = Node.Tag as Selection;
                if (Selection.ID == TreeName)
                {
                    TopNode = Node;
                    break;
                }
            }

            if (TopNode != null)
            {
                CheckRecordTip(TopNode, record);
            }
        }

        void CheckRecordTip(TreeNode TopNode, RecordListElement record)
        {
            Selection Selection = TopNode.Tag as Selection;
            if (Selection.ID == record.Index)
            {
                TopNode.EnsureVisible();
                TopNode.BackColor = Color.LightSeaGreen;
                return;
            }

            foreach (TreeNode Node in TopNode.Nodes)
            {
                CheckRecordTip(Node, record);
            }
        }

        void ResetRecordTip(TreeView View, string TreeName)
        {
            TreeNode TopNode = null;
            foreach (TreeNode Node in View.Nodes)
            {
                Selection Selection = Node.Tag as Selection;
                if (Selection.ID == TreeName)
                {
                    TopNode = Node;
                    break;
                }
            }

            if (TopNode != null)
            {
                ResetRecordTip(TopNode);
            }
        }

        void ResetRecordTip(TreeNode TopNode)
        {
            TopNode.BackColor = Color.White;

            foreach (TreeNode Node in TopNode.Nodes)
            {
                ResetRecordTip(Node);
            }
        }

        #endregion 用来显示父级权限的提示

        #endregion 勾选树节点

        string GetTreeName(TreeNode Node)
        {
            TreeNode tempNode = Node;
            while (tempNode.Level != 0)
            {
                tempNode = tempNode.Parent;
            }

            Selection Selection = tempNode.Tag as Selection;
            return Selection.ID;
        }

        /// <summary>
        /// 点击左边的树节点时，显示该角色的功能权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RolesView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView2.Nodes.Clear();

            if (e.Node.Tag is Role)
            {
                Role role = e.Node.Tag as Role;
                InitAuthInfo(role);
                ShowAuth(role);
            }
        }

        private void InitAuthInfo(Role role)
        {
            PermissionCollection Permissions = DepositoryPermission.Init(role.Index);
            role.Permissions = Permissions;
        }

        private void ShowAuth(Role role)
        {
            ShowFunctionAuth(role);
            ShowRowAuth(role);
            ShowFieldAuth(role);
        }

        private void ShowFieldAuth(Role role)
        {
            for (int i = 0; i < DataColumnsSpread_Sheet1.Rows.Count; i++)
            {
                CheckCell(DataColumnsSpread_Sheet1.Cells[i, 4], false);
                CheckCell(DataColumnsSpread_Sheet1.Cells[i, 5], false);
            }

            foreach (Permission permission in role.Permissions)
            {
                if (permission is FieldsPermission)
                {
                    FieldsPermission Fields = permission as FieldsPermission;
                    foreach (FieldPermission field in Fields.Fields)
                    {
                        CheckField(DataColumnsSpread_Sheet1, 4, Fields.FieldsName, field);
                        CheckField(DataColumnsSpread_Sheet1, 5, Fields.FieldsName, field);
                    }
                }
            }
        }

        private void ShowRowAuth(Role role)
        {
            foreach (TreeNode Node in RecordsView.Nodes)
            {
                Selection Selection = Node.Tag as Selection;
                ResetRecord(RecordsView, Selection.ID);
            }

            foreach (Permission permission in role.Permissions)
            {
                if (permission is RecordsPermission)
                {
                    RecordsPermission Records = permission as RecordsPermission;
                    foreach (RecordListElement record in Records.RecordPermissionList)
                    {
                        CheckRecord(role, RecordsView, permission.Caption, record);
                    }
                }
            }
        }

        private void ShowFunctionAuth(Role role)
        {
            for (int i = 0; i < FunctionsSpread_Sheet1.Rows.Count; i++)
            {
                CheckCell(FunctionsSpread_Sheet1.Cells[i, 3], false);
            }

            foreach (Permission permission in role.Permissions)
            {
                if (permission is FunctionsPermission)
                {
                    FunctionsPermission Functions = permission as FunctionsPermission;
                    foreach (FunctionPermission function in Functions.Functions)
                    {
                        CheckFunction(FunctionsSpread_Sheet1, 3, Functions.ModuleID, function);
                    }
                }
            }
        }

        #region 勾选功能单元格

        void CheckFunction(SheetView Function_Sheet, int ColumnIndex, String ModuleID, FunctionPermission function)
        {
            for (int i = 0; i < Function_Sheet.Rows.Count; i++)
            {
                if (Function_Sheet.Cells[i, ColumnIndex - 1].Tag.ToString() == function.Index &&
                    Function_Sheet.Cells[i, ColumnIndex - 3].Tag.ToString() == ModuleID)
                {
                    CheckCell(Function_Sheet.Cells[i, ColumnIndex], true);
                }
            }
        }

        void CheckFunctionTip(SheetView Function_Sheet, int ColumnIndex, String ModuleID, FunctionPermission function)
        {
            for (int i = 0; i < Function_Sheet.Rows.Count; i++)
            {
                if (Function_Sheet.Cells[i, ColumnIndex - 1].Tag.ToString() == function.Index &&
                    Function_Sheet.Cells[i, ColumnIndex - 3].Tag.ToString() == ModuleID)
                {
                    CheckCellBackground(Function_Sheet.Cells[i, ColumnIndex], true);
                }
            }
        }

        void CheckField(SheetView Field_Sheet, int ColumnIndex, String TableName, FieldPermission field)
        {
            for (int i = 0; i < Field_Sheet.Rows.Count; i++)
            {
                TableInfo tableInfo = DataColumnsSpread_Sheet1.Cells[i, 2].Tag as TableInfo;
                Yqun.Permissions.Common.ColumnInfo columnInfo = DataColumnsSpread_Sheet1.Cells[i, 3].Tag as Yqun.Permissions.Common.ColumnInfo;

                if (tableInfo == null || columnInfo == null)
                    continue;

                if (columnInfo.Index == field.Index && tableInfo.TableName.ToLower() == TableName.ToLower())
                {
                    CheckCell(Field_Sheet.Cells[i, ColumnIndex], true);
                }
            }
        }

        void CheckFieldTip(SheetView Field_Sheet, int ColumnIndex, String TableName, FieldPermission field)
        {
            for (int i = 0; i < Field_Sheet.Rows.Count; i++)
            {
                TableInfo tableInfo = DataColumnsSpread_Sheet1.Cells[i, 2].Tag as TableInfo;
                Yqun.Permissions.Common.ColumnInfo columnInfo = DataColumnsSpread_Sheet1.Cells[i, 3].Tag as Yqun.Permissions.Common.ColumnInfo;

                if (tableInfo == null || columnInfo == null)
                    continue;

                if (columnInfo.Index == field.Index && tableInfo.TableName.ToLower() == TableName.ToLower())
                {
                    CheckCellBackground(Field_Sheet.Cells[i, ColumnIndex], true);
                }
            }
        }
        #endregion 勾选功能单元格

        private void RecordsView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (RolesView.SelectedNode.Tag is Role)
            {
                Role role = RolesView.SelectedNode.Tag as Role;

                string TreeName = GetTreeName(e.Node);
                if (e.Node.Checked)
                {
                    AppendTreeNode(role, e.Node);
                }
                else
                {
                    RemoveTreeNode(role, e.Node);
                }

                FillRecordList(role);
            }
        }
    }
}


