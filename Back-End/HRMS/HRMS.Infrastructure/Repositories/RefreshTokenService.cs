using HRMS.Application.Authentication.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public string GenerateToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
