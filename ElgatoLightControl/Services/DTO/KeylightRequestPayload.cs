using System.Collections.Generic;
using System.Text.Json.Serialization;
using ElgatoLightControl.Models.Keylight;

namespace ElgatoLightControl.Services.DTO;

public class KeylightRequestPayload
{
    [JsonPropertyName("numberOfLights")]
    public int NumberOfLights => Lights.Count;

    public List<KeylightSettingsDto> Lights { get; set; } = [];
}

public static class KeyLightRequestPayloadExtensions
{
    public static KeylightRequestPayload ToKeyLightRequestPayload(this Keylight keylight)
        => new KeylightRequestPayload()
        {
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