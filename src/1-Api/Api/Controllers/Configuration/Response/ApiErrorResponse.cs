using System.ComponentModel;

namespace Api.Controllers.Configuration.Response;

public class ApiErrorResponse
{
    [DefaultValue(false)]
    public bool Success { get; set; }
    public IDictionary<string, string[]> Errors { get; set; }
    public ApiErrorResponse()
    {
        Errors = new Dictionary<string, string[]>
        {
            { "Mensagens", new string[0] }
        };
    }
}
