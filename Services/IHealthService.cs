namespace StepEditor.Services;

public interface IHealthService
{
    // 檢查是否有權限
    bool IsAuthorized();

    Task<bool> RequestAuthorizationAsync();

    Task<bool> AddStepAsync(int step);
}