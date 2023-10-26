using Api.Controllers.Configuration.Response;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Blog;

[Route("api/recipe-blog")]
public class RecipePostController : MainController
{
    private readonly IRecipePostService _service;
    public RecipePostController(IRecipePostService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(Guid id)
    {
        var result = await _service.GetRecipeById(id);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet()]
    public async Task<IActionResult> GetRecipes()
    {
        var result = await _service.GetRecipes();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpPost]
    public async Task<IActionResult> PostRecipe(RecipePost recipe)
    {
        await _service.AddRecipe(recipe);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
}
