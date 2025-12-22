using Zeroconf;

namespace ElgatoLightControl.Models;

public abstract record ElgatoDevice(ElgatoDeviceConfig DeviceConfig, ElgatoDeviceSettings DeviceSettings, AccessoryInfo AccessoryInfo)
{
    public abstract ElgatoDeviceType DeviceType { get; }
}