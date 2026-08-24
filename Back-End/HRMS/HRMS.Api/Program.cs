using HRMS.Api.Middlewares;
using HRMS.Api.Services;
using HRMS.Application;
using HRMS.Application.Authentication.Authorization;
using HRMS.Application.Authentication.Configuration;
using HRMS.Application.Authentication.Services;
using HRMS.Domain.Entities;
using HRMS.Infrastructure;
using HRMS.Persistence;
using HRMS.Persistence.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add Services

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//Identntiy 
builder.Services
    .AddIdentityCore<AppUser>(options =>
    {
        options.Password.RequiredLength = 8;

        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Current User Service
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();
// JWT 

// JWT Settings
var jwtSettings =
    builder.Configuration
        .GetSection(JwtSettings.SectionName)
        .Get<JwtSettings>()
    ?? throw new InvalidOperationException(
        "JWT settings are not configured.");

// JWT Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtSettings.Secret)),

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();

//Role Based Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        AuthorizationPolicies.SuperAdminOnly,
        policy =>
        {
            policy.RequireRole(
                ApplicationRoles.SuperAdmin);
        });

    options.AddPolicy(
        AuthorizationPolicies.HRAdminOnly,
        policy =>
        {
            policy.RequireRole(
                ApplicationRoles.HRAdmin);
        });

    options.AddPolicy(
        AuthorizationPolicies.HRAdminOrSuperAdmin,
        policy =>
        {
            policy.RequireRole(
                ApplicationRoles.HRAdmin,
                ApplicationRoles.SuperAdmin);
        });

    options.AddPolicy(
        AuthorizationPolicies.EmployeeAccess,
        policy =>
        {
            policy.RequireRole(
                ApplicationRoles.Employee,
                ApplicationRoles.HRAdmin,
                ApplicationRoles.SuperAdmin);
        });
});
// DB

// DI

// AutoMapper

// Versioning

// Build
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<RoleManager<ApplicationRole>>();

    await IdentitySeeder.SeedRolesAsync(roleManager);
}

// Middleware
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
