using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.DeleteAttendance
{
    public class DeleteAttendanceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
