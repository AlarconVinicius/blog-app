using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers.Configuration.Response;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Erros = new List<string>();

    protected ActionResult CustomResponse(object result = null!)
    {
        if (OperacaoValida())
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
            AdicionarErroProcessamento(erro.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ICollection<string> mensagens)
    {
        foreach (var erro in mensagens)
        {
            AdicionarErroProcessamento(erro);
        }

        return CustomResponse();
    }

    protected bool OperacaoValida()
    {
        return !Erros.Any();
    }

    protected void AdicionarErroProcessamento(string erro)
    {
        Erros.Add(erro);
    }
}
