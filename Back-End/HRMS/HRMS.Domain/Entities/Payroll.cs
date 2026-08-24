using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Payroll : BaseEntity
    {
        public Guid OrganizationId { get; set; }

        public DateOnly PayrollPeriodStart { get; set; }

        public DateOnly PayrollPeriodEnd { get; set; }

        public PayrollStatus Status { get; set; }

        // Navigation Property
        public virtual ICollection<PayrollItem> PayrollItems { get; set; }
            = new List<PayrollItem>();
    }
}
