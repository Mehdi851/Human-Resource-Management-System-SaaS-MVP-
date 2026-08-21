using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommand : IRequest<UpdateAttendanceResponse>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public TimeOnly? CheckInTime { get; set; }

        public TimeOnly? CheckOutTime { get; set; }

        public AttendanceStatus Status { get; set; }

        public string? Remarks { get; set; }
    }
}
