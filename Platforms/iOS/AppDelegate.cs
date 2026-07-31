using Foundation;
using UIKit;
using System.Linq;
using StepEditor.Services;

namespace StepEditor
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            // 冷啟動時，檢查有沒有帶著 URL 進來
            if (launchOptions != null &&
                launchOptions[UIApplication.LaunchOptionsUrlKey] is NSUrl url)
            {
                HandleIncomingUrl(url);
            }

            return result;
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            HandleIncomingUrl(url);
            return true;
        }

        private void HandleIncomingUrl(NSUrl url)
        {
            // url 範例: steptopikumi://addstep?count=100
            if (url.Host == "addstep")
            {
                var components = new NSUrlComponents(url, false);
                var items = components.QueryItems;
                var countItem = items?.FirstOrDefault(i => i.Name == "count");
                if (countItem != null && int.TryParse(countItem.Value, out int stepCount))
                {
                    // 透過事件通知 MAUI 端執行
                    UrlSchemeService.RaiseStepReceived(stepCount);
                }
            }
        }
    }
}
