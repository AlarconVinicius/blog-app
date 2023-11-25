using Api.Controllers.Configuration.Response;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Blog;

[Route("api/users")]
public class UserController : MainController
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetAuthenticatedUser()
    {
        var result = await _service.GetAuthenticatedUser();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [HttpPut()]
    public async Task<IActionResult> PutAuthenticatedUser(UserUpdDto user)
    {
        await _service.UpdateAuthenticatedUser(user);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
}