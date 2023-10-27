using Api.Controllers.Configuration.Response;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Blog;

[Route("api/blog")]
public class BlogController : MainController
{
    private readonly IBlogService _service;
    public BlogController(IBlogService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogById(Guid id)
    {
        var result = await _service.GetBlogById(id);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllBlogs()
    {
        var result = await _service.GetAllBlogs();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpPost]
    public async Task<IActionResult> PostBlog(BlogEntity blog)
    {
        await _service.AddBlog(blog);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(Guid id, BlogEntity blog)
    {
        await _service.UpdateBlog(id, blog);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        await _service.DeleteBlog(id);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
}
