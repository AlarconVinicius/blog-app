using Business.Models.Blog;

namespace Business.Interfaces.Repositories.Blog;

public interface IBlogRepository : IBaseRepository<BlogEntity>
{
    Task<BlogEntity> GetBlogByName(string blogName);
}
