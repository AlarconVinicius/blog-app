using Business.Auth.Models;
using System.Text.RegularExpressions;

namespace Business.Blog.Models;
public abstract class MainPost : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid BlogId { get; set; }
    public virtual BlogEntity? Blog { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public string URL { get; private set; } = string.Empty;

    protected MainPost()
    {

    }

    protected MainPost(string title, string content, Guid blogId, Guid userId, Guid categoryId)
    {
        Title = title;
        Content = content;
        BlogId = blogId;
        UserId = userId.ToString();
        CategoryId = categoryId;
        GenerateURL();
    }

    public void UpdateBlog(string title, string content, Guid categoryId)
    {
        Title = title;
        Content = content;
        CategoryId = categoryId;
        GenerateURL();
    }

    public void GenerateURL()
    {
        URL = Regex.Replace(Title.ToLower(), @"[^a-z0-9]+", "-");
    }
}