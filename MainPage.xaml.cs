using StepEditor.Services;

namespace StepEditor;

public partial class MainPage : ContentPage
{
    private readonly IHealthService _healthService;
    private readonly IUrlLauncher _urlLauncher;
    
    public MainPage(IHealthService healthService)
    {
        InitializeComponent();
        _healthService = healthService;
        _urlLauncher = urlLauncher;
        
        // 訂閱捷徑傳進來的事件
        UrlSchemeService.StepReceived += OnStepReceivedFromShortcut;

        // 檢查冷啟動時是否有暫存未處理的步數
        var pending = UrlSchemeService.ConsumePendingStep();
        if (pending.HasValue)
        {
            _ = AddStepsAsync(pending.Value);
        }
    }

    // 按鈕點擊：讀取畫面上輸入的數字
    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(txtStep.Text, out int step))
        {
            await DisplayAlert("錯誤", "請輸入數字", "確定");
            return;
        }

        await AddStepsAsync(step);
    }

    // 捷徑呼叫進來：直接帶著參數執行，不用等按鈕
    private async void OnStepReceivedFromShortcut(int step)
    {
        await AddStepsAsync(step);
    }

    // 共用邏輯：手動按鈕跟捷徑都會走到這裡
    private async Task AddStepsAsync(int step)
    {
        if (!_healthService.IsAuthorized())
        {
            bool auth = await _healthService.RequestAuthorizationAsync();
            if (!auth)
            {
                await DisplayAlert("權限不足", "請到健康App開啟權限", "確定");
                return;
            }
        }

        bool result = await _healthService.AddStepAsync(step);
        if (result)
        {
            _urlLauncher.OpenUrl("shortcuts://");
        }
       
      //  await DisplayAlert(
      //     result ? "成功" : "失敗",
      //      result ? "步數已新增" : "新增失敗",
      //      "確定");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        UrlSchemeService.StepReceived -= OnStepReceivedFromShortcut;
    }
}
