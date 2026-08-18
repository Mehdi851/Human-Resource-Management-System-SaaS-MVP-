using HRMS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQuery
    : IRequest<PagedResponse<LeaveRequestListDto>>
    {
        public Guid OrganizationId { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? DepartmentId { get; set; }

        public string? Status { get; set; }

        public string? Search { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }

        public bool SortDescending { get; set; }
    }
}
