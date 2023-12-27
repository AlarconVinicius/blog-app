using Business.Models.Image;

namespace Business.Models.Blog.Dtos;

public record RecipePostViewDto(Guid Id, string Title, ImageViewDto CoverImage, string PreparationSteps, UserViewDto Author, CategoryViewDto Category, DifficultyViewDto Difficulty, string PreparationTime, int Servings, string Ingredients, string Url, string CreatedAt, string UpdatedAt)
{
};