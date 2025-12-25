using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Services.DTO;

namespace ElgatoLightControl.Services.Controllers;

public class AccessoryInfoController(IHttpClientFactory clientFactory)
{
    private static readonly string DeviceUrl = "http://{0}:9123/elgato/accessory-info";
    public async Task<AccessoryInfo> GetInfo(string ipAddress)
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
                Console.WriteLine($"Unable to get accessory info {url}, {response.StatusCode}, {content}");
                throw new Exception("Unable to get accessory info");
            }

            var stringContent = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<AccessoryInfoDto>(stringContent);
            var accInfo = dto?.ToAccessoryInfo();
            return accInfo ?? throw new  Exception("Unable to convert to accessory info");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while executing the request: Url: {url} - {ex}");
            throw;
        }
    }
}