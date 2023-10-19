using System.ComponentModel;

namespace Api.Controllers.Configuration;

public class ApiSuccessResponse<T>
{
    public bool Success { get; set; }
    [DefaultValue(null)]
    public T? Data { get; set; }
}
