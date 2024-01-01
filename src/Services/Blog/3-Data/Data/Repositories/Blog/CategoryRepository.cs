using Business.Interfaces.Repositories.Blog;
using Business.Models.Blog;
using Data.Configuration;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Blog.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Category> GetCategoryByName(string name)
    {
        var categoryDb = await _context.Categories.FirstOrDefaultAsync(b => b.Name == name);
        if (categoryDb == null) return null!;
        return categoryDb;
    }
}
