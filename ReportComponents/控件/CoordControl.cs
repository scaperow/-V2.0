using System;
using System.Windows.Forms;
using ReportCommon;

namespace ReportComponents
{
    public partial class CoordControl : UserControl
    {
        ToolStripItem Item = null;
        ContextMenuStrip menuStrip = new ContextMenuStrip();
        String Value = "";

        public CoordControl()
        {
            InitializeComponent();

            menuStrip.Items.Add(new ToolStripMenuItem("无"));
            menuStrip.Items.Add(new ToolStripSeparator());
            menuStrip.Items.Add(new ToolStripMenuItem("默认"));
            menuStrip.Items.Add(new ToolStripSeparator());
            menuStrip.Items.Add(new ToolStripMenuItem("自定义"));
            menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(menuStrip_ItemClicked);

            menuStrip.AutoSize = false;
            menuStrip.Height = 82;
            menuStrip.ShowImageMargin = false;
            menuStrip.DropShadowEnabled = false;

            numberControl1.ValueChanged += new EventHandler(numberControl_ValueChanged);
            numberControl2.ValueChanged += new EventHandler(numberControl_ValueChanged);
        }

        void numberControl_ValueChanged(object sender, EventArgs e)
        {
            String Value = numberControl1.Value + numberControl2.Value;
            ValueChangedEventArgs valuechangedevent = new ValueChangedEventArgs(ValueSource.UseDefined, Value);
            OnValueChanged(valuechangedevent);
        }

        void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Item = e.ClickedItem;

            ValueChangedEventArgs valuechangedevent = null;
            switch (e.ClickedItem.Text)
            {
                case "无":
                    label2.BringToFront();
                    valuechangedevent = new ValueChangedEventArgs(ValueSource.None, "");
                    OnValueChanged(valuechangedevent);
                    break;
                case "默认":
                    label1.BringToFront();
                    valuechangedevent = new ValueChangedEventArgs(ValueSource.Default, "");
                    OnValueChanged(valuechangedevent);
                    break;
                case "自定义":
                    numberControl1.Value = (Value != "" ? Coords.Split(Value)[0] : "A");
                    numberControl2.Value = (Value != "" ? Coords.Split(Value)[1] : "1");
                    panel1.BringToFront();
                    valuechangedevent = new ValueChangedEventArgs(ValueSource.UseDefined, Value);
                    OnValueChanged(valuechangedevent);
                    break;
            }

            Item = e.ClickedItem;
        }

        private void CoordControl_Resize(object sender, EventArgs e)
        {
            this.radioButton1.Top = 0;
            this.radioButton1.Left = (Width - this.radioButton1.Width > 0 ? Width - this.radioButton1.Width : 0);

            this.label1.Top = 0;
            this.label1.Left = 0;
            this.label1.Width = this.radioButton1.Left;
            this.label1.Height = this.Height;

            this.label2.Top = 0;
            this.label2.Left = 0;
            this.label2.Width = this.radioButton1.Left;
            this.label2.Height = this.Height;

            this.panel1.Top = 0;
            this.panel1.Left = 0;
            this.panel1.Width = this.radioButton1.Left;
            this.panel1.Height = this.Height;

            this.numberControl1.Left = 0;
            this.numberControl1.Top = 1;
            this.numberControl1.Width = this.label1.Width / 2;
            this.numberControl1.Height = 23;

            this.numberControl2.Left = this.numberControl1.Left + this.numberControl1.Width;
            this.numberControl2.Top = this.numberControl1.Top;
            this.numberControl2.Width = this.numberControl1.Width;
            this.numberControl2.Height = this.numberControl1.Height;

            foreach (ToolStripItem Item in menuStrip.Items)
            {
                Item.AutoSize = false;
                Item.Width = this.label1.Width;
            }

            menuStrip.Width = this.label1.Width;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            menuStrip.Show(this, this.label1.Left, this.label1.Top + this.label1.Height);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            menuStrip.Show(this, this.label1.Left, this.label1.Top + this.label1.Height);
        }

        public void setValue(ValueSource source, String value)
        {
            Value = value;

            if (source == ValueSource.Default)
            {
                label1.BringToFront();
            }
            else if (source == ValueSource.None)
            {
                label2.BringToFront();
            }
            else
            {
                numberControl1.Value = (Value != "" ? Coords.Split(value)[0] : "A");
                numberControl2.Value = (Value != "" ? Coords.Split(value)[1] : "1");
                panel1.BringToFront();
            }
        }

        public readonly static object ValueChangedEvent = new object();
        public event EventHandler<ValueChangedEventArgs> ValueChanged
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

        protected void OnValueChanged(ValueChangedEventArgs e)
        {
            EventHandler<ValueChangedEventArgs> h = Events[ValueChangedEvent] as EventHandler<ValueChangedEventArgs>;
            if (h != null)
            {
                h(this, e);
            }
        }
    }

    public enum ValueSource : byte
    {
        None,
        Default,
        UseDefined
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public ValueChangedEventArgs(ValueSource valueSource, String value)
        {
            this._ValueSource = valueSource;
            this._Value = value;
        }

        ValueSource _ValueSource = ValueSource.Default;
        public ValueSource ValueSource
        {
            get
            {
                return _ValueSource;
            }
        }

        String _Value = "";
        public String Value
        {
            get
            {
                return _Value;
            }
        }
    }
}
