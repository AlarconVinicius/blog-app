using Business.Mappings.Auth;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;
using Newtonsoft.Json;

namespace Business.Mappings.Blog;

public static class RecipePostMapping
{
    public static RecipePostViewDto ToDto(this RecipePost recipePost)
    {
        var ingredients = RecipeIngredientsDto.FromString(recipePost.Ingredients);
        var prepSteps = RecipePostViewDto.PrepStepsFromString(recipePost.PreparationSteps);

        return new RecipePostViewDto(recipePost.Id, recipePost.Title, prepSteps!, recipePost.User!.ToDto(), recipePost.Category!.ToDto(), recipePost.Difficulty.ToDto(), recipePost.PreparationTime, recipePost.Servings, ingredients, recipePost.URL, recipePost.CreatedAt.ToString("dd/MM/yyyy"), recipePost.UpdatedAt.ToString("dd/MM/yyyy"));
    }

    public static RecipePost ToDomain(this RecipePostAddDto recipePost)
    {
        var prepSteps = RecipePostAddDto.PrepStepsToString(recipePost.PreparationSteps);
        return new RecipePost(recipePost.Title, recipePost.BlogId, Guid.Empty, recipePost.CategoryId, prepSteps, recipePost.Difficulty, recipePost.PreparationTime, recipePost.Servings, recipePost.Ingredients.ToString());
    }
}
