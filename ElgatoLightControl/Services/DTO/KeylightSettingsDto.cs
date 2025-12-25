using System.Text.Json.Serialization;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Utils;

namespace ElgatoLightControl.Services.DTO;

public class KeylightSettingsDto
{
    [JsonPropertyName("on"), JsonConverter(typeof(BoolConverter))]
    public bool On { get; init; }
    
    [JsonPropertyName("brightness")]
    public int Brightness { get; init; }
    
    [JsonPropertyName("temperature")]
    public int Temperature { get; init; }
}

public static class KeylightSettingsDtoExtensions
{
    public static KeylightSettings ToKeylightSettings(this KeylightSettingsDto dto)
        => new(dto.Brightness, dto.Temperature, dto.On);
}