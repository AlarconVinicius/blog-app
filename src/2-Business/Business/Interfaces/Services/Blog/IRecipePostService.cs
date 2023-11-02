using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Services.Blog;

public interface IRecipePostService : IMainService
{
    public Task AddRecipe(RecipePostAddDto recipe);

    public Task UpdateRecipe(RecipePost recipe);

    public Task DeleteRecipe(Guid id);

    public Task<IEnumerable<RecipePostViewDto>> GetRecipes();

    public Task<RecipePostViewDto> GetRecipeById(Guid id);
}
