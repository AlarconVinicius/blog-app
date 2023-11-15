using Business.Models.Auth;
using Business.Models.Auth.Dto;

namespace Business.Mappings.Auth;

public static class AuthorMapping
{
    public static UserViewDto ToDto(this ApplicationUser user)
    {
        return new UserViewDto(Guid.Parse(user.Id), user.FullName, user.UserName!);
    }
}