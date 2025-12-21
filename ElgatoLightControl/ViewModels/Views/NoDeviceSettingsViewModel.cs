using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;

namespace ElgatoLightControl.ViewModels.Views;

public class NoDeviceSettingsViewModel : IDeviceSettingsViewModel
{
    public async Task DisplayDevice(ElgatoDeviceViewModel? device)
    {
        await Task.Delay(0);
    }
}