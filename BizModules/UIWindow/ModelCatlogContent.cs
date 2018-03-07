using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BizComponents;
using Yqun.Services;
using Yqun.Client;
using BizModules.Properties;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using BizCommon;
using Yqun.Bases;

namespace BizModules
{
    public partial class ModelCatlogContent : DockContent
    {
        internal ModelControl modelControl;
        internal SheetControl sheetControl;

        public ModelCatlogContent()
        {
            InitializeComponent();

            this.Icon = IconHelper.GetIcon(Resources.模板列表);

            modelControl = new ModelControl();
            modelControl.Dock = DockStyle.Fill;
            tPage_Model.Controls.Add(modelControl);

            sheetControl = new SheetControl();
            sheetControl.Dock = DockStyle.Fill;
            tPage_Sheet.Controls.Add(sheetControl);
            tPage_Sheet.Parent = (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ? tControl_Model : null);
        }

        private void ModelCatlogContent_Load(object sender, EventArgs e)
        {
            modelControl.InitModuleCatlog();
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                sheetControl.InitSheetCatlog();
        }
    }
}
