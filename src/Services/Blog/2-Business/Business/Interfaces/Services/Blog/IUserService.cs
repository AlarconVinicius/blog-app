
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;

namespace Business.Interfaces.Services.Blog;

public interface IUserService : IMainService
{
    public Task UpdateAuthenticatedUser(UserUpdDto user);

    public Task<UserViewDto> GetAuthenticatedUser();

    public Task UpdatePassword(UserPasswordDto userPassword);

    public Task FavoriteRecipe(Guid recipeId, bool favorite);
    public Task<IEnumerable<RecipePostViewDto>> GetFavoriteRecipesByUserId();
}