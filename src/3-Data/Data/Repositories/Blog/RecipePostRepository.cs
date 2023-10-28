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
}
