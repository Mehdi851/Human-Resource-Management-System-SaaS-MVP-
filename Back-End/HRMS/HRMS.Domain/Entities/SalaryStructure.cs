using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class SalaryStructure : BaseEntity
    {
        public Guid OrganizationId { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public DateOnly EffectiveFrom { get; set; }

        public DateOnly? EffectiveTo { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public SalaryStructureStatus Status { get; set; }

        // Navigation Property
        public virtual Employee Employee { get; set; } = default!;
    }
}
