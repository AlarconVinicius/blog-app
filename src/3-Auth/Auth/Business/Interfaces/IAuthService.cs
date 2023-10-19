using Auth.Models;
using Utils.Configuration.Business;

namespace Auth.Business.Interfaces;


public interface IAuthService : IMainService
{
    Task<string> RegisterUserAsync(RegisterUserRequest registerUser);
    Task<string> LoginAsync(LoginUserRequest loginUser);
}
