using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Persistence.Configurations
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.ToTable("Payrolls");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrganizationId)
                .IsRequired();

            builder.Property(x => x.PayrollPeriodStart)
                .IsRequired();

            builder.Property(x => x.PayrollPeriodEnd)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasMany(x => x.PayrollItems)
                .WithOne(x => x.Payroll)
                .HasForeignKey(x => x.PayrollId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.OrganizationId,
                x.PayrollPeriodStart,
                x.PayrollPeriodEnd
            })
            .IsUnique();
        }
    }
}
