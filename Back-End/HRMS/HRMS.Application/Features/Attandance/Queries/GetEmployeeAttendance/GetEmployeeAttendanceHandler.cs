using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Attandance.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetEmployeeAttendance
{
    public class GetEmployeeAttendanceHandler
    : IRequestHandler<
        GetEmployeeAttendanceQuery,
        PagedResponse<AttendanceListDto>>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public GetEmployeeAttendanceHandler(
            IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<PagedResponse<AttendanceListDto>> Handle(
            GetEmployeeAttendanceQuery request,
            CancellationToken cancellationToken)
        {
            var result =
                await _attendanceRepository.GetEmployeeAttendanceAsync(
                    employeeId: request.EmployeeId,
                    organizationId: request.OrganizationId,
                    fromDate: request.FromDate,
                    toDate: request.ToDate,
                    status: request.Status,
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
