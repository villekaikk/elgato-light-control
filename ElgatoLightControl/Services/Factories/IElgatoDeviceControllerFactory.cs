using ElgatoLightControl.Models;
using ElgatoLightControl.Services.Controllers;

namespace ElgatoLightControl.Services.Factories;

public interface IElgatoDeviceControllerFactory
{
    public IElgatoDeviceController GetController(ElgatoDeviceType deviceType);
}