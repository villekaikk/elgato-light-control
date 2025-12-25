using ElgatoLightControl.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Models;

public class ElgatoDeviceViewModel : ReactiveObject
{
    public ElgatoDeviceSettings Settings
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ElgatoDeviceConfig DeviceConfig
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public AccessoryInfo AccessoryInfo
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ElgatoDeviceType DeviceType { get; set; }

    public ElgatoDeviceViewModel(ElgatoDeviceConfig config, ElgatoDeviceSettings settings, AccessoryInfo accessoryInfo, ElgatoDeviceType deviceType)
    {
        DeviceConfig = config;
        Settings = settings;
        DeviceType = deviceType;
        AccessoryInfo = accessoryInfo;
    }

    public ElgatoDeviceViewModel(ElgatoDevice device)
    {
        DeviceConfig = device.DeviceConfig;
        Settings = device.DeviceSettings;
        AccessoryInfo = device.AccessoryInfo;
        DeviceType = device.DeviceType;
    }
}