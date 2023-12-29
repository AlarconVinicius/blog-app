using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Blog;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
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

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> PutChangeUserPassword(UserPasswordDto userPassword)
    {
        await _service.UpdatePassword(userPassword);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [HttpPost("favorite-recipes/{recipeId}")]
    public async Task<IActionResult> PostFavoriteRecipe(Guid recipeId)
    {
        await _service.FavoriteRecipe(recipeId);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [HttpGet("favorite-recipes")]
    public async Task<IActionResult> GetFavoriteRecipeByUser()
    {
        var result = await _service.GetFavoriteRecipesByUserId();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }
}