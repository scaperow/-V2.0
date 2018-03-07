using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BizModules.Properties;
using Yqun.Bases;

namespace BizModules
{
    public partial class QueryDataContent : DockContent
    {
        public QueryDataContent()
        {
            InitializeComponent();

            this.Icon = IconHelper.GetIcon(Resources.查询);
        }

        public BizWindow BizWindow
        {
            get
            {
                return DockPanel.Parent as BizWindow;
            }
        }

        private void ButtonQueryData_Click(object sender, EventArgs e)
        {
            BizWindow.BizViewerContent.DataViewControl.QueryData();
        }
    }
}
