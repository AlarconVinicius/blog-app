using Auth;
using Auth.Business.Interfaces;
using Auth.Business.Services;
using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.IoC.Auth;

public static class AuthDependencyInjectionConfig
{
    public static void ConfigureIdentityDbContextJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<ApplicationUserEntity>()
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });

        // Jwt
        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings!.Secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = appSettings.Issuer,

                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidAudience = appSettings.Audience
            };
        });
    }
    public static void ConfigureCustomAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

}