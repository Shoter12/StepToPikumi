#if IOS

using HealthKit;
using Foundation;
using StepEditor.Services;

namespace StepEditor;

public class HealthService : IHealthService
{
    private readonly HKHealthStore _healthStore;


    public HealthService()
    {
        _healthStore = new HKHealthStore();
    }



    // 取得HealthKit授權
    public Task<bool> RequestAuthorizationAsync()
    {
        var tcs = new TaskCompletionSource<bool>();


        var stepType =
            HKQuantityType.GetQuantityType(
                HKQuantityTypeIdentifier.StepCount);



        NSSet writeTypes = new NSSet(stepType);



        NSSet readTypes = new NSSet(stepType);



        _healthStore.RequestAuthorizationToShare(
            writeTypes,
            readTypes,
            (success, error) =>
            {
                tcs.SetResult(success);
            });



        return tcs.Task;
    }




    // 寫入步數
    public Task<bool> AddStepAsync(int step)
    {
        var tcs = new TaskCompletionSource<bool>();


        var stepType =
            HKQuantityType.GetQuantityType(
                HKQuantityTypeIdentifier.StepCount);



        var quantity =
            HKQuantity.FromQuantity(
                HKUnit.Count,
                step);



        var sample =
            HKQuantitySample.Create(
                stepType,
                quantity,
                NSDate.Now,
                NSDate.Now,
                null);



        _healthStore.SaveObject(
            sample,
            (success, error) =>
            {
                tcs.SetResult(success);
            });


        return tcs.Task;
    }
    public bool IsAuthorized()
    {
        var stepType =
            HKQuantityType.GetQuantityType(
                HKQuantityTypeIdentifier.StepCount);


        var status =
            _healthStore.GetAuthorizationStatus(stepType);


        return status == HKAuthorizationStatus.SharingAuthorized;
    }
}

#endif