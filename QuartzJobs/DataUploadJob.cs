using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using BizComponents;

namespace QuartzJobs
{
    public class DataUploadJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            DataUploadOperation Operation = new DataUploadOperation();
            Operation.UpdateData();
        }
    }
}
