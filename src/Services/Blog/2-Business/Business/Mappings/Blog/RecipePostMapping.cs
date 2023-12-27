using Business.Helpers;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;
using Business.Models.Image;

namespace Business.Mappings.Blog;

public static class RecipePostMapping
{
    public static RecipePostViewDto ToDto(this RecipePost recipePost)
    {
        ImageViewDto coverImage = ImageHelper.GetImage(recipePost.CoverImage);
        return new RecipePostViewDto(recipePost.Id, recipePost.Title, coverImage, recipePost.PreparationSteps, recipePost.User!.ToDto(), recipePost.Category!.ToDto(), recipePost.Difficulty.ToDto(), recipePost.PreparationTime, recipePost.Servings, recipePost.Ingredients, recipePost.URL, recipePost.CreatedAt.ToString("dd/MM/yyyy"), recipePost.UpdatedAt.ToString("dd/MM/yyyy"));
    }

    public static RecipePost ToDomain(this RecipePostAddDto recipePost)
    {
        return new RecipePost(recipePost.Title, recipePost.Image.Name, recipePost.BlogId, Guid.Empty, recipePost.CategoryId, recipePost.PreparationSteps, recipePost.Difficulty, recipePost.PreparationTime, recipePost.Servings, recipePost.Ingredients);
    }
}
