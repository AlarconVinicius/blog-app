using Blog.Business.Interfaces.Repositories;
using Blog.Models.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class RecipePostRepository : IRecipePostRepository
{
    private readonly BlogDbContext _context;

    public RecipePostRepository(BlogDbContext context)
    {
        _context = context;
    }
    public async Task AddRecipeAsync(RecipePost objeto)
    {
        await _context.Recipes.AddAsync(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(RecipePost objeto)
    {
        _context.Recipes.Update(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(Guid id)
    {
        var objeto = await GetRecipeByIdAsync(id);
        _context.Recipes.Remove(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RecipePost>> ListRecipesAsync()
    {
        return await _context.Recipes.AsNoTracking().ToListAsync();
    }

    public async Task<RecipePost> GetRecipeByIdAsync(Guid id)
    {
        var recipePostDb = await _context.Recipes.FindAsync(id);
        if (recipePostDb == null) return null!;
        return recipePostDb;
    }
}
