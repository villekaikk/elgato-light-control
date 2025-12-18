using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using Zeroconf;

namespace ElgatoLightControl.Services;

public class ElgatoDeviceService(IElgatoLightController lightController) : IElgatoDeviceService
{
    public async Task<IEnumerable<IElgatoDevice>> ListDevices()
    {
        List<IElgatoDevice> devices = [];
        try
        {
            var results = (await ZeroconfResolver.ResolveAsync("_elg._tcp.local.")).ToList();
            foreach (var device in devices)
            {
                var settings = lightController.GetDevice(device.IpAddress);
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occured while searching for devices");
            Console.WriteLine(ex.Message);
        }

        return devices;
    }

    public Task UpdateDevice(IElgatoDevice elgatoDevice)
    {
        throw new System.NotImplementedException();
    }

    private void ToDevice(IZeroconfHost conf)
    {
        var deviceType = conf.DisplayName.ToDeviceType();
        switch (deviceType)
        {
            case ElgatoDeviceType.KeylightAir:
                break;
            default:
                throw new NotImplementedException();
        }
    }
}