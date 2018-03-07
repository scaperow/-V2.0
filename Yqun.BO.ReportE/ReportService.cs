using System;
using System.Collections.Generic;
using System.Text;
using Yqun.BO.ReportManager;
using ReportCommon;
using Yqun.BO.ReportE.Core;
using System.Collections;
using FarPoint.Win;
using System.Windows.Forms;

namespace Yqun.BO.ReportE
{
    public class ReportService
    {
        ReportConfigurationManager ReportManager = new ReportConfigurationManager();
        public List<ReportParameter> GetReportParameters(String descriptor)
        {
            Yqun.BO.ReportManager.ReportHelper rh = new Yqun.BO.ReportManager.ReportHelper();
            Guid reportID = rh.GetReportIDByName(descriptor);
            return rh.GetReportParametersByReportID(reportID);
        }

        public String DoReport(String descriptor, Hashtable parameters)
        {
            String Index = ReportManager.GetReportIndex(descriptor);
            if (Index != "")
            {
                ReportConfiguration Configuration = ReportManager.InitReportConfiguration(Index);
                Report report = new Report(Configuration);
                report.InitReport(Index);
                ReportPanel reportPanel = new ReportPanel();
                reportPanel.Run(report, parameters);
                string reportStr = Serializer.GetObjectXml(reportPanel.ReportSpread, "FpSpread");
                reportStr = reportStr.Replace("<Data type=\"System.String\">0%</Data>","<Data type=\"System.String\"> </Data>")
                    .Replace("<Data type=\"System.Int32\">0</Data>", "<Data type=\"System.String\"> </Data>")
                    .Replace("<Data type=\"System.Double\">0</Data>", "<Data type=\"System.String\"> </Data>")
                    .Replace("<Data type=\"System.String\">0</Data>", "<Data type=\"System.String\"> </Data>");
                return reportStr;
            }

            return "";
        }
    }
}
