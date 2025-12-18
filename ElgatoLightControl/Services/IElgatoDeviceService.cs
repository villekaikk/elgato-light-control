using System.Collections.Generic;
using System.Threading.Tasks;
using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services;

public interface IElgatoDeviceService
{
    public Task<IEnumerable<IElgatoDevice>> ListDevices();
    public Task UpdateDevice(IElgatoDevice elgatoDevice);
}