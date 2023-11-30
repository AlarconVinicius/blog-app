using Business.Interfaces.Repositories.Blog;
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
    public async Task<List<RecipePost>> GetRecipeBySearch(string searchQuery)
    {
        return await _context.Recipes
                             .Where(rp => rp.Title.Contains(searchQuery) || EF.Functions.Like(rp.Ingredients, "%" + searchQuery + "%"))
                             .Include(rp => rp.User)
                             .Include(rp => rp.Category)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<List<RecipePost>> GetRecipeByCategory(string category)
    {
        return await _context.Recipes
                             .Include(rp => rp.Category)
                             .Where(rp => rp.Category!.Name == category)
                             .Include(rp => rp.User)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<List<RecipePost>> GetAllRecipes()
    {
        return await _context.Recipes
                             .Include(rp => rp.User)
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

    public async Task<RecipePost> GetRecipeByIdAndUser(Guid id, string userId)
    {
        var entityDb = await _context.Recipes
                                     .Include(rp => rp.User)
                                     .Include(rp => rp.Category)
                                     .FirstOrDefaultAsync(rp => rp.Id == id && rp.UserId == userId);
        if (entityDb == null) return null!;
        return entityDb;
    }

    public async Task<List<RecipePost>> GetAllRecipesByUser(string userId)
    {
        return await _context.Recipes
                             .Where(rp => rp.UserId == userId)
                             .Include(rp => rp.User)
                             .Include(rp => rp.Category)
                             .AsNoTracking()
                             .ToListAsync();
    }
}
