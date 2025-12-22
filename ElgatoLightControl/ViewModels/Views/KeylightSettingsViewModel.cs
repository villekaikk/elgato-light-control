using System;
using System.Threading.Tasks;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Controllers;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class KeylightSettingsViewModel(KeylightController ctrl) : ReactiveObject, IDeviceSettingsViewModel
{
    private Keylight _device;

    public KeylightSettingsViewModel() : this(null!)
    {
        _device = null!;
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

        _device = AsKeylight(device);
        var settings = (KeylightSettings)device.Settings;
        DeviceName = device.DeviceConfig.DisplayName;
        FirmwareVersion = device.AccessoryInfo.FirmwareVersion;
        Brightness = settings.Brightness;
        Temperature = BrightnessToKelvin(settings.Temperature);
        On =  settings.On;
        await Task.FromResult(0);
    }

    private Keylight AsKeylight(ElgatoDeviceViewModel device) 
        => new(device.DeviceConfig, (device.Settings as KeylightSettings)!, null!);

    private int BrightnessToKelvin(int x)
    {
        return (int)(Math.Round(7000.0 + (x - 143.0) * (-4100.0 / 201.0)));
    }
    
    private int KelvinToBrightness(int x)
    {
        return (int)(143.0 + (x - 7000.0) * (201.0 / -4100.0));
    }

    private async Task UpdateLightSettings()
    {
        var newSettings = new KeylightSettings(Brightness, Temperature, On);
        var device = _device with { KDeviceSettings = newSettings };
        await ctrl.UpdateDevice(device);
    }

    public string DeviceName
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Task.Run(UpdateLightSettings);
        }
    } = string.Empty;

    public string FirmwareVersion
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Task.Run(UpdateLightSettings);
        }
    } = string.Empty;

    public int Brightness
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Task.Run(UpdateLightSettings);
        }
    } = 0;

    public int Temperature
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Task.Run(UpdateLightSettings);
        }
    } = 0;

    public bool On
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Task.Run(UpdateLightSettings);
        }
    } = false;

    public int MaxTemp => 7000;
    public int MinTemp => 2900;
}