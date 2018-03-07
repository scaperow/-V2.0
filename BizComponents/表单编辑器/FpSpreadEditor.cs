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

namespace BizComponents
{
    public partial class FpSpreadEditor : UserControl
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

        FormulaTextBox formulaTextBox;

        public FpSpreadEditor()
        {
            InitializeComponent();
            EnableToolStrip(false);
            fpSpread.EditMode = false;
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
        }

        void formulaTextBox_Disposed(object sender, EventArgs e)
        {
            FormulaTextBox formulaTextBox = sender as FormulaTextBox;
            formulaTextBox.Detach();
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            #region 常规工具

            if (sender == ExportExcelButton) //导出Excel文件
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
            else if (sender == UndoButton) //撤消
            {
                Undo();
            }
            else if (sender == RedoButton) //重做
            {
                Redo();
            }

            #endregion 常规工具

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

        #region 常规工具

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
        public void Undo()
        {
            fpSpread.AllowUndo = true ;
        }
        public void Redo()
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

        #region 公式

        public void ShowFormulaDialog()
        {
            FormulaEditorUI FormulaEditorUI = new FormulaEditorUI(ActiveSheet);
            FormulaEditorUI.SetFormula(ActiveSheet.ActiveCell.Formula);
            if (DialogResult.OK == FormulaEditorUI.ShowDialog())
            {
                ActiveSheet.ActiveCell.Formula = FormulaEditorUI.GetFormula();
            }
        }

        public void ClearFormula()
        {
            if (DialogResult.Yes == MessageBox.Show("你确定要清空单元格中的公式吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                fpSpread.ActiveSheet.ActiveCell.Formula = "";
                fpSpread.ActiveSheet.ActiveCell.Text = "";
            }
        }


        #endregion 公式

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
            OnUserFormulaEntered(new UserFormulaEnteredEventArgs(FpSpread.GetRootWorkbook(), ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex));
        }

        private void FormulaCancel()
        {
            FormulaCancelButton.Image = Resources.输入2;
            ActiveSheet.ActiveCell.Formula = "";
            formulaTextBox.Text = "";
            formulaTextBox.Focus();

            OnUserFormulaCanceled(new UserFormulaEnteredEventArgs(FpSpread.GetRootWorkbook(), ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex));
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

        private void FpSpreadEditor_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitFunctionsStrip();
            }
        }

        private void fpSpread_SheetTabClick(object sender, SheetTabClickEventArgs e)
        {
            SheetView sheet = fpSpread.Sheets[e.SheetTabIndex];
            int ActiveRowIndex = (sheet.ActiveRowIndex >= 0 ? sheet.ActiveRowIndex : 0);
            int ActiveColumnIndex = (sheet.ActiveColumnIndex >= 0 ? sheet.ActiveColumnIndex : 0);
            tTextBox_XY.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(ActiveColumnIndex) + (ActiveRowIndex + 1).ToString();
        }

        private void fpSpread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ActiveRowIndex = (e.Range.Row >= 0 ? e.Range.Row : 0);
            int ActiveColumnIndex = (e.Range.Column >= 0 ? e.Range.Column : 0);
            tTextBox_XY.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(ActiveColumnIndex) + (ActiveRowIndex + 1).ToString();

            OnCellClick(e);
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

        static readonly object UserFormulaEnteredEvent = new object();
        public event EventHandler<UserFormulaEnteredEventArgs> UserFormulaEntered
        {
            add
            {
                Events.AddHandler(UserFormulaEnteredEvent, value);
            }
            remove
            {
                Events.RemoveHandler(UserFormulaEnteredEvent, value);
            }
        }

        protected virtual void OnUserFormulaEntered(UserFormulaEnteredEventArgs e)
        {
            EventHandler<UserFormulaEnteredEventArgs> handler = (EventHandler<UserFormulaEnteredEventArgs>)Events[UserFormulaEnteredEvent];
            if (handler != null)
                handler(FpSpread, e);
        }

        static readonly object UserFormulaCanceledEvent = new object();
        public event EventHandler<UserFormulaEnteredEventArgs> UserFormulaCanceled
        {
            add
            {
                Events.AddHandler(UserFormulaCanceledEvent, value);
            }
            remove
            {
                Events.RemoveHandler(UserFormulaCanceledEvent, value);
            }
        }

        protected virtual void OnUserFormulaCanceled(UserFormulaEnteredEventArgs e)
        {
            EventHandler<UserFormulaEnteredEventArgs> handler = (EventHandler<UserFormulaEnteredEventArgs>)Events[UserFormulaCanceledEvent];
            if (handler != null)
                handler(FpSpread, e);
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
        }

        public void EnableToolStrip(Boolean Enabled)
        {
            CommonTools.Enabled = Enabled;
        }

        public void ClearSheets()
        {
            FpSpread.Sheets.Clear();
            EnableToolStrip(false);
        }
    }
}
