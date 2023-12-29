using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Blog;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
public class CategoryController : MainController
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var result = await _service.GetCategoryById(id);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllCategoryCategories()
    {
        var result = await _service.GetAllCategories();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpPost("/api/admin/categories")]
    public async Task<IActionResult> PostCategory(Category category)
    {
        await _service.AddCategory(category);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [HttpPut("/api/admin/categories/{id}")]
    public async Task<IActionResult> PutCategory(Guid id, Category category)
    {
        await _service.UpdateCategory(id, category);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [HttpDelete("/api/admin/categories/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _service.DeleteCategory(id);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
}
