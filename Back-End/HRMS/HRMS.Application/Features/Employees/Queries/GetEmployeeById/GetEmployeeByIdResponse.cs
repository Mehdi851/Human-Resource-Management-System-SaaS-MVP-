using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Queries.GetEmployeeById
{


    public class GetEmployeeByIdResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName =>
            $"{FirstName} {LastName}";

        public string Email { get; set; } = string.Empty;

        public string? EmployeeNumber { get; set; }

        public string? Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public Guid OrganizationId { get; set; }

        public string OrganizationName { get; set; } = string.Empty;
    }
}
