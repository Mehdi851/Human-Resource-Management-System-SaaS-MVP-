using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Attandance.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetAttendances
{
    public class GetAttendancesHandler
    : IRequestHandler<
        GetAttendancesQuery,
        PagedResponse<AttendanceListDto>>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public GetAttendancesHandler(
            IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<PagedResponse<AttendanceListDto>> Handle(
            GetAttendancesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _attendanceRepository.GetPagedAsync(
                organizationId: request.OrganizationId,
                employeeId: request.EmployeeId,
                departmentId: request.DepartmentId,
                status: request.Status,
                attendanceDate: request.AttendanceDate,
                fromDate: request.FromDate,
                toDate: request.ToDate,
                search: request.Search,
                sortBy: request.SortBy,
                sortDescending: request.SortDescending,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

            var items = result.Items
                .Select(attendance => new AttendanceListDto
                {
                    Id = attendance.Id,

                    EmployeeId = attendance.EmployeeId,

                    EmployeeName =
                        $"{attendance.Employee.FirstName} {attendance.Employee.LastName}"
                            .Trim(),

                    EmployeeNumber =
                        attendance.Employee.EmployeeNumber,

                    DepartmentName =
                        attendance.Employee.Department?.Name,

                    AttendanceDate =
                        attendance.AttendanceDate,

                    CheckInTime =
                        attendance.CheckInTime,

                    CheckOutTime =
                        attendance.CheckOutTime,

                    Status =
                        attendance.Status,

                    WorkingHours =
                        attendance.WorkingHours,

                    Remarks =
                        attendance.Remarks
                })
                .ToList();

            return new PagedResponse<AttendanceListDto>
            {
                Items = items,
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }
    }
}
