using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.RejectLeaveRequest
{
    public class RejectLeaveRequestCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string RejectionReason { get; set; } = default!;

        public Guid RejectedBy { get; set; }
    }
}
