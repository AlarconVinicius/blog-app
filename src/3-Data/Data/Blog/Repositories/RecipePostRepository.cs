using Business.Blog.Interfaces.Repositories;
using Business.Blog.Models.Recipe;
using Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Data.Blog.Repositories;

public class RecipePostRepository : IRecipePostRepository
{
    private readonly ApplicationDbContext _context;

    public RecipePostRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddRecipeAsync(RecipePost objeto)
    {
        //await _context.Recipes.AddAsync(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(RecipePost objeto)
    {
        //_context.Recipes.Update(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(Guid id)
    {
        var objeto = await GetRecipeByIdAsync(id);
        //_context.Recipes.Remove(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RecipePost>> ListRecipesAsync()
    {
        //return await _context.Recipes.AsNoTracking().ToListAsync();
        return new List<RecipePost>();
    }

    public async Task<RecipePost> GetRecipeByIdAsync(Guid id)
    {
        //var recipePostDb = await _context.Recipes.FindAsync(id);
        //if (recipePostDb == null) return null!;
        //return recipePostDb;        
        return new RecipePost();
    }
}
