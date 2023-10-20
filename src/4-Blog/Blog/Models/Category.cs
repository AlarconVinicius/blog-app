using Blog.Models.Recipes;

namespace Blog.Models;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<RecipeBlog> Blogs { get; set; } = new List<RecipeBlog>();
    public Category()
    {

    }
}
