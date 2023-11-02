using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Repositories.Blog;

public interface IRecipePostRepository : IBaseRepository<RecipePost>
{
    Task<RecipePost> GetRecipeByTitle(string title);
}
