using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers.Configuration.Response;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Erros = new List<string>();

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
            Errors = new Dictionary<string, string[]>
        {
            { "Mensagens", Erros.ToArray() }
        }
        };

        return BadRequest(errorResponse);
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            AddProcessingError(erro.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validation)
    {
        var erros = validation.Errors;
        foreach (var erro in erros)
        {
            AddProcessingError(erro.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ICollection<string> mensagens)
    {
        foreach (var erro in mensagens)
        {
            AddProcessingError(erro);
        }

        return CustomResponse();
    }

    protected bool IsOperationValid()
    {
        return !Erros.Any();
    }

    protected void AddProcessingError(string erro)
    {
        Erros.Add(erro);
    }
}
