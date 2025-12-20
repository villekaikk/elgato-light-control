using ElgatoLightControl.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Models;

public class ElgatoDeviceListViewModel : ReactiveObject
{
    public string DisplayName
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string FirmwareVersion
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public IElgatoDeviceSettings Settings
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ElgatoDeviceType DeviceType { get; set; }

    public ElgatoDeviceListViewModel(string displayName, string firmwareVersion, IElgatoDeviceSettings settings, ElgatoDeviceType deviceType)
    {
        DisplayName = displayName;
        FirmwareVersion = firmwareVersion;
        Settings = settings;
        DeviceType = deviceType;
    }
}