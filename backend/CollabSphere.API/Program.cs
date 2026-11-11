using CollabSphere.API.Services;
using CollabSphere.API.Authorization;
using CollabSphere.Infrastructure.Services;
using CollabSphere.Application.Auth;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CollabSphere.Infrastructure.Persistence;
using CollabSphere.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddOpenApi();

// MySQL
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Không tìm thấy DefaultConnection."
    );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString)
);

// JWT configuration
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Thiếu Jwt:Key.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Thiếu Jwt:Issuer.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Thiếu Jwt:Audience.");

var jwtKeyBytes = Convert.FromBase64String(jwtKey);

if (jwtKeyBytes.Length < 32)
{
    throw new InvalidOperationException(
        "JWT signing key phải có ít nhất 32 bytes."
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

// Middleware xử lý lỗi
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
