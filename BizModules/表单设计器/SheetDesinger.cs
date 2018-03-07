using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizComponents;
using Yqun.Client.BizUI;
using FarPoint.Win;
using Yqun.Common.Encoder;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.Spread.CellType;
using Yqun.Client;
using BizCommon;
using Yqun.Bases;
using Yqun.Permissions.Common;
using System.Xml;
using System.IO;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;

namespace BizModules
{
    public partial class SheetDesinger : Form
    {
        public List<Cell> dataCells = new List<Cell>();
        private Guid sheetID = Guid.Empty;

        public SheetDesinger(Guid _sheetID)
        {
            InitializeComponent();
            sheetID = _sheetID;
            fpSheetEditor1.ExcelOpened += new EventHandler<ExcelsEventArgs>(fpSheetEditor1_ExcelOpened);
            fpSheetEditor1.CellTypeSetting += new EventHandler<CellTypeEventArgs>(fpSheetEditor1_CellTypeSetting);
            fpSheetEditor1.MouseUp += new MouseEventHandler(fpSheetEditor1_MouseUp);

            InitContextMenu();

            this.Disposed += new EventHandler(SheetDesinger_Disposed);
        }

        //清理释放的托管堆中的内存
        void SheetDesinger_Disposed(object sender, EventArgs e)
        {
            GC.Collect(3, GCCollectionMode.Forced);
        }

        internal FpSheetEditor Editor
        {
            get
            {
                return fpSheetEditor1;
            }
        }

        internal FpSpread FpSpread
        {
            get
            {
                return fpSheetEditor1.FpSpread;
            }
        }

        internal String SheetName
        {
            get
            {
                return FpSpread.Sheets[0].SheetName;
            }
        }

        private Boolean _lineStyle = false;
        public Boolean LineStyle
        {
            get
            {
                return _lineStyle;
            }
            set
            {
                _lineStyle = value;
            }
        }
        public JZCellStyle CurrentCellStyle { get; set; }
        public DataTable dtCellStyle { get; set; }
        void fpSheetEditor1_ExcelOpened(object sender, ExcelsEventArgs e)
        {
            if (e.Sheets.Count > 0)
            {
                SheetView SheetView = e.Sheets[0];

                FpSpread.Sheets.Clear();
                FpSpread.Sheets.Add(SheetView);
            }

            if (FpSpread.Sheets.Count > 0)
                FpSpread.ActiveSheet = FpSpread.Sheets[FpSpread.Sheets.Count - 1];

            SheetPropertyDialog PropertyDialog = new SheetPropertyDialog(FpSpread.ActiveSheet);
            PropertyDialog.RowCount.Value = FpSpread.ActiveSheet.RowCount;
            PropertyDialog.ColumnCount.Value = FpSpread.ActiveSheet.ColumnCount;
            string row = (FpSpread.ActiveSheet.GetLastNonEmptyRow(NonEmptyItemFlag.Style) + 1).ToString();
            string col = (Arabic_Numerals_Convert.Excel_Word_Numerals(FpSpread.ActiveSheet.GetLastNonEmptyColumn(NonEmptyItemFlag.Style) + 1)).ToString();
            PropertyDialog.DisplayInformation(row, col);
            PropertyDialog.Show(this);
        }

        private void SheetDesinger_Load(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            ProgressScreen.Current.SetStatus = "正在初始化表单...";

            try
            {
                Sys_Sheet sheetItem = ModuleHelperClient.GetSheetItemByID(sheetID);
                FpSpread.Sheets.Clear();

                if (ModuleHelperClient.IsContainerXml(sheetItem.SheetXML))
                {
                    sheetItem.SheetXML = sheetItem.SheetXML;
                }
                else
                {
                    sheetItem.SheetXML = JZCommonHelper.GZipDecompressString(sheetItem.SheetXML);
                }

                SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetItem.SheetXML, "SheetView") as SheetView;

                SheetView.SheetName = sheetItem.Name;

                List<FarPoint.CalcEngine.FunctionInfo> Infos = FunctionItemInfoUtil.getFunctionItemInfos();
                foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                {
                    SheetView.AddCustomFunction(Info);
                }

                List<JZCell> sheetDataAreaCells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZCell>>(sheetItem.SheetData);
                if (sheetDataAreaCells != null)
                {
                    foreach (JZCell cell in sheetDataAreaCells)
                    {
                        dataCells.Add(SheetView.Cells[cell.Name]);
                    }
                }

                FpSpread.Sheets.Add(SheetView);
                dtCellStyle = ModuleHelperClient.GetCellStyleBySheetID(sheetID);
                for (int i = 0; i < dtCellStyle.Rows.Count; i++)
                {
                    if (dtCellStyle.Rows[i]["CellStyle"] != null)
                    {
                        JZCellStyle CurrentCellStyle = Newtonsoft.Json.JsonConvert.DeserializeObject<JZCellStyle>(dtCellStyle.Rows[i]["CellStyle"].ToString());
                        if (CurrentCellStyle != null)
                        {
                            string strCellName = dtCellStyle.Rows[i]["CellName"].ToString();
                            Cell cell = SheetView.Cells[strCellName];
                            cell.ForeColor = CurrentCellStyle.ForColor;
                            cell.BackColor = CurrentCellStyle.BackColor;
                            cell.Font = new Font(CurrentCellStyle.FamilyName, CurrentCellStyle.FontSize, CurrentCellStyle.FontStyle);
                        }
                    }
                }

                FpSpread.LoadFormulas(true);
                fpSheetEditor1.EnableToolStrip(true);

                UpdateChart();
                UpdateEquation();

            }
            catch (Exception ex)
            {
                MessageBox.Show("加载表单出错！\r\n原因：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.RemoveOwnedForm(ProgressScreen.Current);
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
            }
        }

        public void UpdateChart()
        {
            foreach (SheetView Sheet in FpSpread.Sheets)
            {
                //支持嵌入的图表
                int RowCount = Sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Style);
                int ColumnCount = Sheet.GetLastNonEmptyColumn(NonEmptyItemFlag.Style);
                for (int i = 0; i <= RowCount; i++)
                {
                    for (int j = 0; j <= ColumnCount; j++)
                    {
                        if (Sheet.Cells[i, j].CellType is ChartCellType)
                        {
                            ChartCellType ChartType = Sheet.Cells[i, j].CellType as ChartCellType;
                            Rectangle r = FpSpread.GetCellRectangle(0, 0, i, j);
                            ChartType.ChartSize = r.Size;
                            ChartType.ActiveSheet = Sheet;
                            ChartType.UpdateChart();
                        }
                    }
                }

                //支持浮动的图表
                foreach (IElement Element in Sheet.DrawingContainer.ContainedObjects)
                {
                    if (Element is ChartShape)
                    {
                        ChartShape Shape = Element as ChartShape;
                        Shape.ActiveSheet = Sheet;
                        Shape.Locked = false;
                        Shape.UpdateChart();
                    }
                }
            }
        }

        public void UpdateEquation()
        {
            foreach (SheetView Sheet in FpSpread.Sheets)
            {
                //支持浮动的公式
                foreach (IElement Element in Sheet.DrawingContainer.ContainedObjects)
                {
                    if (Element is EquationShape)
                    {
                        EquationShape Shape = Element as EquationShape;
                        Shape.Locked = false;
                    }
                }
            }
        }

        #region 工具栏

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == SaveSheetButton)
            {
                if (LineStyle)
                {
                    SaveLineStyle();
                }
                else
                {
                    SaveSheetStyle();
                }
            }
            else if (sender == CheckDataZoneButton)
            {
                SheetDataCheckZone();
            }
        }

        private void SaveLineStyle()
        {
            MessageBox.Show("Saved");
        }

        /// <summary>
        /// 保存表单样式
        /// </summary>
        private void SaveSheetStyle()
        {
            try
            {
                Sys_Sheet sheet = new Sys_Sheet();
                sheet.ID = sheetID;
                sheet.Name = SheetName;
                sheet.SheetXML = JZCommonHelper.GZipCompressString(Editor.GetActiveSheetXml());
                List<JZCell> cells = new List<JZCell>();
                List<CellLogic> cellLogicList = new List<CellLogic>();
                List<JZFormulaData> cellFormulasList = new List<JZFormulaData>();
                //bool bIsKeyDescEmpty = false;
                #region CellLogic
                foreach (Cell cell in dataCells)
                {
                    if (cell == null)
                    {
                        continue;
                    }

                    JZCell c = new JZCell();
                    c.Name = cell.Column.Label + cell.Row.Label;
                    c.Value = cell.Value;
                    bool bHasExist = false;
                    foreach (JZCell cc in cells)
                    {
                        if (cc.Name == c.Name)
                        {
                            bHasExist = true;
                            break;
                        }
                    }
                    if (bHasExist==true)
                    {
                        continue;
                    }
                    cells.Add(c);

                    CellLogic cl = new CellLogic();
                    cl.Name = c.Name;
                    JZCellProperty p = cell.Tag as JZCellProperty;
                    if (p != null)
                    {
                        cl.Description = p.Description;
                        cl.IsKey = p.IsKey;
                        cl.IsNotCopy = p.IsNotCopy;
                        cl.IsNotNull = p.IsNotNull;
                        cl.IsPingxing = p.IsPingxing;
                        cl.IsReadOnly = p.IsReadOnly;
                        cl.IsUnique = p.IsUnique;
                        if (cl.IsKey == true && string.IsNullOrEmpty(cl.Description))
                        {
                            //bIsKeyDescEmpty = true;
                            MessageBox.Show(string.Format("单元格{0}的描述不能为空", c.Name));
                            return;
                        }
                    }
                    cellLogicList.Add(cl);
                }
                #endregion
                #region Formulas
                if (FpSpread.Sheets[0] != null)
                {
                    for (int j = 0; j < FpSpread.Sheets[0].ColumnCount; j++)
                    {
                        for (int m = 0; m < FpSpread.Sheets[0].RowCount; m++)
                        {
                            Cell c = FpSpread.Sheets[0].Cells[m, j];
                            JZFormulaData cl = new JZFormulaData();
                            if (c != null)
                            {
                                if (!string.IsNullOrEmpty(c.Formula))
                                {
                                    //logger.Error(string.Format("Name:{0} ColumnIndex:{1} RowIndex:{2} Formula:{3} ", ((char)('A' + c.Column.Index)).ToString() + (c.Row.Index + 1).ToString(), c.Column.Index, c.Row.Index, c.Formula));
                                    cl.ColumnIndex = c.Column.Index;
                                    cl.RowIndex = c.Row.Index;
                                    cl.Formula = c.Formula;
                                    cellFormulasList.Add(cl);
                                }
                            }

                        }
                    }
                }
                #endregion
                sheet.SheetData = Newtonsoft.Json.JsonConvert.SerializeObject(cells);
                sheet.CellLogic = JZCommonHelper.GZipCompressString(Newtonsoft.Json.JsonConvert.SerializeObject(cellLogicList));
                sheet.Formulas = JZCommonHelper.GZipCompressString(Newtonsoft.Json.JsonConvert.SerializeObject(cellFormulasList));

                if (ModuleHelperClient.SaveSheet(sheet))
                {
                    MessageBox.Show("保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("保存失败。" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion 工具栏

        #region 快捷菜单

        private void InitContextMenu()
        {
            ToolStripMenuItem MenuItem = new ToolStripMenuItem("设置表单数据项");
            MenuItem.Tag = "@设置表单数据项";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("查看表单数据项");
            MenuItem.Tag = "@查看表单数据项";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            MenuItem = new ToolStripMenuItem("设置下拉单元格");
            MenuItem.Tag = "@设置下拉单元格";
            contextMenuStrip1.Items.Add(MenuItem);

            ToolStripMenuItem SubMenuItem = new ToolStripMenuItem("字典参照");
            SubMenuItem.Tag = "@字典参照";
            MenuItem.DropDownItems.Add(SubMenuItem);
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            SubMenuItem = new ToolStripMenuItem("表单参照");
            SubMenuItem.Tag = "@表单参照";
            MenuItem.DropDownItems.Add(SubMenuItem);
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("设置图表单元格");
            MenuItem.Tag = "@设置图表单元格";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            MenuItem = new ToolStripMenuItem("清除单元格类型");
            MenuItem.Tag = "@清除单元格类型";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("单元格类型属性");
            MenuItem.Tag = "@单元格类型属性";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("设置自定义的单元格样式");
            MenuItem.Tag = "@设置自定义的单元格样式";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("查看自定义的单元格样式");
            MenuItem.Tag = "@查看自定义的单元格样式";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("取消数据区");
            MenuItem.Tag = "@取消数据区";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            MenuItem = new ToolStripMenuItem("剪切");
            MenuItem.Tag = "@Cut";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("复制");
            MenuItem.Tag = "@Copy";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("粘贴");
            MenuItem.Tag = "@Paste";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            contextMenuStrip1.Opening += new CancelEventHandler(contextMenuStrip1_Opening);
        }

        void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip ContextMenu = sender as ContextMenuStrip;
            foreach (ToolStripItem Item in ContextMenu.Items)
            {
                if (Item is ToolStripMenuItem)
                {
                    switch (Item.Tag.ToString().ToLower())
                    {
                        case "@设置下拉单元格":
                            Item.Enabled = (FpSpread.ActiveSheet.ActiveCell.CellType is DownListCellType);
                            break;
                        case "@设置图表单元格":
                            Item.Enabled = (FpSpread.ActiveSheet.ActiveCell.CellType is ChartCellType);
                            break;
                    }
                }
            }
        }

        void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem Item = sender as ToolStripItem;
            switch (Item.Tag.ToString().ToLower())
            {
                case "@cut":
                    fpSheetEditor1.ShearData();
                    break;
                case "@copy":
                    fpSheetEditor1.CopyData();
                    break;
                case "@paste":
                    fpSheetEditor1.PasteData();
                    break;
                case "@设置表单数据项":
                    SettingSheetDataItem();
                    break;
                case "@查看表单数据项":
                    EditingSheetDataItem();
                    break;
                case "@清除单元格类型":
                    fpSheetEditor1.ClearCellType();
                    break;
                case "@单元格类型属性":
                    ShowPropertyDialog();
                    break;
                case "@设置自定义的单元格样式":
                    ShowCellStyleSettingDialog();
                    break;
                case "@查看自定义的单元格样式":
                    ShowCellStyleShowDialog();
                    break;
                case "@字典参照":
                    ShowDictionaryReferenceDialog();
                    break;
                case "@表单参照":
                    ShowSheetReferenceDialog();
                    break;
                case "@设置图表单元格":
                    ShowChartEditor();
                    break;
                case "@取消数据区":
                    RemoveDataArea();
                    break;
            }
        }

        private void RemoveDataArea()
        {
            if (FpSpread.ActiveSheet.ActiveCell != null)
            {
                FpSpread.ActiveSheet.ActiveCell.BackColor = Color.White;
                if (dataCells.Contains(FpSpread.ActiveSheet.ActiveCell))
                {
                    dataCells.Remove(FpSpread.ActiveSheet.ActiveCell);
                }
            }
        }

        private void ShowChartEditor()
        {
            if (FpSpread.ActiveSheet.ActiveCell.CellType is ChartCellType)
            {
                ChartCellType ChartCellType = FpSpread.ActiveSheet.ActiveCell.CellType as ChartCellType;
                ChartCellType.ShowChartEditor();
            }
        }

        private void ShowDictionaryReferenceDialog()
        {
            if (FpSpread.ActiveSheet.ActiveCell.CellType is DownListCellType)
            {
                DownListCellType CellType = FpSpread.ActiveSheet.ActiveCell.CellType as DownListCellType;


                int RowIndex = FpSpread.ActiveSheet.ActiveRowIndex;
                int ColumnIndex = FpSpread.ActiveSheet.ActiveColumnIndex;
                DictionaryReferenceDialog ReferenceDialog = new DictionaryReferenceDialog(RowIndex, ColumnIndex, CellType);
                ReferenceDialog.ShowDialog();
            }
        }

        private void ShowSheetReferenceDialog()
        {
            if (FpSpread.ActiveSheet.ActiveCell.CellType is DownListCellType)
            {
                DownListCellType CellType = FpSpread.ActiveSheet.ActiveCell.CellType as DownListCellType;

                SheetConfiguration SheetConfiguration = FpSpread.ActiveSheet.Tag as SheetConfiguration;
                int RowIndex = FpSpread.ActiveSheet.ActiveRowIndex;
                int ColumnIndex = FpSpread.ActiveSheet.ActiveColumnIndex;
                //SheetReferenceDialog ReferenceDialog = new SheetReferenceDialog(SheetConfiguration, RowIndex, ColumnIndex, CellType);
                //ReferenceDialog.ShowDialog();
            }
        }

        private void ShowPropertyDialog()
        {
            PropertiesDialog PropertiesDialog = new PropertiesDialog();
            PropertiesDialog.Owner = this;
            PropertiesDialog.SelectObject(FpSpread.ActiveSheet.ActiveCell.CellType);
            PropertiesDialog.Show();
        }
        private void ShowCellStyleShowDialog()
        {
            CellStyleShowDialog PropertiesDialog = new CellStyleShowDialog(sheetID, dtCellStyle);
            PropertiesDialog.Owner = this;
            PropertiesDialog.Show();
        }
        private void ShowCellStyleSettingDialog()
        {
            string CellName = string.Empty;
            CellName = FpSpread.ActiveSheet.ActiveCell.Column.Label + FpSpread.ActiveSheet.ActiveCell.Row.Label;
            DataRow[] dr = dtCellStyle.Select(string.Format("CellName='{0}'", CellName));
            DataRow row = null;
            if (dr != null && dr.Length > 0)
            {
                row = dr[0];
                CurrentCellStyle = Newtonsoft.Json.JsonConvert.DeserializeObject<JZCellStyle>(row["CellStyle"] == null ? null : row["CellStyle"].ToString());
            }
            else
            {
                row = dtCellStyle.NewRow();
                CurrentCellStyle = new JZCellStyle();
                Cell cell = FpSpread.ActiveSheet.Cells[CellName];
                CurrentCellStyle.BackColor = cell.BackColor;
                if (cell.Font != null)
                {
                    CurrentCellStyle.FamilyName = cell.Font.FontFamily.Name;
                    CurrentCellStyle.FontSize = cell.Font.Size;
                    CurrentCellStyle.FontStyle = cell.Font.Style;
                    CurrentCellStyle.ForColor = cell.ForeColor;
                }
                dtCellStyle.Rows.Add(row);
            }
            CellStyleSetting CellStyleSetting = new CellStyleSetting(sheetID, CellName, CurrentCellStyle);
            CellStyleSetting.Owner = this;
            //CellStyleSetting.SelectObject(FpSpread.ActiveSheet.ActiveCell.CellType);
            if (CellStyleSetting.ShowDialog() == DialogResult.OK)
            {
                Cell cell = FpSpread.ActiveSheet.Cells[CellName];
                cell.ForeColor = CurrentCellStyle.ForColor;
                cell.BackColor = CurrentCellStyle.BackColor;
                cell.Font = new Font(CurrentCellStyle.FamilyName, CurrentCellStyle.FontSize, CurrentCellStyle.FontStyle);
                row["CSID"] = 0;
                row["SheetID"] = sheetID;
                row["CellName"] = CellName;
                row["CellStyle"] = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentCellStyle);
            }
        }

        /// <summary>
        /// 设置数据项
        /// </summary>
        private void SettingSheetDataItem()
        {
            List<Cell> cells = new List<Cell>();
            foreach (CellRange Range in Editor.Selections)
            {
                for (int Row = 0; Row < Range.RowCount; Row++)
                {
                    for (int Column = 0; Column < Range.ColumnCount; Column++)
                    {
                        int absRow = Range.Row + Row;
                        int absColumn = Range.Column + Column;

                        string Tag = Arabic_Numerals_Convert.Excel_Word_Numerals(absColumn) + (absRow + 1);
                        cells.Add(FpSpread.ActiveSheet.Cells[Tag]);
                    }
                }
            }
            if (cells.Count > 0)
            {
                SheetDataItemDialog Dialog = new SheetDataItemDialog(this, cells);
                Dialog.ShowDialog();
            }
        }

        /// <summary>
        /// 查看数据项
        /// </summary>
        private void EditingSheetDataItem()
        {
            DataItemsViewDialog DataItemsViewDialog = new DataItemsViewDialog(this, dataCells);
            if (DataItemsViewDialog.ShowDialog() == DialogResult.OK)
            {
                dataCells = DataItemsViewDialog.cells;
            }
        }

        private void SheetDataCheckZone()
        {
            String msg = "未发现问题！";
            foreach (Cell cell in dataCells)
            {
                JZCellProperty p = cell.Tag as JZCellProperty;
                if (p == null || cell.CellType == null)
                {
                    cell.BackColor = Color.Red;
                    msg = "红色单元格未设置类型属性!";
                }
                else
                {
                    cell.BackColor = Color.White;
                }
            }
            MessageBox.Show(msg);
        }

        private void fpSheetEditor1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MyCell fpSpread = fpSheetEditor1.FpSpread;
                if (FpSpread.HitTest(e.X, e.Y).Type == HitTestType.Viewport)
                {
                    contextMenuStrip1.Show(fpSpread, fpSpread.PointToClient(Cursor.Position));
                }
            }
        }

        #endregion 快捷菜单

        private void fpSheetEditor1_CellTypeSetting(object sender, CellTypeEventArgs e)
        {
            if (e.CellType is ChartCellType)
            {
                ChartCellType ChartCellType = e.CellType as ChartCellType;
                ChartCellType.ActiveSheet = FpSpread.ActiveSheet;
            }

            IGetFieldType FieldTypeGetter = e.CellType as IGetFieldType;


            CellRange[] CellRanges = FpSpread.ActiveSheet.GetSelections();

            foreach (CellRange Range in CellRanges)
            {
                int Row = Range.Row;
                int RowCount = Range.RowCount;

                int Column = Range.Column;
                int ColumnCount = Range.ColumnCount;

                if (Row < 0)
                {
                    Row = 0;
                }
                if (Column < 0)
                {
                    Column = 0;
                }
                if (RowCount < 0)
                {
                    RowCount = FpSpread.ActiveSheet.RowCount;
                }
                if (ColumnCount < 0)
                {
                    ColumnCount = FpSpread.ActiveSheet.ColumnCount;
                }
                Cell cell = FpSpread.ActiveSheet.Cells[Row, Column, Row + RowCount - 1, Column + ColumnCount - 1];
                if (cell != null)
                {
                    cell.CellType = e.CellType;
                }
            }
        }

        private void showDataArea_Click(object sender, EventArgs e)
        {
            if (showDataArea.Text == "显示数据区")
            {
                foreach (Cell cell in dataCells)
                {
                    if (cell == null)
                    {
                        continue;
                    }

                    cell.BackColor = Color.LightPink;
                }
                showDataArea.Text = "取消显示数据区";
            }
            else
            {
                for (int i = 0; i < FpSpread.ActiveSheet.Rows.Count; i++)
                {
                    for (int j = 0; j < FpSpread.ActiveSheet.ColumnCount; j++)
                    {
                        FpSpread.ActiveSheet.Cells[i, j].BackColor = Color.White;
                    }
                }
                showDataArea.Text = "显示数据区";
            }
        }

    }
}
