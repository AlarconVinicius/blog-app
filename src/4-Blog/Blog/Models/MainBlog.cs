using System.Text.RegularExpressions;

namespace Blog.Models;

public abstract class MainBlog : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = new Category();
    public string URL { get; private set; } = string.Empty;

    protected MainBlog()
    {

    }

    protected MainBlog(string title, string content, Guid categoryId)
    {
        Title = title;
        Content = content;
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