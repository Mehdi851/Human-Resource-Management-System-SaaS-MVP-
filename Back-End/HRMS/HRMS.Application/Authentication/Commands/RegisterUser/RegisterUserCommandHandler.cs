using HRMS.Application.Authentication.DTOs;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        //private readonly ApplicationDbContext _context;
        //private readonly  _organizationRepository;

        public RegisterUserCommandHandler(
            UserManager<AppUser> userManager,
            RoleManager<ApplicationRole> roleManager)
            //ApplicationDbContext context,
           // IOrganizationRepository organizationRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_context = context;
            //_organizationRepository = organizationRepository;
        }

        public async Task<RegisterUserResponse> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            //var organizationExists = await _organizationRepository.ExistsAsync(
            //    request.OrganizationId,
            //    cancellationToken);

            //if (!organizationExists)
            //{
            //    throw new InvalidOperationException(
            //        "Organization does not exist.");
            //}

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            var role = request.Role.Trim();

            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                throw new InvalidOperationException(
                    $"Role '{role}' does not exist.");
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = email,
                Email = email,
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                OrganizationId = request.OrganizationId,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    "; ",
                    result.Errors.Select(x => x.Description));

                throw new InvalidOperationException(errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                role);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                var errors = string.Join(
                    "; ",
                    roleResult.Errors.Select(x => x.Description));

                throw new InvalidOperationException(errors);
            }

            return new RegisterUserResponse
            {
                UserId = user.Id,
                Email = user.Email!,
                Role = role,
                OrganizationId = user.OrganizationId
            };
        }
    }
}
