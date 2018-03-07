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
    public partial class FormFolder : Form
    {
        PrjFolder _FolderInfo;
        public PrjFolder FolderInfo
        {
            get 
            {
                if (_FolderInfo == null)
                    _FolderInfo = new PrjFolder();
                return _FolderInfo; 
            }
            set 
            { 
                _FolderInfo = value; 
            }
        }


        public FormFolder()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tFolderName.Text))
            {
                MessageBox.Show("试验室名称不可为空！");             
            }
            else
            {
                //if (!string.IsNullOrEmpty(FolderInfo.FolderCode) && txtOrderID.Text.Trim().Length != FolderInfo.FolderCode.Length)
                //{
                //    MessageBox.Show("排序长度必须是" + FolderInfo.FolderCode.Length + "位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    this.DialogResult = DialogResult.None;
                //    return;
                //}

                FolderInfo.FolderName = tFolderName.Text;
                FolderInfo.OrderID = txtOrderID.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormFolder_Load(object sender, EventArgs e)
        {
            this.tFolderName.Text = FolderInfo.FolderName;

            txtOrderID.Text = FolderInfo.OrderID;
            //if (string.IsNullOrEmpty(FolderInfo.OrderID))
            //{
            //    txtOrderID.Enabled = false;
            //}
            //else
            //{
            //    txtOrderID.Enabled = true;
            //}
        }
    }
}
