using System;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class DeviceSettingsViewModel : ReactiveObject
{
    public DeviceSettingsViewModel()
    {
        
    }

    public void DisplayDevice(ElgatoDeviceViewModel device)
    {
        Console.WriteLine($"Device Selected Callback - {device.DisplayName}");
    }
}