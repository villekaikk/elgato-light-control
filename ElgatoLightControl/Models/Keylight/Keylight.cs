using Zeroconf;

namespace ElgatoLightControl.Models.Keylight;

public record Keylight(
    IZeroconfHost DeviceConfig,
    KeylightSettings KDeviceSettings,
    AccessoryInfo AccessoryInfo
) : IElgatoDevice(DeviceConfig, KDeviceSettings, AccessoryInfo)
{
    public override ElgatoDeviceType DeviceType => ElgatoDeviceType.KeylightAir;
    public new KeylightSettings DeviceSettings => KDeviceSettings;
}
