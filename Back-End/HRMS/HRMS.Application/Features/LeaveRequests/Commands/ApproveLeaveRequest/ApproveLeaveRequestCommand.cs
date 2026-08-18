using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest
{
    public class ApproveLeaveRequestCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid ApprovedBy { get; set; }
    }
}
