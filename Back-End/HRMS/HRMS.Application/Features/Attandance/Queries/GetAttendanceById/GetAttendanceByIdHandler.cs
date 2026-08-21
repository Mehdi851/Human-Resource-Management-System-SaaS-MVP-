using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Attandance.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetAttendanceById
{
    public class GetAttendanceByIdHandler
    : IRequestHandler<GetAttendanceByIdQuery, AttendanceDetailsDto>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public GetAttendanceByIdHandler(
            IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<AttendanceDetailsDto> Handle(
            GetAttendanceByIdQuery request,
            CancellationToken cancellationToken)
        {
            var attendance =
                await _attendanceRepository.GetAttendanceWithDetailsAsync(
                    request.Id,
                    request.OrganizationId,
                    cancellationToken);

            if (attendance is null)
            {
                throw new KeyNotFoundException(
                    "Attendance record was not found.");
            }

            return new AttendanceDetailsDto
            {
                Id = attendance.Id,
                EmployeeId = attendance.EmployeeId,

                EmployeeName =
                    $"{attendance.Employee.FirstName} {attendance.Employee.LastName}"
                        .Trim(),

                EmployeeNumber = attendance.Employee.EmployeeNumber,

                DepartmentName =
                    attendance.Employee.Department?.Name,

                AttendanceDate = attendance.AttendanceDate,

                CheckInTime = attendance.CheckInTime,

                CheckOutTime = attendance.CheckOutTime,

                Status = attendance.Status,

                WorkingHours = attendance.WorkingHours,

                Remarks = attendance.Remarks
            };
        }
    }
}
