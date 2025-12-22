using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElgatoLightControl.Models;
using ElgatoLightControl.Models.Keylight;
using ElgatoLightControl.Services.Controllers;
using ElgatoLightControl.Services.Factories;
using Zeroconf;

namespace ElgatoLightControl.Services;

public class ElgatoDeviceService(IElgatoDeviceControllerFactory ctrlFactory, AccessoryInfoController aInfoCtrl) : IElgatoDeviceService
{
    public async Task<IEnumerable<ElgatoDevice>> ListDevices()
    {
        List<ElgatoDevice> devices = [];
        try
        {
            var results = (await ZeroconfResolver.ResolveAsync("_elg._tcp.local.")).ToList();
            foreach (var device in results)
            {
                var accInfo = await aInfoCtrl.GetInfo(device.IPAddress);
                var devType = accInfo.ProductName.ToDeviceType();
                var controller = ctrlFactory.GetController(devType);
                var settings = await controller.GetDevice(device.IPAddress);

                var instance = ToDeviceInstance(devType, device, settings, accInfo);
                
                devices.Add(instance);
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

    private static ElgatoDevice ToDeviceInstance(ElgatoDeviceType deviceType, IZeroconfHost deviceConfig,
        ElgatoDeviceSettings settings, AccessoryInfo accessoryInfo)
    {
        switch (deviceType)
        {
            case ElgatoDeviceType.KeylightAir:
                var klSettings = (settings as KeylightSettings)!; 
                return new Keylight(deviceConfig.ToDeviceConfig(), klSettings, accessoryInfo);
            case ElgatoDeviceType.Unknown:
            default:
                throw new NotImplementedException();
        }
    }
}