using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using FarPoint.Win;
using System.IO;
using FarPoint.Win.Spread.Design;
using Yqun.Common.Encoder;
using ReportCommon;
using Yqun.Client.BizUI;
using ReportCommon.Chart;

namespace ReportComponents
{
    public partial class ReportDesignUI : UserControl
    {
        #region 常量

        const int Incremental = 4;
        string DefaultFontFamily = "宋体";
        string DefaultFontSize = "五号";

        GeneralCellType general = new GeneralCellType();

        #endregion 常量

        #region 网格线

        GridLine VisbleGridLine = new GridLine(GridLineType.Flat);
        GridLine HiddenGridLine = new GridLine(GridLineType.None);

        #endregion 网格线

        public ReportDesignUI()
        {
            InitializeComponent();
            EnableToolStrip(false);
            fpSpread.EditMode = false;

            InitToolStripItems();
        }

        public string GetActiveSheetXml()
        {
            for (int i = 0; i < ActiveSheet.Rows.Count; i++)
            {
                for (int j = 0; j < ActiveSheet.ColumnCount; j++)
                {
                    GridElement e = ActiveSheet.Cells[i, j].Value as GridElement;
                    if (e != null)
                    {
                        ActiveSheet.Cells[i, j].Value = e.Value == null ? "" : e.Value.ToString();
                    }
                }
            }
            return Serializer.GetObjectXml(ActiveSheet, "SheetView");
        }

        [Browsable(false)]
        public List<CellRange> Selections
        {
            get
            {
                return FpSpread.Selections;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SheetView ActiveSheet
        {
            get
            {
                return FpSpread.ActiveSheet;
            }
            set
            {
                if (value == null)
                    return;

                if (value == FpSpread.ActiveSheet)
                    return;

                fpSpread.SuspendLayout();
                fpSpread.Sheets.Clear();
                fpSpread.Sheets.Add(value);
                value.SelectionPolicy = SelectionPolicy.MultiRange;
                value.AutoCalculation = false;
                EnableToolStrip(true);
                fpSpread.ResumeLayout();
            }
        }

        [Browsable(false)]
        public MyCell FpSpread
        {
            get
            {
                return fpSpread;
            }
        }

        protected void InitToolStripItems()
        {
            CellTools.Items.Clear();
            ToolStripTextBox TextBox = new ToolStripTextBox();
            TextBox.Enabled = false;
            TextBox.ForeColor = Color.Black;
            TextBox.BackColor = Color.White;
            TextBox.BorderStyle = BorderStyle.FixedSingle;
            TextBox.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(ActiveSheet.ActiveColumnIndex) + (ActiveSheet.ActiveRowIndex + 1).ToString();
            TextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            CellTools.Items.Add(TextBox);

            CellTools.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem MenuItem = new ToolStripMenuItem();
            MenuItem.Text = "  类型  ";
            MenuItem.ToolTipText = "报表元素类型";
            CellTools.Items.Add(MenuItem);

            ToolStripMenuItem SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "数据列";
            SubMenuItem.Click += new EventHandler(SubMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "普通文本";
            SubMenuItem.Click += new EventHandler(SubMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "公式";
            SubMenuItem.Click += new EventHandler(SubMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "图片";
            SubMenuItem.Click += new EventHandler(SubMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "斜线";
            SubMenuItem.Click += new EventHandler(SubMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "变量";
            SubMenuItem.Click += new EventHandler(SubMenuItem_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            CellTools.Items.Add(new ToolStripSeparator());

            MenuItem = new ToolStripMenuItem();
            MenuItem.Text = "  浮动元素类型  ";
            MenuItem.ToolTipText = "浮动元素类型";
            CellTools.Items.Add(MenuItem);

            SubMenuItem = new ToolStripMenuItem();
            SubMenuItem.Text = "图表";
            SubMenuItem.Click += new EventHandler(SubMenuItem_1_Click);
            MenuItem.DropDownItems.Add(SubMenuItem);

            CellTools.Items.Add(new ToolStripSeparator());
        }

        void SubMenuItem_Click(object sender, EventArgs e)
        {
            Object value = null;
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null)
            {
                switch (MenuItem.Text)
                {
                    case "数据列":
                        value = new ReportCommon.DataColumn();
                        break;
                    case "普通文本":
                        value = new LiteralText();
                        break;
                    case "公式":
                        value = new Formula();
                        break;
                    case "图表":
                        value = new ChartPainter();
                        break;
                    case "图片":
                        value = new ReportCommon.Picture();
                        break;
                    case "斜线":
                        value = new Slash();
                        break;
                    case "变量":
                        value = new Variable();
                        break;
                }

                if (value != null)
                {
                    ActiveSheet.ActiveCell.CellType = general;

                    ReportElementEventArgs elementevent = new ReportElementEventArgs();
                    elementevent.Value = value;
                    OnReportElementAdding(elementevent);
                }
                else
                {
                    String msg = "当前版本不支持" + MenuItem.Text + "报表元素。";
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void SubMenuItem_1_Click(object sender, EventArgs e)
        {
            Object value = null;
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null)
            {
                switch (MenuItem.Text)
                {
                    case "图表":
                        FloatElement Element = new FloatElement();
                        ActiveSheet.AddShape(Element);

                        Element.setName(string.Format("图表_{0}", ActiveSheet.DrawingContainer.ContainedObjects.Count));

                        value = new ChartPainter();
                        Element.setValue(value);

                        Rectangle RowHeaderRectangle = FpSpread.GetRowHeaderRectangle(0);
                        Rectangle ColumnHeaderRectangle = FpSpread.GetColumnHeaderRectangle(0);
                        Point point = new Point(Element.Left + RowHeaderRectangle.Width, Element.Top + ColumnHeaderRectangle.Height);
                        CellRange range = FpSpread.GetCellFromPixel(0, 0, point.X, point.Y);
                        Rectangle r = FpSpread.GetCellRectangle(0, 0, range.Row, range.Column);
                        Element.setRow(range.Row);
                        Element.setColumn(range.Column);
                        Element.setLeftDistance(point.X - r.Left);
                        Element.setTopDistance(point.Y - r.Top);
                        break;
                }

                if (value == null)
                {
                    String msg = "当前版本不支持" + MenuItem.Text + "报表浮动元素。";
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 控件加载时调用这个更新工具栏
        /// </summary>
        protected void InitFpspreadStatus()
        {
            //初始化字体样式
            InitFontFamily();
            //初始化字体大小
            InitFontSize();
        }

        /// <summary>
        /// 选中单元格时调用这个更新工具栏
        /// </summary>
        protected void RefreshFspreadStatus()
        {
            if (CellTools.Items.Count > 0)
            {
                CellTools.Items[0].Text = Arabic_Numerals_Convert.Excel_Word_Numerals(ActiveSheet.ActiveColumnIndex) + (ActiveSheet.ActiveRowIndex + 1).ToString();
            }

            Font font = ActiveSheet.ActiveCell.Font;
            if (font != null)
            {
                FontFamilyComboBox.SelectedIndex = FontFamilyComboBox.Items.IndexOf(font.Name);

                int x = -1;
                string[] Names = FontSizeFactory.FontSize.Sizes();
                for (int i = 0; i < Names.Length; i++)
                {
                    if (font.Size == FontSizeFactory.FontSize.GetSize(Names[i]))
                    {
                        x = i;
                        break;
                    }
                }
                FontSizeComboBox.SelectedIndex = x;

                BoldButton.Checked = font.Bold;
                ItalicButton.Checked = font.Italic;
                UnderlineButton.Checked = font.Underline;
            }
            else
            {
                FontFamilyComboBox.SelectedIndex = FontFamilyComboBox.Items.IndexOf(DefaultFontFamily);
                FontSizeComboBox.SelectedIndex = FontSizeComboBox.Items.IndexOf(DefaultFontSize);
                BoldButton.Checked = false;
                ItalicButton.Checked = false;
                UnderlineButton.Checked = false;
            }

            MiddleCenterButton.Checked = (ActiveSheet.ActiveCell.RowSpan != 1 || ActiveSheet.ActiveCell.ColumnSpan != 1);
        }

        private void InitFontFamily()
        {
            FontFamilyComboBox.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            FontFamilyComboBox.ComboBox.DrawItem -= new DrawItemEventHandler(ComboBoxLetters_DrawItem);
            FontFamilyComboBox.ComboBox.DrawItem += new DrawItemEventHandler(ComboBoxLetters_DrawItem);

            FontFamily[] families = FontFamily.Families;
            Font familiesFont;
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    familiesFont = new Font(family, 10F);
                    FontFamilyComboBox.Items.Add(familiesFont.Name);
                }
            }

            FontFamilyComboBox.SelectedIndex = FontFamilyComboBox.Items.IndexOf(DefaultFontFamily);
        }

        private void InitFontSize()
        {
            string[] FontSize = FontSizeFactory.FontSize.Sizes();
            foreach (string Name in FontSize)
            {
                FontSizeComboBox.Items.Add(Name);
            }

            FontSizeComboBox.SelectedIndex = FontSizeComboBox.Items.IndexOf(DefaultFontSize);
        }

        private void ComboBoxLetters_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1 || e.Index >= FontFamilyComboBox.Items.Count)
                return;

            e.DrawBackground();

            if ((e.State & DrawItemState.Focus) != 0)
                e.DrawFocusRectangle();
            Brush b = null;

            try
            {
                b = new SolidBrush(e.ForeColor);
                e.Graphics.DrawString(FontFamilyComboBox.Items[e.Index].ToString(), new Font(FontFamilyComboBox.Items[e.Index].ToString(), 10F), b, e.Bounds);
            }
            finally
            {
                if (b != null)
                    b.Dispose();
                b = null;
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == SaveButton)
            {
                SaveFileDialog FileDialog = new SaveFileDialog();
                FileDialog.Filter = "xml files (*.xml)|*.xml";
                FileDialog.FilterIndex = 2;
                FileDialog.InitialDirectory = Application.StartupPath;
                FileDialog.RestoreDirectory = true;
                if (FileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (TextWriter sw = new StreamWriter(FileDialog.FileName))
                    {
                        sw.Write(GetActiveSheetXml());
                        sw.Close();
                    }
                }
            }
            else if (sender == LoadButton)
            {
                OpenFileDialog FileDialog = new OpenFileDialog();
                FileDialog.Filter = "xml files (*.xml)|*.xml";
                FileDialog.FilterIndex = 2;
                FileDialog.InitialDirectory = Application.StartupPath;
                FileDialog.RestoreDirectory = true;
                if (FileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (TextReader tr = new StreamReader(FileDialog.FileName))
                    {
                        string xmlData = tr.ReadToEnd();
                        SheetView Sheet = Serializer.LoadObjectXml(typeof(SheetView), xmlData, "SheetView") as SheetView;
                        if (Sheet != null)
                        {
                            int SheetIndex = FpSpread.Sheets.IndexOf(ActiveSheet);
                            FpSpread.Sheets.RemoveAt(SheetIndex);
                            FpSpread.Sheets.Insert(SheetIndex, Sheet);
                        }
                    }
                }
            }

            #region 常规工具

            if (sender == ImportExcelButton) //导入Excel文件
            {
                ImportExcelFile();
            }
            else if (sender == ExportExcelButton) //导出Excel文件
            {
                ExportExcelFile();
            }
            else if (sender == ShearButton) //剪切
            {
                ShearData();
            }
            else if (sender == CopyButton) //复制
            {
                CopyData();
            }
            else if (sender == PasteButton) //粘贴
            {
                PasteData();
            }
            else if (sender == PageSettingButton) //页面设置
            {
                SettingPageInfo();
            }
            else if (sender == PrintButton) //打印
            {
                PrintDocument();
            }
            else if (sender == PrintPreviewButton) //打印预览
            {
                PrintPreviewDocument();
            }

            #endregion 常规工具

            #region 编辑工具

            if (sender == InsertRowButton)
            {
                InsertRows();
            }
            else if (sender == DeleteRowButton)
            {
                DeleteRows();
            }
            else if (sender == InsertColumnButton)
            {
                InsertColumns();
            }
            else if (sender == DeleteColumnButton)
            {
                DeleteColumns();
            }
            else if (sender == FreezeRowButton)
            {
                FreezeRows();
            }
            else if (sender == FreezeColumnButton)
            {
                FreezeColumn();
            }
            else if (sender == AddCellCommentsButton)
            {
                AddCellComments();
            }
            else if (sender == RemoveCellCommentsButton)
            {
                RemoveCellComments();
            }
            else if (sender == RemoveAllCommentsButton)
            {
                RemoveAllComments();
            }
            else if (sender == HorizontalGridLineButton)
            {
                HorizontalGridLine();
            }
            else if (sender == VerticalGridLineButton)
            {
                VerticalGridLine();
            }

            #endregion 编辑工具

            #region 单元格风格工具

            if (sender == BoldButton)
            {
                SettingFontBold();
            }
            else if (sender == ItalicButton)
            {
                SettingFontItalic();
            }
            else if (sender == UnderlineButton)
            {
                SettingFontUnderline();
            }
            else if (sender == LeftAlignButton)
            {
                SettingLeftAlign();
            }
            else if (sender == MiddleButton)
            {
                SettingMiddle();
            }
            else if (sender == RightAlignButton)
            {
                SettingRightAlign();
            }
            else if (sender == MiddleCenterButton)
            {
                SettingMiddleCenter();
            }
            else if (sender == BorderButton)
            {
                SettingBorder();
            }
            else if (sender == ForeColorButton)
            {
                SettingForeColor();
            }
            else if (sender == BackColorButton)
            {
                SettingBackColor();
            }
            else if (sender == SpecialCharButton)
            {
                SettingSpecialChars();
            }
            else if (sender == StyleButton)
            {
                SettingCellStyle();
            }
            else if (sender == HighlightButton)
            {
                SettingConditionFormat();
            }

            #endregion 单元格风格工具
        }

        #region 常规工具

        public void ImportExcelFile()
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "Excel files (*.xls)|*.xls";
            FileDialog.FilterIndex = 2;
            FileDialog.InitialDirectory = Application.StartupPath;
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                fpSpread.OpenExcel(FileDialog.FileName);
            }
        }

        public void ExportExcelFile()
        {
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.Filter = "Excel files (*.xls)|*.xls";
            FileDialog.FilterIndex = 2;
            FileDialog.InitialDirectory = Application.StartupPath;
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                fpSpread.SaveExcel(FileDialog.FileName);
            }
        }

        public void ShearData()
        {
            ActiveSheet.ClipboardCut();
        }

        public void CopyData()
        {
            ActiveSheet.ClipboardCopy();
        }

        public void PasteData()
        {
            ActiveSheet.ClipboardPaste();
        }

        public void SettingPageInfo()
        {
            PrintInfoDialog PrintInfoDialog = new PrintInfoDialog();
            PrintInfoDialog.PrintSet = ActiveSheet.PrintInfo;
            if (PrintInfoDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveSheet.PrintInfo = PrintInfoDialog.PrintSet;
            }
        }

        public void PrintDocument()
        {
            ActiveSheet.PrintInfo.Preview = false;
            ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);
        }

        public void PrintPreviewDocument()
        {
            ActiveSheet.PrintInfo.Preview = true;
            ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);
        }

        #endregion 常规工具

        #region 编辑工具

        public void InsertRows()
        {
            if (Selections.Count > 0)
            {
                if (Selections[0].RowCount < 10)
                {
                    ActiveSheet.AddRows(ActiveSheet.ActiveRowIndex, Selections[0].RowCount);
                }
                else
                {
                    ActiveSheet.AddRows(ActiveSheet.ActiveRowIndex, 10);
                }
            }

            ActiveSheet.RaisePropertyChanged("RowCount");
        }

        public void DeleteRows()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                ActiveSheet.Rows.Remove(Ranges[0].Row, Ranges[0].RowCount);
            }

            ActiveSheet.RaisePropertyChanged("RowCount");
        }

        public void InsertColumns()
        {
            if (Selections.Count > 0)
            {
                if (Selections[0].ColumnCount < 10)
                {
                    ActiveSheet.AddColumns(ActiveSheet.ActiveColumnIndex, Selections[0].ColumnCount);
                }
                else
                {
                    ActiveSheet.AddColumns(ActiveSheet.ActiveColumnIndex, 10);
                }
            }

            ActiveSheet.RaisePropertyChanged("ColumnCount");
        }

        public void DeleteColumns()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                ActiveSheet.Columns.Remove(Ranges[0].Column, Ranges[0].ColumnCount);
            }

            ActiveSheet.RaisePropertyChanged("ColumnCount");
        }

        public void FreezeRows()
        {
            if (FreezeRowButton.Checked)
            {
                ActiveSheet.FrozenRowCount = 0;
            }
            else
            {
                List<CellRange> Ranges = Selections;
                if (Ranges.Count > 0)
                {
                    ActiveSheet.FrozenRowCount = Ranges[0].Row + Ranges[0].RowCount;
                }
            }

            FreezeRowButton.Checked = !FreezeRowButton.Checked;
        }

        public void FreezeColumn()
        {
            if (FreezeColumnButton.Checked)
            {
                ActiveSheet.FrozenColumnCount = 0;
            }
            else
            {
                List<CellRange> Ranges = Selections;
                if (Ranges.Count > 0)
                {
                    ActiveSheet.FrozenColumnCount = Ranges[0].Column + Ranges[0].ColumnCount;
                }
            }

            FreezeColumnButton.Checked = !FreezeColumnButton.Checked;
        }

        public void AddCellComments()
        {
            NoteDialog noteDialog = new NoteDialog();
            noteDialog.NoteContent = ActiveSheet.ActiveCell.Note;
            if (noteDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveSheet.ActiveCell.Note = noteDialog.NoteContent;
            }
        }

        public void RemoveCellComments()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].Note = "";
                    }
                }
            }
        }

        public void RemoveAllComments()
        {
            for (int i = 0; i < ActiveSheet.Rows.Count; i++)
            {
                for (int j = 0; j < ActiveSheet.Columns.Count; j++)
                {
                    ActiveSheet.Cells[i, j].Note = "";
                }
            }
        }

        public void HorizontalGridLine()
        {
            HorizontalGridLineButton.Checked = !HorizontalGridLineButton.Checked;
            HorizontalGridLineButton.Text = HorizontalGridLineButton.Checked ? "显示横向网格线" : "隐藏横向网格线";

            if (HorizontalGridLineButton.Checked)
            {
                ActiveSheet.HorizontalGridLine = HiddenGridLine;
            }
            else
            {
                ActiveSheet.HorizontalGridLine = VisbleGridLine;
            }
        }

        public void VerticalGridLine()
        {
            VerticalGridLineButton.Checked = !VerticalGridLineButton.Checked;
            VerticalGridLineButton.Text = VerticalGridLineButton.Checked ? "显示纵向网格线" : "隐藏纵向网格线";

            if (VerticalGridLineButton.Checked)
            {
                ActiveSheet.VerticalGridLine = HiddenGridLine;
            }
            else
            {
                ActiveSheet.VerticalGridLine = VisbleGridLine;
            }
        }

        #endregion 编辑工具

        #region 单元格风格工具

        public void SettingFontBold()
        {
            BoldButton.Checked = !BoldButton.Checked;
            SettingFont();
        }

        public void SettingFontItalic()
        {
            ItalicButton.Checked = !ItalicButton.Checked;
            SettingFont();
        }

        public void SettingFontUnderline()
        {
            UnderlineButton.Checked = !UnderlineButton.Checked;
            SettingFont();
        }

        public void SettingLeftAlign()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Left;
                    }
                }

                OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
            }
        }

        public void SettingMiddle()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Center;
                    }
                }

                OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
            }
        }

        public void SettingRightAlign()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Right;
                    }
                }

                OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
            }
        }

        public void SettingMiddleCenter()
        {
            MiddleCenterButton.Checked = !MiddleCenterButton.Checked;
            if (MiddleCenterButton.Checked)
            {
                ActiveSheet.ActiveCell.HorizontalAlignment = CellHorizontalAlignment.Center;
                ActiveSheet.ActiveCell.VerticalAlignment = CellVerticalAlignment.Center;

                List<CellRange> Ranges = Selections;
                if (Ranges.Count > 0)
                {
                    ActiveSheet.ActiveCell.RowSpan = Ranges[0].RowCount;
                    ActiveSheet.ActiveCell.ColumnSpan = Ranges[0].ColumnCount;
                }
            }
            else
            {
                ActiveSheet.ActiveCell.HorizontalAlignment = CellHorizontalAlignment.Left;
                ActiveSheet.ActiveCell.VerticalAlignment = CellVerticalAlignment.Top;
                ActiveSheet.ActiveCell.RowSpan = 1;
                ActiveSheet.ActiveCell.ColumnSpan = 1;
            }

            OnCellStyleChanged(ActiveSheet.ActiveCell.Row.Index, ActiveSheet.ActiveCell.Column.Index);
        }

        public void SettingBorder()
        {
            ExternalDialogs.BorderEditor(this.fpSpread);

            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
            }
        }

        public void SettingForeColor()
        {
            System.Windows.Forms.ColorDialog ColorDialog = new System.Windows.Forms.ColorDialog();
            ColorDialog.FullOpen = true;
            ColorDialog.Color = ActiveSheet.ActiveCell.ForeColor;
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                List<CellRange> Ranges = Selections;
                if (Ranges.Count > 0)
                {
                    for (int i = 0; i < Ranges[0].RowCount; i++)
                    {
                        for (int j = 0; j < Ranges[0].ColumnCount; j++)
                        {
                            ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].ForeColor = ColorDialog.Color;
                        }
                    }

                    OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
                }
            }
        }

        public void SettingBackColor()
        {
            System.Windows.Forms.ColorDialog ColorDialog = new System.Windows.Forms.ColorDialog();
            ColorDialog.FullOpen = true;
            ColorDialog.Color = ActiveSheet.ActiveCell.BackColor;
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                List<CellRange> Ranges = Selections;
                if (Ranges.Count > 0)
                {
                    for (int i = 0; i < Ranges[0].RowCount; i++)
                    {
                        for (int j = 0; j < Ranges[0].ColumnCount; j++)
                        {
                            ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].BackColor = ColorDialog.Color;
                        }
                    }

                    OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
                }
            }
        }

        private void SettingFormula()
        {
            //FormulaEditorUI FormulaEditorUI = new FormulaEditorUI(ActiveSheet);
            //FormulaEditorUI.SetFormula(ActiveSheet.ActiveCell.Formula);
            //if (DialogResult.OK == FormulaEditorUI.ShowDialog())
            //{
            //    ActiveSheet.ActiveCell.Formula = FormulaEditorUI.GetFormula();
            //}
        }

        public void SettingSpecialChars()
        {
            
        }

        #endregion 单元格风格工具

        #region 单元格样式

        public void SettingCellStyle()
        {
            //FormatStyleForm FormatStyleForm = new FormatStyleForm();

            //GridElement Element = null;
            //Cell Cell = ActiveSheet.ActiveCell;
            //if (ActiveSheet.ActiveCell.Value is GridElement)
            //{
            //    Element = ActiveSheet.ActiveCell.Value as GridElement;
            //}
            //else
            //{
            //    Element = new GridElement(Cell.Row.Index, Cell.Column.Index, Cell.RowSpan, Cell.ColumnSpan);
            //    ActiveSheet.ActiveCell.VerticalAlignment = CellVerticalAlignment.Center;
            //    ActiveSheet.ActiveCell.HorizontalAlignment = CellHorizontalAlignment.Center;
            //    ActiveSheet.ActiveCell.Value = Element;
            //}

            //if (DialogResult.OK == FormatStyleForm.ShowDialog())
            //{
            //    Element.Style.FormatInfo.Style = FormatStyleForm.FormatStyle;
            //    Element.Style.FormatInfo.Format = FormatStyleForm.FormatString;
            //}
        }

        #endregion 单元格样式

        #region 条件格式化

        public void SettingConditionFormat()
        {

        }

        #endregion 条件格式化

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (sender == FontFamilyComboBox)
            {
                SettingFont();
            }
            else if (sender == FontSizeComboBox)
            {
                SettingFont();
            }
        }

        #region 字体与字体大小

        private void SettingFont()
        {
            List<CellRange> Ranges = Selections;
            if (Ranges.Count > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        FontStyle Style = FontStyle.Regular;
                        if (BoldButton.Checked)
                        {
                            Style = Style | FontStyle.Bold;
                        }

                        if (ItalicButton.Checked)
                        {
                            Style = Style | FontStyle.Italic;
                        }

                        if (UnderlineButton.Checked)
                        {
                            Style = Style | FontStyle.Underline;
                        }

                        Font font = new Font(FontFamilyComboBox.SelectedItem.ToString(), FontSizeFactory.FontSize.GetSize(FontSizeComboBox.SelectedItem.ToString()), Style);
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].Font = font;
                    }
                }

                OnCellStyleChanged(Ranges[0].Row, Ranges[0].Column, Ranges[0].RowCount, Ranges[0].ColumnCount);
            }
        }

        #endregion 字体与字体大小

        private void ReportDesigner_Load(object sender, EventArgs e)
        {
            InitFpspreadStatus();
        }

        private void fpSpread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnCellClick(e);
            RefreshFspreadStatus();
        }

        #region 事件

        static readonly object Row_ColumnCountChangedEvent = new object();
        public event EventHandler<SheetViewEventArgs> Row_ColumnCountChanged
        {
            add
            {
                Events.AddHandler(Row_ColumnCountChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(Row_ColumnCountChangedEvent, value);
            }
        }

        protected virtual void OnRow_ColumnCountChanged(int Row, int Column)
        {
            SheetViewEventArgs e = new SheetViewEventArgs(Row, Column);
            EventHandler<SheetViewEventArgs> handler = (EventHandler<SheetViewEventArgs>)Events[Row_ColumnCountChangedEvent];
            if (handler != null)
                handler(ActiveSheet, e);
        }

        private static readonly object ReportElementAddingEvent = new object();
        public event EventHandler<ReportElementEventArgs> ReportElementAdding
        {
            add { Events.AddHandler(ReportElementAddingEvent, value); }
            remove { Events.RemoveHandler(ReportElementAddingEvent, value); }
        }

        protected virtual void OnReportElementAdding(ReportElementEventArgs e)
        {
            EventHandler<ReportElementEventArgs> handler = (EventHandler<ReportElementEventArgs>)Events[ReportElementAddingEvent];
            if (handler != null)
                handler(this, e);
        }

        static readonly object CellStyleChangedEvent = new object();
        public event EventHandler<SheetViewEventArgs> CellStyleChanged
        {
            add
            {
                Events.AddHandler(CellStyleChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(CellStyleChangedEvent, value);
            }
        }

        protected virtual void OnCellStyleChanged(int Row,int Column)
        {
            SheetViewEventArgs e = new SheetViewEventArgs(Row, Column);
            EventHandler<SheetViewEventArgs> handler = (EventHandler<SheetViewEventArgs>)Events[CellStyleChangedEvent];
            if (handler != null)
                handler(ActiveSheet, e);
        }

        protected virtual void OnCellStyleChanged(int Row, int Column, int RowCount, int ColumnCount)
        {
            SheetViewEventArgs e = new SheetViewEventArgs(Row, Column, RowCount, ColumnCount);
            EventHandler<SheetViewEventArgs> handler = (EventHandler<SheetViewEventArgs>)Events[CellStyleChangedEvent];
            if (handler != null)
                handler(ActiveSheet, e);
        }

        static readonly object CellClickEvent = new object();
        public event EventHandler<SelectionChangedEventArgs> CellClick
        {
            add
            {
                Events.AddHandler(CellClickEvent, value);
            }
            remove
            {
                Events.RemoveHandler(CellClickEvent, value);
            }
        }

        protected virtual void OnCellClick(SelectionChangedEventArgs e)
        {
            EventHandler<SelectionChangedEventArgs> handler = (EventHandler<SelectionChangedEventArgs>)Events[CellClickEvent];
            if (handler != null)
                handler(FpSpread, e);
        }

        #endregion 事件

        private void fpSpread_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            MouseEventArgs eventArgs = new MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta);
            OnMouseUp(eventArgs);
        }


        public void EnableToolStrip(Boolean Enabled)
        {
            CommonTools.Enabled = Enabled;
            OtherTools.Enabled = Enabled;
            CellStyleTools.Enabled = Enabled;
            CellTools.Enabled = Enabled;
        }

        public void ClearSheets()
        {
            FpSpread.Sheets.Clear();
            EnableToolStrip(false);
        }
    }

    public class ReportElementEventArgs : EventArgs
    {
        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }

    public class FontSizeFactory
    {
        readonly public static FontSizeFactory FontSize = new FontSizeFactory();
        Dictionary<string, float> NameSize = new Dictionary<string, float>();
        private FontSizeFactory()
        {
            NameSize.Add("初号", 42);
            NameSize.Add("小初", 36);
            NameSize.Add("一号", 26.25f);
            NameSize.Add("小一", 24);
            NameSize.Add("二号", 21.75f);
            NameSize.Add("小二", 18);
            NameSize.Add("三号", 15.75f);
            NameSize.Add("小三", 15);
            NameSize.Add("四号", 14.25f);
            NameSize.Add("小四", 12);
            NameSize.Add("五号", 10.5f);
            NameSize.Add("小五", 9);
            NameSize.Add("六号", 7.5f);
            NameSize.Add("小六", 6.75f);
            NameSize.Add("七号", 5.25f);
            NameSize.Add("八号", 5.20f);
            NameSize.Add("8", 8.25f);
            NameSize.Add("9", 9);
            NameSize.Add("10", 9.75f);
            NameSize.Add("11", 11.25f);
            NameSize.Add("12", 12);
            NameSize.Add("14", 14.25f);
            NameSize.Add("16", 15.75f);
            NameSize.Add("18", 18);
            NameSize.Add("20", 20.25f);
            NameSize.Add("22", 21.75f);
            NameSize.Add("24", 24);
            NameSize.Add("26", 26.25f);
            NameSize.Add("28", 27.75f);
            NameSize.Add("36", 36);
            NameSize.Add("48", 48);
            NameSize.Add("72", 72);

        }

        public float GetSize(string Name)
        {
            if (NameSize.ContainsKey(Name))
            {
                return NameSize[Name];
            }

            return NameSize["小五"];
        }

        public string[] Sizes()
        {
            List<string> Keys = new List<string>(NameSize.Keys);
            return Keys.ToArray();
        }
    }
}
