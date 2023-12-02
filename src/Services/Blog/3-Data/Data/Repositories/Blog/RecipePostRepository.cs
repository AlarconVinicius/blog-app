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

    public async Task<RecipePost> GetRecipeByTitle(string url)
    {
        var recipeDb = await _context.Recipes.FirstOrDefaultAsync(b => b.URL == url);
        if (recipeDb == null) return null!;
        return recipeDb;
    }
    public async Task<RecipePost> GetRecipeByUrl(string url, Guid? userId = null)
    {
        var query = _context.Recipes.AsQueryable();

        if (userId != null && userId != Guid.Empty)
        {
            query = query.Where(rp => rp.UserId == userId.ToString());
        }
        var recipeDb = await query.FirstOrDefaultAsync(b => b.URL == url);
        if (recipeDb == null) return null!;
        return recipeDb;
    }
    public async Task<List<RecipePost>> GetRecipesBySearch(string searchQuery, Guid? userId = null)
    {
        var query = _context.Recipes.AsQueryable();

        if (userId != null && userId != Guid.Empty)
        {
            query = query.Where(rp => rp.UserId == userId.ToString());
        }

        return await query
                        .Where(rp => rp.Title.Contains(searchQuery) || EF.Functions.Like(rp.Ingredients, "%" + searchQuery + "%"))
                        .Include(rp => rp.User)
                        .Include(rp => rp.Category)
                        .AsNoTracking()
                        .ToListAsync();
    }


    public async Task<List<RecipePost>> GetRecipesByCategory(string category, Guid? userId = null)
    {
        var query = _context.Recipes.AsQueryable();

        if (userId != null && userId != Guid.Empty)
        {
            query = query.Where(rp => rp.UserId == userId.ToString());
        }
        return await query
                        .Include(rp => rp.Category)
                        .Where(rp => rp.Category!.Name == category)
                        .Include(rp => rp.User)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<List<RecipePost>> GetAllRecipes(Guid? userId = null)
    {
        var query = _context.Recipes.AsQueryable();

        if (userId != null && userId != Guid.Empty)
        {
            query = query.Where(rp => rp.UserId == userId.ToString());
        }
        return await query
                        .Include(rp => rp.User)
                        .Include(rp => rp.Category)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<RecipePost> GetRecipeById(Guid id, Guid? userId = null)
    {
        var query = _context.Recipes.AsQueryable();

        if (userId != null && userId != Guid.Empty)
        {
            query = query.Where(rp => rp.UserId == userId.ToString());
        }
        var entityDb = await query
                                .Include(rp => rp.User)
                                .Include(rp => rp.Category)
                                .FirstOrDefaultAsync(rp => rp.Id == id);
        if (entityDb == null) return null!;
        return entityDb;
    }
}
