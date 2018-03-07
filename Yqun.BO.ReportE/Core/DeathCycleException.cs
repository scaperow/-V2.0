using System;

namespace Yqun.BO.ReportE.Core
{
    public class DeathCycleException : ApplicationException
    {
        public DeathCycleException()
            : base()
        {
        }

        public DeathCycleException(String message)
            : base(message)
        {
        }

        public DeathCycleException(String message, Exception innerException)
            : base(message,innerException)
        {
        }
    }
}
