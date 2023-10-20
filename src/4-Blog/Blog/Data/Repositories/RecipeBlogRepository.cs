using Blog.Business.Interfaces.Repositories;
using Blog.Models.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class RecipeBlogRepository : IRecipeBlogRepository
{
    private readonly BlogDbContext _context;

    public RecipeBlogRepository(BlogDbContext context)
    {
        _context = context;
    }
    public async Task AddRecipeAsync(RecipeBlog objeto)
    {
        await _context.Recipes.AddAsync(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(RecipeBlog objeto)
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

    public async Task<List<RecipeBlog>> ListRecipesAsync()
    {
        return await _context.Recipes.AsNoTracking().ToListAsync();
    }

    public async Task<RecipeBlog> GetRecipeByIdAsync(Guid id)
    {
        var recipeBlogDb = await _context.Recipes.FindAsync(id);
        if (recipeBlogDb == null) return null!;
        return recipeBlogDb;
    }
}
