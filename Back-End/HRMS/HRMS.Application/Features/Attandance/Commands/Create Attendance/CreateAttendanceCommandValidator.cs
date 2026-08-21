using FluentValidation;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Commands.Create_Attendance
{
    public class CreateAttendanceCommandValidator
    : AbstractValidator<CreateAttendanceCommand>
    {
        public CreateAttendanceCommandValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee is required.");

            RuleFor(x => x.AttendanceDate)
                .NotEqual(default(DateOnly))
                .WithMessage("Attendance date is required.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid attendance status.");

            RuleFor(x => x.Remarks)
                .MaximumLength(500)
                .WithMessage("Remarks cannot exceed 500 characters.");

            RuleFor(x => x)
                .Must(HaveValidCheckIn)
                .WithMessage(
                    "Check-in time is required for Present, Late, and HalfDay attendance.");

            RuleFor(x => x)
                .Must(HaveValidCheckOut)
                .WithMessage(
                    "Check-out time cannot be earlier than check-in time.");
        }

        private static bool HaveValidCheckIn(
            CreateAttendanceCommand command)
        {
            if (command.Status is AttendanceStatus.Present
                or AttendanceStatus.Late
                or AttendanceStatus.HalfDay)
            {
                return command.CheckInTime.HasValue;
            }

            return true;
        }

        private static bool HaveValidCheckOut(
            CreateAttendanceCommand command)
        {
            if (!command.CheckInTime.HasValue ||
                !command.CheckOutTime.HasValue)
            {
                return true;
            }

            return command.CheckOutTime.Value >= command.CheckInTime.Value;
        }
    }
}
