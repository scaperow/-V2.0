using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Common.ContextCache;
using Yqun.BO.ReminderManager;
using System.Threading;

namespace Yqun.BO.BusinessManager
{
    public class QualifyHelper
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ItemConditionManager icm = new ItemConditionManager();

        public void Qualify(JZDocument document, Sys_Module module)
        {
            ThreadParameter p = new ThreadParameter();
            p.document = document;
            p.module = module;
            p.IsAdministrator = ApplicationContext.Current.IsAdministrator;

            p.LineID = ApplicationContext.Current.InProject.Index;
            p.TestRoomCode = ApplicationContext.Current.InTestRoom.Code;
            p.SegmentName = ApplicationContext.Current.InSegment.Description ;
            p.CompanyName = ApplicationContext.Current.InCompany.Description;
            p.TestRoomName = ApplicationContext.Current.InTestRoom.Description;
            p.LineName = ApplicationContext.Current.InProject.Description;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            JZDocument document = p.document;
            Sys_Module module = p.module;
            String invalidString = "";
            if (module != null)
            {
                if (module.QualifySettings != null && module.QualifySettings.Count > 0)
                {
                    foreach (QualifySetting qs in module.QualifySettings)
                    {
                        Object obj = JZCommonHelper.GetCellValue(document, qs.SheetID, qs.CellName);
                        if (obj != null && obj.ToString() != "")
                        {
                            invalidString = obj.ToString().Trim(' ', '\r', '\n');
                            break;
                        }
                    }
                }
            }
            if (invalidString != "")
            {
                try
                {
                    String[] arr = invalidString.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length < 3)
                    {
                        logger.Error("无效的不合格短信内容，document的ID为" + document.ID + "；短信内容为：" + invalidString);
                        return;
                    }
                    Boolean needSendSMS = false;
                    SMSManager smsManager = new SMSManager();
                    if (!p.IsAdministrator)
                    {
                        needSendSMS = smsManager.NeedSendSMS(document.ID.ToString(), invalidString);
                        logger.Error(invalidString + "needsendsms=" + needSendSMS);
                    }
                    
                    if (needSendSMS)
                    {
                        smsManager.SendSMS(document.ID.ToString(), invalidString,p.LineID,p.TestRoomCode,p.SegmentName,p.CompanyName,p.TestRoomName,p.LineName);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("更新合格字段IsQualified失败，原因是" + ex.Message);
                }
            }
            icm.SyncInvalidReport(document.ID, invalidString);
        }


        private class ThreadParameter
        {
            public JZDocument document;
            public Sys_Module module;
            public Boolean IsAdministrator;

            public string LineID;
            public string TestRoomCode;
            public string SegmentName;
            public string CompanyName;
            public string TestRoomName;
            public string LineName;
        }
    }
}
