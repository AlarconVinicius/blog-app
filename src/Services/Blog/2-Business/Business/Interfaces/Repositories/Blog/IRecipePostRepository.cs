using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Repositories.Blog;

public interface IRecipePostRepository : IBaseRepository<RecipePost>
{
    Task<RecipePost> GetRecipeByTitle(string title);
    Task<RecipePost> GetRecipeById(Guid id);
    Task<List<RecipePost>> GetRecipeBySearch(string searchQuery);
    Task<List<RecipePost>> GetRecipeByCategory(string category);
    Task<List<RecipePost>> GetAllRecipes();
    Task<RecipePost> GetRecipeByIdAndUser(Guid id, string userId);
    Task<List<RecipePost>> GetAllRecipesByUser(string userId);

}
