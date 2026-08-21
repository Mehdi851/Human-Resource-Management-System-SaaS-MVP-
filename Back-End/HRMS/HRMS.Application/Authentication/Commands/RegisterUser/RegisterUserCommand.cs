using HRMS.Application.Authentication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string ConfirmPassword { get; set; } = default!;

        public Guid OrganizationId { get; set; }

        public string Role { get; set; } = default!;
    }
}
