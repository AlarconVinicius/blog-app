using Business.Models.Blog;

namespace Business.Interfaces.Repositories.Blog;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> GetCategoryByName(string name);
}
