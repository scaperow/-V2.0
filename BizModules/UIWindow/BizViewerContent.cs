using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BizComponents;
using FarPoint.Win.Spread;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using BizModules.Properties;
using System.Diagnostics;
using Yqun.Client;
using FarPoint.Win.Spread.Model;
using Yqun.Client.BizUI;
using BizCommon;
using Yqun.Bases;

namespace BizModules
{
    public partial class BizViewerContent : DockContent 
    {
        internal DataViewControl DataViewControl;

        public BizViewerContent()
        {
            InitializeComponent();

            this.Icon = IconHelper.GetIcon(Resources.台账面板);

            DataViewControl = new DataViewControl(this);
            DataViewControl.Dock = DockStyle.Fill;
            Controls.Add(DataViewControl);
        }

        public BizWindow BizWindow
        {
            get
            {
                return DockPanel.Parent as BizWindow;
            }
        }

        public void SetDockState(Boolean fullscreen)
        {
            if (fullscreen)
            {
                DockState = DockState.Float;
            }
            else
            {
                DockState = PanelPane.DockState;
            }
        }

        private void BizViewerContent_DockStateChanged(object sender, EventArgs e)
        {
            CloseButtonVisible = (DockState != DockState.Float);

            if (DockState == DockState.Float)
            {
                Rectangle r = Screen.PrimaryScreen.WorkingArea;
                FloatPane.FloatWindow.SetBounds(r.X, r.Y, r.Width, r.Height);
                FloatPane.FloatWindow.Text = "双击退出全屏";
                FloatPane.FloatWindow.Refresh();
            }
        }
    }
}
