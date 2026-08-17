using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Designations.Commands.UpdateDesignation
{
    public class UpdateDesignationCommandValidator
     : AbstractValidator<UpdateDesignationCommand>
    {
        public UpdateDesignationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

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
