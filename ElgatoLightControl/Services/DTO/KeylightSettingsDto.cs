using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services.DTO;

public class KeylightSettingsDto
{
    public bool On { get; set; }
    
    public int Brightness { get; set; }
    
    public int Temperature { get; set; }
}

public static class KeylightSettingsDtoExtensions
{
    public static KeylightSettings ToKeylightSettings(this KeylightSettingsDto dto)
        => new(dto.Brightness, dto.Temperature, dto.On);
}