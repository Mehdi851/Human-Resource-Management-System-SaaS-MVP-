using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentValidator
         : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250);
        }
    }
}
