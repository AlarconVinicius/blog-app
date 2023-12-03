using Business.Models.Auth;
using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Repositories.Blog;
public interface IUserRepository
{
    Task FavoriteRecipe(UserFavoriteRecipe userRecipe);
    Task UnfavoriteRecipe(UserFavoriteRecipe userRecipe);
    Task<List<RecipePost>> GetFavoriteRecipesByUserId(Guid userId);
    Task<UserFavoriteRecipe> GetFavoriteRecipeByUserAndRecipeId(UserFavoriteRecipe userRecipe);
}
