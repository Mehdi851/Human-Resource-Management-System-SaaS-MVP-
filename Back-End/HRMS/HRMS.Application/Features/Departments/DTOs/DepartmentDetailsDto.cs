using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.DTOs
{
    public class DepartmentDetailsDto
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string Organization { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid? ManagerId { get; set; }

        public int EmployeeCount { get; set; }
    }
}
