namespace Business.Models.Blog.Dtos;

public record UserPasswordDto(string OldPassword, string NewPassword, string ConfirmNewPassword);
