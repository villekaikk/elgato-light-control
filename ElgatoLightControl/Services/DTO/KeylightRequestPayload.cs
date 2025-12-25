using System.Collections.Generic;
using System.Text.Json.Serialization;
using ElgatoLightControl.Models.Keylight;

namespace ElgatoLightControl.Services.DTO;

public class KeylightRequestPayload
{
    [JsonPropertyName("numberOfLights")]
    public int NumberOfLights { get; init; }

    [JsonPropertyName("lights")]
    public List<KeylightSettingsDto> Lights { get; init; }
}

public static class KeyLightRequestPayloadExtensions
{
    public static KeylightRequestPayload ToKeyLightRequestPayload(this Keylight keylight)
        => new()
        {
            NumberOfLights = 1,
            Lights = [
                new KeylightSettingsDto()
                {
                    Brightness = keylight.DeviceSettings.Brightness,
                    Temperature = keylight.DeviceSettings.Temperature,
                    On = keylight.DeviceSettings.On,
                }
            ]
        };
}