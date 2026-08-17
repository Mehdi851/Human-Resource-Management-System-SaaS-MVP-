using HRMS.Application.Common.Models;
using HRMS.Application.Features.LeaveTypes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Queries.GetLeaveTypes
{
    public class GetLeaveTypesQuery : IRequest<PagedResponse<LeaveTypeListDto>>
    {
        public Guid OrganizationId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool SortDescending { get; set; }
    }
}
