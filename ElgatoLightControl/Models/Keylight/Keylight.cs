using System.Runtime.CompilerServices;
using Zeroconf;

namespace ElgatoLightControl.Models.Keylight;

public record Keylight(
    string IpAddress,
    string DisplayName,
    KeylightSettings? Settings
) : IElgatoDevice(IpAddress, DisplayName)
{
    public new ElgatoDeviceType DeviceType => ElgatoDeviceType.KeylightAir;
}

public static class KeylightExtensions
{
    public static Keylight ToKeyLight(this IZeroconfHost deviceMetaData, KeylightSettings? settings)
        => new(
            deviceMetaData.DisplayName,
            deviceMetaData.IPAddress,
            settings
            );
}