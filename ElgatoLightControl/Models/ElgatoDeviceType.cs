using System;

namespace ElgatoLightControl.Models;

public enum ElgatoDeviceType
{
    KeylightAir,
}

public static class ElgatoDeviceTypeExtensions
{
    public static ElgatoDeviceType ToDeviceType(this string value) => value switch
    {
        "Elgato Key Light Air 3A63" => ElgatoDeviceType.KeylightAir,
        _ => throw new ArgumentException($"{nameof(value)} is not a supported Elgato Device Type"),
    };
}