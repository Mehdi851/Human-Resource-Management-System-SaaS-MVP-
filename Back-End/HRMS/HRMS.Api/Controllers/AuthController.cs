using HRMS.Application.Authentication.Commands.Login;
using HRMS.Application.Authentication.Commands.Logout;
using HRMS.Application.Authentication.Commands.RefreshToken;
using HRMS.Application.Authentication.Commands.RegisterUser;
using HRMS.Application.Authentication.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        public AuthController(
                IMediator mediator,
                ICurrentUserService currentUserService)
        {
              _mediator = mediator;
              _currentUserService = currentUserService;
         }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            return Ok(new
            {
                UserId = _currentUserService.UserId,
                Email = _currentUserService.Email,
                Role = _currentUserService.Role,
                OrganizationId = _currentUserService.OrganizationId,
                IsAuthenticated = _currentUserService.IsAuthenticated
            });
        }

        // =========================================================
        // LOGIN
        // =========================================================

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // REGISTER
        // =========================================================

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // REFRESH TOKEN
        // =========================================================

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
            [FromBody] RefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // CURRENT USER
        // =========================================================

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = _currentUserService.UserId,
                Email = _currentUserService.Email,
                Role = _currentUserService.Role,
                OrganizationId = _currentUserService.OrganizationId,
                IsAuthenticated =
                    _currentUserService.IsAuthenticated
            });
        }

        // =========================================================
        // LOGOUT
        // =========================================================

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            [FromBody] LogoutCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                command,
                cancellationToken);

            return Ok(new
            {
                Message = "Logged out successfully."
            });
        }
    }
}
