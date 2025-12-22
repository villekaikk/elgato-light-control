using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.DTO;

namespace ElgatoLightControl.Services.Controllers;

public class KeylightController(IHttpClientFactory clientFactory) : IElgatoDeviceController
{
    private static readonly string DeviceUrl = "http://{0}:9123/elgato/lights";
    public async Task UpdateDevice(ElgatoDevice device)
    {
        if (device is not Keylight keylight)
            return;
        
        var url = string.Format(DeviceUrl, keylight.DeviceConfig.IpAddress);
        using var client = clientFactory.CreateClient();
        var data = keylight.ToKeyLightRequestPayload();
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        using var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Content = content;
        try
        {
            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Unable to update device settings {url}, {response.StatusCode}, {content}");
                return;
            }

            Console.WriteLine("Updated device settings");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while updating device settings: Url: {url} - {ex}");
            throw;
        }
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
            Console.WriteLine($"An error occured while fetching device settings: Url: {url} - {ex}");
            throw;
        }
    }
}