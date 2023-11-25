namespace Business.Models.Blog.Dtos;

public record UserViewDto(Guid Id, string Name, string LastName, string FullName, string Email, string PhoneNumber);
