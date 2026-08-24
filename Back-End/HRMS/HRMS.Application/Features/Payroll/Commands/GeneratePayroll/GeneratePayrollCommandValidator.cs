using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.GeneratePayroll
{
    public class GeneratePayrollCommandValidator
        : AbstractValidator<GeneratePayrollCommand>
    {
        public GeneratePayrollCommandValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("OrganizationId is required.");

            RuleFor(x => x.PayrollPeriodStart)
                .NotEmpty()
                .WithMessage("Payroll period start is required.");

            RuleFor(x => x.PayrollPeriodEnd)
                .NotEmpty()
                .WithMessage("Payroll period end is required.");

            RuleFor(x => x)
                .Must(x => x.PayrollPeriodEnd >= x.PayrollPeriodStart)
                .WithMessage(
                    "Payroll period end must be greater than or equal to payroll period start.");
        }
    }
}
