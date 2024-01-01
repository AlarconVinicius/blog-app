using System.ComponentModel;

namespace Api.Controllers.Configuration.Response;

public class ApiErrorResponse
{
    [DefaultValue(false)]
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public ApiErrorResponse()
    {
        Errors = new List<string>();
    }
}
