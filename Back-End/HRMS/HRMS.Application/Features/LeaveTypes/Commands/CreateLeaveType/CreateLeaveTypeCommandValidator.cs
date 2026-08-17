using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator
    : AbstractValidator<CreateLeaveTypeCommand>
    {
        public CreateLeaveTypeCommandValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Leave type name is required.")
                .MaximumLength(100)
                .WithMessage("Leave type name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.DefaultDays)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Default days cannot be negative.");
        }
    }
}
