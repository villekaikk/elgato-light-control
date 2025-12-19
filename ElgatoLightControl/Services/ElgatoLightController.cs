using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.DTO;

namespace ElgatoLightControl.Services;

public class ElgatoLightController(IHttpClientFactory clientFactory) : IElgatoLightController
{
    private static readonly string DeviceUrl = "http://{0}:9123/elgato/lights";
    public Task<KeylightSettings?> UpdateDevice(IElgatoDevice device)
    {
        throw new NotImplementedException();
    }

    public async Task<KeylightSettings?> GetDevice(string ipaddress)
    {
        var url = string.Format(DeviceUrl, ipaddress);
        Console.WriteLine(url);
        using var client = clientFactory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Unable to get device settings {url}, {response.StatusCode},  {content}");
                return null;
            }
            
            var dto = JsonSerializer.Deserialize<KeylightSettingsDto>(await response.Content.ReadAsStringAsync());
            return dto?.ToKeylightSettings();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while executing the request: Url: {url} - {ex}");
            throw;
        }
    }
}