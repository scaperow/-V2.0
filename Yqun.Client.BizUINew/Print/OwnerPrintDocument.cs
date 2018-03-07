using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using FarPoint.Win.Spread;
using System.Windows.Forms;
using System.Drawing;

namespace Yqun.Client.BizUI
{
    public class OwnerPrintDocument : PrintDocument
    {
        private FpSpread m_spread;
        private int PageCount, PageIndex;

        public OwnerPrintDocument(FpSpread spread)
            : base()
        {
            m_spread = spread;

            DefaultPageSettings.Landscape = (spread.Sheets[0].PrintInfo.Orientation == PrintOrientation.Landscape ? true : false);
            
            DefaultPageSettings.Margins.Left = spread.Sheets[0].PrintInfo.Margin.Left;
            DefaultPageSettings.Margins.Right = spread.Sheets[0].PrintInfo.Margin.Right;
            DefaultPageSettings.Margins.Top = spread.Sheets[0].PrintInfo.Margin.Top;
            DefaultPageSettings.Margins.Bottom = spread.Sheets[0].PrintInfo.Margin.Bottom;
            
            DefaultPageSettings.PaperSize = spread.Sheets[0].PrintInfo.PaperSize;
            DefaultPageSettings.PaperSource = spread.Sheets[0].PrintInfo.PaperSource;
        }

        protected override void OnBeginPrint(PrintEventArgs ev)
        {
            base.OnBeginPrint(ev);

            if (ev.PrintAction == PrintAction.PrintToPrinter && ShowPrinterDialog)
            {
                PrintDialog Dialog = new PrintDialog();
                Dialog.Document = this;
                Dialog.AllowSomePages = true;
                Dialog.UseEXDialog = true;
                Dialog.PrinterSettings.PrintRange = PrintRange.SomePages;
                Dialog.PrinterSettings.FromPage = 1;
                Dialog.PrinterSettings.ToPage = 1;
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    PrinterSettings = Dialog.PrinterSettings;
                    PrintInfo = m_spread.Sheets[0].PrintInfo;
                }
                else
                {
                    ev.Cancel = true;
                }
            }

            if (ev.PrintAction == PrintAction.PrintToPrinter)
                UsePrintableArea = true;
            else
                UsePrintableArea = false;

            PageCount = 0;
            if (PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                PageIndex = PrinterSettings.FromPage < 1 ? 1 : PrinterSettings.FromPage;
            }
            else
            {
                PageIndex = 1;
            }
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
                                        e.PageBounds.X + DefaultPageSettings.Margins.Left - (int)Math.Round(PrintableArea.Left),
                                        e.PageBounds.Y + DefaultPageSettings.Margins.Top - (int)Math.Round(PrintableArea.Top),
                                        e.PageBounds.Width - DefaultPageSettings.Margins.Left - DefaultPageSettings.Margins.Right,
                                        e.PageBounds.Height - DefaultPageSettings.Margins.Top - DefaultPageSettings.Margins.Bottom
                                        );
                }
                else
                {
                    r = new Rectangle(
                                        e.PageBounds.X + DefaultPageSettings.Margins.Left,
                                        e.PageBounds.Y + DefaultPageSettings.Margins.Top,
                                        e.PageBounds.Width - DefaultPageSettings.Margins.Left - DefaultPageSettings.Margins.Right,
                                        e.PageBounds.Height - DefaultPageSettings.Margins.Top - DefaultPageSettings.Margins.Bottom
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

        public PrintInfo PrintInfo
        {
            get
            {
                return m_spread.Sheets[0].PrintInfo;
            }
            set
            {
                m_spread.SetPrintInfo(value, 0);
                //m_spread.Sheets[0].PrintInfo.Opacity = 100;
                DefaultPageSettings.Landscape = (value.Orientation == PrintOrientation.Landscape ? true : false);

                DefaultPageSettings.Margins.Left = value.Margin.Left;
                DefaultPageSettings.Margins.Right = value.Margin.Right;
                DefaultPageSettings.Margins.Top = value.Margin.Top;
                DefaultPageSettings.Margins.Bottom = value.Margin.Bottom;

                DefaultPageSettings.PaperSize = value.PaperSize;
                DefaultPageSettings.PaperSource = value.PaperSource;
            }
        }

        bool _ShowPrinterDialog = true;
        public bool ShowPrinterDialog
        {
            get
            {
                return _ShowPrinterDialog;
            }
            set
            {
                _ShowPrinterDialog = value;
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

                if (m_spread.Sheets.Count > 0)
                    m_spread.Sheets[0].SheetCorner.Cells[0, 0].Tag = !value;
            }
        }
    }
}
