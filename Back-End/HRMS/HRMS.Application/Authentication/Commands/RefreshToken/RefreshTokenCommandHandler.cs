using HRMS.Application.Authentication.Configuration;
using HRMS.Application.Authentication.DTOs;
using HRMS.Application.Authentication.Services;
using HRMS.Domain.Entities;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using RefreshTokenEntity = HRMS.Domain.Entities.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler
     : IRequestHandler<RefreshTokenCommand, AuthenticationResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenCommandHandler(
            UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService,
            IRefreshTokenService refreshTokenService,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(
                request.Token,
                cancellationToken);

            if (storedToken is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }

            if (storedToken.IsRevoked)
            {
                throw new UnauthorizedAccessException(
                    "Refresh token has been revoked.");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException(
                    "Refresh token has expired.");
            }

            var user = storedToken.User;

            if (user is null)
            {
                throw new UnauthorizedAccessException(
                    "Refresh token user was not found.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException(
                    "User account is inactive.");
            }

            if (user.OrganizationId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "User is not associated with an organization.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new UnauthorizedAccessException(
                    "User does not have an assigned role.");
            }

            // Revoke the old refresh token.
            storedToken.IsRevoked = true;
            storedToken.RevokedAt = DateTime.UtcNow;

            // Generate new access token.
            var tokenResult =
                await _jwtTokenService.GenerateAccessTokenAsync(
                    user,
                    cancellationToken);

            // Generate new refresh token.
            var newRefreshToken =
                _refreshTokenService.GenerateToken();

            var newRefreshTokenEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = newRefreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(
                _jwtSettings.RefreshTokenExpirationDays),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(
                newRefreshTokenEntity,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new AuthenticationResponse
            {
                UserId = user.Id,
                Email = user.Email!,
                Role = role,
                OrganizationId = user.OrganizationId,
                AccessToken = tokenResult.Token,
                RefreshToken = newRefreshToken,
                ExpiresAt = tokenResult.ExpiresAt
            };
        }
    }
}
