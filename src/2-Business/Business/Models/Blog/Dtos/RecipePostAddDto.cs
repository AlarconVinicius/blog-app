using Business.Models.Blog.Recipe;

namespace Business.Models.Blog.Dtos;

public record RecipePostAddDto(string title, string content, Guid blogId, Guid userId, Guid categoryId, Difficulty difficulty, string preparationTime, int servings, RecipeIngredientsDto ingredients);
