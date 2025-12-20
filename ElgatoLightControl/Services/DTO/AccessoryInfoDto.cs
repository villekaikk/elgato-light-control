using System.Text.Json.Serialization;
using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services.DTO;

public class AccessoryInfoDto
{
    [JsonPropertyName("productName")]
    public string ProductName { get; init; }
    
    [JsonPropertyName("firmwareVersion")]
    public string FirmwareVersion { get; init; }
    
    [JsonPropertyName("serialNumber")]
    public string SerialNumber { get; init; }
}

public static class AccessoryInfoDtoExtensions
{
    public static AccessoryInfo ToAccessoryInfo(this AccessoryInfoDto dto)
        => new(dto.ProductName, dto.FirmwareVersion, dto.SerialNumber); 
}