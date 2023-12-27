using Business.Models.Image;

namespace Business.Models.Blog.Dtos;

public record UserUpdDto(string Name, string LastName, string FullName, string Email, string PhoneNumber, ImageAddDto ProfileImage);
