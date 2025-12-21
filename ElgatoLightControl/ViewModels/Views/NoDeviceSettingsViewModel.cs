using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class NoDeviceSettingsViewModel : ReactiveObject, IDeviceSettingsViewModel
{
    public async Task DisplayDevice(ElgatoDeviceViewModel? device)
    {
        await Task.FromResult(0);
    }
}