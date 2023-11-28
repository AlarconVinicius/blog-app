using Business.Models.Blog.Recipe;
using Newtonsoft.Json;

namespace Business.Models.Blog.Dtos;

public record RecipePostAddDto(string Title, string PreparationSteps, Guid BlogId, Guid CategoryId, Difficulty Difficulty, string PreparationTime, int Servings, string Ingredients)
{
    // public static string PrepStepsToString(List<string> prepSteps)
    // {
    //     return JsonConvert.SerializeObject(prepSteps);
    // }
};
