using Business.Interfaces.Services;
using FluentValidation.Results;

namespace Business.Services;

public class MainService : IMainService
{
    protected ICollection<string> Errors = new List<string>();

    public ICollection<string> GetErrors()
    {
        return Errors;
    }

    public void AddProcessingError(string error)
    {
        Errors.Add(error);
    }

    public void AddProcessingError(ValidationResult validation)
    {
        var errors = validation.Errors;
        foreach (var error in errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
    public void AddProcessingError(List<string> errors)
    {
        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }

    public bool IsOperationValid()
    {
        return !Errors.Any();
    }
}
