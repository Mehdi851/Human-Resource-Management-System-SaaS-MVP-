using HRMS.Application.Authentication.Services;
using HRMS.Application.Common.Interfaces;
using HRMS.Infrastructure.Repositories;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection")));

            // Generic Repository
            services.AddScoped(
                typeof(IRepository<>),
                typeof(Repository<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<ISalaryStructureRepository, SalaryStructureRepository>();


            //JWT Reps
            services.AddScoped<IJwtTokenService, JwtTokenService>(); 
            services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
            services.AddScoped<IRefreshTokenService,RefreshTokenService>();
            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
