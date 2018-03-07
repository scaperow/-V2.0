using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizModules
{
    public partial class CellStyleSetting : Form
    {
        private Guid SheetID;
        private string CellName;
        private JZCellStyle CellStyle;
        public CellStyleSetting(Guid _SheetID, string _CellName, JZCellStyle _JZCellStyle)
        {
            SheetID = _SheetID;
            CellName = _CellName;
            CellStyle = _JZCellStyle;
            
            InitializeComponent();
            propertyGrid1.SelectedObject = CellStyle;
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            CellStyle = (JZCellStyle)propertyGrid1.SelectedObject;
            bool bSuccess = BizComponents.ModuleHelperClient.SaveCellStyle(SheetID, CellName, CellStyle);
            if (bSuccess)
            {
                SheetDesinger sheetDesinger = (SheetDesinger)this.Owner;
                sheetDesinger.CurrentCellStyle = CellStyle;
                this.DialogResult = DialogResult.OK;
            }
            else {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

    }
}
