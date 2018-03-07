using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Windows.Forms;

namespace QuartzJobs
{
    public class DemoJob : IJob
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            logger.Info("服务正在运行，时间是：" + DateTime.Now.ToString());
        }
    }
}
