namespace StepEditor.Services;

public static class UrlSchemeService
{
    public static event Action<int>? StepReceived;

    // 暫存尚未被處理的步數（因為冷啟動時 MainPage 可能還沒訂閱）
    public static int? PendingStep { get; private set; }

    public static void RaiseStepReceived(int count)
    {
        if (StepReceived != null)
        {
            // 有人在監聽，直接觸發
            StepReceived.Invoke(count);
        }
        else
        {
            // 還沒人監聽（例如冷啟動中），先存起來
            PendingStep = count;
        }
    }

    public static int? ConsumePendingStep()
    {
        var value = PendingStep;
        PendingStep = null;
        return value;
    }
}
