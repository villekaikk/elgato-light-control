using System;
using ElgatoLightControl.Models;
using ElgatoLightControl.Services.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace ElgatoLightControl.Services.Factories;

public class ElgatoDeviceControllerFactory(IServiceProvider serviceProvider) : IElgatoDeviceControllerFactory
{
    public IElgatoDeviceController GetController(ElgatoDeviceType deviceType) => deviceType switch
    {
        ElgatoDeviceType.KeylightAir => serviceProvider.GetService<KeylightController>()!,
        _ => throw new NotImplementedException()
    };
}