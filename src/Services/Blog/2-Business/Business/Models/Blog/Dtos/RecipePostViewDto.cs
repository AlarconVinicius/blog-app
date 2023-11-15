using Business.Models.Auth.Dto;

namespace Business.Models.Blog.Dtos;

public record RecipePostViewDto(Guid Id, string Title, string Content, UserViewDto Author, CategoryViewDto Category, DifficultyViewDto Difficulty, string PreparationTime, int Servings, RecipeIngredientsDto Ingredients);