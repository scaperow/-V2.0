using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread.Model;
using Yqun.Client.BizUI;
using FarPoint.Win.Spread;
using System.IO;
using Yqun.Common.Encoder;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing.Imaging;
using BizCommon;

namespace BizComponents
{
    public partial class FpSpreadViewer : UserControl
    {
        SymbolManager symbolManager;

        public FpSpreadViewer()
        {
            InitializeComponent();
            fpSpread.EditMode = true;

            symbolManager = new SymbolManager(FpSpread, SymbolBar);
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

        public void CloseSheets()
        {
            fpSpread.SuspendLayout();
            fpSpread.Sheets.Clear();
            fpSpread.ResumeLayout();
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == ExportExcelButton)
            {
                ExportToExcel();
            }
            else if (sender == ExportPDFButton)
            {
                ExportToPDF();
            }
            else if (sender == CutButton)
            {
                Cut();
            }
            else if (sender == CopyButton)
            {
                Copy();
            }
            else if (sender == PasteButton)
            {
                Paste();
            }
            else if (sender == PageSettingButton)
            {
                ShowPageSettingDialog();
            }
            else if (sender == PrintButton)
            {
                PrintSheet();
            }
            else if (sender == PrintAllButton)
            {
                PrintAllSheet();
            }
            else if (sender == PrintPreviewButton)
            {
                PrintPreview();
            }
            else if (sender == UndoButton)
            {
                Undo();
            }
            else if (sender == RedoButton)
            {
                Redo();
            }

            #region 单元格风格工具

            if (sender == SpecialCharButton)
            {
                SettingSpecialChars();
            }

            #endregion 单元格风格工具
        }

        /// <summary>
        /// 导出到Excel工作簿
        /// </summary>
        private void ExportToExcel()
        {
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.Filter = "Excel 97-2003 工作簿(*.xls)|*.xls|Excel 2007 工作簿(*.xlsx)|*.xlsx";
            FileDialog.FilterIndex = 1;
            FileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileDialog.FileName = GetFileName(FileDialog.InitialDirectory, Text, FileDialog.FilterIndex == 1 ? ".xls" : ".xlsx");
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                if (FileDialog.FilterIndex == 1)
                {
                    Bitmap bitmap = new Bitmap(130, 170);
                    int ImgRowIndex = -1;
                    int ImgColumnIndex = -1;
                    string strImageFileName = "";
                    SheetView view = fpSpread.ActiveSheet;
                    string strSheetID = view.Tag.ToString();
                    bool bHasImg = false;
                    string strRangeName = string.Empty;
                    #region 循环处理单元格
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        for (int j = 0; j < view.ColumnCount; j++)
                        {
                            Cell cell = view.Cells[i, j];
                            if (cell != null)
                            {
                                IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                                JZCellProperty property = cell.Tag as JZCellProperty;
                                if (property != null)
                                {
                                    if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "图片")
                                    {
                                        if (cell.Value != null && cell.Value is Bitmap)
                                        {
                                            strImageFileName = FileDialog.FileName;
                                            strImageFileName = strImageFileName.Replace(".xls", ".jpg");
                                            ImgRowIndex = i;
                                            ImgColumnIndex = j;
                                            //创建一个bitmap类型的bmp变量来读取文件。
                                            bitmap = cell.Value as Bitmap;
                                            //新建第二个bitmap类型的bmp2变量，我这里是根据我的程序需要设置的。
                                            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format16bppRgb555);//PixelFormat.Format16bppRgb555
                                            //将第一个bmp拷贝到bmp2中
                                            Graphics draw = Graphics.FromImage(bitmap2);
                                            draw.DrawImage(bitmap, 0, 0);
                                            bitmap2.Save(strImageFileName, ImageFormat.Jpeg);
                                            cell.Value = null;
                                            bHasImg = true;
                                            strRangeName = ((char)('A' + ImgColumnIndex)).ToString() + (ImgRowIndex + 1).ToString() + ":" + ((char)('A' + ImgColumnIndex + cell.ColumnSpan - 1)).ToString() + (ImgRowIndex + cell.RowSpan).ToString();
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        if (bHasImg == true)
                        {
                            break;
                        }
                    }
                    #endregion

                    fpSpread.SaveExcel(FileDialog.FileName, FarPoint.Excel.ExcelSaveFlags.NoFormulas | FarPoint.Excel.ExcelSaveFlags.SaveAsViewed);
                    #region 添加图片到Excel
                    if (bHasImg == true)
                    {
                        view.Cells[ImgRowIndex, ImgColumnIndex].Value = bitmap;
                        PictureToExcel pictrue2excel = new PictureToExcel();
                        pictrue2excel.Open(FileDialog.FileName);
                        if (!string.IsNullOrEmpty(strRangeName))
                        {
                            pictrue2excel.InsertPicture(strRangeName, strImageFileName);//"L6:M9"
                            if (File.Exists(strImageFileName))
                            {
                                File.Delete(strImageFileName);
                            }
                        }
                        pictrue2excel.SaveFile(FileDialog.FileName);
                        pictrue2excel.Dispose();
                    }
                    #endregion
                }
                else if (FileDialog.FilterIndex == 2)
                {
                    //fpSpread.SaveExcel(FileDialog.FileName, FarPoint.Excel.ExcelSaveFlags.NoFormulas | FarPoint.Excel.ExcelSaveFlags.SaveAsViewed | FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);

                    Bitmap bitmap = new Bitmap(130, 170);
                    int ImgRowIndex = -1;
                    int ImgColumnIndex = -1;
                    string strImageFileName = "";
                    SheetView view = fpSpread.ActiveSheet;
                    string strSheetID = view.Tag.ToString();
                    bool bHasImg = false;
                    string strRangeName = string.Empty;
                    #region 循环处理单元格
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        for (int j = 0; j < view.ColumnCount; j++)
                        {
                            Cell cell = view.Cells[i, j];
                            if (cell != null)
                            {
                                IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                                JZCellProperty property = cell.Tag as JZCellProperty;
                                if (property != null)
                                {
                                    if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "图片")
                                    {
                                        if (cell.Value != null && cell.Value is Bitmap)
                                        {
                                            strImageFileName = FileDialog.FileName;
                                            strImageFileName = strImageFileName.Replace(".xls", ".jpg");
                                            ImgRowIndex = i;
                                            ImgColumnIndex = j;
                                            //创建一个bitmap类型的bmp变量来读取文件。
                                            bitmap = cell.Value as Bitmap;
                                            //新建第二个bitmap类型的bmp2变量，我这里是根据我的程序需要设置的。
                                            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format16bppRgb555);//PixelFormat.Format16bppRgb555
                                            //将第一个bmp拷贝到bmp2中
                                            Graphics draw = Graphics.FromImage(bitmap2);
                                            draw.DrawImage(bitmap, 0, 0);
                                            bitmap2.Save(strImageFileName, ImageFormat.Jpeg);
                                            cell.Value = null;
                                            bHasImg = true;
                                            strRangeName = ((char)('A' + ImgColumnIndex)).ToString() + (ImgRowIndex + 1).ToString() + ":" + ((char)('A' + ImgColumnIndex + cell.ColumnSpan - 1)).ToString() + (ImgRowIndex + cell.RowSpan).ToString();
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        if (bHasImg == true)
                        {
                            break;
                        }
                    }
                    #endregion
                    fpSpread.SaveExcel(FileDialog.FileName, FarPoint.Excel.ExcelSaveFlags.NoFormulas | FarPoint.Excel.ExcelSaveFlags.SaveAsViewed | FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
                    #region 添加图片到Excel
                    if (bHasImg == true)
                    {
                        view.Cells[ImgRowIndex, ImgColumnIndex].Value = bitmap;
                        PictureToExcel pictrue2excel = new PictureToExcel();
                        pictrue2excel.Open(FileDialog.FileName);
                        if (!string.IsNullOrEmpty(strRangeName))
                        {
                            pictrue2excel.InsertPicture(strRangeName, strImageFileName);//"L6:M9"
                            if (File.Exists(strImageFileName))
                            {
                                File.Delete(strImageFileName);
                            }
                        }
                        pictrue2excel.SaveFile(FileDialog.FileName);
                        pictrue2excel.Dispose();
                    }
                    #endregion
                }
            }
        }

        private string GetFileName(String FilePath, string FileName, string Extension)
        {
            int Index = 1;
            string tempFileName = FileName;
            string fullPath = Path.Combine(FilePath, tempFileName) + Extension;
            while (File.Exists(fullPath))
            {
                tempFileName = FileName + "_" + (Index++).ToString();
                fullPath = Path.Combine(FilePath, tempFileName) + Extension;
            }

            return tempFileName;
        }

        /// <summary>
        /// 导出到PDF
        /// </summary>
        private void ExportToPDF()
        {
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.Filter = "PDF文件 (*.pdf)|*.pdf";
            FileDialog.FilterIndex = 1;
            FileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                int Index = FpSpread.Sheets.IndexOf(ActiveSheet);
                PrintInfo Info = new PrintInfo();
                Info.CopyFrom(ActiveSheet.PrintInfo);
                Info.ShowGrid = false;
                Bitmap Panel = new Bitmap(Info.PaperSize.Width, Info.PaperSize.Height);

                using (Graphics g = Graphics.FromImage(Panel))
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                    Rectangle r = new Rectangle(
                                            Info.Margin.Left,
                                            Info.Margin.Top,
                                            Info.PaperSize.Width - Info.Margin.Left - Info.Margin.Right,
                                            Info.PaperSize.Height - Info.Margin.Top - Info.Margin.Bottom
                                            );

                    int Foot, FootW, Heard;
                    Foot = (int)Math.Round(g.MeasureString(Info.Footer, FpSpread.Font).Height);
                    FootW = (int)Math.Round(g.MeasureString(Info.Footer, FpSpread.Font).Width);
                    Heard = (int)Math.Round(g.MeasureString(Info.Header, FpSpread.Font).Height);

                    r = new Rectangle(r.X, r.Y + Heard, r.Width, r.Height - Foot - Heard);
                    FpSpread.OwnerPrintDraw(g, r, Index, 1);
                }

                PdfDocument doc = new PdfDocument();
                doc.Pages.Add(new PdfPage());
                XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
                XImage img = XImage.FromGdiPlusImage(Panel);

                xgr.DrawImage(img, 0, 0);

                doc.Save(FileDialog.FileName);
                doc.Close();
                //Panel.Save("", ImageFormat.Jpeg);
                Panel.Dispose();

            }
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void Cut()
        {
            ActiveSheet.ClipboardCut();
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void Copy()
        {
            ActiveSheet.ClipboardCopy();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void Paste()
        {
            Cell cell = ActiveSheet.ActiveCell;
            //JZCellProperty p = cell.Tag as JZCellProperty;
            if (cell.Locked == true)
            {
                return;
            }
            ActiveSheet.ClipboardPaste(ClipboardPasteOptions.AsString);
        }

        /// <summary>
        /// 显示页面设置对话框
        /// </summary>
        private void ShowPageSettingDialog()
        {
            PrintInfoDialog PrintInfoDialog = new PrintInfoDialog();
            PrintInfoDialog.PrintSet = ActiveSheet.PrintInfo;
            if (PrintInfoDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveSheet.PrintInfo = PrintInfoDialog.PrintSet;
            }
        }

        /// <summary>
        /// 打印当前表单
        /// </summary>
        private void PrintSheet()
        {
            ActiveSheet.PrintInfo.Preview = false;
            ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);
        }

        /// <summary>
        /// 打印全部表单
        /// </summary>
        private void PrintAllSheet()
        {

        }

        /// <summary>
        /// 打印预览
        /// </summary>
        private void PrintPreview()
        {
            ActiveSheet.PrintInfo.Preview = true;
            ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        private void Undo()
        {
            FpSpread.UndoManager.Undo();
        }

        /// <summary>
        /// 重复
        /// </summary>
        private void Redo()
        {
            FpSpread.UndoManager.Redo();
        }

        /// <summary>
        /// 特殊字符对话框
        /// </summary>
        public void SettingSpecialChars()
        {
            symbolManager.ShowSymbolDialog();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpSpreadViewer_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                symbolManager.InitSymbolBar(SymbolBar);
                if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                {
                    ExportPDFButton.Visible = true;
                }
                else
                {
                    ExportPDFButton.Visible = false;
                }
            }
        }

        private void fpSpread_EnterCell(object sender, EnterCellEventArgs e)
        {
            if (FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType is DownListCellType)
            {
                DownListCellType CellType = FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType as DownListCellType;
                CellType.DropDownButton = true;
            }
        }

        private void fpSpread_LeaveCell(object sender, LeaveCellEventArgs e)
        {
            if (FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType is DownListCellType)
            {
                DownListCellType CellType = FpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType as DownListCellType;
                CellType.DropDownButton = false;
            }
        }

        /// <summary>
        /// 处理键盘操作
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg != null && keyData != null)
            {
                if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
                {
                    if (keyData == (Keys.Control | Keys.P))
                    {
                        PrintSheet();
                        return true;
                    }
                    else if (keyData == Keys.Delete)
                    {
                        CellRange cr = FpSpread.ActiveSheet.GetSelection(0);
                        if (cr != null)
                        {
                            for (int r = 0; r < cr.RowCount; r++)
                            {
                                for (int c = 0; c < cr.ColumnCount; c++)
                                {
                                    if (FpSpread.ActiveSheet.Cells[cr.Row + r, cr.Column + c] == null)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        if (FpSpread.ActiveSheet.Cells[cr.Row + r, cr.Column + c].Locked)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (keyData == Keys.Up)
                    {
                        if (FpSpread.ActiveSheet.ActiveRowIndex > 0 && FpSpread.IsEditing)
                        {
                            FpSpread.ActiveSheet.ActiveRowIndex = FpSpread.ActiveSheet.ActiveRowIndex - 1;
                        }
                    }
                    else if (keyData == Keys.Down)
                    {
                        if (FpSpread.ActiveSheet.ActiveRowIndex < FpSpread.ActiveSheet.RowCount - 1 && FpSpread.IsEditing)
                        {

                            FpSpread.ActiveSheet.ActiveRowIndex = FpSpread.ActiveSheet.ActiveRowIndex + 1;
                        }
                    }
                    else if (keyData == Keys.Left)
                    {
                        if (FpSpread.ActiveSheet.ActiveColumnIndex > 0 && FpSpread.IsEditing)
                        {
                            FpSpread.ActiveSheet.ActiveColumnIndex = FpSpread.ActiveSheet.ActiveColumnIndex - 1;
                        }
                    }
                    else if (keyData == Keys.Right)
                    {
                        if (FpSpread.ActiveSheet.ActiveColumnIndex < FpSpread.ActiveSheet.ColumnCount - 1 && FpSpread.IsEditing)
                        {
                            FpSpread.ActiveSheet.ActiveColumnIndex = FpSpread.ActiveSheet.ActiveColumnIndex + 1;
                        }
                    }
                }
            }
            else
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
