using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
