using HRMS.Application.Authentication.Configuration;
using HRMS.Application.Authentication.Services;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace HRMS.Infrastructure.Repositories
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(
            UserManager<AppUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<(string Token, DateTime ExpiresAt)> GenerateAccessTokenAsync(AppUser user, CancellationToken cancellationToken = default)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var expiresAt = DateTime.UtcNow.AddMinutes(
                _jwtSettings.AccessTokenExpirationMinutes);

            var claims = new List<Claim>
    {
        new(
            JwtRegisteredClaimNames.Sub,
            user.Id.ToString()),

        new(
            JwtRegisteredClaimNames.Email,
            user.Email ?? string.Empty),

        new(
            ClaimTypes.NameIdentifier,
            user.Id.ToString()),

        new(
            ClaimTypes.Email,
            user.Email ?? string.Empty),

        new(
            "organizationId",
            user.OrganizationId.ToString())
    };

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return (tokenString, expiresAt);
        }

        
    }
}
