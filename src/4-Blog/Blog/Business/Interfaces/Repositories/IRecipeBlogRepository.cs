using Blog.Models.Recipes;

namespace Blog.Business.Interfaces.Repositories;

public interface IRecipeBlogRepository
{
    public Task AddRecipeAsync(RecipeBlog objeto);

    public Task UpdateRecipeAsync(RecipeBlog objeto);

    public Task DeleteRecipeAsync(Guid id);

    public Task<List<RecipeBlog>> ListRecipesAsync();

    public Task<RecipeBlog> GetRecipeByIdAsync(Guid id);
}
