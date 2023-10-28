using Business.Models.Auth;

namespace Business.Models.Blog;

public class UserBlog : Entity
{
    public string UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Guid BlogId { get; set; }
    public BlogEntity? Blog { get; set; }

    public UserBlog(string userId, Guid blogId)
    {
        UserId = userId;
        BlogId = blogId;
    }
}
