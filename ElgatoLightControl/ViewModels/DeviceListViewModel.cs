using System;
using System.Threading.Tasks;
using ElgatoLightControl.Services;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels;

public class DeviceListViewModel: ViewModelBase
{
    private readonly IElgatoDeviceService _deviceService;
    
    public DeviceListViewModel(IElgatoDeviceService deviceService)
    {
        Console.WriteLine("CCtor");
        _deviceService = deviceService;
    }

    public DeviceListViewModel()
    {
        Console.WriteLine("Wrong ctor");
        _deviceService = new ElgatoDeviceService(null!);
        _ = Task.Run(LoadDevicesAsync);
    }
    
    private async Task LoadDevicesAsync()
    {
        Console.WriteLine("Pressing...");
        var devices = await _deviceService.ListDevices();
        foreach (var device in devices)
        {
            Console.WriteLine($"Device: {device.DeviceType.ToString()}, {device.DisplayName}, {device.IpAddress}");
        }
    }
}