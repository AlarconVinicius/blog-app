using Business.Models.Blog.Recipe;
using Business.Models.Image;

namespace Business.Models.Blog.Dtos;

public record RecipePostAddDto(string Title, string PreparationSteps, Guid BlogId, Guid CategoryId, Difficulty Difficulty, string PreparationTime, int Servings, string Ingredients, ImageAddDto Image)
{
};
