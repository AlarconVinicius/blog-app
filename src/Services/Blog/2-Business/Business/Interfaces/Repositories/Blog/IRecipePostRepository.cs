using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Repositories.Blog;

public interface IRecipePostRepository : IBaseRepository<RecipePost>
{
    Task<RecipePost> GetRecipeByTitle(string title);
    Task<RecipePost> GetRecipeByUrl(string url, Guid? userId = null);
    Task<RecipePost> GetRecipeById(Guid id, Guid? userId = null);
    Task<List<RecipePost>> GetRecipesBySearch(string searchQuery, Guid? userId = null);
    Task<List<RecipePost>> GetRecipesByCategory(string category, Guid? userId = null);
    Task<List<RecipePost>> GetAllRecipes(Guid? userId = null);

}
