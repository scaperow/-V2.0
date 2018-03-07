using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using ReportCommon;
using FarPoint.Win.Spread;
using Yqun.Client.BizUI;
using FarPoint.Win;

namespace ReportComponents
{
    public partial class EntireLineReportDialog : Form
    {
        public EntireLineReportDialog()
        {
            InitializeComponent();
        }

        private void EntireLineReportDialog_Load(object sender, EventArgs e)
        {
            //从数据库中的sys_biz_ReportRTConfig获得手工录入的报表名称
            cBox_Types.Items.Clear();
            List<String> Reports = DepositoryRTReportConfig.GetRTReports("全线");
            cBox_Types.Items.AddRange(Reports.ToArray());
            if (cBox_Types.Items.Count > 0)
                cBox_Types.SelectedIndex = 0;
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            ProgressScreen.Current.SetStatus = "正在初始化报表参数......";
            String descriptor = cBox_Types.SelectedItem.ToString();

            Guid reportID = ReportService.GetReportIDByName(descriptor);
            List<ReportParameter> Parameters = ReportService.GetReportParameters(reportID);

            foreach (ReportParameter parameter in Parameters)
            {
                if (parameter.Name.ToLower() == "@startdate")
                {
                    parameter.Value = string.Format("{0}-{1}-{2} 00:00:00", @startdate.Value.Year, @startdate.Value.Month, @startdate.Value.Day);
                }
                if (parameter.Name.ToLower() == "@enddate")
                {
                    parameter.Value = string.Format("{0}-{1}-{2} 23:59:59", @enddate.Value.Year, @enddate.Value.Month, @enddate.Value.Day);
                }
            }

            ProgressScreen.Current.SetStatus = "正在分析并生成报表......";
            String ReportXml = ReportService.GetReportString(reportID,
                string.Format("{0}-{1}-{2} 00:00:00", @startdate.Value.Year, @startdate.Value.Month, @startdate.Value.Day),
                string.Format("{0}-{1}-{2} 23:59:59", @enddate.Value.Year, @enddate.Value.Month, @enddate.Value.Day),
                "",
                Parameters);
            if (ReportXml != "")
            {
                ProgressScreen.Current.SetStatus = "正在显示报表......";
                FpSpread ReportSpread = Serializer.LoadObjectXml(typeof(FpSpread), ReportXml, "FpSpread") as FpSpread;
                OwnerPrintDocument Document = new OwnerPrintDocument(ReportSpread);
                Document.ShowPrinterDialog = false;
                reportViewer1.UseAntiAlias = true;
                reportViewer1._FpSpreed = ReportSpread;
                reportViewer1.Document = Document;
            }

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();

            if (ReportXml == "")
                MessageBox.Show("报表‘" + descriptor + "’没有找到！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();
        }

        private void cBox_Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            String descriptor = cBox_Types.SelectedItem.ToString();
            Guid reportID = ReportService.GetReportIDByName(descriptor);
            List<ReportParameter> Parameters = ReportService.GetReportParameters(reportID);
            panel1.Visible = (Parameters.Count > 0);
        }
    }
}
