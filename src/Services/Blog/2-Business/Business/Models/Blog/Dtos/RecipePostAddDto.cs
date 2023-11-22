using Business.Models.Blog.Recipe;
using Newtonsoft.Json;

namespace Business.Models.Blog.Dtos;

public record RecipePostAddDto(string Title, List<string> PreparationSteps, Guid BlogId, Guid CategoryId, Difficulty Difficulty, string PreparationTime, int Servings, RecipeIngredientsDto Ingredients)
{
    public static string PrepStepsToString(List<string> prepSteps)
    {
        return JsonConvert.SerializeObject(prepSteps);
    }
};
