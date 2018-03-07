using System;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class FormCreatPrjsc : Form
    {
        private Prjsct _PrjsctInfo;
        public Prjsct PrjsctInfo
        {
            get
            {
                return _PrjsctInfo;
            }
            set
            {
                _PrjsctInfo = value;
            }
        }
        
        public FormCreatPrjsc()
        {
            InitializeComponent();            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPjsct.Text.Trim() == string.Empty)
            {
                MessageBox.Show("标段名称不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            //if (!string.IsNullOrEmpty(PrjsctInfo.PrjsctCode) && txtOrderID.Text.Trim().Length != PrjsctInfo.PrjsctCode.Length)
            //{
            //    MessageBox.Show("排序长度必须是" + PrjsctInfo.PrjsctCode.Length + "位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    this.DialogResult = DialogResult.None;
            //    return;
            //}

            PrjsctInfo.PegFrom = QzMtxt.Text;
            PrjsctInfo.PegTo = ZzMtxt.Text;

            PrjsctInfo.PrjsctName = txtPjsct.Text;
            PrjsctInfo.OrderID = txtOrderID.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }

        private void FormCreatPrjsc_Load(object sender, EventArgs e)
        {
            txtPjsct.Text = PrjsctInfo.PrjsctName;
            QzMtxt.Text = PrjsctInfo.PegFrom;
            ZzMtxt.Text = PrjsctInfo.PegTo;

            txtOrderID.Text = PrjsctInfo.OrderID;
            //if (string.IsNullOrEmpty(PrjsctInfo.OrderID))
            //{
            //    txtOrderID.Enabled = false;
            //}
            //else
            //{
            //    txtOrderID.Enabled = true;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
