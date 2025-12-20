using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Factories;
using Zeroconf;

namespace ElgatoLightControl.Services;

public class ElgatoDeviceService(IElgatoDeviceControllerFactory ctrlFactory) : IElgatoDeviceService
{
    public async Task<IEnumerable<IElgatoDevice>> ListDevices()
    {
        List<IElgatoDevice> devices = [];
        try
        {
            var results = (await ZeroconfResolver.ResolveAsync("_elg._tcp.local.")).ToList();
            foreach (var device in results)
            {
                var devType = device.DisplayName.ToDeviceType();
                var controller = ctrlFactory.GetController(devType);
                var settings = await controller.GetDevice(device.IPAddress);
                devices.Add(new Keylight(device.IPAddress, device.DisplayName, settings));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occured while searching for devices");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine($"Found {devices.Count()} devices");
        return devices;
    }

    public Task UpdateDevice(IElgatoDevice elgatoDevice)
    {
        throw new NotImplementedException();
    }
}