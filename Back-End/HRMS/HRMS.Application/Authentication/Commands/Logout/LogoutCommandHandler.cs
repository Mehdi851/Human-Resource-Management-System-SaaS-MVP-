using HRMS.Application.Authentication.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.Logout
{
    public class LogoutCommandHandler
    : IRequestHandler<LogoutCommand, bool>
{
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LogoutCommandHandler(
            IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
            var refreshToken =
                await _refreshTokenRepository.GetByTokenAsync(
                    request.RefreshToken,
                    cancellationToken);

            if (refreshToken is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");
        }

        if (refreshToken.IsRevoked)
        {
            return true;
        }

        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            return true;
        }

        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;

            await _refreshTokenRepository.UpdateAsync(
                refreshToken,
                cancellationToken);

            return true;
    }
}
}
