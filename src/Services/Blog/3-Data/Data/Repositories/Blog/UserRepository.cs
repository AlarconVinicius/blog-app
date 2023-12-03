using Business.Interfaces.Repositories.Blog;
using Business.Models.Blog.Recipe;
using Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Blog;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task FavoriteRecipe(UserFavoriteRecipe userRecipe)
    {
        await _context.UserFavoriteRecipes.AddAsync(userRecipe);
        await _context.SaveChangesAsync();
    }

    public async Task UnfavoriteRecipe(UserFavoriteRecipe userRecipe)
    {
        _context.UserFavoriteRecipes.Remove(userRecipe);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RecipePost>> GetFavoriteRecipesByUserId(Guid userId)
    {
        var favoriteRecipeIds = await _context.UserFavoriteRecipes
                                              .Where(fr => fr.UserId == userId.ToString())
                                              .Select(fr => fr.RecipeId)
                                              .ToListAsync();

        var favoriteRecipes = await _context.Recipes
                                            .Where(rp => favoriteRecipeIds.Contains(rp.Id))
                                            .Include(rp => rp.User)
                                            .Include(rp => rp.Category)
                                            .AsNoTracking()
                                            .ToListAsync();
        return favoriteRecipes;
    }

    public async Task<UserFavoriteRecipe> GetFavoriteRecipeByUserAndRecipeId(UserFavoriteRecipe userRecipe)
    {
        var entityDb =  await _context.UserFavoriteRecipes
                                              .Where(fr => fr.UserId == userRecipe.UserId)
                                              .Where(fr => fr.RecipeId == userRecipe.RecipeId)
                                              .FirstOrDefaultAsync();
        if (entityDb == null) return null!;
        return entityDb;
    }
}
