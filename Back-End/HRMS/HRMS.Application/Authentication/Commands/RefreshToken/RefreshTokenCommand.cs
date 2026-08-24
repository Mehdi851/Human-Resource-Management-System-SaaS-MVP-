using HRMS.Application.Authentication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommand
    : IRequest<AuthenticationResponse>
    {
        public string Token { get; set; } = default!;
    }
}
