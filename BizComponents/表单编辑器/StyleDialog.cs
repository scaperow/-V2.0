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
    public partial class StyleDialog : Form
    {
        ConditionalFormat[] Formats;

        public StyleDialog(ConditionalFormat[] formats)
        {
            InitializeComponent();

            Formats = formats;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StyleDialog_Load(object sender, EventArgs e)
        {
            ConditionalFormat format = Formats[0];

            Font_Regular.Checked = (format.Style.Font.Style == FontStyle.Regular);
            Font_Bold.Checked = (format.Style.Font.Style == FontStyle.Bold);
            Font_Italic.Checked = (format.Style.Font.Style == FontStyle.Italic);
            Font_Strikeout.Checked = (format.Style.Font.Style == FontStyle.Strikeout);
            Font_Underline.Checked = (format.Style.Font.Style == FontStyle.Underline);

            label1.Font = format.Style.Font;

            ForeColorPicker.Value = format.Style.ForeColor;
            BackColorPicker.Value = format.Style.BackColor;
        }

        private void ForeColorPicker_ValueChanged(object sender, EventArgs e)
        {
            label1.ForeColor = ForeColorPicker.Value;
        }

        private void BackColorPicker_ValueChanged(object sender, EventArgs e)
        {
            label2.BackColor = BackColorPicker.Value;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            FontStyle Style = label1.Font.Style;
            if (sender == Font_Regular)
            {
                if (checkBox.Checked)
                {
                    Style = Style | FontStyle.Regular;
                }
                else
                {
                    Style = Style & ~FontStyle.Regular;
                }
            }
            else if (sender == Font_Bold)
            {
                if (checkBox.Checked)
                {
                    Style = Style | FontStyle.Bold;
                }
                else
                {
                    Style = Style & ~FontStyle.Bold;
                }
            }
            else if (sender == Font_Italic)
            {
                if (checkBox.Checked)
                {
                    Style = Style | FontStyle.Italic;
                }
                else
                {
                    Style = Style & ~FontStyle.Italic;
                }
            }
            else if (sender == Font_Strikeout)
            {
                if (checkBox.Checked)
                {
                    Style = Style | FontStyle.Strikeout;
                }
                else
                {
                    Style = Style & ~FontStyle.Strikeout;
                }
            }
            else if (sender == Font_Underline)
            {
                if (checkBox.Checked)
                {
                    Style = Style | FontStyle.Underline;
                }
                else
                {
                    Style = Style & ~FontStyle.Underline;
                }
            }

            label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size, Style, label1.Font.Unit, label1.Font.GdiCharSet);
        }
    }
}
