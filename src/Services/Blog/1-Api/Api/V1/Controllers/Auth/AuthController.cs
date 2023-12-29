using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services.Auth;
using Business.Models.Auth.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Auth;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : MainController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Register(RegisterUserRequest registerUser)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        var result = await _authService.RegisterUserAsync(registerUser);
        return _authService.IsOperationValid() ? CustomResponse(result) : CustomResponse(_authService.GetErrors());
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserRequest loginUser)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        var result = await _authService.LoginAsync(loginUser);
        return _authService.IsOperationValid() ? CustomResponse(result) : CustomResponse(_authService.GetErrors());
    }
}