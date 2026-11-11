 feature/leader-backend-core
﻿using CollabSphere.API.Services;
using CollabSphere.API.Authorization;
using CollabSphere.API.Services;
 main
using CollabSphere.Infrastructure.Services;
using CollabSphere.Application.Auth;
using CollabSphere.Application.Admin;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CollabSphere.Infrastructure.Persistence;
using CollabSphere.Infrastructure.Database;
using CollabSphere.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "KhĂ´ng tĂ¬m tháº¥y DefaultConnection."
    );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString)
);

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Thiáº¿u Jwt:Key.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Thiáº¿u Jwt:Issuer.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Thiáº¿u Jwt:Audience.");

var jwtKeyBytes = Convert.FromBase64String(jwtKey);

if (jwtKeyBytes.Length < 32)
{
    throw new InvalidOperationException(
        "JWT signing key pháº£i cĂ³ Ă­t nháº¥t 32 bytes."
    );
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RolePolicies.AdminOnly,
        policy => policy.RequireRole(RoleNames.Admin));

    options.AddPolicy(RolePolicies.HeadOnly,
        policy => policy.RequireRole(RoleNames.Head));

    options.AddPolicy(RolePolicies.StaffOnly,
        policy => policy.RequireRole(RoleNames.Staff));

    options.AddPolicy(RolePolicies.LecturerOnly,
        policy => policy.RequireRole(RoleNames.Lecturer));

    options.AddPolicy(RolePolicies.StudentOnly,
        policy => policy.RequireRole(RoleNames.Student));
});

var app = builder.Build();

feature/leader-backend-core
// Middleware xá»­ lĂ½ lá»—i

var migrationPath = Path.Combine(AppContext.BaseDirectory, "migrations", "versions");
await MigrationEnvironment.ApplyPendingAsync(connectionString, migrationPath);

if (app.Environment.IsDevelopment())
{
    var seedPassword = builder.Configuration["Seed:DefaultPassword"];

    if (string.IsNullOrWhiteSpace(seedPassword))
    {
        Console.WriteLine("[Seed] Bỏ qua: chưa đặt Seed:DefaultPassword.");
    }
    else
    {
        var seedPath = Path.Combine(AppContext.BaseDirectory, "seeds");
        await DatabaseSeeder.SeedAsync(connectionString, seedPath, seedPassword);
    }
}

main
app.UseMiddleware<Middleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
