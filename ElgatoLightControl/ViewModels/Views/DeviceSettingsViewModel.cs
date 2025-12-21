using System;
using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class DeviceSettingsViewModel : ReactiveObject
{
    public DeviceSettingsViewModel()
    {
        
    }

    public async Task DisplayDevice(ElgatoDeviceViewModel? device)
    {
        await Task.Delay(1);
        if (device is null)
            return;
        
        Console.WriteLine($"Device Selected Callback - {device.DisplayName}");
    }
}