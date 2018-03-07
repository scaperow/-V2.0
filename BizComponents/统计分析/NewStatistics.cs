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
    public partial class NewStatistics : Form
    {
        public Sys_Statistics_Item Result { set; get; }

        public NewStatistics(Sys_Statistics_Item model)
        {
            InitializeComponent();

            this.TextItemName.Text = model.Name;
            this.TextWeight.Text = model.Weight.ToString();
            this.RadioMaterial.Checked = model.Type == 1;
            this.Result = model;
        }

        public NewStatistics()
        {
            InitializeComponent();
        }

        private void Input_Load(object sender, EventArgs e)
        {

        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {

                if (Result == null)
                {
                    Result = new Sys_Statistics_Item();
                    Result.Columns = "";
                }

                Result.Name = TextItemName.Text;
                Result.Weight = int.Parse(TextWeight.Text);
                Result.Type = RadioMaterial.Checked ? 1 : 2;
                Result.Status = 1;
                DialogResult = DialogResult.OK;

            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void LabelWeight_Click(object sender, EventArgs e)
        {

        }

        private void TextItemName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(TextItemName.Text))
            {
                Errors.SetError(TextItemName, "项目名称不能为空");
                e.Cancel = true;
            }
            else
            {
                Errors.SetError(TextItemName, "");
            }
        }

        private void TextWeight_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(TextWeight.Text) == false)
            {
                var weight = 0;
                if (int.TryParse(TextWeight.Text, out weight))
                {
                    Errors.SetError(TextWeight, "");
                    return;
                }
                else
                {
                    Errors.SetError(TextWeight, "请输入数字格式的数据");
                }
            }
            else
            {
                Errors.SetError(TextWeight, "请输入不能为空");
            }

            e.Cancel = true;
        }
    }
}
