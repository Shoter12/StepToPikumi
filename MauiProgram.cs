using StepEditor.Services;
namespace StepEditor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

#if IOS
        builder.Services.AddSingleton<IHealthService, HealthService>();
#endif

        return builder.Build();
    }
}
