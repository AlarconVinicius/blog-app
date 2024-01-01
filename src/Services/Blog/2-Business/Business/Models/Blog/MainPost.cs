using Business.Models.Auth;
using System.Text.RegularExpressions;

namespace Business.Models.Blog;
public abstract class MainPost : Entity
{
    public string Title { get; set; } = string.Empty;
    public string CoverImage { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public string URL { get; private set; } = string.Empty;

    protected MainPost()
    {

    }

    protected MainPost(string title, string coverImage, Guid userId, Guid categoryId)
    {
        Title = title;
        CoverImage = coverImage;
        UserId = userId.ToString();
        CategoryId = categoryId;
        GenerateURL();
    }

    public void GenerateURL()
    {
        URL = Regex.Replace(Title.ToLower(), @"[^a-z0-9]+", "-");
    }
}