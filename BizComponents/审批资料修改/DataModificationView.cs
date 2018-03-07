using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread.Model;
using BizCommon;

namespace BizComponents
{
    public partial class DataModificationView : Form
    {
        public DataModificationView()
        {
            InitializeComponent();
        }

        private void DataModificationView_Load(object sender, EventArgs e)
        {
            toolStripLabel2.Text = DateTime.Now.ToShortDateString();
        }

        private void FpSpread_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            label_Sponsor.Text = FpSpread.ActiveSheet.Cells[e.Row, 3].Text;
            label_Date.Text = FpSpread.ActiveSheet.Cells[e.Row, 4].Text;
            TextBox_Content.Text = FpSpread.ActiveSheet.Cells[e.Row, 5].Text;
            TextBox_Reason.Text = FpSpread.ActiveSheet.Cells[e.Row, 6].Text;
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton Button = sender as ToolStripButton;
            int RowIndex = FpSpread.ActiveSheet.ActiveRowIndex;
            if (RowIndex != -1)
            {
                if (DialogResult.OK == MessageBox.Show("确认将选中的申请设置为 ‘" + Button.Text + "’？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    List<DataModificationInfo> Infos = new List<DataModificationInfo>();
                    List<String> IDs = new List<String>();

                    String Tag = FpSpread.ActiveSheet.ActiveRow.Tag.ToString();
                    String[] Tokens = Tag.Split(',');
                    IDs.Add(Tokens[0]);

                    Boolean Result = DepositoryDataModificationInfo.UpdateDataModificationInfo(IDs.ToArray(), Yqun.Common.ContextCache.ApplicationContext.Current.UserName, Button.Text,"");
                    String Message = (Result ? "设置成功。" : "设置失败");
                    MessageBoxIcon Icon = (Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);

                    if (Result)
                    {
                        FpSpread.ActiveSheet.Rows[RowIndex].Visible = false;
                    }
                }
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
