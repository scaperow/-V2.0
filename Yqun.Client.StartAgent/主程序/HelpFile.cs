using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.ContextCache;

namespace Yqun.MainUI
{
    public partial class HelpFile : Form
    {
        public HelpFile()
        {
            InitializeComponent();

            this.Text = "用户手册 " + Application.ProductName;
            this.Icon = Yqun.MainUI.Properties.Resources.logo16;
        }

    }
}
