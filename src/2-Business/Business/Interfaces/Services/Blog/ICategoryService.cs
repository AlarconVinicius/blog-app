using Business.Models.Blog;

namespace Business.Interfaces.Services.Blog;

public interface ICategoryService : IMainService
{
    public Task AddCategory(Category category);

    public Task UpdateCategory(Guid id, Category category);

    public Task DeleteCategory(Guid id);

    public Task<List<Category>> GetAllCategories();

    public Task<Category> GetCategoryById(Guid id);
}
