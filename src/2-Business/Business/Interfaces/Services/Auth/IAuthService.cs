using Business.Models.Auth.Dto;

namespace Business.Interfaces.Services.Auth;


public interface IAuthService : IMainService
{
    Task<string> RegisterUserAsync(RegisterUserRequest registerUser);
    Task<string> LoginAsync(LoginUserRequest loginUser);
}
