using Business.Models.Blog.Recipe;

namespace Business.Models.Blog;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<RecipePost>? RecipePosts { get; set; }

    public Category()
    {

    }

    public Category(string name)
    {
        Name = name;
    }
}
