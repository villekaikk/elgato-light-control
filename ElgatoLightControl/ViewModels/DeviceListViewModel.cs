using System;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Services;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels;

public class DeviceListViewModel: ViewModelBase
{
    private readonly IElgatoDeviceService _deviceService;
    
    public DeviceListViewModel()
    {
        Console.WriteLine("Wrong Ctor");
        _deviceService = null!;
    }

    public DeviceListViewModel(IElgatoDeviceService deviceService)
    {
        Console.WriteLine("Ctor");
        _deviceService = deviceService;
        _ = Task.Run(LoadDevicesAsync);
    }
    
    private async Task LoadDevicesAsync()
    {
        Console.WriteLine("Pressing...");
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