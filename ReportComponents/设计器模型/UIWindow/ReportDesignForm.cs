using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using ReportCommon;
using ReportCommon.Chart;

namespace ReportComponents
{
    public partial class ReportDesignForm : DockContent
    {
        private GeneralCellType GeneralType = new GeneralCellType();
        private TextCellType TextType = new TextCellType();

        public ReportDesignForm()
        {
            InitializeComponent();

            ReportDesignPanel.ActiveSheet.Protect = true;
            ReportDesignPanel.ReportElementAdding += new EventHandler<ReportElementEventArgs>(reportDesigner1_ReportElementAdding);
            ReportDesignPanel.CellStyleChanged += new EventHandler<SheetViewEventArgs>(ReportDesignPanel_CellStyleChanged);
            ReportDesignPanel.MouseUp += new MouseEventHandler(reportDesigner1_MouseUp);
            //ReportDesignPanel.FpSpread.EditModeOff += new EventHandler(FpSpread_EditModeOff);
            //ReportDesignPanel.FpSpread.CellDoubleClick += new CellClickEventHandler(FpSpread_CellDoubleClick);
            //ReportDesignPanel.FpSpread.UserFormulaEntered += new UserFormulaEnteredEventHandler(FpSpread_UserFormulaEntered);
            //ReportDesignPanel.FpSpread.Sheets.Changed += new CollectionChangeEventHandler(Sheets_Changed);
        }

        void Sheets_Changed(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
            {
                SheetView sheet = e.Element as SheetView;
                sheet.PropertyChanged -= new SheetViewPropertyChangeEventHandler(ActiveSheet_PropertyChanged);
                sheet.PropertyChanged += new SheetViewPropertyChangeEventHandler(ActiveSheet_PropertyChanged);
            }
        }

        public ReportWindow ReportWindow
        {
            get
            {
                return DockPanel.Parent as ReportWindow;
            }
        }

        public Report Report
        {
            get
            {
                return ReportWindow.report;
            }
        }

        public SheetView ActiveSheet
        {
            get
            {
                return ReportDesignPanel.ActiveSheet;
            }
        }

        public FpSpread FpSpread
        {
            get
            {
                return ReportDesignPanel.FpSpread;
            }
        }

        public String ActiveElement
        {
            get
            {
                if (ReportDesignPanel.ActiveSheet.ActiveCell.Tag == null || ReportDesignPanel.ActiveSheet.ActiveCell.Tag.ToString() == "")
                    ReportDesignPanel.ActiveSheet.ActiveCell.Tag = Guid.NewGuid().ToString();
                return ReportDesignPanel.ActiveSheet.ActiveCell.Tag.ToString();
            }
        }

        private void ReportDesignForm_Load(object sender, EventArgs e)
        {
            InitContextMenu();
        }

        void ActiveSheet_PropertyChanged(object sender, SheetViewPropertyChangeEventArgs e)
        {
            if (e.PropertyName == "RowCount" || e.PropertyName == "ColumnCount")
            {
                Report.RefreshReport(ActiveSheet);
            }
        }

        #region 快捷菜单

        private void InitContextMenu()
        {
            ToolStripMenuItem MenuItem = new ToolStripMenuItem("单元格元素");
            MenuItem.Tag = "@ReportElement";
            contextMenuStrip1.Items.Add(MenuItem);

            ToolStripMenuItem SubMenuItem = new ToolStripMenuItem("数据列");
            SubMenuItem.Tag = "@DataColumn";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("普通文本");
            SubMenuItem.Tag = "@PlainText";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("公式");
            SubMenuItem.Tag = "@Formula";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("图片");
            SubMenuItem.Tag = "@Picture";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("斜线");
            SubMenuItem.Tag = "@Slash";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("变量");
            SubMenuItem.Tag = "@Variable";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("图表");
            SubMenuItem.Tag = "@Chart";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            MenuItem = new ToolStripMenuItem("样式");
            MenuItem.Tag = "@FormatStyle";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("条件格式化");
            MenuItem.Tag = "@ConditionFormat";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            MenuItem = new ToolStripMenuItem("分页属性");
            MenuItem.Tag = "@PageBreak";
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

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            MenuItem = new ToolStripMenuItem("清除");
            MenuItem.Tag = "@Clear";
            contextMenuStrip1.Items.Add(MenuItem);

            SubMenuItem = new ToolStripMenuItem("全部");
            SubMenuItem.Tag = "@ClearAll";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("格式");
            SubMenuItem.Tag = "@ClearForamtStyle";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem("内容");
            SubMenuItem.Tag = "@ClearContent";
            SubMenuItem.ShortcutKeyDisplayString = "Delete";
            SubMenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);
        }

        void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null && MenuItem.Tag != null)
            {
                switch (MenuItem.Tag.ToString().ToLower())
                {
                    case "@datacolumn":
                        EditElement(new DataColumn());
                        break;
                    case "@plaintext":
                        EditElement(new LiteralText());
                        break;
                    case "@formula":
                        EditElement(new Formula());
                        break;
                    case "@picture":
                        EditElement(new Picture());
                        break;
                    case "@slash":
                        EditElement(new Slash());
                        break;
                    case "@variable":
                        EditElement(new Variable());
                        break;
                    case "@chart":
                        EditElement(new ChartPainter());
                        break;
                    case "@formatstyle":
                        ReportDesignPanel.SettingCellStyle();
                        break;
                    case "@conditionformat":
                        ReportDesignPanel.SettingConditionFormat();
                        break;
                    case "@cut":
                        ReportDesignPanel.ShearData();
                        break;
                    case "@copy":
                        ReportDesignPanel.CopyData();
                        break;
                    case "@paste":
                        ReportDesignPanel.PasteData();
                        break;
                    case "@clearall":
                        ClearAll();
                        break;
                    case "@clearformatstyle":
                        ClearFormatStyle();
                        break;
                    case "@clearcontent":
                        ClearContent();
                        break;
                    case "@pagebreak":
                        ShowPageBreakForm();
                        break;
                }
            }
        }

        public void EnableToolStripButton(Boolean Enabled)
        {
            this.ParameterButton.Enabled = Enabled;
        }

        /// <summary>
        /// 设置分页属性
        /// </summary>
        private void ShowPageBreakForm()
        {
            PageElementDialog form = new PageElementDialog(ReportWindow);
            form.ShowDialog();
        }

        /// <summary>
        /// 清除所有
        /// </summary>
        private void ClearAll()
        {
            Cell Cell = ReportDesignPanel.ActiveSheet.ActiveCell;
            String Index = Report.GetElementIndex(Cell.Row.Index, Cell.Column.Index);
            GridElement Element = Report.GetElement(Index);
            if (Element != null)
            {
                Report.DelElement(Element);
            }
        }

        /// <summary>
        /// 清除格式
        /// </summary>
        private void ClearFormatStyle()
        {
            Cell Cell = ReportDesignPanel.ActiveSheet.ActiveCell;
            String Index = Report.GetElementIndex(Cell.Row.Index, Cell.Column.Index);
            GridElement Element = Report.GetElement(Index);
            if (Element != null)
            {
                Report.DelStyle(Element);
            }
        }

        /// <summary>
        /// 清除内容
        /// </summary>
        private void ClearContent()
        {
            Cell Cell = ReportDesignPanel.ActiveSheet.ActiveCell;
            String Index = Report.GetElementIndex(Cell.Row.Index, Cell.Column.Index);
            GridElement Element = Report.GetElement(Index);
            if (Element != null)
            {
                Report.DelContent(Element);
            }
        }

        private CellRange getActiveCell(int row, int col)
        {
            CellRange cr = ActiveSheet.GetSpanCell(row, col);
            if (cr == null)
            {
                Cell Cell = ActiveSheet.Cells[row,col];
                cr = new CellRange(Cell.Row.Index, Cell.Column.Index, Cell.RowSpan, Cell.ColumnSpan);
            }

            return cr;
        }

        private void EditElement(object value)
        {
            Cell Cell = ActiveSheet.ActiveCell;
            CellRange Range = getActiveCell(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            GridElement Element = Report.GetElement(ActiveElement);
            if (Element == null)
            {
                Element = new GridElement();
            }

            Element.Index = ActiveElement;
            Element.Row = Range.Row;
            Element.Column = Range.Column;
            Element.RowSpan = Range.RowCount;
            Element.ColumnSpan = Range.ColumnCount;
            Element.Report = ActiveSheet;
            Element.Value = value;

            if (!(Element.Value is LiteralText))
            {
                Cell.CellType = GeneralType;
            }
            else
            {
                Cell.CellType = TextType;
            }

            Report.SetElement(Element);

            EditElement(Element, ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
        }

        #endregion 快捷菜单

        void ReportDesignPanel_CellStyleChanged(object sender, SheetViewEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                for (int j = 0; j < e.ColumnCount; j++)
                {
                    GridElement Element = ActiveSheet.Cells[i + e.Row, j + e.Column].Value as GridElement;
                    if (Element == null)
                    {
                        Element = new GridElement();
                        Element.Index = Report.GetElementIndex(i + e.Row, j + e.Column);

                        CellRange Range = getActiveCell(i + e.Row, j + e.Column);
                        Element.Row = Range.Row;
                        Element.Column = Range.Column;
                        Element.RowSpan = Range.RowCount;
                        Element.ColumnSpan = Range.ColumnCount;
                        Element.Report = ActiveSheet;
                    }

                    if (Element.Value == null)
                        ActiveSheet.Cells[i + e.Row, j + e.Column].CellType = GeneralType;

                    Report.SetElement(Element);
                }
            }
        }

        void FpSpread_UserFormulaEntered(object sender, UserFormulaEnteredEventArgs e)
        {
            GridElement Element = ActiveSheet.Cells[e.Row, e.Column].Value as GridElement;
            if (Element == null)
            {
                Cell Cell = ActiveSheet.Cells[e.Row, e.Column];
                CellRange Range = getActiveCell(e.Row, e.Column);
                Element = new GridElement();
                Element.Index = Report.GetElementIndex(e.Row, e.Column);
                Element.Row = Range.Row;
                Element.Column = Range.Column;
                Element.RowSpan = Range.RowCount;
                Element.ColumnSpan = Range.ColumnCount;
                Element.Report = ActiveSheet;

                Cell.VerticalAlignment = CellVerticalAlignment.Center;
                Cell.HorizontalAlignment = CellHorizontalAlignment.Center;
                Cell.Value = Element;
            }

            Formula Formula;
            if (!(Element.Value is Formula))
            {
                Formula = new Formula();
                Element.Value = Formula;
            }
            else
            {
                Formula = Element.Value as Formula;
            }

            Formula.Expression = Report.ReportSheet.Cells[e.Row, e.Column].Formula;
            Report.SetElement(Element);

            Update(e.Row, e.Column);
        }

        void reportDesigner1_ReportElementAdding(object sender, ReportComponents.ReportElementEventArgs e)
        {
            EditElement(e.Value);
        }

        private void FpSpread_EditModeOff(object sender, EventArgs e)
        {
            //CellRange Range = getActiveCell(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            //string Index = Report.GetElementIndex(Range.Row, Range.Column);
            //GridElement Element = Report.GetElement(Index);
            //if (Element == null)
            //{
            //    Element = new GridElement();
            //}

            //Element.Index = Index;
            //Element.Row = Range.Row;
            //Element.Column = Range.Column;
            //Element.RowSpan = Range.RowCount;
            //Element.ColumnSpan = Range.ColumnCount;
            //Element.Report = ActiveSheet;

            //LiteralText literalText = null;
            //if (!(Element.Value is LiteralText))
            //{
            //    literalText = new LiteralText();
            //    Element.Value = literalText;
            //}
            //else
            //{
            //    literalText = Element.Value as LiteralText;
            //}

            //literalText.Text = FpSpread.EditingControl.Text;
            //Report.SetElement(Element);
        }

        void FpSpread_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            object value = ActiveSheet.Cells[e.Row, e.Column].Value;
            EditElement(value, e.Row, e.Column);

            e.Cancel = (value is GridElement);
        }

        void EditElement(object value, int RowIndex, int ColumnIndex)
        {
            GridElement Element = value as GridElement;
            if (Element != null)
            {
                if (Element.Value is DataColumn)
                {
                    DataColumnEditor dataColumnEditor = new DataColumnEditor(Report, ActiveElement, Element);
                    if (DialogResult.OK == dataColumnEditor.ShowDialog())
                    {
                        ActiveSheet.Cells[ActiveElement].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].VerticalAlignment = CellVerticalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].Value = dataColumnEditor.ReportElement;

                        Cell Cell = ActiveSheet.Cells[ActiveElement];
                        Update(Cell.Row.Index, Cell.Column.Index);
                    }
                }
                else if (Element.Value is Picture)
                {
                    ImageEditor imageEditor = new ImageEditor(Report, ActiveElement, Element);
                    if (DialogResult.OK == imageEditor.ShowDialog())
                    {
                        ActiveSheet.Cells[ActiveElement].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].VerticalAlignment = CellVerticalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].Value = imageEditor.ReportElement;

                        Cell Cell = ActiveSheet.Cells[ActiveElement];
                        Update(Cell.Row.Index, Cell.Column.Index);
                    }
                }
                else if (Element.Value is Slash)
                {
                    SlashEditor slashEditor = new SlashEditor(Report, ActiveElement, Element);
                    if (DialogResult.OK == slashEditor.ShowDialog())
                    {
                        ActiveSheet.Cells[ActiveElement].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].VerticalAlignment = CellVerticalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].Value = slashEditor.ReportElement;

                        Cell Cell = ActiveSheet.Cells[ActiveElement];
                        Update(Cell.Row.Index, Cell.Column.Index);
                    }
                }
                else if (Element.Value is Formula)
                {
                    FormulaEditorUI FormulaEditorUI = new FormulaEditorUI(ActiveSheet);
                    FormulaEditorUI.ShowInTaskbar = false;

                    Formula Formula = Element.Value as Formula;

                    FormulaEditorUI.SetFormula(Formula.Expression);
                    if (DialogResult.OK == FormulaEditorUI.ShowDialog())
                    {
                        Formula.Expression = FormulaEditorUI.GetFormula();

                        ActiveSheet.Cells[ActiveElement].Formula = FormulaEditorUI.GetFormula();
                        ActiveSheet.Cells[ActiveElement].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].VerticalAlignment = CellVerticalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].Value = Element;

                        Cell Cell = ActiveSheet.Cells[ActiveElement];
                        Update(Cell.Row.Index, Cell.Column.Index);
                    }
                }
                else if (Element.Value is LiteralText)
                {
                    Cell Cell = ActiveSheet.Cells[ActiveElement];
                    if (!(Cell.CellType is TextCellType))
                    {
                        Cell.CellType = new TextCellType();
                    }

                    EventArgs eventArgs = new EventArgs();
                    FpSpread.StartCellEditing(eventArgs, false);
                }
                else if (Element.Value is Variable)
                {
                    VariableEditor VariableEditor = new VariableEditor(Report.Configuration.ReportParameters, Element);
                    if (DialogResult.OK == VariableEditor.ShowDialog())
                    {
                        ActiveSheet.Cells[ActiveElement].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].VerticalAlignment = CellVerticalAlignment.Center;
                        ActiveSheet.Cells[ActiveElement].Value = VariableEditor.ReportElement;

                        Cell Cell = ActiveSheet.Cells[ActiveElement];
                        Update(Cell.Row.Index, Cell.Column.Index);
                    }
                }
                else if (Element.Value is ChartPainter)
                {
                    ChartEditor ChartEditor = new ChartEditor();
                    ChartEditor.ChartPainter = Element.Value as ChartPainter;
                    if (DialogResult.OK == ChartEditor.ShowDialog())
                    {
                        Element.Value = ChartEditor.ChartPainter;

                        Cell Cell = ActiveSheet.Cells[ActiveElement];
                        Update(Cell.Row.Index, Cell.Column.Index);
                    }
                }
            }
            else
            {
                Cell Cell = ActiveSheet.Cells[ActiveElement];
                if (!(Cell.CellType is TextCellType))
                {
                    Cell.CellType = new TextCellType();
                }

                EventArgs eventArgs = new EventArgs();
                FpSpread.StartCellEditing(eventArgs, false);
            }
        }

        private void Update(int Row, int Column)
        {
            int RowViewportIndex = FpSpread.GetActiveRowViewportIndex();
            int ColumnViewportIndex = FpSpread.GetActiveColumnViewportIndex();
            Rectangle rect = FpSpread.GetCellRectangle(RowViewportIndex, ColumnViewportIndex, Row, Column);

            FpSpread.Invalidate(rect);
        }

        private void reportDesigner1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(FpSpread, FpSpread.PointToClient(Cursor.Position));
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == ParameterButton)
            {
                ShowParameterDialog();
            }
        }

        private void ShowParameterDialog()
        {
            ParameterDialog ParameterDialog = new ParameterDialog();
            ParameterDialog.Report = Report.Configuration;
            ParameterDialog.ShowDialog();
        }

        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            //{
            //    switch (keyData)
            //    {
            //        case Keys.Delete:
            //            ClearContent();
            //            return true;
            //    }
            //}

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
