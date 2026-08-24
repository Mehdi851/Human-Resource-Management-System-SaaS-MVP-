using HRMS.Application.Authentication.DTOs;
using HRMS.Application.Authentication.Services;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using HRMS.Application.Authentication.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using RefreshTokenEntity = HRMS.Domain.Entities.RefreshToken;
using HRMS.Application.Common.Interfaces;

namespace HRMS.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler
    : IRequestHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService,
            IRefreshTokenService refreshTokenService,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
            _refreshTokenService = refreshTokenService;

        }

        public async Task<AuthenticationResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email
                .Trim()
                .ToLowerInvariant();

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException(
                    "User account is inactive.");
            }

            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    request.Password);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
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

            // Generate JWT access token
            var tokenResult =
                await _jwtTokenService.GenerateAccessTokenAsync(
                    user,
                    cancellationToken);

            // Generate secure refresh token
            var refreshToken =
                _refreshTokenService.GenerateToken();

            // Persist refresh token
            var refreshTokenEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    _jwtSettings.RefreshTokenExpirationDays),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(
                refreshTokenEntity,
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
                RefreshToken = refreshToken,
                ExpiresAt = tokenResult.ExpiresAt
            };
        
        }
    }
}
