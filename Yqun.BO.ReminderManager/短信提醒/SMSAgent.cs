using System;
using System.Collections.Generic;
using System.Text;
using SMSService;

namespace Yqun.BO.ReminderManager
{
    public class SMSAgent
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string CallRemoteService(string[] code, string context)
        {
            string errorMsg = "";
            try
            {
                IMyService proxy = ServiceBroker.FindService<IMyService>();
                proxy.SendSMS(string.Join(",", code), Format(context), "", "1");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                errorMsg = ex.Message;
            }

            return errorMsg;
        }

        private static string[] Libraries = new string[] { "隧道", "隧/道" };

        public static string Format(string context)
        {
            if (string.IsNullOrEmpty(context))
            {
                return context;
            }

            for (var i = 0; i < Libraries.Length; i++)
            {
                if (Libraries.Length > i + 1)
                {
                    context = context.Replace(Libraries[i], Libraries[i + 1]);
                }
            }

            return context;
        }
    }
}
