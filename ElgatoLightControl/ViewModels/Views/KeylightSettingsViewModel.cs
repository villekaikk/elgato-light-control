using System;
using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class KeylightSettingsViewModel :  ReactiveObject, IDeviceSettingsViewModel
{
    public KeylightSettingsViewModel()
    {
        
    }

    public async Task DisplayDevice(ElgatoDeviceViewModel? device)
    {
        await Task.Delay(1);
        if (device is null)
            return;
        
        Console.WriteLine($"Keylight Device Selected Callback - {device.DisplayName}");
    }
}