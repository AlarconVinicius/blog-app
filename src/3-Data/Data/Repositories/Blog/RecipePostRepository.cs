using Business.Interfaces.Repositories.Blog;
using Business.Models.Blog;
using Business.Models.Blog.Recipe;
using Data.Configuration;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Blog.Repositories;

public class RecipePostRepository : BaseRepository<RecipePost>, IRecipePostRepository
{

    public RecipePostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<RecipePost> GetRecipeByTitle(string title)
    {
        var recipeDb = await _context.Recipes.FirstOrDefaultAsync(b => b.Title == title);
        if (recipeDb == null) return null!;
        return recipeDb;
    }

    public async Task<List<RecipePost>> GetAllRecipes()
    {
        return await _context.Recipes
                             .Include(rp =>  rp.User)
                             .Include(rp => rp.Category)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<RecipePost> GetRecipeById(Guid id)
    {
        var entityDb = await _context.Recipes
                                     .Include(rp => rp.User)
                                     .Include(rp => rp.Category)
                                     .FirstOrDefaultAsync(rp => rp.Id == id);
        if (entityDb == null) return null!;
        return entityDb;
    }
}
