using Business.Interfaces.Services;
using Business.Services.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers.Configuration.Response;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    protected ICollection<string> Erros = new List<string>();

    protected MainController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyInvalidModelError(modelState);
        return CustomResponse();
    }
    protected ActionResult CustomResponse(object result = null!)
    {
        if (IsOperationValid())
        {
            var response = new ApiSuccessResponse<object>
            {
                Success = true,
                Data = result
            };

            return Ok(response);
        }
        var errorResponse = new ApiErrorResponse
        {
            Success = false,
            Errors = _notifier.GetNotifications().Select(n => n.Message)
        };

        return BadRequest(errorResponse);
    }
    protected void NotifyInvalidModelError(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
    
    protected bool IsOperationValid()
    {
        return !_notifier.HasNotification();
    }
}
