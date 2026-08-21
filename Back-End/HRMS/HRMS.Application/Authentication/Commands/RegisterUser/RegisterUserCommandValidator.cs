using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandValidator
    : AbstractValidator<RegisterUserCommand>
    {
        private static readonly string[] AllowedRoles =
        {
        "SuperAdmin",
        "HRAdmin",
        "Employee"
    };

        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");

            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Organization is required.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role =>
                    AllowedRoles.Contains(
                        role,
                        StringComparer.OrdinalIgnoreCase))
                .WithMessage("Invalid role.");
        }
    }
}
