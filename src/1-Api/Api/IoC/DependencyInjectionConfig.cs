using Business;
using Business.Interfaces.Repositories;
using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Auth;
using Business.Interfaces.Services.Blog;
using Business.Models.Auth;
using Business.Services.Auth;
using Business.Services.Blog;
using Data.Blog.Repositories;
using Data.Configuration;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.IoC;

public static class DependencyInjectionConfig
{
    public static void ConfigureDbContextJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<ApplicationUser>()
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

    public static void ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }

    public static void ConfigureCustomAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void ConfigureCustomBlogServices(this IServiceCollection services)
    {
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IBlogService, BlogService>();
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IRecipePostRepository, RecipePostRepository>();
        services.AddScoped<IRecipePostService, RecipePostService>();
    }
}