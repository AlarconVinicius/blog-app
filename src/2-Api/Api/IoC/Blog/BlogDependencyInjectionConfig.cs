using Blog.Data;
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
    }

}
