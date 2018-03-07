using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Bases.Exceptions
{
    public class ServiceAccessException : ApplicationException
    {
        public ServiceAccessException()
            : base()
        {
        }

        public ServiceAccessException(String message)
            : base(message)
        {
        }

        public ServiceAccessException(String message,Exception innerException)
            : base(message,innerException)
        {
        }
    }

    public class SoftwareVersionErrorException : ApplicationException
    {
        public SoftwareVersionErrorException()
            : base()
        {
        }

        public SoftwareVersionErrorException(String message)
            : base(message)
        {
        }

        public SoftwareVersionErrorException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
