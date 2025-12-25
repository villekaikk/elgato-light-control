using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class DeviceListViewModel: ReactiveObject
{
    private readonly IElgatoDeviceService _deviceService;
    private bool _initialSetupDone = false;

    public delegate void DeviceSelectedEventHandler(ElgatoDeviceViewModel? device);
    public event DeviceSelectedEventHandler? DeviceSelectedEvent;
    
    public ElgatoDeviceViewModel? SelectedDevice
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            DeviceSelectedEvent?.Invoke(SelectedDevice);
        }
    }

    public string StatusText
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = "Searching devices";

    public bool Loading
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = false;

    public ObservableCollection<ElgatoDeviceViewModel> Devices
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public DeviceListViewModel()
    {
        // Design time Ctor
        _deviceService = null!;
        SelectedDevice = null;
        // StatusText = string.Empty;
        Loading = true;
        Devices =
        [
            new ElgatoDeviceViewModel(
                new ElgatoDeviceConfig("Test device 1", "1.0.0"),
                new KeylightSettings(45, 200, true),
                new AccessoryInfo("Test Keylight 1", "1.0.0", "1234abcd"),
                ElgatoDeviceType.KeylightAir
                ),
            new ElgatoDeviceViewModel(
                new ElgatoDeviceConfig("Test device 2", "1.0.1"),
                new KeylightSettings(77, 150, false),
                new AccessoryInfo("Test Keylight 2", "1.0.1", "1234abcd"),
                ElgatoDeviceType.KeylightAir
                )
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
        try
        {
            StatusText = "Searching devices...";
            Loading = true;
            var devices = await _deviceService.ListDevices();
            var elgatoDevices = devices.ToList();
            if (!elgatoDevices.Any())
                Console.WriteLine("No devices found");

            foreach (var device in elgatoDevices)
            {
                Devices.Add(new ElgatoDeviceViewModel(device));
            }
        }
        finally
        {
            StatusText = string.Empty;
            Loading = false;
            
            if (!_initialSetupDone && Devices.Any())
            {
                SelectedDevice = Devices.First();
                _initialSetupDone = true;
            }
        }
    }
}