using Business.Models.Blog.Dtos;

namespace Business.Interfaces.Services.Blog;

public interface IRecipePostService : IMainService
{
    public Task AddRecipe(RecipePostAddDto recipe);

    public Task UpdateRecipe(Guid recipeId, RecipePostAddDto recipe);

    public Task DeleteRecipe(Guid id);

    public Task<IEnumerable<RecipePostViewDto>> GetRecipes();

    Task<IEnumerable<RecipePostViewDto>> GetRecipeBySearch(string searchQuery);
    public Task<RecipePostViewDto> GetRecipeById(Guid id);
    public Task<RecipePostViewDto> GetRecipeByIdForCurrentUser(Guid id);
    public Task<IEnumerable<RecipePostViewDto>> GetRecipesForCurrentUser();
}
