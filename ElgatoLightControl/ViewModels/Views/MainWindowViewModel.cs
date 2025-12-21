using System;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.ViewModels.Models;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class MainWindowViewModel : ReactiveObject
{
    private readonly IServiceProvider _serviceProvider;
    public MainWindowViewModel()
    {
        _serviceProvider = null!;
        DeviceSettingsViewModel = new NoDeviceSettingsViewModel();
        DeviceListViewModel = new DeviceListViewModel();
    }

    public MainWindowViewModel(IServiceProvider serviceProvider, DeviceListViewModel deviceListViewModel)
    {
        _serviceProvider = serviceProvider;
        DeviceListViewModel = deviceListViewModel;
        DeviceListViewModel.DeviceSelectedEvent += DeviceSelectedCallback;
        DeviceSettingsViewModel = _serviceProvider.GetService<NoDeviceSettingsViewModel>()!;
    }

    public DeviceListViewModel DeviceListViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    public IDeviceSettingsViewModel DeviceSettingsViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    private async Task DeviceSelectedCallback(ElgatoDeviceViewModel? device)
    {
        SetDeviceSettingsViewModel(device?.DeviceType);
        await DeviceSettingsViewModel.DisplayDevice(device);
    }

    private void SetDeviceSettingsViewModel(ElgatoDeviceType? deviceType)
    {
        IDeviceSettingsViewModel? selectedViewModel = deviceType switch
        {
            ElgatoDeviceType.KeylightAir => _serviceProvider.GetRequiredService<KeylightSettingsViewModel>(),
            _ => null
        };

        DeviceSettingsViewModel = selectedViewModel ?? _serviceProvider.GetRequiredService<NoDeviceSettingsViewModel>();
    }
}
