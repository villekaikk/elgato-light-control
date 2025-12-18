namespace ElgatoLightControl.Models;


public record KeylightSettings(int Brightness, int Temperature, bool On)
{
    public static KeylightSettings None => new KeylightSettings(0, 0, false);
}
