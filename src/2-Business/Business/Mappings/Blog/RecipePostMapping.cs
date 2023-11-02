using Business.Mappings.Auth;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;

namespace Business.Mappings.Blog;

public static class RecipePostMapping
{
    public static RecipePostViewDto ToDto(this RecipePost recipePost) 
    {

        return new RecipePostViewDto(recipePost.Id, recipePost.Title, recipePost.Content, recipePost.User!.ToDto(), recipePost.Category!.ToDto(), recipePost.Difficulty.ToDto(), recipePost.PreparationTime, recipePost.Servings);
    }

    public static RecipePost ToDomain(this RecipePostAddDto recipePost)
    {

        return new RecipePost(recipePost.title, recipePost.content, recipePost.blogId, recipePost.userId, recipePost.categoryId, recipePost.difficulty, recipePost.preparationTime, recipePost.servings);
    }
}
