using System;
using System.Windows.Forms;
using BizCommon;
using System.Text.RegularExpressions;

namespace BizComponents
{
    public partial class FormCreatProJect : Form
    {
        private string pattern = @"^[0-9]*$";
        private string param1 = null;
        private Project _ProjectInfo;
        public Project ProjectInfo
        {
            get
            {
                return _ProjectInfo;
            }
            set
            {
                _ProjectInfo = value;
            }
        }

        public FormCreatProJect()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPRJNAME.Text.Trim() == string.Empty)
            {
                MessageBox.Show("工程名称不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            if (!string.IsNullOrEmpty(ProjectInfo.Code) && txtOrderID.Text.Trim().Length != ProjectInfo.Code.Length)
            {
                MessageBox.Show("排序长度必须是" + ProjectInfo.Code.Length + "位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            ProjectInfo.Description = txtPRJNAME.Text;
            ProjectInfo.LineName = txtRoadName.Text;
            ProjectInfo.HigWayClassification = txtROADRANK.Text;

            ProjectInfo.PegFrom = QzMtxt.Text;
            ProjectInfo.PegTo = ZzMtxt.Text;

            double price = 0.0;
            double distance = 0.0;
            if (txtPLANINVEST.Text != string.Empty)
            {
                price = Convert.ToDouble(txtPLANINVEST.Text);
            }
            if (txtMILEAGE.Text != string.Empty)
            {
                distance = Convert.ToDouble(txtMILEAGE.Text);
            }

            ProjectInfo.ToltalPrice = price;
            ProjectInfo.TotalDistance = distance;
            ProjectInfo.OrderID = txtOrderID.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCreatProJect_Load(object sender, EventArgs e)
        {
            txtPRJNAME.Text = ProjectInfo.Description;
            txtRoadName.Text = ProjectInfo.LineName;
            txtROADRANK.Text = ProjectInfo.HigWayClassification;

            txtPLANINVEST.Text = ProjectInfo.ToltalPrice.ToString();
            txtMILEAGE.Text = ProjectInfo.TotalDistance.ToString();

            QzMtxt.Text = ProjectInfo.PegFrom;
            ZzMtxt.Text = ProjectInfo.PegTo;
            txtOrderID.Text = ProjectInfo.OrderID;
            //if (string.IsNullOrEmpty(ProjectInfo.OrderID))
            //{
            //    txtOrderID.Enabled = false;
            //}
            //else
            //{
            //    txtOrderID.Enabled = true;
            //}
        }

        private void txtOrderID_TextChanged(object sender, EventArgs e)
        {
            Match m = Regex.Match(this.txtOrderID.Text, pattern);   // 匹配正则表达式

            if (!m.Success)   // 输入的不是数字
            {
                this.txtOrderID.Text = param1;   // textBox内容不变

                // 将光标定位到文本框的最后
                this.txtOrderID.SelectionStart = this.txtOrderID.Text.Length;
            }
            else   // 输入的是数字
            {
                param1 = this.txtOrderID.Text;   // 将现在textBox的值保存下来
            }
        }
    }
}
