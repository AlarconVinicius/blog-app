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

namespace Api.IoC;

public static class DependencyInjectionConfig
{
    public static void ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IValidator<RecipePostAddDto>, RecipePostAddValidator>();
    }
}