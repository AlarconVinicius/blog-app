using Business.Mappings.Auth;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;

namespace Business.Mappings.Blog;

public static class RecipePostMapping
{
    public static RecipePostViewDto ToDto(this RecipePost recipePost) 
    {
        var ingredients = RecipeIngredientsDto.FromString(recipePost.Ingredients);
        return new RecipePostViewDto(recipePost.Id, recipePost.Title, recipePost.Content, recipePost.User!.ToDto(), recipePost.Category!.ToDto(), recipePost.Difficulty.ToDto(), recipePost.PreparationTime, recipePost.Servings, ingredients);
    }

    public static RecipePost ToDomain(this RecipePostAddDto recipePost)
    {

        return new RecipePost(recipePost.Title, recipePost.Content, recipePost.BlogId, Guid.Empty, recipePost.CategoryId, recipePost.Difficulty, recipePost.PreparationTime, recipePost.Servings, recipePost.Ingredients.ToString());
    }
}
