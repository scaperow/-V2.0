using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using ReportCommon;

namespace ReportComponents
{
    public partial class FormatStyleForm : Form
    {
        Dictionary<FormatStyle, FormatStringGroup> Groups = null;
        public FormatStyleForm()
        {
            InitializeComponent();

            lBox_FormatString.SelectedIndexChanged += new EventHandler(lBox_FormatString_SelectedIndexChanged);
        }

        FormatStyle _FormatStyle = FormatStyle.General;
        public FormatStyle FormatStyle
        {
            get
            {
                return _FormatStyle;
            }
            set
            {
                _FormatStyle = value;
            }
        }

        String _FormatString = "";
        public String FormatString
        {
            get
            {
                return _FormatString;
            }
            set
            {
                _FormatString = value;
            }
        }

        private void FormatStyleForm_Load(object sender, EventArgs e)
        {
            Groups = DepositoryReportFormat.InitReportFormats();

            //给每一类赋一个示例
            Groups[FormatStyle.General].Example = "";
            Groups[FormatStyle.Number].Example = "1234.56789";
            Groups[FormatStyle.Currency].Example = "1234.56789";
            Groups[FormatStyle.Date].Example = DateTime.Now.ToString();
            Groups[FormatStyle.Time].Example = DateTime.Now.ToString();
            Groups[FormatStyle.Percent].Example = "1234.56789";
            Groups[FormatStyle.ScientificCount].Example = "1234.56789";
            Groups[FormatStyle.Text].Example = "";


            if (_FormatStyle == FormatStyle.General)
            {
                rButton_General.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Number)
            {
                rButton_Number.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Currency)
            {
                rButton_Currency.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Percent)
            {
                rButton_Percent.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Date)
            {
                rButton_Date.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Time)
            {
                rButton_Time.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.ScientificCount)
            {
                rButton_ScientificCount.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Text)
            {
                rButton_Text.Checked = true;
            }

            ShowFormatString(_FormatStyle);

            foreach (FormatInfo String in lBox_FormatString.Items)
            {
                if (String.Format.ToLower() == FormatString.ToLower())
                {
                    lBox_FormatString.SelectedIndex = lBox_FormatString.Items.IndexOf(String);
                    break;
                }
            }
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            if (sender == rButton_General)
            {
                ShowFormatString(FormatStyle.General);
            }
            else if (sender == rButton_Number)
            {
                ShowFormatString(FormatStyle.Number);
            }
            else if (sender == rButton_Currency)
            {
                ShowFormatString(FormatStyle.Currency);
            }
            else if (sender == rButton_Percent)
            {
                ShowFormatString(FormatStyle.Percent);
            }
            else if (sender == rButton_Date)
            {
                ShowFormatString(FormatStyle.Date);
            }
            else if (sender == rButton_Time)
            {
                ShowFormatString(FormatStyle.Time);
            }
            else if (sender == rButton_ScientificCount)
            {
                ShowFormatString(FormatStyle.ScientificCount);
            }
            else if (sender == rButton_Text)
            {
                ShowFormatString(FormatStyle.Text);
            }
        }

        /// <summary>
        /// 加载指定类型的格式字符串到ListBox中
        /// </summary>
        /// <param name="formatStyle"></param>
        FormatStyle rButton_Style;
        private void ShowFormatString(FormatStyle formatStyle)
        {
            rButton_Style = formatStyle;

            #region 字典的实现

            lBox_FormatString.Items.Clear();
            tBox_FormatString.Text = "";
            label_Example.Text = "";

            FormatStringGroup NowformatStringGroup = Groups[formatStyle];
            if (NowformatStringGroup != null)
            {
                for (int i = 0; i < NowformatStringGroup.FormatInfos.Count; i++)
                {
                    lBox_FormatString.Items.Add(NowformatStringGroup.FormatInfos[i]);
                }

                if (lBox_FormatString.Items.Count > 0)
                {
                    lBox_FormatString.SelectedIndex = 0;
                }
            }

            #endregion
        }

        void lBox_FormatString_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBox_FormatString.SelectedIndex != -1)
            {
                tBox_FormatString.Text = lBox_FormatString.SelectedItem.ToString();
                if (rButton_Style != FormatStyle.Date && rButton_Style != FormatStyle.Time)
                {
                    label_Example.Text = Demonstration.Format(Groups[rButton_Style].Example, lBox_FormatString.SelectedItem.ToString());
                }
                else
                {
                    label_Example.Text = DateTimeDemonstration.Format(Groups[rButton_Style].Example, lBox_FormatString.SelectedItem.ToString());
                }
            }
        }

        private void Button_Append_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }

            Boolean Found = false;
            FormatStringGroup NowformatStringGroup = Groups[rButton_Style];
            for (int i = 0; i < NowformatStringGroup.FormatInfos.Count; i++)
            {
                if (NowformatStringGroup.FormatInfos[i].ToString() == textBox1.Text)
                {
                    Found = true;
                    break;
                }
            }

            if (!Found)
            {
                FormatInfo fs = new FormatInfo();
                fs.Format = textBox1.Text;
                fs.Style = rButton_Style;

                DepositoryReportFormat.UpdateReportFormats(fs);
                NowformatStringGroup.FormatInfos.Add(fs);
                lBox_FormatString.Items.Add(fs);
                tBox_FormatString.Text = lBox_FormatString.Items[0].ToString();
                textBox1.Text = "";
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            FormatInfo fs = (FormatInfo)lBox_FormatString.SelectedItem;
            FormatStringGroup Fsg = Groups[rButton_Style];
            if (fs != null)
            {
                if (DialogResult.Yes == MessageBox.Show("确定删除格式串 ‘" + fs.Format + "’ 吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DepositoryReportFormat.DeleteReportFormats(fs);
                    lBox_FormatString.Items.Remove(lBox_FormatString.SelectedItem);
                    Fsg.FormatInfos.Remove(fs);
                    if (lBox_FormatString.Items.Count > 0)
                    {
                        lBox_FormatString.SelectedIndex = 0;
                    }
                    else
                    {
                        tBox_FormatString.Text = "";
                    }
                }
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            _FormatStyle = rButton_Style;
            _FormatString = (lBox_FormatString.SelectedItem != null ? lBox_FormatString.SelectedItem.ToString() : "");
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class Demonstration
    {
        static FpSpread fpSpread;
        static SheetView sheetView;
        static FarPoint.Win.Spread.CellType.GeneralCellType cellType;
        static Demonstration()
        {
            fpSpread = new FpSpread();
            sheetView = new SheetView();
            fpSpread.Sheets.Add(sheetView);
            cellType = new FarPoint.Win.Spread.CellType.GeneralCellType();
            sheetView.Cells[0, 0].CellType = cellType;
        }

        public static String Format(String Value, String FormatString)
        {
            cellType.FormatString = FormatString;
            sheetView.Cells[0, 0].Text = Value;
            return sheetView.Cells[0, 0].Text;
        }
    }

    public class DateTimeDemonstration
    {
        static FpSpread fpSpread;
        static SheetView sheetView;
        static FarPoint.Win.Spread.CellType.DateTimeCellType cellType;
        static DateTimeDemonstration()
        {
            fpSpread = new FpSpread();
            sheetView = new SheetView();
            fpSpread.Sheets.Add(sheetView);
            cellType = new DateTimeCellType();
            sheetView.Cells[0, 0].CellType = cellType;
        }

        public static String Format(String Value, String FormatString)
        {
            cellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            cellType.UserDefinedFormat = FormatString;
            sheetView.Cells[0, 0].Value = Value;
            return sheetView.Cells[0, 0].Text;
        }

    }
}
