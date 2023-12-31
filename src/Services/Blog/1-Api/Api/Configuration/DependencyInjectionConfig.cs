using Api.Configuration.Swagger;
using Business.Interfaces.Repositories;
using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Auth;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Dtos;
using Business.Services.Auth;
using Business.Services.Blog;
using Business.Validators;
using Data.Blog.Repositories;
using Data.Repositories;
using Data.Repositories.Blog;
using FluentValidation;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.ResolveApiDependencies();
        services.ResolveAuthDependencies();
        services.ResolveBlogDependencies();

        return services;
    }


    public static IServiceCollection ResolveApiDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    public static IServiceCollection ResolveAuthDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection ResolveBlogDependencies(this IServiceCollection services)
    {
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IBlogService, BlogService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IRecipePostRepository, RecipePostRepository>();
        services.AddScoped<IRecipePostService, RecipePostService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IValidator<RecipePostAddDto>, RecipePostAddValidator>();

        return services;
    }
}