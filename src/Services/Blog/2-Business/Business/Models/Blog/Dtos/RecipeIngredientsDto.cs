using Newtonsoft.Json;

namespace Business.Models.Blog.Dtos;

public class RecipeIngredientsDto
{
    public Dictionary<string, List<string>> Ingredients { get; set; }

    public RecipeIngredientsDto()
    {
        Ingredients = new Dictionary<string, List<string>>();
    }

    public void AddIngredient(string group, string ingredient)
    {
        if (!Ingredients.ContainsKey(group))
        {
            Ingredients[group] = new List<string>();
        }

        Ingredients[group].Add(ingredient);
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static RecipeIngredientsDto FromString(string json)
    {
        return JsonConvert.DeserializeObject<RecipeIngredientsDto>(json);
    }
}
