using Business.Models.Auth;

namespace Business.Models.Blog.Recipe;
public class UserFavoriteRecipe : Entity
{
    public Guid RecipeId { get; set; }
    public string UserId { get; set; }

    public ApplicationUser? User { get; set; }
    public RecipePost? Recipe { get; set; }

    public UserFavoriteRecipe(Guid recipeId, string userId)
    {
        RecipeId = recipeId;
        UserId = userId;
    }
    
}
