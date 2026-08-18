using FluentValidation;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetEmployeeLeaveRequests
{
    public class GetEmployeeLeaveRequestsQueryValidator
    : AbstractValidator<GetEmployeeLeaveRequestsQuery>
    {
        public GetEmployeeLeaveRequestsQueryValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage(
                    "Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage(
                    "Page size must be between 1 and 100.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x =>
                    x.StartDate.HasValue &&
                    x.EndDate.HasValue)
                .WithMessage(
                    "End date cannot be earlier than start date.");

            RuleFor(x => x.Status)
                .Must(BeValidStatus)
                .When(x =>
                    !string.IsNullOrWhiteSpace(x.Status))
                .WithMessage(
                    "Invalid leave request status.");
        }

        private static bool BeValidStatus(string? status)
        {
            return Enum.TryParse<LeaveRequestStatus>(
                status,
                true,
                out _);
        }
    }
}
