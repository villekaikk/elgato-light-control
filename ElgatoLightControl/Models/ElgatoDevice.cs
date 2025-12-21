using Zeroconf;

namespace ElgatoLightControl.Models;

public abstract record ElgatoDevice(IZeroconfHost DeviceConfig, ElgatoDeviceSettings DeviceSettings, AccessoryInfo AccessoryInfo)
{
    public abstract ElgatoDeviceType DeviceType { get; }
}