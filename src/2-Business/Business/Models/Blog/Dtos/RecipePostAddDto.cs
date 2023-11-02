using Business.Models.Blog.Recipe;

namespace Business.Models.Blog.Dtos;

public record RecipePostAddDto(string Title, string Content, Guid BlogId, Guid UserId, Guid CategoryId, Difficulty Difficulty, string PreparationTime, int Servings, RecipeIngredientsDto Ingredients);
