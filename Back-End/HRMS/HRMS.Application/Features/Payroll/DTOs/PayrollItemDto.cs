using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.DTOs
{
    public class PayrollItemDto
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal NetSalary { get; set; }
    }
}
