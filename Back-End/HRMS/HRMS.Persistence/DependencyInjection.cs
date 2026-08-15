using HRMS.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            // Database
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    }));

            return services;
        }
    }
}
