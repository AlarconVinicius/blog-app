using Blog.Models.Recipes;

namespace Blog.Business.Interfaces.Repositories;

public interface IRecipePostRepository
{
    public Task AddRecipeAsync(RecipePost objeto);

    public Task UpdateRecipeAsync(RecipePost objeto);

    public Task DeleteRecipeAsync(Guid id);

    public Task<List<RecipePost>> ListRecipesAsync();

    public Task<RecipePost> GetRecipeByIdAsync(Guid id);
}
