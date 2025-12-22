using Zeroconf;

namespace ElgatoLightControl.Models.Keylight;

public record Keylight(
    ElgatoDeviceConfig DeviceConfig,
    KeylightSettings KDeviceSettings,
    AccessoryInfo AccessoryInfo
) : ElgatoDevice(DeviceConfig, KDeviceSettings, AccessoryInfo)
{
    public override ElgatoDeviceType DeviceType => ElgatoDeviceType.KeylightAir;
    public new KeylightSettings DeviceSettings => KDeviceSettings;
}
