using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using System.IO;

namespace BizComponents
{
    public partial class StadiumDialog : Form
    {
        public StadiumDialog()
        {
            InitializeComponent();
        }

        private void StadiumDialog_Load(object sender, EventArgs e)
        {
            BindModelList();
        }

        public void BindModelList()
        {
            //ListView_StadiumInfo.Items.Clear();

            //DataTable dt = DepositoryStadiumInfo.InitStadiumConfig();
            //if (dt != null)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        ListViewItem Item = new ListViewItem(row["Description"].ToString());
            //        Item.Tag = row;
            //        Item.SubItems.Add(row["SearchRange"].ToString());
            //        ListView_StadiumInfo.Items.Add(Item);
            //    }
            //}
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (sender == Button_AddItem)
            //{
            //    ItemInfoDialog Dialog = new ItemInfoDialog(null, null);
            //    Dialog.ShowDialog();
            //}
            //else if (sender == Button_DeleteItem)
            //{
            //    if (ListView_StadiumInfo.SelectedItems.Count > 0)
            //    {
            //        DataRow row = ListView_StadiumInfo.SelectedItems[0].Tag as DataRow;
            //        String Message = string.Format("确认删除试验{0}的龄期配置吗？", row["Description"].ToString());
            //        if (DialogResult.OK == MessageBox.Show(Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            //        {
            //            Boolean r = DepositoryStadiumInfo.DeleteStadiumInfo(row["ID"].ToString());
            //            if (r)
            //            {
            //                ListView_StadiumInfo.SelectedItems[0].Remove();
            //            }
            //            Message = (r ? "删除试验龄期配置成功！" : "删除试验龄期配置失败！");
            //            MessageBoxIcon Icon = (r ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            //            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
            //        }
            //    }
            //}
            //else if (sender == bt_moduleMove)
            //{
            //    ModelDialog Dialog = new ModelDialog();
            //    Dialog.ShowDialog();
            //}
            //DepositoryStadiumInfo.MoveSheetCellLogic();
            DepositoryStadiumInfo.MoveSheetFormulas();
            MessageBox.Show("finished");
        }

        private void ListView_StadiumInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem Item = ListView_StadiumInfo.GetItemAt(e.X, e.Y);
            if (Item == null)
                return;

            ItemInfoDialog Dialog = new ItemInfoDialog(Item.Tag as DataRow, null);
            Dialog.ShowDialog();
            //BindModelList();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 生成本地测试数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_createTest_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.CreateTestStadiumData();
            MessageBox.Show("ok");
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.DeleteWrongStadiumData();
            MessageBox.Show("ok");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.SpStadiumData();
            MessageBox.Show("ok");
        }

        private void b_bgrq_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.CopyBGRQToExtentTable();
            MessageBox.Show("ok");
        }

        private void b_IsQualified_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.UpdateIsQualified();
            MessageBox.Show("ok");
        }

        private void bt_UpdateModel_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.UpdateModulesVision();
            MessageBox.Show("ok");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            dmh.MoveDocument();
            MessageBox.Show("资料导入完成");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //修改日志迁移
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            dmh.MoveEditLog();
            MessageBox.Show("修改日志迁移完成");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //不合格报表迁移
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            dmh.MoveInvalidDocument();
            MessageBox.Show("不合格报表迁移完成");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //审批修改迁移
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            dmh.MoveModifyChange();
            MessageBox.Show("审批修改迁移完成");
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //龄期迁移
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            dmh.MoveStadiumData();
            MessageBox.Show("龄期迁移完成");
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            TestCaiJiUpload();
        }

        private void TestCaiJiUpload()
        {
            //模拟上传试验数据
            //TestDemo td = new TestDemo();
            //td.ShowDialog();
        }


        private void bt_moduleMove_Click(object sender, EventArgs e)
        {
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            dmh.AutoSaveAllDocuments();
            MessageBox.Show("自动保存完成");
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Boolean flag = DocumentHelperClient.GeneratePLDTable();
            MessageBox.Show(flag.ToString());
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            UpdateModule um = new UpdateModule();
            um.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            MoveDocumentDialog mdd = new MoveDocumentDialog();
            mdd.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            DepositoryStadiumInfo.GenerateCellLogic();
            MessageBox.Show("finish");
        }

        private void ButtonDeviceConvert_Click(object sender, EventArgs e)
        {
           DataMoveHelperClient.ConvertDevice();
        }

    }
}
