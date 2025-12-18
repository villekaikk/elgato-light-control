using System;
using System.Threading.Tasks;
using ElgatoLightControl.Services;

namespace ElgatoLightControl.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IElgatoDeviceService _deviceService;

    public MainWindowViewModel()
    {
        Console.WriteLine("Wrong Ctor");
        var lightController = new ElgatoLightController(null!);
        _deviceService = new ElgatoDeviceService(lightController);
    }
    
    public MainWindowViewModel(IElgatoDeviceService deviceService)
    {
        Console.WriteLine("Ctor");
        _deviceService = deviceService;
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