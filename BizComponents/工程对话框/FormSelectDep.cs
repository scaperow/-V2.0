using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class FormSelectDep : Form
    {
        Orginfo _UnitInfo;
        public Orginfo UnitInfo
        {
            get
            {
                return _UnitInfo;
            }
            set
            {
                _UnitInfo = value;
            }
        }

        public FormSelectDep()
        {
            InitializeComponent();
        }

        private void FormSelectDep_Load(object sender, EventArgs e)
        {
            cbCOMPACT.Items.Clear();
            List<Orginfo> UnitInfos = new List<Orginfo>();
            UnitInfos = DepositoryOrganInfo.QueryOrgans("","");
            if (UnitInfos.Count > 0)
            {
                foreach (Orginfo dep in UnitInfos)
                {
                    cbCOMPACT.Items.Add(dep);
                }
                cbCOMPACT.SelectedIndex = 0;
            }
        }

        private void cbCOMPACT_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitInfo = cbCOMPACT.SelectedItem as Orginfo;

            DepNameTxt.Text = UnitInfo.DepName;
            DepAbbrevTxt.Text = UnitInfo.DepAbbrev;
            DepTypeCom.Text = UnitInfo.DepType.ToString().Substring(6);
        }

        private void cbCOMPACT_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                ComboBox cBox = sender as ComboBox;
                Orginfo orgInfo = cBox.Items[e.Index] as Orginfo;

                if ((e.State & (DrawItemState.Selected | DrawItemState.Focus)) != (DrawItemState.Selected | DrawItemState.Focus))
                {
                    e.Graphics.DrawString(orgInfo.DepName, e.Font, Brushes.Black, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
                else
                {
                    e.Graphics.DrawString(orgInfo.DepName, e.Font, Brushes.White, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                }
            }

            e.DrawFocusRectangle();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ProtectBtn_Click(object sender, EventArgs e)
        {
            FormDep DepForm = new FormDep(this.cbCOMPACT);
            DepForm.ShowDialog();        
        }

        private void CancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
