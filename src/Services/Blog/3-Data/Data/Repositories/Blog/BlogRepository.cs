using Business.Interfaces.Repositories.Blog;
using Business.Models.Blog;
using Data.Configuration;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Blog.Repositories;

public class BlogRepository : BaseRepository<BlogEntity>, IBlogRepository
{

    public BlogRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<BlogEntity> GetBlogByName(string blogName)
    {
        var blogNameNormalized = blogName.ToLower();
        var blogDb = await _context.Blogs.FirstOrDefaultAsync(b => b.NormalizedName == blogNameNormalized);
        if (blogDb == null) return null!;
        return blogDb;
    }
}
