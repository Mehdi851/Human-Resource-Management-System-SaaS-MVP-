using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandValidator
    : AbstractValidator<CancelLeaveRequestCommand>
    {
        public CancelLeaveRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Leave request is required.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");
        }
    }
}
