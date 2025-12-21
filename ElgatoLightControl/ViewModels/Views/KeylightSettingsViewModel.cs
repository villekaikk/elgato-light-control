using System;
using System.Threading.Tasks;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Controllers;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class KeylightSettingsViewModel : ReactiveObject, IDeviceSettingsViewModel
{
    // private readonly KeylightController _ctrl = ctrl;

    public KeylightSettingsViewModel()
    {
        DeviceName = "Keylight Air";
        FirmwareVersion = "1.0.0";
        Brightness = 40;
        Temperature = 1000;
        On = true;
    }

    public async Task DisplayDevice(ElgatoDeviceViewModel? device)
    {
        if (device is null)
            return;
        
        var settings = (KeylightSettings)device.Settings;
        DeviceName = device.DisplayName;
        FirmwareVersion = device.FirmwareVersion;
        Brightness = settings.Brightness;
        Temperature = settings.Temperature;
        On =  settings.On;
        await Task.FromResult(0);
    }

    public string DeviceName
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    public string FirmwareVersion
    {
        get => field;
        set =>  this.RaiseAndSetIfChanged(ref field, value);
    }

    public int Brightness
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = 0;

    public int Temperature
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = 0;

    public bool On
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = false;

    public int MaxTemp => 7000;
    public int MinTemp => 2900;
}