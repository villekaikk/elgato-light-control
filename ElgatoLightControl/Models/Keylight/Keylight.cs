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

public static class KeylightExtensions
{
    public static Keylight ToKeyLight(this IZeroconfHost deviceMetaData, KeylightSettings settings)
        => new(
            deviceMetaData,
            settings,
            null
            );
}