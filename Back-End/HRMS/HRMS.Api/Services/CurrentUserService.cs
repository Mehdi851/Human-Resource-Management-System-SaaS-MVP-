using HRMS.Application.Authentication.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRMS.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User =>
            _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        public Guid? UserId =>
            GetGuidClaim(ClaimTypes.NameIdentifier);

        public Guid? OrganizationId =>
            GetGuidClaim("organizationId");

        public string? Email =>
            User?.FindFirstValue(ClaimTypes.Email)
            ?? User?.FindFirstValue(
                JwtRegisteredClaimNames.Email);

        public string? Role =>
            User?.FindFirstValue(ClaimTypes.Role);

        private Guid? GetGuidClaim(string claimType)
        {
            var value = User?.FindFirstValue(claimType);

            if (Guid.TryParse(value, out var id))
            {
                return id;
            }

            return null;
        }
    }
}
