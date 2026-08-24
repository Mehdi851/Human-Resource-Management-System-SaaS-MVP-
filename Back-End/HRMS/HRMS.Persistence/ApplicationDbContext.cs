using HRMS.Domain.Common;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HRMS.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, ApplicationRole, Guid>
    {

        public ApplicationDbContext(
         DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Organization> Organizations => Set<Organization>();
        //public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Designation> Designations => Set<Designation>();
        public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            ConfigureIdentity(builder);
            ConfigureRefreshToken(builder);

            builder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendances");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.OrganizationId)
                    .IsRequired();

                entity.Property(a => a.EmployeeId)
                    .IsRequired();

                entity.Property(a => a.AttendanceDate)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(a => a.CheckInTime)
                    .HasColumnType("time");

                entity.Property(a => a.CheckOutTime)
                    .HasColumnType("time");

                entity.Property(a => a.Status)
                    .IsRequired();

                entity.Property(a => a.WorkingHours)
                    .HasColumnType("time");

                entity.Property(a => a.Remarks)
                    .HasMaxLength(500);

                // Organization relationship
                entity.HasOne(a => a.Organization)
                    .WithMany()
                    .HasForeignKey(a => a.OrganizationId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Employee relationship
                // Attendance history must survive employee deletion.
                entity.HasOne(a => a.Employee)
                    .WithMany()
                    .HasForeignKey(a => a.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                // One attendance record per employee per calendar date.
                entity.HasIndex(a => new
                {
                    a.EmployeeId,
                    a.AttendanceDate
                })
                .IsUnique();
            });

            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Token)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasIndex(x => x.Token)
                    .IsUnique();

                entity.Property(x => x.ExpiresAt)
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .IsRequired();

                entity.Property(x => x.IsRevoked)
                    .IsRequired();

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        private static void ConfigureIdentity(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(x => x.OrganizationId)
                    .IsRequired();

                entity.HasOne(x => x.Organization)
                    .WithMany()
                    .HasForeignKey(x => x.OrganizationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => new
                {
                    x.OrganizationId,
                    x.Email
                })
                .IsUnique();
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
            });

            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
            });
        }

        private static void ConfigureRefreshToken(ModelBuilder builder)
        {
            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Token)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasIndex(x => x.Token)
                    .IsUnique();

                entity.Property(x => x.ExpiresAt)
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .IsRequired();

                entity.HasOne<AppUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
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
