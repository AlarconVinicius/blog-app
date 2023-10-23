using Blog.Business.Interfaces.Repositories;
using Blog.Business.Interfaces.Services;
using Blog.Business.Services;
using Blog.Data;
using Blog.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.IoC.Blog;

public static class BlogDependencyInjectionConfig
{
    public static void ConfigureBlogDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    public static void ConfigureCustomBlogServices(this IServiceCollection services)
    {
        services.AddScoped<IRecipeBlogRepository, RecipeBlogRepository>();
        services.AddScoped<IRecipeBlogService, RecipeBlogService>();
    }

}
