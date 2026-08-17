using HRMS.Domain.Common;
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HRMS.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Designation> Designations => Set<Designation>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreatedAt =
                            DateTime.UtcNow;

                        // Later pull from CurrentUserService
                        //entry.Entity.CreatedBy ??=
                        //    "System";

                        break;


                    case EntityState.Modified:

                        entry.Entity.UpdatedAt =
                            DateTime.UtcNow;

                        //entry.Entity.ModifiedBy =
                        //    "System";

                        break;


                    case EntityState.Deleted:

                        // Convert hard delete into soft delete
                        entry.State = EntityState.Modified;

                        entry.Entity.IsDeleted = true;

                        entry.Entity.UpdatedAt =
                            DateTime.UtcNow;

                        //entry.Entity.ModifiedBy =
                        //    "System";

                        break;
                }
            }

            // Future Domain Events Hook
            // DispatchDomainEvents();

            return await base.SaveChangesAsync(
                cancellationToken);
        }
    }
}
