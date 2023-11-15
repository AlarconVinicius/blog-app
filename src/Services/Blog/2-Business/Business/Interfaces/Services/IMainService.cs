namespace Business.Interfaces.Services;

public interface IMainService
{
    ICollection<string> GetErrors();
    void AddProcessingError(string error);
    bool IsOperationValid();
}