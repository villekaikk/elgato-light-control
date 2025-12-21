using System;
using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ElgatoLightControl.Views;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class MainWindowViewModel : ReactiveObject
{
    public MainWindowViewModel()
    {
        DeviceSettingsViewModel = new DeviceSettingsViewModel();
        DeviceListViewModel = new DeviceListViewModel();
    }

    public MainWindowViewModel(DeviceListViewModel deviceListViewModel, DeviceSettingsViewModel deviceSettingsViewModel)
    {
        DeviceSettingsViewModel = deviceSettingsViewModel;
        DeviceListViewModel = deviceListViewModel;
        DeviceListViewModel.DeviceSelectedEvent += DeviceSelectedCallback;
    }


    public DeviceListViewModel DeviceListViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    public DeviceSettingsViewModel DeviceSettingsViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    private async Task DeviceSelectedCallback(ElgatoDeviceViewModel? device)
    {
        await Task.Delay(1);
        if (device is null)
            return;
        
        DeviceSettingsViewModel.DisplayDevice(device);
    }
}
