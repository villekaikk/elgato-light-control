using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class NoDeviceSettingsViewModel : ReactiveObject, IDeviceSettingsViewModel
{
    public void DisplayDevice(ElgatoDeviceViewModel? device)
    {
        return;
    }
}