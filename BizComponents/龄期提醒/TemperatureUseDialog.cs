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
    public partial class TemperatureUseDialog : Form
    {
        private Guid DocumentID;
        private string TestRoomCode;
        public TemperatureUseDialog(Guid docID, string testRoomCode)
        {
            DocumentID = docID;
            TestRoomCode = testRoomCode;
            InitializeComponent();
        }

        private void SaveCustom()
        {
            //var temperature = new Sys_Temperature()
            //{
            //    IsSystem = 1,
            //    Name = TextCustomTemerature.Text
            //};

            //if (string.IsNullOrEmpty(temperature.Name))
            //{
            //    MessageBox.Show("类型名称不能为空");
            //    return;
            //}

            //var result = TemperatureHelperClient.NewTemperature(temperature);
            //if (string.IsNullOrEmpty(result))
            //{
            //    MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void SaveChoice()
        {

        }

        private void SetTemperatureToDocument()
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (TextCustomTemerature.Visible)
            //{
            //    SaveCustom();
            //}
            //else
            //{
            //    SaveChoice();
            //}
            var result = DocumentHelperClient.SaveDocumentTemperatureType(DocumentID, Convert.ToInt32(ComboAllTemperature.SelectedValue));

            if (result)
            {
                MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("设置失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Close();
            //int TemperatureType = 0;
            //if (rbSDNei.Checked)
            //{
            //    TemperatureType = 1;
            //}

            //Boolean bResult = DocumentHelperClient.SaveDocumentTemperatureType(DocumentID, TemperatureType);
            //if (bResult)
            //{
            //    MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("设置失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //this.Close();
        }

        private void TemperatureUseDialog_Load(object sender, EventArgs e)
        {
            ComboAllTemperature.DataSource = TemperatureHelperClient.GetTemperatureTypes(TestRoomCode);

            var table = DocumentHelperClient.GetDocumentExt(DocumentID);

            if (table != null && table.Rows.Count > 0)
            {
                var type = int.Parse(table.Rows[0]["TemperatureType"].ToString());

                ComboAllTemperature.SelectedValue = type;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LinkCustom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //LabelCustomTemerature.Visible = TextCustomTemerature.Visible = LinkSaveCustomTemerature.Visible = LinkCancel.Visible = true;
            //ComboAllTemperature.Enabled = LinkCustomTemerature.Enabled = false;
        }

        private void LinkSaveCustomTemerature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void LinkCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //LabelCustomTemerature.Visible = TextCustomTemerature.Visible = LinkSaveCustomTemerature.Visible = LinkCancel.Visible = false;
            //ComboAllTemperature.Enabled = LinkCustomTemerature.Enabled = true;
        }
    }
}
