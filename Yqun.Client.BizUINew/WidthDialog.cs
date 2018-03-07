using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yqun.Client.BizUI
{
    public partial class WidthDialog : Form
    {
        public WidthDialog()
        {
            InitializeComponent();
        }

        public new String Text
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return new Size(10,10);
            }
            set
            {
                base.MinimumSize = value;
            }
        }
    }
}
