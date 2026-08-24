using HRMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class PayrollItem : BaseEntity
    {
        public Guid OrganizationId { get; set; }

        public Guid PayrollId { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal NetSalary { get; set; }

        // Navigation Properties
        public virtual Payroll Payroll { get; set; } = default!;

        public virtual Employee Employee { get; set; } = default!;
    }
}
