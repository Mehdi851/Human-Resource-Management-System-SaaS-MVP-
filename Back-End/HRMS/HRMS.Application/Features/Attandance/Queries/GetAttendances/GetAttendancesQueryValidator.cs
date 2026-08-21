using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.Queries.GetAttendances
{
    public class GetAttendancesQueryValidator
    : AbstractValidator<GetAttendancesQuery>
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

        public GetAttendancesQueryValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .When(x => x.Status.HasValue)
                .WithMessage("Invalid attendance status.");

            RuleFor(x => x)
                .Must(HaveValidDateRange)
                .WithMessage("From date cannot be later than to date.");

            RuleFor(x => x)
                .Must(NotMixSpecificDateAndRange)
                .WithMessage(
                    "Attendance date cannot be combined with from/to date filtering.");

            RuleFor(x => x.SortBy)
                .Must(IsAllowedSortField)
                .When(x => !string.IsNullOrWhiteSpace(x.SortBy))
                .WithMessage("Invalid sort field.");

            RuleFor(x => x.Search)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Search))
                .WithMessage("Search cannot exceed 100 characters.");
        }

        private static bool HaveValidDateRange(
            GetAttendancesQuery query)
        {
            if (!query.FromDate.HasValue ||
                !query.ToDate.HasValue)
            {
                return true;
            }

            return query.FromDate.Value <= query.ToDate.Value;
        }

        private static bool NotMixSpecificDateAndRange(
            GetAttendancesQuery query)
        {
            return !query.AttendanceDate.HasValue ||
                   (!query.FromDate.HasValue &&
                    !query.ToDate.HasValue);
        }

        private static bool IsAllowedSortField(
            string? sortBy)
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
