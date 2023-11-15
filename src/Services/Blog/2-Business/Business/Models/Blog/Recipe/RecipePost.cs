namespace Business.Models.Blog.Recipe;

public class RecipePost : MainPost
{
    public Difficulty Difficulty { get; set; }
    public string PreparationTime { get; set; } = string.Empty;
    public int Servings { get; set; }
    public string Ingredients { get; set; } = string.Empty;

    public RecipePost(
        string title,
        string content,
        Guid blogId,
        Guid userId,
        Guid categoryId,
        Difficulty difficulty,
        string preparationTime,
        int servings,
        string ingredients)
        : base(title, content, blogId, userId, categoryId)
    {
        Difficulty = difficulty;
        PreparationTime = preparationTime;
        Servings = servings;
        Ingredients = ingredients;
    }

    public RecipePost()
    {
    }

    public void UpdateRecipe(
        string title,
        string content,
        Guid categoryId,
        Difficulty difficulty,
        string preparationTime,
        int servings,
        string ingredients)
    {
        UpdateBlog(title, content, categoryId);
        Difficulty = difficulty;
        PreparationTime = preparationTime;
        Servings = servings;
        Ingredients = ingredients;
    }
}
