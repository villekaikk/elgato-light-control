using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.DTO;

namespace ElgatoLightControl.Services.Controllers;

public class KeylightController(IHttpClientFactory clientFactory) : IElgatoDeviceController
{
    private static readonly string DeviceUrl = "http://{0}:9123/elgato/lights";
    public Task<ElgatoDeviceSettings> UpdateDevice(IElgatoDevice device)
    {
        throw new NotImplementedException();
    }

    public async Task<ElgatoDeviceSettings> GetDevice(string ipAddress)
    {
        var url = string.Format(DeviceUrl, ipAddress);
        using var client = clientFactory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Unable to get device settings {url}, {response.StatusCode}, {content}");
                return KeylightSettings.None;
            }
            
            var dto = JsonSerializer.Deserialize<KeylightSettingsDto>(await response.Content.ReadAsStringAsync());
            var settings = dto?.ToKeylightSettings(); 
            return settings ??  KeylightSettings.None;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while executing the request: Url: {url} - {ex}");
            throw;
        }
    }
}