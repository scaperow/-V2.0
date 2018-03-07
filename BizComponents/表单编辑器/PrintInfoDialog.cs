using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.SuperEdit;
using FarPoint.Excel;
using FarPoint.CalcEngine;
using FarPoint.PluginCalendar;
using FarPoint.PluginCalendar.WinForms;
using System.Collections;
using System.Data;
using FarPoint.Win;
using System.Runtime.Serialization;
using System.Reflection;
using System.Drawing.Printing;
using BizCommon;

namespace BizComponents
{
    public partial class PrintInfoDialog : Form
    {
        TextBox HeaderFooterTextBox;
        PaperSize DefaultPaperSize;

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
            foreach (PaperSize size in document.PrinterSettings.PaperSizes)
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
                PaperSize size = comboBox.Items[e.Index] as PaperSize;
                double Width = Convert.ToDouble(PrinterUnitConvert.Convert(size.Width * 1.0, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0);
                double Height = Convert.ToDouble(PrinterUnitConvert.Convert(size.Height * 1.0, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter) / 100.0);

                b = new SolidBrush(e.ForeColor);
                e.Graphics.DrawString(string.Format("{0} [{1}ÀåÃ×¡Á{2}ÀåÃ×]", size.PaperName, Width, Height), new Font(comboBox.Items[e.Index].ToString(), 10F), b, e.Bounds);
            }
            finally
            {
                if (b != null)
                    b.Dispose();
                b = null;
            }
        }


        PrintInfo pInfo;
        public PrintInfo PrintSet
        {
            get
            {
                pInfo = new PrintInfomation();
                PrintInfomation info = (PrintInfomation)pInfo;

                //Ò»°ã
                pInfo.UseMax = this.C_OnlyDataRow.Checked;
                pInfo.BestFitRows = this.C_AdjustRowHeight.Checked;
                pInfo.BestFitCols = this.C_AdjustColWidth.Checked;
                pInfo.ZoomFactor = (float)N_Zoom.Value / 100;

                //Êä³ö
                pInfo.ShowBorder = this.C_ShowBorder.Checked;
                pInfo.ShowColor = this.C_ShowColor.Checked;
                pInfo.ShowColumnHeader = (this.C_ShowColTitle.Checked? PrintHeader.Show:PrintHeader.Hide);
                pInfo.ShowRowHeader = (this.C_ShowRowTitle.Checked? PrintHeader.Show:PrintHeader.Hide);
                pInfo.ShowGrid = this.C_ShowGrid.Checked;
                pInfo.ShowShadows = this.C_ShowShadow.Checked;
                pInfo.ShowPrintDialog = true;

                //Ò³Ã¼Ò³½Å
                pInfo.Header = this.T_Header.Text;
                pInfo.Footer = this.T_Footer.Text;

                pInfo.Margin.Top = (int)UnitConverter.MillimeterToCentiInch(N_MarginUp.Value * 10);
                pInfo.Margin.Bottom = (int)UnitConverter.MillimeterToCentiInch(N_MarginDown.Value * 10);
                pInfo.Margin.Left = (int)UnitConverter.MillimeterToCentiInch(N_MarginLeft.Value * 10);
                pInfo.Margin.Right = (int)UnitConverter.MillimeterToCentiInch(N_MarginRight.Value * 10);
                pInfo.Margin.Header = (int)UnitConverter.MillimeterToCentiInch(N_MarginHeader.Value * 10);
                pInfo.Margin.Footer = (int)UnitConverter.MillimeterToCentiInch(N_MarginFooter.Value * 10);


                if (rButton_Portrait.Checked)
                    pInfo.Orientation = PrintOrientation.Portrait;
                if (rButton_Landscape.Checked)
                    pInfo.Orientation = PrintOrientation.Landscape;

                if (C_CenterHor.Checked && !C_CenterVer.Checked)
                {
                    this.pInfo.Centering = Centering.Horizontal;
                }
                else if (!C_CenterHor.Checked && C_CenterVer.Checked)
                {
                    this.pInfo.Centering = Centering.Vertical;
                }
                else if (C_CenterHor.Checked && C_CenterVer.Checked)
                {
                    this.pInfo.Centering = Centering.Both;
                }
                else if (!C_CenterHor.Checked && !C_CenterVer.Checked)
                {
                    this.pInfo.Centering = Centering.None;
                }

                if (this.comboBox_Papers.SelectedItem == null)
                {
                    this.comboBox_Papers.SelectedItem = DefaultPaperSize;
                }
                this.pInfo.PaperSize = this.comboBox_Papers.SelectedItem as PaperSize;

                return pInfo;
            }
            set
            {
                pInfo = value;
                try
                {
                    if (value == null) return;

                    C_OnlyDataRow.Checked = value.UseMax;
                    C_AdjustRowHeight.Checked = value.BestFitRows;
                    C_AdjustColWidth.Checked = value.BestFitCols;

					decimal dd = (decimal)value.ZoomFactor;
					if (dd == 0) 
					{
						dd = 1;
					}
                    N_Zoom.Value = (decimal)dd * 100;

                    //Êä³ö
                    C_ShowBorder.Checked = value.ShowBorder;
                    C_ShowColor.Checked = value.ShowColor;
                    C_ShowColTitle.Checked = (value.ShowColumnHeader == PrintHeader.Show);
                    C_ShowRowTitle.Checked = (value.ShowRowHeader == PrintHeader.Show);
                    C_ShowGrid.Checked = value.ShowGrid;
                    C_ShowShadow.Checked = value.ShowShadows;

                    //Ò³Ã¼Ò³½Å
                    T_Header.Text = value.Header;
                    T_Footer.Text = value.Footer;

                    N_MarginUp.Value = (UnitConverter.CentiInchToMillimeter(value.Margin.Top))/10;
                    N_MarginDown.Value = (UnitConverter.CentiInchToMillimeter(value.Margin.Bottom)) / 10;
                    N_MarginLeft.Value = (UnitConverter.CentiInchToMillimeter(value.Margin.Left)) / 10;
                    N_MarginRight.Value = (UnitConverter.CentiInchToMillimeter(value.Margin.Right)) / 10;
                    N_MarginHeader.Value = UnitConverter.CentiInchToMillimeter(value.Margin.Header) / 10;
                    N_MarginFooter.Value = UnitConverter.CentiInchToMillimeter(value.Margin.Footer) / 10;

                    rButton_Portrait.Checked = (pInfo.Orientation == PrintOrientation.Portrait);
                    rButton_Landscape.Checked = (pInfo.Orientation == PrintOrientation.Landscape);

                    if (pInfo.Centering == Centering.None)
                    {
                        C_CenterHor.Checked = false;
                        C_CenterVer.Checked = false;
                    }
                    else if (pInfo.Centering == Centering.Horizontal)
                    {
                        C_CenterHor.Checked = true;
                        C_CenterVer.Checked = false;
                    }
                    else if (pInfo.Centering == Centering.Vertical)
                    {
                        C_CenterHor.Checked = false;
                        C_CenterVer.Checked = true;
                    }
                    else if (pInfo.Centering == Centering.Both)
                    {
                        C_CenterHor.Checked = true;
                        C_CenterVer.Checked = true;
                    }

                    if (value.PaperSize == null)
                    {
                        value.PaperSize = DefaultPaperSize;
                    }
                    foreach (PaperSize size in comboBox_Papers.Items)
                    {
                        if (size.PaperName == value.PaperSize.PaperName)
                        {
                            comboBox_Papers.SelectedItem = size;
                            break;
                        }
                    } 
                }
                catch
                {}
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

            this.HeaderFooterTextBox.Text =str1 + "/p" + str2;
        }

        private void B_PageCount_Click(object sender, EventArgs e)
        {
            int x = this.HeaderFooterTextBox.SelectionStart;
            string str1 = this.HeaderFooterTextBox.Text.Substring(0, x);
            string str2 = this.HeaderFooterTextBox.Text.Substring(x, this.HeaderFooterTextBox.Text.Length - x);

            this.HeaderFooterTextBox.Text = str1 + "/pc" + str2;
        }
    }

    public class Papers
    {
        static Papers() 
        {
            if (_PaperDic == null) 
            {
                _PaperDic = new Dictionary<string, string>();
                _PaperDic.Add("A3","A3,1169,1654");
                _PaperDic.Add("A4", "A4,827,1169");
                _PaperDic.Add("A5", "A5,583,827");
                _PaperDic.Add("B5", "B5,717,1012");
            }
        }

        static Dictionary<string, string> _PaperDic;
        public static Dictionary<string, string> PaperDic 
        {
            get 
            {
                return _PaperDic;
            }
            set 
            {
                _PaperDic = value;
            }
        }
    }

    public class CellSerialize
    {
        public static void CopyPrintInfo(PrintInfo FromInfo, PrintInfo ToInfo)
        {
            Type t = FromInfo.GetType();
            PropertyInfo[] infos = t.GetProperties();            

            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo info = infos[i];
                if (info.CanWrite && info.CanRead)
                {
                    try
                    {
                        object o = info.GetValue(FromInfo,
                            null);

                        string name = info.Name;

                        info.SetValue(ToInfo, o, null);
                    }
                    catch { }
                }
            }
        }
    }
}