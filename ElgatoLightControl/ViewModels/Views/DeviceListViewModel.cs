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
    
    private ElgatoDeviceListViewModel? _selectedDevice;

    public ElgatoDeviceListViewModel? SelectedDevice
    {
        get => _selectedDevice;
        set => this.RaiseAndSetIfChanged(ref _selectedDevice, value);
    }
    
    private ObservableCollection<ElgatoDeviceListViewModel> _devices = [];

    public ObservableCollection<ElgatoDeviceListViewModel> Devices
    {
        get => _devices;
        set => this.RaiseAndSetIfChanged(ref _devices, value);
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
        Devices = new ObservableCollection<ElgatoDeviceListViewModel>();
        _ = Task.Run(LoadDevicesAsync);
        
        _deviceService = null!;
        SelectedDevice = null;
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