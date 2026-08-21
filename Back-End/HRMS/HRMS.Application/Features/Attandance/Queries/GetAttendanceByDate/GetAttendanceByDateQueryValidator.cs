using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetAttendanceByDate
{
    public class GetAttendanceByDateQueryValidator
    : AbstractValidator<GetAttendanceByDateQuery>
    {
        private static readonly string[] AllowedSortFields =
        {
        "date",
        "employeename",
        "employeenumber",
        "status",
        "checkin",
        "checkout",
        "workinghours"
    };

        public GetAttendanceByDateQueryValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.AttendanceDate)
                .NotEmpty()
                .WithMessage("Attendance date is required.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .When(x => x.Status.HasValue)
                .WithMessage("Invalid attendance status.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");

            RuleFor(x => x.SortBy)
                .Must(IsAllowedSortField)
                .When(x => !string.IsNullOrWhiteSpace(x.SortBy))
                .WithMessage("Invalid sort field.");
        }

        private static bool IsAllowedSortField(string? sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return true;
            }

            return AllowedSortFields.Contains(
                sortBy.Trim().ToLowerInvariant());
        }
    }
}
