using Foundation;
using UIKit;
using StepEditor.Services;

namespace StepEditor;

public class UrlLauncher : IUrlLauncher
{
    public void OpenUrl(string url)
    {
        var nsUrl = new NSUrl(url);
        UIApplication.SharedApplication.OpenUrl(nsUrl, new UIApplicationOpenUrlOptions(), null);
    }
}
