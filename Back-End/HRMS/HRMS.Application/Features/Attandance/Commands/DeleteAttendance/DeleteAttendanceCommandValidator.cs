using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.DeleteAttendance
{
    public class DeleteAttendanceCommandValidator
    : AbstractValidator<DeleteAttendanceCommand>
    {
        public DeleteAttendanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Attendance ID is required.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");
        }
    }
}
