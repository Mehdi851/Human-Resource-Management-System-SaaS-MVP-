using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.RejectLeaveRequest
{
    public class RejectLeaveRequestCommandValidator
    : AbstractValidator<RejectLeaveRequestCommand>
    {
        public RejectLeaveRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Leave request is required.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.RejectionReason)
                .NotEmpty()
                .WithMessage("Rejection reason is required.")
                .MaximumLength(1000)
                .WithMessage("Rejection reason cannot exceed 1000 characters.");
        }
    }
}
