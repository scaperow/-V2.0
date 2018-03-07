using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.BO.ReportE.Core
{
    public class ReportException : Exception
    {
        public ReportException()
        {
        }

        public ReportException(String message)
            : base(message)
        {
        }

        public ReportException(String message, Exception cause) :
            base(message, cause)
        {
        }
    }

    public class ReportRuntimeException : ReportException
    {
        public ReportRuntimeException()
        {
        }

        public ReportRuntimeException(String message)
            : base(message)
        {
        }

        public ReportRuntimeException(String message, Exception cause) :
            base(message, cause)
        {
        }
    }

    public class DataSourceNotFoundException : ReportRuntimeException
    {
        public DataSourceNotFoundException()
        {
        }

        public DataSourceNotFoundException(String message)
            : base(message)
        {
        }

        public DataSourceNotFoundException(String message, Exception cause) :
            base(message, cause)
        {
        }
    }

    public class DataSourceFillDataException : ReportRuntimeException
    {
        public DataSourceFillDataException()
        {
        }

        public DataSourceFillDataException(String message)
            : base(message)
        {
        }

        public DataSourceFillDataException(String message, Exception cause) :
            base(message, cause)
        {
        }
    }
}
