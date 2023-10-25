using Business.Blog.Models.Recipe;

namespace Business.Blog.Models;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public Guid BlogId { get; set; }
    public virtual BlogEntity? Blog { get; set; }
    public ICollection<RecipePost>? RecipePosts { get; set; }

    public Category()
    {

    }
}
