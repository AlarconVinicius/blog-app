using Business.Models.Auth.Dto;

namespace Business.Interfaces.Services.Auth;


public interface IAuthService : IMainService
{
    Task<LoginUserResponse> RegisterUserAsync(RegisterUserRequest registerUser);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUser);
}
