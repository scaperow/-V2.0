using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using Yqun.Bases;
using BizCommon;
using System.Data;
using FarPoint.Win.Spread;
using System.Drawing.Printing;
using FarPoint.Win;
using System.Drawing;
using Yqun.Client.BizUI;

namespace BizComponents
{
    public class BatchPrintDocument : PrintDocument
    {
        private FpSpread m_spread;
        private int PageCount, PageIndex;

        public BatchPrintDocument(FpSpread spread)
            : base()
        {
            m_spread = spread;
            PrintController = new System.Drawing.Printing.StandardPrintController();
        }

        protected override void OnBeginPrint(PrintEventArgs ev)
        {
            base.OnBeginPrint(ev);

            if (ev.PrintAction == PrintAction.PrintToPrinter)
                UsePrintableArea = true;
            else
                UsePrintableArea = false;

            PageCount = 0;
            PageIndex = 1;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            if (m_spread.Sheets.Count > 0)
            {
                PrintInfo Info = m_spread.Sheets[0].PrintInfo;
                Rectangle r;
                if (UsePrintableArea)
                {
                    RectangleF PrintableArea = DefaultPageSettings.PrintableArea;
                    r = new Rectangle(
                                        e.PageBounds.X + Info.Margin.Left - (int)Math.Round(PrintableArea.Left),
                                        e.PageBounds.Y + Info.Margin.Top - (int)Math.Round(PrintableArea.Top),
                                        e.PageBounds.Width - Info.Margin.Left - Info.Margin.Right,
                                        e.PageBounds.Height - Info.Margin.Top - Info.Margin.Bottom
                                        );
                }
                else
                {
                    r = new Rectangle(
                                        e.PageBounds.X + Info.Margin.Left,
                                        e.PageBounds.Y + Info.Margin.Top,
                                        e.PageBounds.Width - Info.Margin.Left - Info.Margin.Right,
                                        e.PageBounds.Height - Info.Margin.Top - Info.Margin.Bottom
                                        );
                }

                int Foot, FootW, Heard;
                Foot = (int)Math.Round(e.Graphics.MeasureString(Info.Footer, m_spread.Font).Height);
                FootW = (int)Math.Round(e.Graphics.MeasureString(Info.Footer, m_spread.Font).Width);
                Heard = (int)Math.Round(e.Graphics.MeasureString(Info.Header, m_spread.Font).Height);

                r = new Rectangle(r.X, r.Y + Heard, r.Width, r.Height - Foot - Heard);
                PageCount = m_spread.GetOwnerPrintPageCount(e.Graphics, r, 0);
                if (PageCount > 0)
                {
                    m_spread.OwnerPrintDraw(e.Graphics, r, 0, PageIndex);
                    PageIndex += 1;

                    if (UsePrintableArea)
                    {
                        if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages)
                        {
                            e.HasMorePages = ((PageIndex <= PageCount) && e.PageSettings.PrinterSettings.ToPage >= PageIndex);
                        }
                        else
                        {
                            e.HasMorePages = (PageIndex <= PageCount);
                        }
                    }
                    else
                    {
                        e.HasMorePages = (PageIndex <= PageCount);
                    }
                }
            }
        }

        bool _PrintableArea = true;
        private bool UsePrintableArea
        {
            get
            {
                return _PrintableArea;
            }
            set
            {
                _PrintableArea = value;
            }
        }
    }

    public class BatchPrintClass
    {
        BackgroundWorker worker;
        ModuleConfiguration modelInfo;
        String dataCode;
        String[] dataID;

        public BatchPrintClass()
        {
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerReportsProgress = true;
        }

        public void Run(ModuleConfiguration ModelInfo,String DataCode,String[] DataID)
        {
            modelInfo = ModelInfo;
            dataCode = DataCode;
            if (DataID != null)
            {
                dataID = DataID;
            }

            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "开始准备打印的资料...";
            worker.RunWorkerAsync(this);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressScreen.Current.CloseSplashScreen();
            worker.Dispose();
            worker = null;

            MessageBox.Show(e.Result.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressScreen.Current.SetStatus = string.Format("已输出全部资料的 {0}% 到打印机", e.ProgressPercentage);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            BatchPrintClass Params = e.Argument as BatchPrintClass;
            ModuleConfiguration Model = Params.modelInfo;
            String DataCode = Params.dataCode;
            
            ModelDataManager DataManager = new ModelDataManager();
            DataSet dataSet = new DataSet();
            if (dataID != null)
            {
                dataSet = DataManager.GetData(Model, dataID, DataCode);
            }
            else
            {
                dataSet = DataManager.GetData(Model, DataCode);
            }

            FpSpread fpSpread = new MyCell();

            PrintDialog Dialog = new PrintDialog();
            Dialog.AllowSomePages = true;
            Dialog.PrinterSettings.PrintRange = PrintRange.SomePages;
            Dialog.PrinterSettings.FromPage = 1;
            Dialog.PrinterSettings.ToPage = 1;
            if (DialogResult.OK == Dialog.ShowDialog())
            {
                //初始化模板样式
                fpSpread.Sheets.Clear();
                foreach (SheetConfiguration Sheet in Model.Sheets)
                {
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), Sheet.SheetStyle, "SheetView") as SheetView;
                    SheetView.Tag = Sheet;
                    SheetView.SheetName = Sheet.Description;
                    fpSpread.Sheets.Add(SheetView);
                }

                fpSpread.LoadFormulas(true);

                //加载数据到模板样式
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        Application.DoEvents();

                        int Index = dataSet.Tables[0].Rows.IndexOf(Row);
                        worker.ReportProgress((Index + 1) / dataSet.Tables[0].Rows.Count);

                        String DataID = Row["ID"].ToString();
                        foreach (SheetView SheetView in fpSpread.Sheets)
                        {
                            SheetConfiguration SheetConfiguration = SheetView.Tag as SheetConfiguration;
                            TableDefineInfo TableInfo = SheetConfiguration.DataTableSchema.Schema;
                            if (TableInfo != null)
                            {
                                foreach (FieldDefineInfo FieldInfo in TableInfo.FieldInfos)
                                {
                                    DataRow[] DataRows = dataSet.Tables[GetBracketName(TableInfo.Name)].Select("ID='" + DataID + "'");
                                    SheetView.Cells[FieldInfo.RangeInfo].Value = DataRows[0][FieldInfo.FieldName];
                                }
                            }
                        }

                        //批量打印
                        BatchPrintDocument Document = new BatchPrintDocument(fpSpread);
                        Document.PrinterSettings = Dialog.PrinterSettings;
                        Document.Print();
                    }

                    e.Result = "已输出全部资料到打印机。";
                }
                else
                {
                    e.Result = string.Format("模板 {0} 中没有资料数据。", Model.Description);
                }

                fpSpread.Dispose();
                fpSpread = null;
            }
            else
            { 
                return; 
            }

        }

        String GetBracketName(String Name)
        {
            return "[" + Name.Trim("[]".ToCharArray()) + "]";
        }
    }
}
