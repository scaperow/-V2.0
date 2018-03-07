using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using BizCommon;
using FarPoint.Win.Spread;
using Yqun.Bases;
using System.IO;
using System.Collections;
using System.ServiceModel;
using TransferServiceCommon;
using Kingrocket.NotifyContract;

namespace BizComponents
{
    public partial class NotifyUserMsgDlg : Form
    {
        DataTable DataOnlineUser;
        public NotifyUserMsgDlg()
        {
            InitializeComponent();
        }
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            List<string> lstReceiveIDs = new List<string>();
            string strMsg = string.Empty;
            for (int i = 0; i < FpSpread_Info.Rows.Count; i++)
            {
                Cell cell = FpSpread_Info.Cells[i, 2];
                if (cell.Text.ToLower() == "true")
                {
                    lstReceiveIDs.Add(FpSpread_Info.Cells[i, 0].Text);
                }
            }
            if (lstReceiveIDs.Count <= 0)
            {
                lblMsg.Text = "请选择用户";
                return;
            }
            if (string.IsNullOrEmpty(txtMsg.Text.Trim()))
            {
                lblMsg.Text = "请填写消息内容";
                return;
            }
            strMsg = "msg:" + txtMsg.Text.Trim();//msg:表示是消息 cmd:表示是命令

            IMessageService svc;
            DuplexChannelFactory<IMessageService> channelFactory;
            var client = new Kingrocket.NotifyClient.MessageClient();
            InstanceContext instanceContext = new InstanceContext(client);
            NetTcpBinding ntb = new NetTcpBinding(SecurityMode.None);
            channelFactory = new DuplexChannelFactory<IMessageService>(
                instanceContext, ntb, "net.tcp://nsmsg.kingrocket.com:9999/KingrocketMessageService/");

            svc = channelFactory.CreateChannel();
            svc.NotifyUsers(lstReceiveIDs, strMsg);
            lblMsg.Text = "发送成功";
        }
        private void BindLines()
        {
            DataTable dt = LineHelperClient.GetLineList();
            //cmbLines.Items.Insert(0, "根据线路选择");
            DataRow row = dt.NewRow();//ID,LineName,Description,IPAddress,Port
            row["ID"] = Guid.NewGuid();
            row["LineName"] = "根据线路选择";
            row["Description"] = "根据线路选择";
            dt.Rows.InsertAt(row, 0);
            cmbLines.DisplayMember = "Description";
            cmbLines.DataSource = dt;
        }

        private void NotifyUserMsgDlg_Load(object sender, EventArgs e)
        {

            FpSpread_Info.Columns.Count = 10;
            FpSpread_Info.ColumnHeader.RowCount = 1;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "SessionID";
            FpSpread_Info.ColumnHeader.Cells[0, 0].Tag = "SessionID";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "LineID";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Tag = "LineID";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "选择";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Tag = "SelectTag";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "线路名称";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Tag = "LineName";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "标段名称";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Tag = "SegmentName";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "单位名称";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Tag = "CompanyName";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "试验室名称";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Tag = "TestRoomName";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "用户名";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Tag = "UserName";
            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "最后活动时间";
            FpSpread_Info.ColumnHeader.Cells[0, 8].Tag = "LastActiveTime";
            FpSpread_Info.ColumnHeader.Cells[0, 9].Text = "登录时间";
            FpSpread_Info.ColumnHeader.Cells[0, 9].Tag = "LoginTime";
            FpSpread_Info.Columns[0].Width = 10;
            FpSpread_Info.Columns[1].Width = 10;
            FpSpread_Info.Columns[2].Width = 40;
            FpSpread_Info.Columns[3].Width = 100;
            FpSpread_Info.Columns[4].Width = 60;
            FpSpread_Info.Columns[5].Width = 100;
            FpSpread_Info.Columns[6].Width = 80;
            FpSpread_Info.Columns[7].Width = 80;
            FpSpread_Info.Columns[8].Width = 120;
            FpSpread_Info.Columns[9].Width = 120;

            FpSpread_Info.Columns[0].Visible = false;
            FpSpread_Info.Columns[1].Visible = false;
            FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.LongDateWithTime;
            FpSpread_Info.Columns[8].CellType = datetime;
            FpSpread_Info.Columns[9].CellType = datetime;
            CheckBoxCellType checkBox = new CheckBoxCellType();
            FpSpread_Info.Columns[2].CellType = checkBox;

            DataOnlineUser = LineHelperClient.GetOnlineUserList();
            BindLines();

            //BindData();
        }

        private void BindData()
        {
            try
            {
                String type = "";
                if (DataOnlineUser != null)
                {
                    //FpSpread_Info.DataSource = DataOnlineUser;
                    string strFilter = string.Empty;
                    if (cmbLines.Text == "根据线路选择")
                    { strFilter = " 1=1 "; }
                    else
                    {
                        strFilter = "LineName='" + cmbLines.Text + "'";
                    }
                    DataRow[] drData = DataOnlineUser.Select(strFilter);

                    FpSpread_Info.Rows.Count = drData.Length;
                    if (drData.Length > 0)
                    {
                        //if (FpSpread_Info != null && FpSpread_Info.Rows != null && FpSpread_Info.Rows.Count > 0)
                        //    FpSpread_Info.Rows.Clear();
                        //FpSpread_Info.DataSource = drData;
                        int i, j;
                        foreach (System.Data.DataColumn Column in DataOnlineUser.Columns)
                        {
                            i = DataOnlineUser.Columns.IndexOf(Column);

                            FpSpread_Info.Columns[i].Width = 100;
                            j = 0;
                            foreach (DataRow Row in drData)
                            {
                                FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                                FpSpread_Info.Cells[j, i].Value = Row[Column.ColumnName];
                                j++;
                            }
                        }
                        FpSpread_Info.Columns[0].Width = 10;
                        FpSpread_Info.Columns[1].Width = 10;
                        FpSpread_Info.Columns[2].Width = 40;
                        FpSpread_Info.Columns[3].Width = 100;
                        FpSpread_Info.Columns[4].Width = 60;
                        FpSpread_Info.Columns[5].Width = 100;
                        FpSpread_Info.Columns[6].Width = 80;
                        FpSpread_Info.Columns[7].Width = 80;
                        FpSpread_Info.Columns[8].Width = 120;
                        FpSpread_Info.Columns[9].Width = 120;

                        lblMsg.Text = "加载完成";
                    }
                    else
                    {
                        lblMsg.Text = "无数据";
                    }
                }
                else
                {
                    lblMsg.Text = "无数据";
                }
                if (DataOnlineUser == null)
                {
                    totalCount.Text = string.Format("共 {0} 条记录", 0);
                }
                else
                {
                    totalCount.Text = string.Format("共 {0} 条记录", DataOnlineUser.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            chkSelectAll.Checked = false;
            DataOnlineUser = LineHelperClient.GetOnlineUserList();
            BindData();
        }

        private void cmbLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bChecked = chkSelectAll.Checked;
            for (int i = 0; i < FpSpread_Info.Rows.Count; i++)
            {
                Cell cell = FpSpread_Info.Cells[i, 2];
                cell.Text = bChecked.ToString();
            }
        }
    }
}
