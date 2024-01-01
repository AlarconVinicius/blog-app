using Business.Models.Auth.Dto;

namespace Business.Interfaces.Services.Auth;


public interface IAuthService
{
    Task<LoginUserResponse> RegisterUserAsync(RegisterUserRequest registerUser);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUser);
}
