using Business.Models.Auth;
using Business.Models.Blog.Recipe;

namespace Business.Models.Blog;

public class BlogEntity : Entity
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public ICollection<ApplicationUser>? Users { get; set; }
    public ICollection<RecipePost>? RecipePosts { get; set; }
    public ICollection<Category>? Categories { get; set; }

    public BlogEntity()
    {

    }

    public BlogEntity(string name)
    {
        Name = name;
        NormalizeName();
    }
    public void NormalizeName()
    {
        NormalizedName = Name.ToLower();
    }
}
