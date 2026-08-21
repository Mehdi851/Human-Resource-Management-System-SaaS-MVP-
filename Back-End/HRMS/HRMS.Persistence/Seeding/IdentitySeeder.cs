using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Persistence.Seeding
{
    public static class IdentitySeeder
    {
        private static readonly string[] Roles =
        {
        "SuperAdmin",
        "HRAdmin",
        "Employee"
    };

        public static async Task SeedRolesAsync(
            RoleManager<ApplicationRole> roleManager)
        {
            foreach (var roleName in Roles)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    continue;
                }

                var role = new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpperInvariant()
                };

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(
                        "; ",
                        result.Errors.Select(x => x.Description));

                    throw new InvalidOperationException(
                        $"Unable to create role '{roleName}': {errors}");
                }
            }
        }
    }
}
