using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace BizUpgrade
{
    public partial class UpgradeBar : Form
    {
        public UpgradeBar()
        {
            InitializeComponent();
        }

        public void SetProgressBar(String title, Int32 step, Int32 max)
        {
            StatusLabel.Text = title;
            progressBar1.Value = step;
            progressBar1.Maximum = max;
        }
    }
}
