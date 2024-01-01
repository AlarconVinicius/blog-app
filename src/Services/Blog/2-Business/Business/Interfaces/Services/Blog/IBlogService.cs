using Business.Models.Blog;

namespace Business.Interfaces.Services.Blog;

public interface IBlogService
{
    public Task AddBlog(BlogEntity blog);

    public Task UpdateBlog(Guid id, BlogEntity blog);

    public Task DeleteBlog(Guid id);

    public Task<List<BlogEntity>> GetAllBlogs();

    public Task<BlogEntity> GetBlogById(Guid id);
}
