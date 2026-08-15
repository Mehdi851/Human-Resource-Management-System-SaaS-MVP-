    using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.DTOs
{
    public class EmployeeListItemDto
    {
        public Guid Id { get; set; }

        public string EmployeeNumber { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Position { get; set; }

        public string Department { get; set; } = default!;

        public string Organization { get; set; } = default!;

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }

        public string Status { get; set; } = default!;
    }

}
