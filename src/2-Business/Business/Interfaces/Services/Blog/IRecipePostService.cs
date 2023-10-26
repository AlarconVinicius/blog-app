using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Services.Blog;

public interface IRecipePostService : IMainService
{
    public Task AddRecipe(RecipePost objeto);

    public Task UpdateRecipe(RecipePost objeto);

    public Task DeleteRecipe(Guid id);

    public Task<List<RecipePost>> GetRecipes();

    public Task<RecipePost> GetRecipeById(Guid id);
}
