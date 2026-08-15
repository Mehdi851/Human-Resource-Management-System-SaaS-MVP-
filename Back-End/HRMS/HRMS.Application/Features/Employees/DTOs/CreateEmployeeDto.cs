using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Application.Features.Employees.DTOs
{
    public class CreateEmployeeDto
    {
        [Required]
        public Guid OrganizationId { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }
        public Guid DesignationId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public Guid? UserId { get; set; }

        public string? Position { get; set; }

        public string? EmployeeNumber { get; set; }

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }

        public EmployeeStatus Status { get; set; }
            = EmployeeStatus.Active;

        public Guid? ManagerId { get; set; }
    }
}
