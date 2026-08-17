using HRMS.Application.Features.LeaveTypes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Queries.GetLeaveTypeById
{
    public class GetLeaveTypeByIdQuery : IRequest<LeaveTypeDetailsDto>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
