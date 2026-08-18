using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetPendingLeaveRequests
{
    public class GetPendingLeaveRequestsQueryValidator
    : AbstractValidator<GetPendingLeaveRequestsQuery>
    {
        public GetPendingLeaveRequestsQueryValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .When(x => x.EmployeeId.HasValue)
                .WithMessage("Employee is required.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .When(x => x.DepartmentId.HasValue)
                .WithMessage("Department is required.");

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
        }
    }
}
