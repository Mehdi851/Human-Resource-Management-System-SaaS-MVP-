using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.DTOs
{
    public class SalaryStructureListDto
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public DateOnly EffectiveFrom { get; set; }

        public DateOnly? EffectiveTo { get; set; }

        public string PaymentFrequency { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
