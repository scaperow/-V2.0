using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public partial class ConditionFormatDialog : Form
    {
        SheetView ActiveSheet;
        ConditionalFormat[] Formats;

        public ConditionFormatDialog(SheetView activeSheet)
        {
            InitializeComponent();

            ActiveSheet = activeSheet;
        }

        private void ConditionFormatDialog_Load(object sender, EventArgs e)
        {
            cBox_Value.Items.Add("单元格数值");

            cBox_Value.SelectedIndex = 0;

            //初始化活动单元格的条件格式化信息
            Formats = ActiveSheet.GetConditionalFormats(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);
            if (Formats == null || Formats.Length == 0)
            {
                ConditionalFormat cf = new ConditionalFormat();
                cf.ComparisonOperator = ComparisonOperator.Between;
                cf.Style = new NamedStyle();
                cf.Style.Font = ActiveSheet.ActiveCell.Font;
                cf.Style.BackColor = Color.White;
                cf.Style.ForeColor = Color.Black;

                Formats = new ConditionalFormat[1] { cf };
            }
            
            ConditionalFormat format = Formats[0];

            if (format.ComparisonOperator != ComparisonOperator.IsTrue &&
                format.ComparisonOperator != ComparisonOperator.IsFalse &&
                format.ComparisonOperator != ComparisonOperator.IsEmpty)
            {
                cBox_Value.SelectedIndex = 0;
            }

            if (cBox_Value.SelectedIndex == 0)
            {
                expressionControl1.BringToFront();

                expressionControl1.ComparisonOperator = format.ComparisonOperator;
                if (format.ComparisonOperator == ComparisonOperator.Between ||
                    format.ComparisonOperator == ComparisonOperator.NotBetween)
                {
                    expressionControl1.StartValue = format.FirstCondition;
                    expressionControl1.EndValue = format.LastCondition;
                }
                else
                {
                    expressionControl1.StartValue = format.FirstCondition;
                }
            }

            UpdateExample(format.Style);
        }

        private void cBox_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Value.SelectedItem.ToString() == "单元格数值")
            {
                expressionControl1.BringToFront();
                label1.Text = "说明：在文本框中填入常数或公式。示例：12.5,E6,IIF(F5=C6,A1,B1)";
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ActiveSheet.ClearConditionalFormats(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);

            ConditionalFormat format = Formats[0];
            format.ComparisonOperator = expressionControl1.ComparisonOperator;

            if (format.ComparisonOperator != ComparisonOperator.IsTrue &&
                format.ComparisonOperator != ComparisonOperator.IsFalse &&
                format.ComparisonOperator != ComparisonOperator.IsEmpty)
            {
                if (format.ComparisonOperator == ComparisonOperator.Between ||
                    format.ComparisonOperator == ComparisonOperator.NotBetween)
                {
                    format.FirstCondition = expressionControl1.StartValue;
                    format.LastCondition = expressionControl1.EndValue;
                    if (!string.IsNullOrEmpty(format.FirstCondition) && !string.IsNullOrEmpty(format.LastCondition))
                    {
                        ActiveSheet.SetConditionalFormat(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex, 1, 1, format.Style, format.ComparisonOperator, format.FirstCondition, format.LastCondition);
                    }
                }
                else
                {
                    format.FirstCondition = expressionControl1.StartValue;
                    if (!string.IsNullOrEmpty(format.FirstCondition))
                    {
                        ActiveSheet.SetConditionalFormat(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex, 1, 1, format.Style, format.ComparisonOperator, format.FirstCondition);
                    }
                }
            }

            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonFormatDialog_Click(object sender, EventArgs e)
        {
            StyleDialog Dialog = new StyleDialog(Formats);
            if (DialogResult.OK == Dialog.ShowDialog())
            {
                ConditionalFormat format = Formats[0];

                FontStyle Style = FontStyle.Regular;
                if (Dialog.Font_Regular.Checked)
                    Style = Style | FontStyle.Regular;
                if (Dialog.Font_Bold.Checked)
                    Style = Style | FontStyle.Bold;
                if (Dialog.Font_Italic.Checked)
                    Style = Style | FontStyle.Italic;
                if (Dialog.Font_Strikeout.Checked)
                    Style = Style | FontStyle.Strikeout;
                if (Dialog.Font_Underline.Checked)
                    Style = Style | FontStyle.Underline;
                format.Style.Font = new Font(format.Style.Font.FontFamily, format.Style.Font.Size, Style, format.Style.Font.Unit, format.Style.Font.GdiCharSet);
                format.Style.ForeColor = Dialog.ForeColorPicker.Value;
                format.Style.BackColor = Dialog.BackColorPicker.Value;

                UpdateExample(format.Style);
            }
        }

        void UpdateExample(NamedStyle Style)
        {
            lb_Example.Font = Style.Font;
            lb_Example.ForeColor = Style.ForeColor;
            lb_Example.BackColor = Style.BackColor;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ActiveSheet.ClearConditionalFormats(ActiveSheet.ActiveRowIndex, ActiveSheet.ActiveColumnIndex);

            expressionControl1.StartValue = "";
            expressionControl1.EndValue = "";

            lb_Example.Font = new Font(lb_Example.Font.FontFamily, lb_Example.Font.Size, FontStyle.Regular, lb_Example.Font.Unit, lb_Example.Font.GdiCharSet);
            lb_Example.ForeColor = Color.Black;
            lb_Example.BackColor = Color.White;

        }
    }
}
