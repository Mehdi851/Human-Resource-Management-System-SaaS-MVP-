using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Persistence.Configurations
{
    public class SalaryStructureConfiguration : IEntityTypeConfiguration<SalaryStructure>
    {
        public void Configure(EntityTypeBuilder<SalaryStructure> builder)
        {
            builder.ToTable("SalaryStructures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BasicSalary)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Allowances)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Deductions)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.EffectiveFrom)
                .IsRequired();

            builder.Property(x => x.PaymentFrequency)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.OrganizationId)
                .IsRequired();

            builder.Property(x => x.EmployeeId)
                .IsRequired();

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.OrganizationId,
                x.EmployeeId
            });
        }
    }
}
