using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Services;
using ElgatoLightControl.Services.Factories;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class DeviceListViewModel: ReactiveObject
{
    private readonly IElgatoDeviceService _deviceService;

    public ElgatoDeviceListViewModel? SelectedDevice
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }


    public ObservableCollection<ElgatoDeviceListViewModel> Devices
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public DeviceListViewModel()
    {
        _deviceService = null!;
        SelectedDevice = null;
        Devices =
        [
            new ElgatoDeviceListViewModel("Test device 1", "1.0.0", KeylightSettings.None, ElgatoDeviceType.Unknown),
            new ElgatoDeviceListViewModel("Test device 2", "1.0.1", KeylightSettings.None, ElgatoDeviceType.Unknown)
        ];
    }

    public DeviceListViewModel(IElgatoDeviceService deviceService)
    {
        _deviceService = deviceService;
        SelectedDevice = null;
        Devices = [];
        _ = Task.Run(LoadDevicesAsync);
        
    }
    
    private async Task LoadDevicesAsync()
    {
        var devices = await _deviceService.ListDevices();
        var elgatoDevices = devices.ToList();
        if (!elgatoDevices.Any())
            Console.WriteLine("No devices found");
        
        foreach (var device in elgatoDevices)
        {
            Console.WriteLine($"Device: {device.DeviceType.ToString()}, {device.DisplayName}, {device.IpAddress}");
        }
    }
}