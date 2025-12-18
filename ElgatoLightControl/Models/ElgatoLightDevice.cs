using System.Runtime.CompilerServices;
using Zeroconf;

namespace ElgatoLightControl.Models;

public record ElgatoKeylightDevice(
    string IpAddress,
    string DisplayName,
    KeylightSettings? Settings
) : IElgatoDevice(IpAddress, DisplayName)
{
    public ElgatoDeviceType DeviceType => ElgatoDeviceType.KeylightAir;
}

public static class ElgatoKeylightExtensions
{
    public static ElgatoKeylightDevice ToKeyLight(this IZeroconfHost deviceMetaData, KeylightSettings? settings)
        => new(
            deviceMetaData.DisplayName,
            deviceMetaData.IPAddress,
            settings
            );
}