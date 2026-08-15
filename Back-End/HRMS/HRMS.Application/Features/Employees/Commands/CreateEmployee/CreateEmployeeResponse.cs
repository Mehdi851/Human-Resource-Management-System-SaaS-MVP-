using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeResponse
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
