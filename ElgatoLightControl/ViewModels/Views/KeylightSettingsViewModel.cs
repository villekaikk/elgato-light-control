#nullable enable
using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Controllers;
using ElgatoLightControl.ViewModels.Models;
using ElgatoLightControl.ViewModels.Utils;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class KeylightSettingsViewModel : ReactiveObject, IDeviceSettingsViewModel
{
    private Keylight? _device;
    private readonly DispatcherTimer _timer;
    private readonly KeylightController _ctrl;
    private bool _deviceInit;
    
    public ReactiveCommand<Unit, Unit> ToggleDevicePowerStateCommand { get; }

    public KeylightSettingsViewModel() : this(null!)
    {
        _device = null!;
        DeviceName = "Keylight Air";
        FirmwareVersion = "1.0.0";
        Brightness = 40;
        Temperature = 200;
        DevicePowerState = "On";
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

    public async Task DisplayDevice(ElgatoDeviceViewModel device)
    {
        _deviceInit = true;
        try
        {
            _device = AsKeylight(device);
            var settings = (KeylightSettings)device.Settings;
            DeviceName = device.DeviceConfig.DisplayName;
            FirmwareVersion = device.AccessoryInfo.FirmwareVersion;
            Brightness = settings.Brightness;
            Temperature = settings.Temperature;
            await ToggleDevicePowerState(settings.On);
        }
        finally
        {
            _deviceInit = false;
        }
    }

    private static Keylight AsKeylight(ElgatoDeviceViewModel device) 
        => new(device.DeviceConfig, (device.Settings as KeylightSettings)!, device.AccessoryInfo);

    private async Task UpdateLightSettings()
    {
        if (_device is null) return;
        
        var newSettings = new KeylightSettings(Brightness, Temperature, On);
        var device = _device with { KDeviceSettings = newSettings };
        await _ctrl.UpdateDevice(device);
    }

    private async Task ToggleDevicePowerState(bool? state = null)
    {
        On = state ?? !On;
        DevicePowerState = On ? "On" : "Off";
        _timer.Stop();
        await UpdateLightSettings();
    }

    public string DeviceName
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    public string FirmwareVersion
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    public int Brightness
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            _timer.Stop();
            _timer.Start();
        }
    } = 0;

    public int Temperature
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            TempValueKelvin = value;
            _timer.Stop();
            _timer.Start();
        }
    } = 0;
    
    public int MaxTemp => 344;
    public int MinTemp => 134;

    public int TempValueKelvin
    {
        get;
        private set
        {
            value = value.BrightnessToKelvin();
            this.RaiseAndSetIfChanged(ref field, value);
        }
    } = 134;

    public bool On
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string DevicePowerState
    {
        get;
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
}