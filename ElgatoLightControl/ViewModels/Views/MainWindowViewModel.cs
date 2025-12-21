using System;
using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class MainWindowViewModel : ReactiveObject
{
    public MainWindowViewModel()
    {
        DeviceListViewModel = new DeviceListViewModel();
    }

    public MainWindowViewModel(DeviceListViewModel deviceListViewModel)
    {
        DeviceListViewModel = deviceListViewModel;
        DeviceListViewModel.DeviceSelectedEvent += DeviceSelectedCallback;
    }


    public DeviceListViewModel DeviceListViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    private async Task DeviceSelectedCallback(ElgatoDeviceListViewModel? device)
    {
        await Task.Delay(1);
        if (device is null)
            return;
        
        Console.WriteLine($"Device Selected Callback - {device.DisplayName}");
    }
}
