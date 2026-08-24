using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Commands.CreateSalaryStructure
{
    public class CreateSalaryStructureCommandValidator
        : AbstractValidator<CreateSalaryStructureCommand>
    {
        public CreateSalaryStructureCommandValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("OrganizationId is required.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .WithMessage("EmployeeId is required.");

            RuleFor(x => x.BasicSalary)
                .GreaterThan(0)
                .WithMessage("Basic salary must be greater than zero.");

            RuleFor(x => x.Allowances)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Allowances cannot be negative.");

            RuleFor(x => x.Deductions)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Deductions cannot be negative.");

            RuleFor(x => x.EffectiveFrom)
                .NotEmpty()
                .WithMessage("Effective From is required.");

            RuleFor(x => x.EffectiveTo)
                .GreaterThanOrEqualTo(x => x.EffectiveFrom)
                .When(x => x.EffectiveTo.HasValue)
                .WithMessage("Effective To must be greater than or equal to Effective From.");

            RuleFor(x => x.PaymentFrequency)
                .IsInEnum()
                .WithMessage("Invalid payment frequency.");
        }
    }
}
