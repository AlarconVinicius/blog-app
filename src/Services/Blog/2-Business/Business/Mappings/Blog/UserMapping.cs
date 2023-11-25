using Business.Models.Auth;
using Business.Models.Blog.Dtos;

namespace Business.Mappings.Blog;

public static class UserMapping
{
    public static UserViewDto ToDto(this ApplicationUser user)
    {
        return new UserViewDto(Guid.Parse(user.Id), user.Name, user.LastName, user.FullName, user.Email!, user.PhoneNumber!);
    }
}