namespace ElgatoLightControl.Models.Keylight;


public record KeylightSettings(int Brightness, int Temperature, bool On) : ElgatoDeviceSettings
{
    public static KeylightSettings None => new KeylightSettings(0, 0, false);
}
