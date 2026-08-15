using HRMS.Application.Common.Models;
using HRMS.Application.Features.Employees.DTOs;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery
         : PagedRequest,
           IRequest<PagedResponse<EmployeeListItemDto>>
    {
        // Search
        public string? Search { get; set; }

        // Filters
        public Guid? OrganizationId { get; set; }

        public Guid? DepartmentId { get; set; }

        public EmployeeStatus? Status { get; set; }

        // Sorting
        public string SortBy { get; set; } = "FirstName";

        public bool Descending { get; set; } = false;
    }
}
