using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest
{
    public class ApproveLeaveRequestCommandValidator
    : AbstractValidator<ApproveLeaveRequestCommand>
    {
        public ApproveLeaveRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Leave request is required.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.ApprovedBy)
                .NotEmpty()
                .WithMessage("Approver is required.");
        }
    }
}
