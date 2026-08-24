using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.MarkPayrollAsPaid
{
    public class MarkPayrollAsPaidCommandValidator
       : AbstractValidator<MarkPayrollAsPaidCommand>
    {
        public MarkPayrollAsPaidCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Payroll ID is required.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("OrganizationId is required.");
        }
    }
}
