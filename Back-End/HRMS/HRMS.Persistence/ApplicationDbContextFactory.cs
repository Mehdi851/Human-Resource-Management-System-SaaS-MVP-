using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Persistence
{
    public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
             new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=WIN-NVIM3SVLCA8\\SQLEXPRESS;Database=HRMS_MVP_DB;Trusted_Connection=True;TrustServerCertificate=True");

            return new ApplicationDbContext(
                optionsBuilder.Options);
        }
    }
}
