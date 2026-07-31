namespace StepEditor.Services;

public static class UrlSchemeService
{
    public static event Action<int>? StepReceived;

    public static void RaiseStepReceived(int count)
    {
        StepReceived?.Invoke(count);
    }
}
