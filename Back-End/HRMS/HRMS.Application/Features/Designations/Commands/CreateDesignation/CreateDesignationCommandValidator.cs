using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.CreateDesignation
{
    public class CreateDesignationCommandValidator : AbstractValidator<CreateDesignationCommand>
    {
        public CreateDesignationCommandValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
