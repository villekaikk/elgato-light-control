using Zeroconf;

namespace ElgatoLightControl.Models;

public record ElgatoDeviceConfig(string DisplayName, string IpAddress);

public static class ElgatoDeviceConfigExtensions
{
    public static ElgatoDeviceConfig ToDeviceConfig(this IZeroconfHost zeroConfHost)
        => new ElgatoDeviceConfig(zeroConfHost.DisplayName, zeroConfHost.IPAddress);
}