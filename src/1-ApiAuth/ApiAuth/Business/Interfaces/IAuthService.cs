using ApiAuth.Models;
using Utils.Configuration.Business;

namespace ApiAuth.Business.Interfaces;


public interface IAuthService : IMainService
{
    Task<string> RegisterUserAsync(RegisterUserRequest registerUser);
    Task<string> LoginAsync(LoginUserRequest loginUser);
}
