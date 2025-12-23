using System;
using System.Threading.Tasks;
using ElgatoLightControl.ViewModels.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public interface IDeviceSettingsViewModel
{
    public void DisplayDevice(ElgatoDeviceViewModel? device);
}