using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Services
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateAsync(
        Guid userId,
        CancellationToken cancellationToken);

        Task<bool> ValidateAsync(
            string refreshToken,
            CancellationToken cancellationToken);

        Task RevokeAsync(
            string refreshToken,
            CancellationToken cancellationToken);
    }
}
