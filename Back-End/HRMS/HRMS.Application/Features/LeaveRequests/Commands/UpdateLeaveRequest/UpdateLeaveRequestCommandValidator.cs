using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator
     : AbstractValidator<UpdateLeaveRequestCommand>
    {
        public UpdateLeaveRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Leave request is required.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.LeaveTypeId)
                .NotEmpty()
                .WithMessage("Leave type is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date cannot be earlier than start date.");

            RuleFor(x => x.Reason)
                .MaximumLength(1000)
                .WithMessage("Reason cannot exceed 1000 characters.");
        }
    }
}
