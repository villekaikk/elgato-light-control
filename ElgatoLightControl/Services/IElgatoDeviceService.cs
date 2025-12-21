using System.Collections.Generic;
using System.Threading.Tasks;
using ElgatoLightControl.Models;

namespace ElgatoLightControl.Services;

public interface IElgatoDeviceService
{
    public Task<IEnumerable<ElgatoDevice>> ListDevices();
    public Task UpdateDevice(ElgatoDevice elgatoDevice);
}