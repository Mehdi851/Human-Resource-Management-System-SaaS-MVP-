using HRMS.Application.Common.Models;
using HRMS.Application.Features.Attandance.DTOs;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetAttendances
{
    public class GetAttendancesQuery
    : IRequest<PagedResponse<AttendanceListDto>>
    {
        public Guid OrganizationId { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? DepartmentId { get; set; }

        public AttendanceStatus? Status { get; set; }

        public DateOnly? AttendanceDate { get; set; }

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool SortDescending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
