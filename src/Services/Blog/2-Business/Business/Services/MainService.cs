using Business.Interfaces.Services;
using Business.Models.Blog;
using Business.Services.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services;

public class MainService
{
    private readonly INotifier _notifier;

    protected MainService(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }

}
