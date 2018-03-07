using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using FarPoint.Win.Spread;
using System.Threading;
using System.IO;

namespace Yqun.BO.BusinessManager
{
    public class SourceHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void CreateRalationFiles(FpSpread fpSpread, Guid documentID, Int32 reportIndex, string docDateDir)
        {
            ThreadParameter p = new ThreadParameter();
            p.fpSpread = fpSpread;
            p.documentID = documentID;
            p.reportIndex = reportIndex;
            p.docDateDir = docDateDir;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }
        /// <summary>
        /// 同步生成相关文件
        /// </summary>
        /// <param name="fpSpread"></param>
        /// <param name="documentID"></param>
        /// <param name="reportIndex"></param>
        public void CreateRalationFilesSync(FpSpread fpSpread, Guid documentID, Int32 reportIndex, string docDateDir)
        {
            if (documentID == null || fpSpread == null)
            {
                return;
            }
            try
            {
                String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "source/" + docDateDir );
                JZCommonHelper.SaveToExcel(documentID, fpSpread, path);
                JZCommonHelper.FarpointToImage0(documentID, fpSpread, path, reportIndex);
                //String excelPath = Path.Combine(path,documentID.ToString() + ".xls");
                String pdfFilePath = Path.Combine(path, documentID.ToString() + ".pdf");
                JZCommonHelper.FarpointToPDF0(fpSpread, pdfFilePath, reportIndex);
            }
            catch (Exception ex)
            {
                logger.Error("create ralation files:" + ex.Message);
            }
        }
        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            if (p.documentID == null || p.fpSpread == null)
            {
                return;
            }
            try
            {
                String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "source/" + p.docDateDir);
                JZCommonHelper.SaveToExcel(p.documentID, p.fpSpread, path);
                JZCommonHelper.FarpointToImage(p.documentID, p.fpSpread, path);
                String excelPath = Path.Combine(path, p.documentID.ToString() + ".xls");
                String pdfFilePath = Path.Combine(path, p.documentID.ToString() + ".pdf");
                JZCommonHelper.FarpointToPDF(p.fpSpread, pdfFilePath, p.reportIndex);
            }
            catch (Exception ex)
            {
                logger.Error("create ralation files:" + ex.Message);
            }
        }

        private class ThreadParameter
        {
            public FpSpread fpSpread;
            public Guid documentID;
            public Int32 reportIndex;
            public string docDateDir;
        }
    }
}
