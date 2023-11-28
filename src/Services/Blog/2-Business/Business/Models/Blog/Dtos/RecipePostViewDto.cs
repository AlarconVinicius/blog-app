using Business.Models.Auth.Dto;
using Newtonsoft.Json;

namespace Business.Models.Blog.Dtos;

public record RecipePostViewDto(Guid Id, string Title, string PreparationSteps, UserViewDto Author, CategoryViewDto Category, DifficultyViewDto Difficulty, string PreparationTime, int Servings, string Ingredients, string Url, string CreatedAt, string UpdatedAt)
{
    // public static List<string> PrepStepsFromString(string prepSteps)
    // {
    //     return JsonConvert.DeserializeObject<List<string>>(prepSteps);
    // }
};