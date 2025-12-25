using System;
using System.Reactive;
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
    
    public ReactiveCommand<Unit, Unit>? ToggleDevicePowerStateCommand { get; }

    public KeylightSettingsViewModel() : this(null!)
    {
        _device = null!;
        DeviceName = "Keylight Air";
        FirmwareVersion = "1.0.0";
        Brightness = 40;
        Temperature = 1000;
        Task.Run(() => ToggleDevicePowerState(true));
    }

    public KeylightSettingsViewModel(KeylightController ctrl)
    {
        _ctrl = ctrl;
        _device = null!;
        _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(300) };
        _timer.Tick += OnTimerTick;
        
        ToggleDevicePowerStateCommand = ReactiveCommand.CreateFromTask(() => ToggleDevicePowerState());
        ToggleDevicePowerStateCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine($"Exception thrown: {ex}"));
    }

    public async Task DisplayDevice(ElgatoDeviceViewModel? device)
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
            await ToggleDevicePowerState(settings.On);
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
        var newSettings = new KeylightSettings(Brightness, KelvinToBrightness(Temperature), _on);
        var device = _device with { KDeviceSettings = newSettings };
        await _ctrl.UpdateDevice(device);
    }

    private async Task ToggleDevicePowerState(bool? state = null)
    {
        _on = state ?? !_on;
        DevicePowerState = _on ? "On" : "Off";
        _timer.Stop();
        await UpdateLightSettings();
    }

    public string DeviceName
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    public string FirmwareVersion
    {
        get => field;
        set => this.RaiseAndSetIfChanged(ref field, value);
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

    private bool _on;

    public string DevicePowerState
    {
        get => field;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            _timer.Stop();
            Task.Run(UpdateLightSettings);
        }
    } = string.Empty;

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