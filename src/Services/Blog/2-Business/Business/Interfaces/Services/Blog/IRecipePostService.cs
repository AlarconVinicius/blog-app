using Business.Models.Blog.Dtos;

namespace Business.Interfaces.Services.Blog;

public interface IRecipePostService : IMainService
{
    public Task AddRecipe(RecipePostAddDto recipe);

    public Task UpdateRecipe(Guid recipeId, RecipePostAddDto recipe);

    public Task DeleteRecipe(Guid id);

    public Task<RecipePostViewDto> GetRecipeById(Guid id, Guid? userId);
    public Task<RecipePostViewDto> GetRecipeByUrl(string url, Guid? userId);

    Task<IEnumerable<RecipePostViewDto>> GetRecipesBySearch(string searchQuery, Guid? userId);
    Task<IEnumerable<RecipePostViewDto>> GetRecipesByCategory(string category, Guid? userId);
    public Task<IEnumerable<RecipePostViewDto>> GetRecipes(Guid? userId);
}
