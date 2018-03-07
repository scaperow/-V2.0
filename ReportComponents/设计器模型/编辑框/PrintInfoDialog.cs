using System;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using System.Drawing.Printing;

namespace ReportComponents
{
    public partial class PrintInfoDialog : Form
    {
        TextBox HeaderFooterTextBox;
        System.Drawing.Printing.PaperSize DefaultPaperSize;

        public PrintInfoDialog()
        {
            InitializeComponent();
            LoadPapers();

            HeaderFooterTextBox = T_Header;
        }

        void LoadPapers()
        {
            comboBox_Papers.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox_Papers.DrawItem -= new DrawItemEventHandler(comboBox_Papers_DrawItem);
            comboBox_Papers.DrawItem += new DrawItemEventHandler(comboBox_Papers_DrawItem);
            comboBox_Papers.DropDownClosed -= new EventHandler(comboBox_Papers_DropDownClosed);
            comboBox_Papers.DropDownClosed += new EventHandler(comboBox_Papers_DropDownClosed);

            comboBox_Papers.Items.Clear();
            PrintDocument document = new PrintDocument();
            foreach (System.Drawing.Printing.PaperSize size in document.PrinterSettings.PaperSizes)
            {
                if (size.PaperName.ToLower() == "a4")
                    DefaultPaperSize = size;

                comboBox_Papers.Items.Add(size);
            }
        }

        void comboBox_Papers_DropDownClosed(object sender, EventArgs e)
        {
            B_OK.Focus();
        }

        void comboBox_Papers_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (e.Index == -1 || e.Index >= comboBox.Items.Count)
                return;

            e.DrawBackground();

            if ((e.State & DrawItemState.Focus) != 0)
                e.DrawFocusRectangle();
            Brush b = null;

            try
            {
                System.Drawing.Printing.PaperSize size = comboBox.Items[e.Index] as System.Drawing.Printing.PaperSize;
                double Width = Convert.ToDouble(PrinterUnitConvert.Convert(size.Width * 1.0, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0);
                double Height = Convert.ToDouble(PrinterUnitConvert.Convert(size.Height * 1.0, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0);

                b = new SolidBrush(e.ForeColor);
                e.Graphics.DrawString(string.Format("{0} [{1}厘米×{2}厘米]", size.PaperName, Width, Height), new Font(comboBox.Items[e.Index].ToString(), 10F), b, e.Bounds);
            }
            finally
            {
                if (b != null)
                    b.Dispose();
                b = null;
            }
        }

        PrintInfo _PrintInfo;
        public PrintInfo PrintSet
        {
            get
            {
                _PrintInfo = new PrintInfo();

                //一般
                _PrintInfo.UseMax = this.C_OnlyDataRow.Checked;
                _PrintInfo.BestFitRows = this.C_AdjustRowHeight.Checked;
                _PrintInfo.BestFitCols = this.C_AdjustColWidth.Checked;
                _PrintInfo.ZoomFactor = (float)N_Zoom.Value / 100;

                //输出
                _PrintInfo.ShowBorder = this.C_ShowBorder.Checked;
                _PrintInfo.ShowColor = this.C_ShowColor.Checked;
                _PrintInfo.ShowColumnHeader = (this.C_ShowColTitle.Checked? PrintHeader.Show:PrintHeader.Hide);
                _PrintInfo.ShowRowHeader = (this.C_ShowRowTitle.Checked ? PrintHeader.Show : PrintHeader.Hide);
                _PrintInfo.ShowGrid = this.C_ShowGrid.Checked;
                _PrintInfo.ShowShadows = this.C_ShowShadow.Checked;
                _PrintInfo.ShowPrintDialog = true;

                //页眉页脚
                _PrintInfo.Header = this.T_Header.Text;
                _PrintInfo.Footer = this.T_Footer.Text;

                //页边距
                _PrintInfo.Margin.Top = Convert.ToInt32(PrinterUnitConvert.Convert(Convert.ToDouble(N_MarginUp.Value * 100), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                _PrintInfo.Margin.Bottom = Convert.ToInt32(PrinterUnitConvert.Convert(Convert.ToDouble(N_MarginDown.Value * 100), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                _PrintInfo.Margin.Left = Convert.ToInt32(PrinterUnitConvert.Convert(Convert.ToDouble(N_MarginLeft.Value * 100), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                _PrintInfo.Margin.Right = Convert.ToInt32(PrinterUnitConvert.Convert(Convert.ToDouble(N_MarginRight.Value * 100), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                _PrintInfo.Margin.Header = Convert.ToInt32(PrinterUnitConvert.Convert(Convert.ToDouble(N_MarginHeader.Value * 100), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                _PrintInfo.Margin.Footer = Convert.ToInt32(PrinterUnitConvert.Convert(Convert.ToDouble(N_MarginFooter.Value * 100), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

                //页面方向
                if (rButton_Portrait.Checked)
                    _PrintInfo.Orientation = PrintOrientation.Portrait;
                if (rButton_Landscape.Checked)
                    _PrintInfo.Orientation = PrintOrientation.Landscape;

                //对齐方式
                if (C_CenterHor.Checked && !C_CenterVer.Checked)
                {
                    _PrintInfo.Centering = Centering.Horizontal;
                }
                else if (!C_CenterHor.Checked && C_CenterVer.Checked)
                {
                    _PrintInfo.Centering = Centering.Vertical;
                }
                else if (C_CenterHor.Checked && C_CenterVer.Checked)
                {
                    _PrintInfo.Centering = Centering.Both;
                }
                else if (!C_CenterHor.Checked && !C_CenterVer.Checked)
                {
                    _PrintInfo.Centering = Centering.None;
                }

                if (this.comboBox_Papers.SelectedItem == null)
                {
                    this.comboBox_Papers.SelectedItem = DefaultPaperSize;
                }
                _PrintInfo.PaperSize = this.comboBox_Papers.SelectedItem as System.Drawing.Printing.PaperSize;

                //重复标题范围
                _PrintInfo.RepeatRowStart = Convert.ToInt32(HeaderRow1.Value);
                _PrintInfo.RepeatRowEnd = Convert.ToInt32(HeaderRow2.Value);
                _PrintInfo.RepeatColStart = Convert.ToInt32(HeaderCol1.Value);
                _PrintInfo.RepeatColEnd = Convert.ToInt32(HeaderCol2.Value);

                //重复结尾范围
                _PrintInfo.RowStart = Convert.ToInt32(FooterRow1.Value);
                _PrintInfo.RowEnd = Convert.ToInt32(FooterRow2.Value);
                _PrintInfo.ColStart = Convert.ToInt32(FooterCol1.Value);
                _PrintInfo.ColEnd = Convert.ToInt32(FooterCol2.Value);

                //页面打印顺序
                _PrintInfo.PageOrder = PrintPageOrder.DownThenOver;

                return _PrintInfo;
            }
            set
            {
                _PrintInfo = value;

                if (_PrintInfo == null) return;

                PrintInfo Info = _PrintInfo;

                C_OnlyDataRow.Checked = Info.UseMax;
                C_AdjustRowHeight.Checked = Info.BestFitRows;
                C_AdjustColWidth.Checked = Info.BestFitCols;

                N_Zoom.Value = Convert.ToDecimal(Info.ZoomFactor > 0 ? Info.ZoomFactor : 1) * 100;

                //输出
                C_ShowBorder.Checked = Info.ShowBorder;
                C_ShowColor.Checked = Info.ShowColor;
                C_ShowColTitle.Checked = (Info.ShowColumnHeader == PrintHeader.Hide ? false : true);
                C_ShowRowTitle.Checked = (Info.ShowRowHeader == PrintHeader.Hide ? false : true);
                C_ShowGrid.Checked = Info.ShowGrid;
                C_ShowShadow.Checked = Info.ShowShadows;

                //页眉页脚
                T_Header.Text = Info.Header;
                T_Footer.Text = Info.Footer;

                //页边距
                N_MarginUp.Value = Math.Round(Convert.ToDecimal(PrinterUnitConvert.Convert(Info.Margin.Top, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0M));
                N_MarginDown.Value = Math.Round(Convert.ToDecimal(PrinterUnitConvert.Convert(Info.Margin.Bottom, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0M));
                N_MarginLeft.Value = Math.Round(Convert.ToDecimal(PrinterUnitConvert.Convert(Info.Margin.Left, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0M));
                N_MarginRight.Value = Math.Round(Convert.ToDecimal(PrinterUnitConvert.Convert(Info.Margin.Right, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0M));
                N_MarginHeader.Value = Math.Round(Convert.ToDecimal(PrinterUnitConvert.Convert(Info.Margin.Header, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0M));
                N_MarginFooter.Value = Math.Round(Convert.ToDecimal(PrinterUnitConvert.Convert(Info.Margin.Footer, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0M));

                //页面方向
                rButton_Portrait.Checked = (_PrintInfo.Orientation == PrintOrientation.Portrait);
                rButton_Landscape.Checked = (_PrintInfo.Orientation == PrintOrientation.Landscape);

                //对齐方式
                if (_PrintInfo.Centering == Centering.None)
                {
                    C_CenterHor.Checked = false;
                    C_CenterVer.Checked = false;
                }
                else if (_PrintInfo.Centering == Centering.Horizontal)
                {
                    C_CenterHor.Checked = true;
                    C_CenterVer.Checked = false;
                }
                else if (_PrintInfo.Centering == Centering.Vertical)
                {
                    C_CenterHor.Checked = false;
                    C_CenterVer.Checked = true;
                }
                else if (_PrintInfo.Centering == Centering.Both)
                {
                    C_CenterHor.Checked = true;
                    C_CenterVer.Checked = true;
                }

                if (Info.PaperSize == null)
                {
                    Info.PaperSize = DefaultPaperSize;
                }
                foreach (System.Drawing.Printing.PaperSize size in comboBox_Papers.Items)
                {
                    if (size.PaperName == Info.PaperSize.PaperName)
                    {
                        comboBox_Papers.SelectedItem = size;
                        break;
                    }
                }

                HeaderRow1.Value = Info.RepeatRowStart;
                HeaderRow2.Value = Info.RepeatRowEnd;
                HeaderCol1.Value = Info.RepeatColStart;
                HeaderCol2.Value = Info.RepeatColEnd;

                FooterRow1.Value = Info.RowStart;
                FooterRow2.Value = Info.RowEnd;
                FooterCol1.Value = Info.ColStart;
                FooterCol2.Value = Info.ColEnd;
            }
        }

        private void PrintInfoDialog_Load(object sender, EventArgs e)
        {
            T_Header.GotFocus += new EventHandler(T_Header_GotFocus);
            T_Footer.GotFocus += new EventHandler(T_Footer_GotFocus);
        }

        void T_Footer_GotFocus(object sender, EventArgs e)
        {
            this.HeaderFooterTextBox = this.T_Footer;
            int x = this.HeaderFooterTextBox.Text.IndexOf("/l");
            if (x != -1)
            {
                this.C_Left.Checked = true;
            }
            else
            {
                this.C_Left.Checked = false;
            }

            x = this.HeaderFooterTextBox.Text.IndexOf("/r");
            if (x != -1)
            {
                this.C_Right.Checked = true;
            }
            else
            {
                this.C_Right.Checked = false;
            }

            x = this.HeaderFooterTextBox.Text.IndexOf("/c");
            if (x != -1)
            {
                this.C_Center.Checked = true;
            }
            else
            {
                this.C_Center.Checked = false;
            }
        }

        void T_Header_GotFocus(object sender, EventArgs e)
        {
            this.HeaderFooterTextBox = this.T_Header;
            int x = this.HeaderFooterTextBox.Text.IndexOf("/l");
            if (x != -1)
            {
                this.C_Left.Checked = true;
            }
            else
            {
                this.C_Left.Checked = false;
            }

            x = this.HeaderFooterTextBox.Text.IndexOf("/r");
            if (x != -1)
            {
                this.C_Right.Checked = true;
            }
            else
            {
                this.C_Right.Checked = false;
            }

            x = this.HeaderFooterTextBox.Text.IndexOf("/c");
            if (x != -1)
            {
                this.C_Center.Checked = true;
            }
            else
            {
                this.C_Center.Checked = false;
            }
        }

        private void B_Font_Click(object sender, EventArgs e)
        {
            FontDialog dia = new FontDialog();
            DialogResult re = dia.ShowDialog();
            if (re != DialogResult.OK)
            {
                return;
            }

            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fb0", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fb1", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fi0", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fi1", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fu0", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fu1", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fk0", "");
            this.HeaderFooterTextBox.Text =
                this.HeaderFooterTextBox.Text.Replace("/fk1", "");

            int x = HeaderFooterTextBox.Text.IndexOf("/fn\"");
            if (x != -1)
            {
                string str1 = HeaderFooterTextBox.Text.Substring(0, x);
                string str2 = HeaderFooterTextBox.Text.Substring(x + 4,
                    HeaderFooterTextBox.Text.Length - x - 4);

                int y = str2.IndexOf("\"");
                if (y != -1)
                {
                    str2 =
                        str2.Substring(y + 1,
                        str2.Length - y - 1);
                }

                this.HeaderFooterTextBox.Text = str1 + str2;
            }

            x = HeaderFooterTextBox.Text.IndexOf("/fz\"");
            if (x != -1)
            {
                string str1 = HeaderFooterTextBox.Text.Substring(0, x);
                string str2 = HeaderFooterTextBox.Text.Substring(x + 4,
                    HeaderFooterTextBox.Text.Length - x - 4);

                int y = str2.IndexOf("\"");
                if (y != -1)
                {
                    str2 =
                        str2.Substring(y + 1,
                        str2.Length - y - 1);
                }

                this.HeaderFooterTextBox.Text = str1 + str2;
            }

            string str = "";
            Font f = dia.Font;
            str += "/fn" + "\"" + f.Name + "\"";
            str += "/fz" + "\"" + f.Size + "\"";
            if (f.Bold)
            {
                str += "/fb1";
            }
            else
            {
                str += "/fb0";
            }

            if (f.Italic)
            {
                str += "/fi1";
            }
            else
            {
                str += "/fi0";
            }

            if (f.Underline)
            {
                str += "/fu1";
            }
            else
            {
                str += "/fu0";
            }

            if (f.Strikeout)
            {
                str += "/fk1";
            }
            else
            {
                str += "/fk0";
            }

            this.HeaderFooterTextBox.Text = str + this.HeaderFooterTextBox.Text;
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                this.HeaderFooterTextBox.Text = this.HeaderFooterTextBox.Text.Replace("/c", "");
                this.HeaderFooterTextBox.Text = this.HeaderFooterTextBox.Text.Replace("/r", "");
                this.HeaderFooterTextBox.Text = this.HeaderFooterTextBox.Text.Replace("/l", "");

                if (r.Equals(this.C_Center))
                {
                    this.HeaderFooterTextBox.Text = "/c" + this.HeaderFooterTextBox.Text;
                }

                if (r.Equals(this.C_Left))
                {
                    this.HeaderFooterTextBox.Text = "/l" + this.HeaderFooterTextBox.Text;
                }

                if (r.Equals(this.C_Right))
                {
                    this.HeaderFooterTextBox.Text = "/r" + this.HeaderFooterTextBox.Text;
                }
            }
        }

        private void B_PageIndex_Click(object sender, EventArgs e)
        {
            int x = this.HeaderFooterTextBox.SelectionStart;
            string str1 = this.HeaderFooterTextBox.Text.Substring(0, x);
            string str2 = this.HeaderFooterTextBox.Text.Substring(x, this.HeaderFooterTextBox.Text.Length - x);

            this.HeaderFooterTextBox.Text = str1 + "/p" + str2;
        }

        private void B_PageCount_Click(object sender, EventArgs e)
        {
            int x = this.HeaderFooterTextBox.SelectionStart;
            string str1 = this.HeaderFooterTextBox.Text.Substring(0, x);
            string str2 = this.HeaderFooterTextBox.Text.Substring(x, this.HeaderFooterTextBox.Text.Length - x);

            this.HeaderFooterTextBox.Text = str1 + "/pc" + str2;
        }
    }

    

    
}