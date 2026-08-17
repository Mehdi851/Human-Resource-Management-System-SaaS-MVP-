using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{

    public class DeleteLeaveTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
