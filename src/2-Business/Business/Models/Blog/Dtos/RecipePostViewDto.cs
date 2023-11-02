using Business.Models.Auth.Dto;

namespace Business.Models.Blog.Dtos;

public record RecipePostViewDto(Guid id, string title, string content, UserViewDto author, CategoryViewDto category, DifficultyViewDto difficulty, string preparationTime, int servings);