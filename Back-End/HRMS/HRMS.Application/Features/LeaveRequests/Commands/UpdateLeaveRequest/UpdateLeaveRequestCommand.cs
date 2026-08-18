using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand
    : IRequest<UpdateLeaveRequestResponse>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid LeaveTypeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Reason { get; set; }
    }
}
