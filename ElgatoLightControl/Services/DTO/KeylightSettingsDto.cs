using ElgatoLightControl.Models.Keylight;

namespace ElgatoLightControl.Services.DTO;

public class KeylightSettingsDto
{
    public bool On { get; init; }
    
    public int Brightness { get; init; }
    
    public int Temperature { get; init; }
}

public static class KeylightSettingsDtoExtensions
{
    public static KeylightSettings ToKeylightSettings(this KeylightSettingsDto dto)
        => new(dto.Brightness, dto.Temperature, dto.On);
}