using System.Threading.Tasks;
using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services.Controllers;

public interface IElgatoDeviceController
{
    public Task<KeylightSettings?> UpdateDevice(IElgatoDevice device);
    public Task<KeylightSettings?> GetDevice(string ipaddress);
}