using StepEditor.Services;

namespace StepEditor;

public partial class MainPage : ContentPage
{
    private readonly IHealthService _healthService;

    public MainPage(IHealthService healthService)
    {
        InitializeComponent();

        _healthService = healthService;
    }

    private async void btnAdd_Clicked(
        object sender,
        EventArgs e)
    {

        if (!_healthService.IsAuthorized())
        {

            bool auth =
                await _healthService
                .RequestAuthorizationAsync();


            if (!auth)
            {
                await DisplayAlert(
                    "權限不足",
                    "請到健康App開啟權限",
                    "確定");

                return;
            }
        }



        if (!int.TryParse(
            txtStep.Text,
            out int step))
        {
            await DisplayAlert(
                "錯誤",
                "請輸入數字",
                "確定");

            return;
        }



        bool result =
            await _healthService
            .AddStepAsync(step);



        await DisplayAlert(
            result ? "成功" : "失敗",
            result ?
            "步數已新增" :
            "新增失敗",
            "確定");
    }
    private async void btnAuth_Clicked(
    object sender,
    EventArgs e)
    {
        bool result =
            await _healthService.RequestAuthorizationAsync();


        if (result)
        {
            await DisplayAlert(
                "成功",
                "HealthKit授權完成",
                "確定");
        }
        else
        {
            await DisplayAlert(
                "失敗",
                "使用者拒絕授權",
                "確定");
        }
    }
}