using Business.Models.Blog;
using Data.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Data.Seed.Auth;

public class CreateInitialBlogsSeed
{
    private readonly ApplicationDbContext _context;

    public CreateInitialBlogsSeed(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Create()
    {
        var blogId = Guid.Parse("2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62");
        var userId = "bdad85a9-f6c4-46df-bbf4-618718f5a3cb";
        var userBlogId = Guid.Parse("dc97459f-1924-49a1-a956-17a52b231d46");
        var blogName = "Receitas";
        var blog = new BlogEntity(blogName);
        blog.Id = blogId;

        var blogExists = _context.Blogs.Find(blogId);
        var userBlogExists = _context.UserBlogs.Find(userBlogId);

        if (blogExists is null)
        {
            _context.Blogs.Add(blog);
        }
        if (userBlogExists is null)
        {
            var userBlog = new UserBlog(userId, blogId);
            userBlog.Id = userBlogId;
            _context.UserBlogs.Add(userBlog);
        }
        _context.SaveChanges();
    }
}
