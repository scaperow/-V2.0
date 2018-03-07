using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Yqun.Client.BizUI;
using FarPoint.Excel;
using FarPoint.Win;
using System.IO;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Design;
using FarPoint.Win.Spread.DrawingSpace;
using BizCommon;
using Yqun.Common.Encoder;
using BizComponents.Properties;
using System.Xml;

namespace BizComponents
{
    public partial class FpSheetEditor : UserControl
    {
        #region 常量

        const int Incremental = 4;
        string DefaultFontFamily = "宋体";
        string DefaultFontSize = "五号";

        #endregion 常量

        #region 网格线

        GridLine VisbleGridLine = new GridLine(GridLineType.Flat);
        GridLine HiddenGridLine = new GridLine(GridLineType.None);

        #endregion 网格线

        SymbolManager SymbolManager;
        FormulaTextBox formulaTextBox;

        WidthDialog widthDialog;

        public FpSheetEditor()
        {
            InitializeComponent();
            EnableToolStrip(false);
            fpSpread.EditMode = false;
            SymbolManager = new SymbolManager(FpSpread, SymbolBar);
        }

        public string GetActiveSheetXml()
        {
            return Serializer.GetObjectXml(ActiveSheet, "SheetView");
        }

        [Browsable(false)]
        public CellRange[] Selections
        {
            get
            {
                return ActiveSheet.GetSelections();
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

        /// <summary>
        /// 初始化公式工具栏
        /// </summary>
        protected void InitFunctionsStrip()
        {
            formulaTextBox = new FormulaTextBox();
            formulaTextBox.Size = new Size(650, 20);
            formulaTextBox.Dock = DockStyle.Fill;
            formulaTextBox.BorderStyle = BorderStyle.FixedSingle;
            formulaTextBox.Attach(FpSpread);
            formulaTextBox.Disposed += new EventHandler(formulaTextBox_Disposed);
            formulaTextBox.GotFocus += new EventHandler(formulaTextBox_GotFocus);

            ToolStripControlHost ctlHost = new ToolStripControlHost(formulaTextBox, "FormulaTextBox");
            ctlHost.AutoSize = false;
            ctlHost.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            ctlHost.Dock = DockStyle.Fill;

            FunctionStrip.Items.Add(ctlHost);
        }

        void formulaTextBox_GotFocus(object sender, EventArgs e)
        {
            FormulaOkButton.Image = Resources.输入1;
            FormulaCancelButton.Image = Resources.取消1;

            formulaTextBox.SelectAll();
        }

        void formulaTextBox_Disposed(object sender, EventArgs e)
        {
            FormulaTextBox formulaTextBox = sender as FormulaTextBox;
            formulaTextBox.Detach();
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
            Font font = ActiveSheet.ActiveCell.Font;
            if (font != null)
            {
                FontFamilyComboBox.SelectedIndex = FontFamilyComboBox.Items.IndexOf(font.Name);

                int x = -1;
                string[] Names = FontFactory.Instance.Sizes();
                for (int i = 0; i < Names.Length; i++)
                {
                    if (font.Size == FontFactory.Instance.GetSize(Names[i]))
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

            string[] FontName = FontFactory.Instance.FontNames();
            FontFamilyComboBox.Items.AddRange(FontName);

            FontFamilyComboBox.SelectedIndex = FontFamilyComboBox.Items.IndexOf(DefaultFontFamily);
        }

        private void InitFontSize()
        {
            string[] FontSize = FontFactory.Instance.Sizes();
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

        /// <summary>
        /// 移除xml中的Tag标记
        /// </summary>
        /// <param name="xml"></param>
        private string RemoveTagMarkupInXml(string xml, string TagName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList NodeList = doc.GetElementsByTagName(TagName);
            foreach (XmlNode Node in NodeList)
            {
                if (Node.ParentNode.LocalName != "PSShape")
                {
                    Node.Attributes.RemoveAll();
                    Node.InnerXml = "";
                }
            }

            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);

            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string XmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            return XmlString;
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
                        String sSheet = RemoveTagMarkupInXml(GetActiveSheetXml(), "Tag");
                        sw.Write(sSheet);
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
                            ExcelsEventArgs exceleventargs = new ExcelsEventArgs();
                            exceleventargs.Sheets.Add(Sheet);
                            OnExcelOpened(exceleventargs);
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
            else if (sender == unlockButton) //解锁
            {
                setunlockButton();
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
            else if (sender == ShowDataZoneButton)
            {
                ShowDataZone();
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
            else if (sender == BottomMiddleButton)
            {
                SettingBottomMiddle();
            }
            else if (sender == RightAlignButton)
            {
                SettingRightAlign();
            }
            else if (sender == MiddleCenterButton)
            {
                SettingMiddleCenter();
            }
            else if (sender == RightTopButton)
            {
                Settingrighttop();
            }
            else if (sender == LeftTopButton4)
            {
                SettingleftTop();
            }
            else if (sender == MiddleTopButton)
            {
                Settingmiddletop();
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
            else if (sender == ChartShapeButton)
            {
                SettingChartShape();
            }
            else if (sender == ConditionFormatButton)
            {
                ShowConditionFormatDialog();
            }
            else if (sender == EquationButton)
            {
                SettingEquation();
            }
            else if (sender == rightbottomButton)
            {
                SettingRightbottom();
            }
            else if (sender == leftbottomButton)
            {
                Settingleftbottom();
            }

            #endregion 单元格风格工具

            #region 单元格类型工具

            if (sender == TextCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new TextCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == LongTextCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new LongTextCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == RichTextButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new RichTextCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == NumberCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new NumberCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == PercentCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new PercentCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == CurrencyCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new CurrencyCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == DropDownListCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new DownListCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == DateTimeCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new DateTimeCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == BarCodeCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new BarCodeCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == ImageCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new ImageCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == HyperLinkCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new HyperLinkCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == CheckBoxCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new CheckBoxCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == ChartCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new ChartCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }
            else if (sender == MaskCellTypeButton)
            {
                CellTypeEventArgs cellTypeEvent = new CellTypeEventArgs();
                cellTypeEvent.CellType = new MaskCellType();
                cellTypeEvent.ActiveCell = ActiveSheet.ActiveCell;
                OnCellTypeSetting(cellTypeEvent);
            }

            #endregion 单元格类型工具

            #region 编辑公式工具

            else if (sender == FormulaOkButton)
            {
                FormulaOk();
            }
            else if (sender == FormulaCancelButton)
            {
                FormulaCancel();
            }
            else if (sender == FormulaButton2)
            {
                FormulaDialog();
            }

            #endregion 编辑公式工具
        }

        private void setunlockButton()
        {
            ActiveSheet.Rows[0, ActiveSheet.RowCount - 1].Visible = true;
            ActiveSheet.Columns[0, ActiveSheet.ColumnCount - 1].Visible = true;
        }

        private void ShowDataZone()
        {
            SheetConfiguration Sheet = ActiveSheet.Tag as SheetConfiguration;
            if (Sheet.DataTableSchema.Schema == null)
                return;

            foreach (FieldDefineInfo field in Sheet.DataTableSchema.Schema.FieldInfos)
            {
                if (ActiveSheet.Cells[field.RangeInfo].BackColor == Color.LightPink)
                {
                    ActiveSheet.Cells[field.RangeInfo].BackColor = Color.Empty;
                }
                else
                {
                    ActiveSheet.Cells[field.RangeInfo].BackColor = Color.LightPink;
                }
                ActiveSheet.Cells[field.RangeInfo].Tag = field;
            }
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
                try
                {
                    ExcelsEventArgs exceleventargs = new ExcelsEventArgs();
                    
                    SheetView sheetview = new SheetView();
                    sheetview.OpenExcel(FileDialog.FileName, 0);
                    exceleventargs.Sheets.Add(sheetview);

                    OnExcelOpened(exceleventargs);
                }
                catch
                {
                    MessageBox.Show("导入Excel文件失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                FpSpread fpspread = new FpSpread();
                fpspread.Sheets.Add(FpSpread.ActiveSheet);
                fpspread.SaveExcel(FileDialog.FileName);
                fpspread.Sheets.Clear();
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

            ShapeAttachParent();
        }

        public void PrintPreviewDocument()
        {
            ActiveSheet.PrintInfo.Preview = true;
            ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);

            ShapeAttachParent();
        }

        /// <summary>
        /// 解决预览或打印完毕后PSShape对象不能移动Bug
        /// </summary>
        protected void ShapeAttachParent()
        {
            foreach (IElement Element in ActiveSheet.DrawingContainer.ContainedObjects)
            {
                PSShape Shape = Element as PSShape;
                Shape.AttachParent(FpSpread, false, false);
            }
        }

        #endregion 常规工具

        #region 编辑工具

        public void InsertRows()
        {
            if (Selections.Length > 0)
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
        }

        public void DeleteRows()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                ActiveSheet.Rows.Remove(Ranges[0].Row, Ranges[0].RowCount);
            }
        }

        public void InsertColumns()
        {
            if (Selections.Length > 0)
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
        }

        public void DeleteColumns()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                ActiveSheet.Columns.Remove(Ranges[0].Column, Ranges[0].ColumnCount);
            }
        }

        public void FreezeRows()
        {
            if (FreezeRowButton.Checked)
            {
                ActiveSheet.FrozenRowCount = 0;
            }
            else
            {
                CellRange[] Ranges = Selections;
                if (Ranges.Length > 0)
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
                CellRange[] Ranges = Selections;
                if (Ranges.Length > 0)
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
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
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
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Left;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Center;
                    }
                }
            }
        }

        public void SettingMiddle()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Center;
                    }
                }
            }
        }

        public void SettingBottomMiddle()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Bottom;
                    }
                }
            }
        }
        public void SettingRightAlign()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Right;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Center;
                    }
                }
            }
        }

        public void SettingRightbottom()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Right;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Bottom;
                    }
                }
            }
        }

        public void Settingleftbottom()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Left;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Bottom;
                    }
                }
            }
        }
        public void Settingmiddletop()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Center;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Top;
                    }
                }
            }
        }
        public void Settingrighttop()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Right;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Top;
                    }
                }
            }
        }
        public void SettingleftTop()
        {
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
            {
                for (int i = 0; i < Ranges[0].RowCount; i++)
                {
                    for (int j = 0; j < Ranges[0].ColumnCount; j++)
                    {
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].HorizontalAlignment = CellHorizontalAlignment.Left;

                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].VerticalAlignment = CellVerticalAlignment.Top;
                    }
                }
            }
        }

        public void SettingMiddleCenter()
        {
            MiddleCenterButton.Checked = !MiddleCenterButton.Checked;
            if (MiddleCenterButton.Checked)
            {
                ActiveSheet.ActiveCell.HorizontalAlignment = CellHorizontalAlignment.Center;
                ActiveSheet.ActiveCell.VerticalAlignment = CellVerticalAlignment.Center;

                CellRange[] Ranges = Selections;
                if (Ranges.Length > 0)
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
        }

        public void SettingBorder()
        {
            //CellRange selection = new CellRange(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex, 1, 1);
            //if (ActiveSheet.SelectionCount > 0)
            //{
            //    selection = ActiveSheet.GetSelection(ActiveSheet.SelectionCount - 1);
            //}

            //FarPoint.Win.Spread.Design.BorderEditor Editor = new FarPoint.Win.Spread.Design.BorderEditor(this.fpSpread);
            //Editor.StartRow = selection.Row;
            //Editor.StartColumn = selection.Column;
            //Editor.RowCount = selection.RowCount;
            //Editor.ColumnCount = selection.ColumnCount;
            //DialogResult Result = Editor.ShowDialog();

            ExternalDialogs.BorderEditor(this.fpSpread);
        }

        public void SettingForeColor()
        {
            System.Windows.Forms.ColorDialog ColorDialog = new System.Windows.Forms.ColorDialog();
            ColorDialog.FullOpen = true;
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                CellRange[] Ranges = Selections;
                if (Ranges.Length > 0)
                {
                    for (int i = 0; i < Ranges[0].RowCount; i++)
                    {
                        for (int j = 0; j < Ranges[0].ColumnCount; j++)
                        {
                            ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].ForeColor = ColorDialog.Color;
                        }
                    }
                }
            }
        }

        public void SettingBackColor()
        {
            System.Windows.Forms.ColorDialog ColorDialog = new System.Windows.Forms.ColorDialog();
            ColorDialog.FullOpen = true;
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                CellRange[] Ranges = Selections;
                if (Ranges.Length > 0)
                {
                    for (int i = 0; i < Ranges[0].RowCount; i++)
                    {
                        for (int j = 0; j < Ranges[0].ColumnCount; j++)
                        {
                            ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].BackColor = ColorDialog.Color;
                        }
                    }
                }
            }
        }

        public void SettingSpecialChars()
        {
            SymbolManager.ShowSymbolDialog();
        }

        /// <summary>
        /// 添加图表
        /// </summary>
        private void SettingChartShape()
        {
            ChartShape chart = new ChartShape();
            chart.ActiveSheet = ActiveSheet;
            chart.Name = GetChartName("图表");
            chart.BackColor = Color.White;

            Rectangle rect = FpSpread.GetCellRectangle(0, 0, ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            chart.Left = rect.Left;
            chart.Top = rect.Top;
            chart.Width = 200;
            chart.Height = 200;

            chart.UpdateChart();

            ActiveSheet.AddShape(chart);
        }

        private String GetChartName(String PrefixName)
        {
            String Name = PrefixName;
            String tmpName = Name;
            Boolean Have = false;
            int Index = 1;
            do
            {
                Have = false;
                foreach (IElement Element in ActiveSheet.DrawingContainer.ContainedObjects)
                {
                    Have = Have | (Element.Name == tmpName);
                }

                if (!Have)
                {
                    break;
                }
                else
                {
                    tmpName = Name + (Index++).ToString();
                }
            } while (true);

            return tmpName;
        }

        /// <summary>
        /// 条件格式化
        /// </summary>
        private void ShowConditionFormatDialog()
        {
            ConditionFormatDialog Dialog = new ConditionFormatDialog(ActiveSheet);
            Dialog.Show(this.Parent);
        }

        /// <summary>
        /// 公式
        /// </summary>
        private void SettingEquation()
        {
            EquationShape equation = new EquationShape();
            equation.Name = GetChartName("公式");
            equation.BackColor = Color.White;

            Rectangle rect = FpSpread.GetCellRectangle(0, 0, ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            equation.Left = rect.Left;
            equation.Top = rect.Top;
            equation.Width = 100;
            equation.Height = 100;

            ActiveSheet.AddShape(equation);
            //RectShape rect1 = new RectShape();
            //rect1.Name = GetChartName("rect");
            //rect1.BackColor = Color.Black;

            //Rectangle rect = FpSpread.GetCellRectangle(0, 0, ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            //rect1.Left = rect.Left;
            //rect1.Top = rect.Top;
            //rect1.Width = 100;
            //rect1.Height = 100;

            //ActiveSheet.AddShape(rect1);
        }

        #endregion 单元格风格工具

        #region 公式编辑工具

        private void FormulaOk()
        {
            FormulaOkButton.Image = Resources.输入2;

            try
            {
                ActiveSheet.ActiveCell.Formula = formulaTextBox.Text.TrimStart('=');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "公式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            formulaTextBox.Focus();
        }

        private void FormulaCancel()
        {
            FormulaCancelButton.Image = Resources.输入2;
            ActiveSheet.ActiveCell.Formula = "";
            formulaTextBox.Focus();
        }

        private void FormulaDialog()
        {
            FunctionsDialog FunctionsDialog = new FunctionsDialog(FpSpread);
            FunctionsDialog.SetFormula(formulaTextBox.Text);
            if (DialogResult.OK == FunctionsDialog.ShowDialog())
            {
                formulaTextBox.Text = "=" + FunctionsDialog.GetFormula().TrimStart('=');
                formulaTextBox.Focus();
                Update();
            }
        }

        #endregion 公式编辑工具

        #region 清除单元格类型

        public void ClearCellType()
        {
            if (DialogResult.Yes == MessageBox.Show("你确定要清空单元格类型吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                fpSpread.ActiveSheet.ActiveCell.CellType = null;
            }
        }

        #endregion 清除单元格类型

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
            CellRange[] Ranges = Selections;
            if (Ranges.Length > 0)
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

                        Font font = new Font(FontFamilyComboBox.SelectedItem.ToString(), FontFactory.Instance.GetSize(FontSizeComboBox.SelectedItem.ToString()), Style);
                        ActiveSheet.Cells[Ranges[0].Row + i, Ranges[0].Column + j].Font = font;
                    }
                }
            }
        }

        #endregion 字体与字体大小

        private void FpSpreadEditor_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitFpspreadStatus();
                SymbolManager.InitSymbolBar(SymbolBar);
                InitFunctionsStrip();

                widthDialog = new WidthDialog();
                widthDialog.Size = new Size(80, 25);
                widthDialog.Owner = this.FindForm();
            }
        }

        private void fpSpread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ActiveRowIndex = (e.Range.Row >= 0 ? e.Range.Row : 0);
            int ActiveColumnIndex = (e.Range.Column >= 0 ? e.Range.Column : 0);
            tTextBox_XY.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(ActiveColumnIndex) + (ActiveRowIndex + 1).ToString();

            OnCellClick(e);
            RefreshFspreadStatus();
        }

        #region 事件

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

        private static readonly object CellTypeSettingEvent = new object();
        public event EventHandler<CellTypeEventArgs> CellTypeSetting
        {
            add { Events.AddHandler(CellTypeSettingEvent, value); }
            remove { Events.RemoveHandler(CellTypeSettingEvent, value); }
        }

        protected void OnCellTypeSetting(CellTypeEventArgs e)
        {
            EventHandler<CellTypeEventArgs> handler = (EventHandler<CellTypeEventArgs>)Events[CellTypeSettingEvent];
            if (handler != null)
                handler(this, e);
        }

        private static readonly object ExcelEvent = new object();
        public event EventHandler<ExcelsEventArgs> ExcelOpened
        {
            add { Events.AddHandler(ExcelEvent, value); }
            remove { Events.RemoveHandler(ExcelEvent, value); }
        }

        protected void OnExcelOpened(ExcelsEventArgs e)
        {
            EventHandler<ExcelsEventArgs> handler = (EventHandler<ExcelsEventArgs>)Events[ExcelEvent];
            if (handler != null)
                handler(this, e);
        }

        #endregion 事件

        private void fpSpread_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = fpSpread.PointToClient(Cursor.Position);
                MouseEventArgs eventArgs = new MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta);
                OnMouseUp(eventArgs);
            }

            HitTestInformation htf = fpSpread.HitTest(e.X, e.Y);
            if ((htf.Type == HitTestType.RowHeader && htf.HeaderInfo.InRowResize) || (htf.Type == HitTestType.ColumnHeader && htf.HeaderInfo.InColumnResize))
                widthDialog.Hide();
        }

        public void EnableToolStrip(Boolean Enabled)
        {
            CommonTools.Enabled = Enabled;
            OtherTools.Enabled = Enabled;
            CellStyleTools.Enabled = Enabled;
            CellTypeTools.Enabled = Enabled;
        }

        public void ClearSheets()
        {
            FpSpread.Sheets.Clear();
            EnableToolStrip(false);
        }

        #region ISymbolInterface 成员

        public void RefreshSymbolBar()
        {
            String SymbollibFile = Path.Combine(Application.StartupPath, "Symbollib.dll");
            String Symbols;
            if (File.Exists(SymbollibFile))
            {
                IniFile iniFile = new IniFile(SymbollibFile);
                Symbols = iniFile.IniReadValue("symbolbar", "Value");

                Char[] chars = Symbols.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    SymbolBar.Items[i].Text = chars[i].ToString();
                }
            }
            else
            {
                MessageBox.Show("未找到符号库文件 ‘" + SymbollibFile + "’。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public FpSpread SymbolBarParent
        {
            get
            {
                return this.FpSpread;
            }
        }

        #endregion

        private void toolStripContainer1_DragDrop(object sender, DragEventArgs e)
        {
            this.toolStripContainer1.ContentPanel.Hide();
            this.toolStripContainer1.Height = this.toolStripContainer1.TopToolStripPanel.Height;
        }

        private void fpSpread_ColumnWidthChanging(object sender, FarPoint.Win.Spread.ColumnWidthChangingEventArgs e)
        {
            FpSpread fpSpread = sender as FpSpread;
            widthDialog.Text = string.Format("宽度:{0}", e.Width.ToString());
        }

        private void fpSpread_RowHeightChanging(object sender, RowHeightChangingEventArgs e)
        {
            FpSpread fpSpread = sender as FpSpread;
            widthDialog.Text = string.Format("高度:{0}", e.Height.ToString());
        }

        private void fpSpread_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestInformation htf = fpSpread.HitTest(e.X, e.Y);
            if (htf.Type == HitTestType.RowHeader && htf.HeaderInfo.InRowResize && fpSpread.ActiveSheet != null)
            {
                float height = fpSpread.ActiveSheet.GetRowHeight(htf.HeaderInfo.Row);
                widthDialog.Location = fpSpread.PointToScreen(new Point(e.X, (int)(e.Y - height - widthDialog.Height)));
                widthDialog.Show();
            }
            else if (htf.Type == HitTestType.ColumnHeader && htf.HeaderInfo.InColumnResize)
            {
                Rectangle rect = fpSpread.GetColumnHeaderRectangle(fpSpread.GetColumnViewportIndexFromX(0));
                widthDialog.Location = fpSpread.PointToScreen(new Point(e.X, (int)(e.Y - rect.Height - widthDialog.Height)));
                widthDialog.Show();
            }
        }
    }
}
