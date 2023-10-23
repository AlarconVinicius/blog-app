using Blog.Models.Recipes;
using Utils.Configuration.Business;

namespace Blog.Business.Interfaces.Services;

public interface IRecipeBlogService : IMainService
{
    public Task AddRecipe(RecipeBlog objeto);

    public Task UpdateRecipe(RecipeBlog objeto);

    public Task DeleteRecipe(Guid id);

    public Task<List<RecipeBlog>> GetRecipes();

    public Task<RecipeBlog> GetRecipeById(Guid id);
}
