using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services;
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

    public UserController(IUserService service, INotifier notifier) : base(notifier)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetAuthenticatedUser()
    {
        var result = await _service.GetAuthenticatedUser();
        return CustomResponse(result);
    }

    [Authorize]
    [HttpPut()]
    public async Task<IActionResult> PutAuthenticatedUser(UserUpdDto user)
    {
        await _service.UpdateAuthenticatedUser(user);
        return CustomResponse();
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> PutChangeUserPassword(UserPasswordDto userPassword)
    {
        await _service.UpdatePassword(userPassword);
        return CustomResponse();
    }

    [HttpPost("favorite-recipes/{recipeId}")]
    public async Task<IActionResult> PostFavoriteRecipe(Guid recipeId)
    {
        await _service.FavoriteRecipe(recipeId);
        return CustomResponse();
    }

    [HttpGet("favorite-recipes")]
    public async Task<IActionResult> GetFavoriteRecipeByUser()
    {
        var result = await _service.GetFavoriteRecipesByUserId();
        return CustomResponse(result);
    }
}