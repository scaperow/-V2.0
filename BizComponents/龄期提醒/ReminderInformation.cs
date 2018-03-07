using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using System.Windows.Forms;
using System.Data;
using FarPoint.Win.Spread;
using System.Drawing;
using BizCommon;
using Yqun.Permissions.Runtime;
using Yqun.Bases;

namespace BizComponents
{
    public class ReminderInformation
    {
        public static void AddReminderLabelToStatusBar(Boolean SYLQTX, Boolean JLPXPLTX, Boolean BHGBGTZ, Boolean SPXG)
        {
            ToolStripStatusLabel Label = Cache.CustomCache[SystemString.龄期提醒] as ToolStripStatusLabel;
            Label.Visible = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator || SYLQTX;
            Label.Text = "待做事项列表";
            Label.Click -= new EventHandler(Label_Click);
            Label.Click += new EventHandler(Label_Click);

            ToolStripStatusLabel Label1 = Cache.CustomCache[SystemString.监理平行频率提醒] as ToolStripStatusLabel;
            Label1.Visible = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator || JLPXPLTX;
            Label1.Text = "监理平行频率提醒";
            Label1.Click -= new EventHandler(Label1_Click);
            Label1.Click += new EventHandler(Label1_Click);

            ToolStripStatusLabel Label2 = Cache.CustomCache[SystemString.报告不合格提醒] as ToolStripStatusLabel;
            Label2.Visible = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator || BHGBGTZ;
            Label2.Text = "不合格报告提醒";
            Label2.Click -= new EventHandler(Label2_Click);
            Label2.Click += new EventHandler(Label2_Click);

            ToolStripStatusLabel Label3 = Cache.CustomCache[SystemString.审批修改] as ToolStripStatusLabel;
            Label3.Visible = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator || SPXG;
            Label3.Text = "审批修改";
            Label3.Click -= new EventHandler(Label3_Click);
            Label3.Click += new EventHandler(Label3_Click);
        }

        public static void ClearReminderLabelToStatusBar()
        {
            ToolStripStatusLabel Label = Cache.CustomCache[SystemString.龄期提醒] as ToolStripStatusLabel;
            Label.Visible = false;
            Label.Text = "待做事项列表";
            Label.Click -= new EventHandler(Label_Click);

            ToolStripStatusLabel Label1 = Cache.CustomCache[SystemString.监理平行频率提醒] as ToolStripStatusLabel;
            Label1.Visible = false;
            Label1.Text = "监理平行频率提醒";
            Label1.Click -= new EventHandler(Label1_Click);

            ToolStripStatusLabel Label2 = Cache.CustomCache[SystemString.报告不合格提醒] as ToolStripStatusLabel;
            Label2.Visible = false;
            Label2.Text = "不合格报告提醒";
            Label2.Click -= new EventHandler(Label2_Click);

            ToolStripStatusLabel Label3 = Cache.CustomCache[SystemString.审批修改] as ToolStripStatusLabel;
            Label3.Visible = false;
            Label3.Text = "审批修改";
            Label3.Click -= new EventHandler(Label3_Click);
        }

        /// <summary>
        /// 提醒试验室龄期信息
        /// </summary>
        public static void ReminderLabStadiumList()
        {
            Label_Click(null, null);
        }

        /// <summary>
        /// 试验龄期提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Label_Click(object sender, EventArgs e)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator || Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
            {
                //StadiumReminderDialog Dialog = new StadiumReminderDialog();
                //Dialog.ShowDialog();
                TaskView tv = new TaskView();
                tv.ShowDialog();
            }
            else
            {
                //StadiumReminderGeneral Dialog = new StadiumReminderGeneral();
                //Dialog.ShowDialog();
                TaskView tv = new TaskView();
                tv.ShowDialog();
            }

        }

        /// <summary>
        /// 监理平行频率提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Label1_Click(object sender, EventArgs e)
        {            
            PXReportDialog Dialog = new PXReportDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.Show();
        }

        /// <summary>
        /// 报告不合格提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Label2_Click(object sender, EventArgs e)
        {            
            QueryReportEvaluateDialog Dialog = new QueryReportEvaluateDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            Dialog.Size = Owner.ClientRectangle.Size;
            Dialog.ShowDialog();
        }

        /// <summary>
        /// 审批修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Label3_Click(object sender, EventArgs e)
        {
            //获得当前用户名称和单位类型
            
            List<String> RoomCodes = DepositoryEvaluateDataList.GetTestRoomList();

            DataTable Data = DepositoryDataModificationInfo.InitDataModificationList(RoomCodes.ToArray());
            if (Data != null)
            {               
                QuerySponsorModifyDialog Dialog = new QuerySponsorModifyDialog();
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Dialog.Size = Owner.ClientRectangle.Size;
                Dialog.Show();
            }
        }

        /// <summary>
        /// 追溯不合格报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void FpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FpSpread fpSpread = sender as FpSpread;
            Row Row = fpSpread.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String)
            {
                String[] Tokens = Row.Tag.ToString().Split(',');

                DataDialog Dialog = new DataDialog(new Guid(Tokens[0]), new Guid(Tokens[1]));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Dialog.Owner = Owner;
                Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Dialog.Size = Owner.ClientRectangle.Size;
                Dialog.ReadOnly = true;
                Dialog.Show();
            }
        }
    }
}
