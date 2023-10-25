namespace Business.Blog.Models.Recipe;

public class RecipePost : MainPost
{
    public Difficulty Difficulty { get; set; }
    public string PreparationTime { get; set; } = string.Empty;
    public int Servings { get; set; }

    public RecipePost(
        string title,
        string content,
        Guid blogId,
        Guid userId,
        Guid categoryId,
        Difficulty difficulty,
        string preparationTime,
        int servings)
        : base(title, content, blogId, userId, categoryId)
    {
        Difficulty = difficulty;
        PreparationTime = preparationTime;
        Servings = servings;
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
        int servings)
    {
        UpdateBlog(title, content, categoryId);
        Difficulty = difficulty;
        PreparationTime = preparationTime;
        Servings = servings;
    }
}
