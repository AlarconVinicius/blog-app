using Business.Helpers;
using Business.Models.Auth;
using Business.Models.Blog.Dtos;
using Business.Models.Image;

namespace Business.Mappings.Blog;

public static class UserMapping
{
    public static UserViewDto ToDto(this ApplicationUser user)
    {
        var profileImageExists = string.IsNullOrEmpty(user.ProfileImage) ? "profile-img.jpeg" : user.ProfileImage;
        ImageViewDto profileImage = ImageHelper.GetImage(profileImageExists);
        return new UserViewDto(Guid.Parse(user.Id), user.Name, user.LastName, user.FullName, user.Email!, user.PhoneNumber!, profileImage);
    }
}