using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Controllers;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class KeylightSettingsViewModel : ReactiveObject, IDeviceSettingsViewModel
{
    private Keylight _device;
    private readonly DispatcherTimer _timer;
    private readonly KeylightController _ctrl;
    private bool _deviceInit = false;

    public KeylightSettingsViewModel() : this(null!)
    {
        _device = null!;
        DeviceName = "Keylight Air";
        FirmwareVersion = "1.0.0";
        Brightness = 40;
        Temperature = 1000;
        On = true;
    }

    public KeylightSettingsViewModel(KeylightController ctrl)
    {
        _ctrl = ctrl;
        _device = null!;
        _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(300) };
        _timer.Tick += OnTimerTick;
    }

    public void DisplayDevice(ElgatoDeviceViewModel? device)
    {
        if (device is null)
            return;

        _deviceInit = true;
        try
        {
            _device = AsKeylight(device);
            var settings = (KeylightSettings)device.Settings;
            DeviceName = device.DeviceConfig.DisplayName;
            FirmwareVersion = device.AccessoryInfo.FirmwareVersion;
            Brightness = settings.Brightness;
            Temperature = BrightnessToKelvin(settings.Temperature);
            On = settings.On;
        }
        finally
        {
            _deviceInit = false;
        }
    }

    private static Keylight AsKeylight(ElgatoDeviceViewModel device) 
        => new(device.DeviceConfig, (device.Settings as KeylightSettings)!, device.AccessoryInfo);

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
        await _ctrl.UpdateDevice(device);
    }

    public string DeviceName
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
        }
    } = string.Empty;

    public string FirmwareVersion
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
        }
    } = string.Empty;

    public int Brightness
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            _timer.Stop();
            _timer.Start();
        }
    } = 0;

    public int Temperature
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            _timer.Stop();
            _timer.Start();
        }
    } = 0;

    public bool On
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            _timer.Stop();
            Task.Run(UpdateLightSettings);
        }
    } = false;

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _timer.Stop();
        
        if (_deviceInit)
            return;
        
        Task.Run(UpdateLightSettings);
    }

    public int MaxTemp => 7000;
    public int MinTemp => 2900;
}