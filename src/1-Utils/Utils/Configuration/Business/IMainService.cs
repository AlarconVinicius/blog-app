namespace Utils.Configuration.Business;

public interface IMainService
{
    ICollection<string> GetErrors();
    void AddProcessingError(string error);
    bool IsOperationValid();
}