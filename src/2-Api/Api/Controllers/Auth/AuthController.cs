using Api.Controllers.Configuration.Response;
using Auth.Business.Interfaces;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Auth;

[Route("api/auth")]
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