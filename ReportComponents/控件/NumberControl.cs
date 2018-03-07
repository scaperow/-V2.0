using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.Encoder;
using System.Text.RegularExpressions;

namespace ReportComponents
{
    public partial class NumberControl : UserControl
    {
        string charTest = @"^[A-Za-z]+$";
        string intTest = @"^[0-9]*$";

        public NumberControl()
        {
            InitializeComponent();

            vScrollBar1.ValueChanged += new EventHandler(vScrollBar1_ValueChanged);
        }

        private void NumberControl_Load(object sender, EventArgs e)
        {
            if (Style == DisplayStyle.Digital)
            {
                this.textBox1.Text = (vScrollBar1.Minimum + 1).ToString();
            }
            else if (Style == DisplayStyle.Letter)
            {
                this.textBox1.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(vScrollBar1.Minimum);
            }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (Style == DisplayStyle.Digital)
            {
                this.textBox1.Text = (vScrollBar1.Value + 1).ToString();
            }
            else if (Style == DisplayStyle.Letter)
            {
                this.textBox1.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(vScrollBar1.Value);
            }

            OnValueChanged(e);
        }

        private void NumberControl_Resize(object sender, EventArgs e)
        {
            vScrollBar1.Top = 0;
            vScrollBar1.Left = Width - vScrollBar1.Width;

            textBox1.Left = 0;
            textBox1.Top = vScrollBar1.Top;
            textBox1.Width = vScrollBar1.Left;
        }

        /// <summary>
        /// 显示方式
        /// </summary>
        DisplayStyle _Style = DisplayStyle.Digital;
        public DisplayStyle Style
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;

                if (Style == DisplayStyle.Digital)
                {
                    this.textBox1.Text = (vScrollBar1.Minimum + 1).ToString();
                }
                else if (Style == DisplayStyle.Letter)
                {
                    this.textBox1.Text = Arabic_Numerals_Convert.Excel_Word_Numerals(vScrollBar1.Minimum);
                }
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public int Min
        {
            get
            {
                return this.vScrollBar1.Minimum;
            }
            set
            {
                if (value >= 0)
                {
                    this.vScrollBar1.Minimum = value;
                }
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public int Max
        {
            get
            {
                return this.vScrollBar1.Maximum;
            }
            set
            {
                if (value >= 0)
                {
                    this.vScrollBar1.Maximum = value;
                }
            }
        }

        /// <summary>
        /// 获得坐标值
        /// </summary>
        public String Value
        {
            get
            {
                String Pattern = "";
                String Default = "";
                String _Value = "";
                if (_Style == DisplayStyle.Digital)
                {
                    Pattern = intTest;
                    Default = "1";
                }
                else if (_Style == DisplayStyle.Letter)
                {
                    Pattern = charTest;
                    Default = "A";
                }

                Regex r = new Regex(Pattern);
                if (r.IsMatch(textBox1.Text))
                {
                    _Value = textBox1.Text;
                }
                else
                {
                    _Value = Default;
                }

                return _Value;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public readonly static object ValueChangedEvent = new object();
        public event EventHandler ValueChanged
        {
            add
            {
                Events.AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(ValueChangedEvent, value);
            }
        }

        protected void OnValueChanged(EventArgs e)
        {
            EventHandler h = Events[ValueChangedEvent] as EventHandler;
            if (h != null)
            {
                h(this, e);
            }
        }
    }

    /// <summary>
    /// 显示方式
    /// </summary>
    public enum DisplayStyle : byte
    {
        Letter,
        Digital
    }
}
