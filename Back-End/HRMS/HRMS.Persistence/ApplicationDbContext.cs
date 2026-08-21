using HRMS.Domain.Common;
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
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
        public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<Attendance> Attendances => Set<Attendance>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

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
