using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Persistence.Configurations
{
    public class PayrollItemConfiguration : IEntityTypeConfiguration<PayrollItem>
    {
        public void Configure(EntityTypeBuilder<PayrollItem> builder)
        {
            builder.ToTable("PayrollItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrganizationId)
                .IsRequired();

            builder.Property(x => x.PayrollId)
                .IsRequired();

            builder.Property(x => x.EmployeeId)
                .IsRequired();

            builder.Property(x => x.BasicSalary)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Allowances)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Deductions)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.GrossSalary)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.NetSalary)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(x => x.Payroll)
                .WithMany(x => x.PayrollItems)
                .HasForeignKey(x => x.PayrollId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.PayrollId,
                x.EmployeeId
            })
            .IsUnique();

            builder.HasIndex(x => new
            {
                x.OrganizationId,
                x.EmployeeId
            });
        }
    }
}
