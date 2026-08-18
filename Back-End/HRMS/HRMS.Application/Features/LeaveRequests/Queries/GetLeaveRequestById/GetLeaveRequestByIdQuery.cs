using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequestById
{
    public class GetLeaveRequestByIdQuery
    : IRequest<LeaveRequestDetailsDto?>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
