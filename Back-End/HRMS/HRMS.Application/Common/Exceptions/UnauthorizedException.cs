using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
