using Business.Models.Blog.Recipe;

namespace Business.Models.Blog;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public Guid BlogId { get; set; }
    public virtual BlogEntity? Blog { get; set; }
    public ICollection<RecipePost>? RecipePosts { get; set; }

    public Category()
    {

    }

    public Category(string name, Guid blogId)
    {
        Name = name;
        BlogId = blogId;
    }
}
