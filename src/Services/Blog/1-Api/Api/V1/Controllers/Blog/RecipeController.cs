using Api.Configuration.Filter;
using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Blog;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/recipes")]
public class RecipeController : MainController
{
    private readonly IRecipePostService _service;
    public RecipeController(IRecipePostService service, INotifier notifier) : base(notifier)
    {
        _service = service;
    }

    #region Public Methods
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(Guid id, [FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipeById(id, userId);
        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpGet("url/{url}")]
    public async Task<IActionResult> GetRecipeByUrl(string url, [FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipeByUrl(url, userId);
        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpGet("search/{search}")]
    public async Task<IActionResult> GetRecipesBySearch([FromRoute] string search, [FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipesBySearch(search, userId);
        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetRecipesByCategory([FromRoute] string category, [FromQuery] Guid? userId)
    {
        Console.WriteLine(category);
        var result = await _service.GetRecipesByCategory(category, userId);
        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpGet()]
    public async Task<IActionResult> GetRecipes([FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipes(userId);
        return CustomResponse(result);
    }
    #endregion

    #region Authenticated Methods
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpPost()]
    public async Task<IActionResult> PostRecipe(RecipePostAddDto recipe)
    {
        await _service.AddRecipe(recipe);
        return CustomResponse();
    }

    [ClaimsAuthorize("Permission", "Writer")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRecipe(Guid id, RecipePostAddDto recipe)
    {
        await _service.UpdateRecipe(id, recipe);
        return CustomResponse();
    }

    [ClaimsAuthorize("Permission", "Writer")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        await _service.DeleteRecipe(id);
        return CustomResponse();
    }
    #endregion
}
