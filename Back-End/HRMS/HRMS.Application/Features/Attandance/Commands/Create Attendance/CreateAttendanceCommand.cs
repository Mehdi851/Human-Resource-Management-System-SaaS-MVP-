using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.Create_Attendance
{
    public class CreateAttendanceCommand : IRequest<CreateAttendanceResponse>
    {
        public Guid OrganizationId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public TimeOnly? CheckInTime { get; set; }

        public TimeOnly? CheckOutTime { get; set; }

        public AttendanceStatus Status { get; set; }

        public string? Remarks { get; set; }
    }
}
