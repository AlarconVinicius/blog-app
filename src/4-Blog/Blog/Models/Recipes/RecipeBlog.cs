namespace Blog.Models.Recipes;

public class RecipeBlog : MainBlog
{
    public Difficulty Difficulty { get; set; }
    public string PreparationTime { get; set; } = string.Empty;
    public int Servings { get; set; }

    public RecipeBlog() { }

    public RecipeBlog(
        string title,
        string content,
        Guid categoryId,
        Difficulty difficulty,
        string preparationTime,
        int servings)
        : base(title, content, categoryId)
    {
        Difficulty = difficulty;
        PreparationTime = preparationTime;
        Servings = servings;
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