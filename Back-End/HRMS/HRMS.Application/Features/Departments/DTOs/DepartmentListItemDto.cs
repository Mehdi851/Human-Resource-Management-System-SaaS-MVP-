using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.DTOs
{
    public class DepartmentListItemDto
    {
        /// <summary>
        /// Department Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Department name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional department description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Organization name.
        /// </summary>
        public string Organization { get; set; } = string.Empty;

        /// <summary>
        /// Number of employees assigned to this department.
        /// </summary>
        public int EmployeeCount { get; set; }
    }
}
