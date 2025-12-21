using System.Threading.Tasks;
using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services.Controllers;

public interface IElgatoDeviceController
{
    public Task<ElgatoDeviceSettings> UpdateDevice(ElgatoDevice device);
    public Task<ElgatoDeviceSettings> GetDevice(string ipAddress);
}