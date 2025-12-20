using System;

namespace ElgatoLightControl.Models;

public enum ElgatoDeviceType
{
    Unknown,
    KeylightAir,
}

public static class ElgatoDeviceTypeExtensions
{
    public static ElgatoDeviceType ToDeviceType(this string value) => value switch
    {
        "Elgato Key Light Air" => ElgatoDeviceType.KeylightAir,
        _ => ElgatoDeviceType.Unknown,
    };
}