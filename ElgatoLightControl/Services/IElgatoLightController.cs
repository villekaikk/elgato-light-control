using System.Threading.Tasks;
using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services;

public interface IElgatoLightController
{
    public Task<KeylightSettings?> UpdateDevice(IElgatoDevice device);
    public Task<KeylightSettings?> GetDevice(string ipaddress);
}