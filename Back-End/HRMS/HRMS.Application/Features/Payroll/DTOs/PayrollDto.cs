using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.DTOs
{
    public class PayrollDto
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public DateOnly PayrollPeriodStart { get; set; }

        public DateOnly PayrollPeriodEnd { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal TotalBasicSalary { get; set; }

        public decimal TotalAllowances { get; set; }

        public decimal TotalDeductions { get; set; }

        public decimal TotalGrossSalary { get; set; }

        public decimal TotalNetSalary { get; set; }

        public List<PayrollItemDto> Items { get; set; } = new();
    }
}
