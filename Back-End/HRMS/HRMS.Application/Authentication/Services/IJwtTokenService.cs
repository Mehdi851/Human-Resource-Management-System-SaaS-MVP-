using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Services
{
    public interface IJwtTokenService
    {
        Task<(string Token, DateTime ExpiresAt)> GenerateAccessTokenAsync(
            AppUser user,
            CancellationToken cancellationToken = default);
    }
}
