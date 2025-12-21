using System;
using ElgatoLightControl.Models;
using ElgatoLightControl.Services.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace ElgatoLightControl.Services.Factories;

public class ElgatoDeviceControllerFactory(IServiceProvider serviceProvider) : IElgatoDeviceControllerFactory
{
    public IElgatoDeviceController GetController(ElgatoDeviceType deviceType)
    {
        IElgatoDeviceController? controller = deviceType switch
        {
            ElgatoDeviceType.KeylightAir => serviceProvider.GetRequiredService<KeylightController>(),
            _ => null
        };

        return controller ?? throw new NullReferenceException($"Unable to resolve controller for device type {deviceType}");;
    }
}