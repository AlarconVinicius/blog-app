namespace Business.Configuration;

public interface IMainService
{
    ICollection<string> GetErrors();
    void AddProcessingError(string error);
    bool IsOperationValid();
}