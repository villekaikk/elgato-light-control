using System;

namespace ElgatoLightControl.ViewModels.Utils;

public static class LightTemperatureConverter
{
    public static int BrightnessToKelvin(this int x)
    {
        return (int)(Math.Round(7000.0 + (x - 143.0) * (-4100.0 / 201.0)));
    }
    
    public static int KelvinToBrightness(this int x)
    {
        return (int)(143.0 + (x - 7000.0) * (201.0 / -4100.0));
    }
}