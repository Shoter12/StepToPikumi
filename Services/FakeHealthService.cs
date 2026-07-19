namespace StepEditor.Services;

public class FakeHealthService : IHealthService
{

    public bool IsAuthorized()
    {
        return true;
    }


    public Task<bool> RequestAuthorizationAsync()
    {
        return Task.FromResult(true);
    }


    public Task<bool> AddStepAsync(int step)
    {
        return Task.FromResult(true);
    }
}