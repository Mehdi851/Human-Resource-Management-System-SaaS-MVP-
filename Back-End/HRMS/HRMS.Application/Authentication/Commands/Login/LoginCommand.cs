using HRMS.Application.Authentication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.Login
{
    public class LoginCommand : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
