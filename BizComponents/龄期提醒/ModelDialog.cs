using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class ModelDialog : Form
    {
        public Guid ModuleID;

        public ModelDialog()
        {
            InitializeComponent();
        }

        private void ModelDialog_Load(object sender, EventArgs e)
        {
            DataTable dt = ModuleHelperClient.GetActiveModuleList();
            if (dt != null)
            {
                fpSpread_Models_Sheet.Rows.Count = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fpSpread_Models_Sheet.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    fpSpread_Models_Sheet.Rows[i].Tag = dt.Rows[i][0];
                    fpSpread_Models_Sheet.Cells[i, 0].Value = dt.Rows[i][0];
                    fpSpread_Models_Sheet.Cells[i, 1].Value = dt.Rows[i][1];
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (fpSpread_Models_Sheet.ActiveRow != null)
            {
                ModuleID = new Guid(fpSpread_Models_Sheet.ActiveRow.Tag.ToString());
                ModuleHelperClient.SaveStadiumConfig(ModuleID);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ModuleID = Guid.Empty;
                this.DialogResult = DialogResult.Cancel;
            }
            
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
