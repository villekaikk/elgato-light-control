using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Services;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class DeviceListViewModel: ReactiveObject
{
    private readonly IElgatoDeviceService _deviceService;
    
    private ObservableCollection<IElgatoDeviceViewModel> _devices = [];

    public ObservableCollection<IElgatoDeviceViewModel> Devices
    {
        get => _devices;
        set => this.RaiseAndSetIfChanged(ref _devices, value);
    }

    public DeviceListViewModel()
    {
        _deviceService = null!;
    }

    public DeviceListViewModel(IElgatoDeviceService deviceService)
    {
        _deviceService = deviceService;
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