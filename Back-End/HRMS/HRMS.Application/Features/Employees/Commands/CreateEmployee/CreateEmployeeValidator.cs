using FluentValidation;
using HRMS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeValidator
    : AbstractValidator<CreateEmployeeCommand>
    {
        public class CreateEmployeeCommandValidator
         : AbstractValidator<CreateEmployeeCommand>
        {
            public CreateEmployeeCommandValidator(
                IEmployeeRepository employeeRepository)
            {
                RuleFor(x => x.Employee.FirstName)
                    .NotEmpty()
                    .MaximumLength(100);

                RuleFor(x => x.Employee.LastName)
                    .NotEmpty()
                    .MaximumLength(100);

                RuleFor(x => x.Employee.Email)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(x => x.Employee.DepartmentId)
                    .NotEmpty();

                RuleFor(x => x.Employee.OrganizationId)
                    .NotEmpty();

                RuleFor(x => x.Employee.Salary)
                    .GreaterThanOrEqualTo(0);

                RuleFor(x => x.Employee.EmployeeNumber)
                    .MustAsync(async (employeeNumber, cancellation) =>
                    {
                        if (string.IsNullOrWhiteSpace(employeeNumber))
                            return true;

                        return !await employeeRepository
                            .EmployeeNumberExistsAsync(
                                employeeNumber,
                                cancellation);
                    })
                    .WithMessage("Employee number already exists.");

                RuleFor(x => x.Employee.Email)
                    .MustAsync(async (email, cancellation) =>
                    {
                        return !await employeeRepository
                            .EmailExistsAsync(email, cancellation);
                    })
                    .WithMessage("Email already exists.");
            }
        }
    }
}
