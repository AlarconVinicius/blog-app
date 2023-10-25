using Business.Auth.Models;
using Business.Configuration;

namespace Business.Auth.Interfaces;


public interface IAuthService : IMainService
{
    Task<string> RegisterUserAsync(RegisterUserRequest registerUser);
    Task<string> LoginAsync(LoginUserRequest loginUser);
}
