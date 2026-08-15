using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.DTOs
{
    public class EmployeeResponseDTO
    {
        public Guid Id { get; set; }

        public string EmployeeNumber { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }

        public string Department { get; set; } = string.Empty;

        public string Organization { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
