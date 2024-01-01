using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services;
using Business.Interfaces.Services.Auth;
using Business.Models.Auth.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Auth;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : MainController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, INotifier notifier) : base(notifier)
    {
        _authService = authService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Register(RegisterUserRequest registerUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _authService.RegisterUserAsync(registerUser);
        return CustomResponse(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserRequest loginUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _authService.LoginAsync(loginUser);
        return CustomResponse(result);
    }
}