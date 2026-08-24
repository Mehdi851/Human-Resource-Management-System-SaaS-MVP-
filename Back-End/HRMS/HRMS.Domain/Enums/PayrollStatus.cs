using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Enums
{
    public enum PayrollStatus
    {
        Draft = 1,
        Processed = 2,
        Approved = 3,
        Paid = 4,
        Cancelled = 5
    }
}
