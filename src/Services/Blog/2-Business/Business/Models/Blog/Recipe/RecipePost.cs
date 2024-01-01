namespace Business.Models.Blog.Recipe;

public class RecipePost : MainPost
{
    public string PreparationSteps { get; set; } = string.Empty;
    public Difficulty Difficulty { get; set; }
    public string PreparationTime { get; set; } = string.Empty;
    public int Servings { get; set; }
    public string Ingredients { get; set; } = string.Empty;
    public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }

    public RecipePost(
        string title,
        string coverImage,
        Guid userId,
        Guid categoryId,
        string preparationSteps,
        Difficulty difficulty,
        string preparationTime,
        int servings,
        string ingredients)
        : base(title, coverImage, userId, categoryId)
    {
        PreparationSteps = preparationSteps;
        Difficulty = difficulty;
        PreparationTime = preparationTime;
        Servings = servings;
        Ingredients = ingredients;
    }

    public RecipePost()
    {
    }
}
