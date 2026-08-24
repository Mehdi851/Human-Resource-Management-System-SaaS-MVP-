using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.DTOs
{
    public class PayrollSummaryDto
    {
        public Guid PayrollId { get; set; }

        public Guid OrganizationId { get; set; }

        public DateOnly PayrollPeriodStart { get; set; }

        public DateOnly PayrollPeriodEnd { get; set; }

        public string Status { get; set; } = string.Empty;

        public int TotalEmployees { get; set; }

        public decimal TotalBasicSalary { get; set; }

        public decimal TotalAllowances { get; set; }

        public decimal TotalDeductions { get; set; }

        public decimal TotalGrossSalary { get; set; }

        public decimal TotalNetSalary { get; set; }
    }
}
