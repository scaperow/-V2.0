using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Printing;
using Yqun.Client.BizUI;
using System.IO;
using ReportComponents.Properties;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.Spread;

namespace ReportComponents
{
    public partial class ReportViewer : UserControl
    {
        private PrintDocument mDocument;
        private PrintInfoDialog mPrintInfoDialog;
        private PrintDialog mPrintDialog;
        private int mVisibilePages = 1;
        private int mTotalPages = 0;
        private bool mShowPageSettingsButton = true;
        private bool mShowPrinterSettingsBeforePrint = true;

        private System.Reflection.FieldInfo m_Position;
        private MethodInfo m_SetPositionMethod;
        private bool isMouseDown;
        private Point startPosition;
        private Point endPosition;
        private Point curPos;

        private Cursor cur0;
        private Cursor cur1;

        public FpSpread _FpSpreed = null;

        public ReportViewer()
        {
            InitializeComponent();

            tsComboZoom.SelectedIndex = 4;//默认的缩放大小是100%

            Type type = typeof(System.Windows.Forms.PrintPreviewControl);
            m_Position = type.GetField("position", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);
            m_SetPositionMethod = type.GetMethod("SetPositionNoInvalidate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);

            PrintPreviewControl1.StartPageChanged += new EventHandler(printPreviewControl1_StartPageChanged);

            PrintPreviewControl1.MouseWheel += new MouseEventHandler(PrintPreviewControl1_MouseWheel);
            PrintPreviewControl1.Click += new EventHandler(PrintPreviewControl1_Click);
            PrintPreviewControl1.MouseDown += new MouseEventHandler(PrintPreviewControl1_MouseDown);
            PrintPreviewControl1.MouseUp += new MouseEventHandler(PrintPreviewControl1_MouseUp);
            PrintPreviewControl1.MouseMove += new MouseEventHandler(PrintPreviewControl1_MouseMove);
            this.MouseWheel += new MouseEventHandler(PrintPreviewControl1_MouseWheel);

            Stream stream0 = new MemoryStream(Resources.cur0);
            cur0 = new Cursor(stream0);
            Stream stream1 = new MemoryStream(Resources.cur1);
            cur1 = new Cursor(stream1);

            PrintPreviewControl1.Cursor = cur0;
        }

        public PrintInfoDialog PrintInfoDialog
        {
            get
            {
                if (mPrintInfoDialog == null)
                {
                    mPrintInfoDialog = new PrintInfoDialog();
                    mPrintInfoDialog.tabControl1.TabPages.RemoveAt(2);
                    mPrintInfoDialog.tabControl1.TabPages.RemoveAt(3);
                    mPrintInfoDialog.tabControl1.TabPages.RemoveAt(3);
                }

                return mPrintInfoDialog;
            }
        }

        public PrintDialog PrintDialog
        {
            get
            {
                if (mPrintDialog == null) mPrintDialog = new PrintDialog();
                return mPrintDialog;
            }
            set
            {
                mPrintDialog = value;
            }
        }

        public bool ShowPageSettingsButton
        {
            get
            {
                return mShowPageSettingsButton;
            }
            set
            {
                mShowPageSettingsButton = value;
            }
        }

        public bool ShowPrinterSettingsBeforePrint
        {
            get
            {
                return mShowPrinterSettingsBeforePrint;
            }
            set
            {
                mShowPrinterSettingsBeforePrint = value;
            }
        }

        public PrintPreviewControl PrintPreviewControl
        {
            get
            {
                return PrintPreviewControl1;
            }
        }

        public PrintDocument Document
        {
            get
            {
                return mDocument;
            }
            set
            {
                SwitchPrintDocumentHandlers(mDocument, false);
                mDocument = value;
                SwitchPrintDocumentHandlers(mDocument, true);
                PrintPreviewControl1.Document = mDocument;
                PrintPreviewControl1.InvalidatePreview();
            }
        }

        public bool UseAntiAlias
        {
            get
            {
                return PrintPreviewControl1.UseAntiAlias;
            }
            set
            {
                PrintPreviewControl1.UseAntiAlias = value;
            }
        }

        void mDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            mTotalPages = 0;
        }

        void mDocument_EndPrint(object sender, PrintEventArgs e)
        {
            tsLblTotalPages.Text = " / " + mTotalPages.ToString();
            tsTxtCurrentPage.Text = (mTotalPages > 0 ? 1 : 0).ToString();
        }

        void mDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            mTotalPages++;
        }

        private void tsTxtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                int startPage;
                if (Int32.TryParse(tsTxtCurrentPage.Text, out startPage))
                {
                    try
                    {
                        startPage--;
                        if (startPage < 0) startPage = 0;
                        if (startPage > mTotalPages - 1) startPage = mTotalPages - mVisibilePages;
                        PrintPreviewControl1.StartPage = startPage;
                    }
                    catch
                    { }
                }
            }
        }

        private void NumOfPages_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
                tsDDownPages.Image = MenuItem.Image;
                mVisibilePages = Int32.Parse((string)MenuItem.Tag);

                switch (mVisibilePages)
                {
                    case 1:
                        PrintPreviewControl1.Rows = 1;
                        PrintPreviewControl1.Columns = 1;
                        break;
                    case 2:
                        PrintPreviewControl1.Rows = 1;
                        PrintPreviewControl1.Columns = 2;
                        break;
                    case 4:
                        PrintPreviewControl1.Rows = 2;
                        PrintPreviewControl1.Columns = 2;
                        break;
                    case 6:
                        PrintPreviewControl1.Rows = 2;
                        PrintPreviewControl1.Columns = 3;
                        break;
                    case 8:
                        PrintPreviewControl1.Rows = 2;
                        PrintPreviewControl1.Columns = 4;
                        break;
                }

                tsBtnZoom_Click(null, null);
            }
        }

        //支持导航栏
        private void Navigate_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            int startPage = PrintPreviewControl1.StartPage;
            try
            {
                if (btn.Name == "tsBtnNext")
                {
                    startPage += mVisibilePages;
                }
                else if (btn.Name == "tsBtnPrev")
                {
                    startPage -= mVisibilePages;
                }
                else if (btn.Name == "_btnFirst")
                {
                    startPage = 0;
                }
                else if (btn.Name == "_btnLast")
                {
                    startPage = mTotalPages - 1;
                }

                if (startPage < 0) startPage = 0;
                if (startPage > mTotalPages - 1) startPage = mTotalPages - mVisibilePages;
                PrintPreviewControl1.StartPage = startPage;
            }
            catch
            { }
        }

        void printPreviewControl1_StartPageChanged(object sender, EventArgs e)
        {
            int tmp = PrintPreviewControl1.StartPage + 1;
            tsTxtCurrentPage.Text = tmp.ToString();
        }

        private void tsComboZoom_Leave(object sender, EventArgs e)
        {
            if (tsComboZoom.SelectedIndex == 0)
            {
                PrintPreviewControl1.AutoZoom = true;
                return;
            }

            string sZoomVal = tsComboZoom.Text.Replace("%", "");
            double zoomval;
            if (double.TryParse(sZoomVal, out zoomval))
            {
                try
                {
                    PrintPreviewControl1.Zoom = zoomval / 100;
                }
                catch
                { }

                zoomval = (PrintPreviewControl1.Zoom * 100);
                tsComboZoom.Text = zoomval.ToString() + "%";
            }
        }

        private void tsBtnZoom_Click(object sender, EventArgs e)
        {
            tsComboZoom.SelectedIndex = 5;
            tsComboZoom_Leave(null, null);
        }

        private void tsComboZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tsComboZoom_Leave(null, null);
            }
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            if (this.mDocument is OwnerPrintDocument)
            {
                PrintDialog.Document = this.mDocument;
                PrintDialog.AllowSomePages = true;
                PrintDialog.PrinterSettings.FromPage = 1;
                PrintDialog.PrinterSettings.ToPage = mTotalPages;
                PrintDialog.AllowCurrentPage = true;
                PrintDialog.UseEXDialog = true;

                if (mShowPrinterSettingsBeforePrint)
                {
                    PageSettings PageSettings = mDocument.DefaultPageSettings;
                    if (PrintDialog.ShowDialog() == DialogResult.OK)
                    {
                        mDocument.PrinterSettings = PrintDialog.PrinterSettings;
                        mDocument.DefaultPageSettings = PageSettings;
                        mDocument.Print();
                    }
                }
                else
                {
                    mDocument.Print();
                }
            }
        }

        private void tsBtnPageSettings_Click(object sender, EventArgs e)
        {
            OwnerPrintDocument Document = this.mDocument as OwnerPrintDocument;
            if (Document != null)
            {
                PrintInfoDialog.PrintSet = Document.PrintInfo;
                if (DialogResult.OK == PrintInfoDialog.ShowDialog())
                {
                    Document.PrintInfo = PrintInfoDialog.PrintSet;
                    PrintPreviewControl1.InvalidatePreview();
                }
            }
        }

        private void SwitchPrintDocumentHandlers(PrintDocument Document, bool Attach)
        {
            if (Document == null) return;
            if (Attach)
            {
                mDocument.BeginPrint += new PrintEventHandler(mDocument_BeginPrint);
                mDocument.PrintPage += new PrintPageEventHandler(mDocument_PrintPage);
                mDocument.EndPrint += new PrintEventHandler(mDocument_EndPrint);
            }
            else
            {
                mDocument.BeginPrint -= new PrintEventHandler(mDocument_BeginPrint);
                mDocument.PrintPage -= new PrintPageEventHandler(mDocument_PrintPage);
                mDocument.EndPrint -= new PrintEventHandler(mDocument_EndPrint);
            }
        }

        /// <summary> 
        /// 鼠标滚轮
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param>
        void PrintPreviewControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!SystemInformation.MouseWheelPresent)
            {
                //If have no wheel
                return;
            }

            int scrollAmount;
            float amount = Math.Abs(e.Delta) / SystemInformation.MouseWheelScrollDelta;
            amount *= SystemInformation.MouseWheelScrollLines;
            amount *= 12;//Row height
            amount *= (float)PrintPreviewControl1.Zoom;//Zoom Rate
            if (e.Delta < 0)
            {
                scrollAmount = (int)amount;
            }
            else
            {
                scrollAmount = -(int)amount;
            }
            Point curPos = (Point)(m_Position.GetValue(PrintPreviewControl1));
            m_SetPositionMethod.Invoke(PrintPreviewControl1, new object[] { new Point(curPos.X + 0, curPos.Y + scrollAmount) });
        }

        /// <summary> 
        /// 鼠标在控件上点击时，需要处理获得焦点，因为默认不会获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPreviewControl1_Click(object sender, EventArgs e)
        {
            PrintPreviewControl1.Select();
            PrintPreviewControl1.Focus();
        }

        /// <summary>
        /// 鼠标按下，开始拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPreviewControl1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            startPosition = new Point(e.X, e.Y);
            curPos = (Point)(m_Position.GetValue(PrintPreviewControl1));
            PrintPreviewControl1.Cursor = cur1;
        }

        /// <summary>
        /// 鼠标释放，完成拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPreviewControl1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            endPosition = new Point(e.X, e.Y);
            m_SetPositionMethod.Invoke(PrintPreviewControl1, new object[] { new Point(curPos.X + (startPosition.X - endPosition.X), curPos.Y + (startPosition.Y - endPosition.Y)) });
            PrintPreviewControl1.Cursor = cur0;
        }

        /// <summary>
        /// 鼠标移动，拖动中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPreviewControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                endPosition = new Point(e.X, e.Y);
                m_SetPositionMethod.Invoke(PrintPreviewControl1, new object[] { new Point(curPos.X + (startPosition.X - endPosition.X), curPos.Y + (startPosition.Y - endPosition.Y)) });
            }
        }

        /// <summary>
        /// 重置查看控件
        /// </summary>
        public void Reset()
        {
            this.tsTxtCurrentPage.Text = "0";
            this.tsLblTotalPages.Text = "/ {0}";
            this.Document = null;
            this.Update();
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        private void Button_ImportExcel_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        /// <summary>
        /// 导出周报月报
        /// </summary>
        private void ExportData()
        {
            if (_FpSpreed != null)
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
                {
                    String fullPath = folderBrowserDialog1.SelectedPath;
                    String fileName = Path.Combine(fullPath, "报表.xls");
                    _FpSpreed.SaveExcel(fileName, IncludeHeaders.ColumnHeadersCustomOnly);
                    MessageBox.Show(string.Format("“{0}”导出完成", fileName));
                }
            }
        }
    }
}
