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
#else
        builder.Services.AddSingleton<IHealthService, FakeHealthService>();
#endif

        return builder.Build();
    }
}