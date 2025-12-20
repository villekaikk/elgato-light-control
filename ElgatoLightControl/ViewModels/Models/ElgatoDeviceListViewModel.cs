using ElgatoLightControl.Models;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Models;

public class ElgatoDeviceListViewModel : ReactiveObject
{
    public string DisplayName
    {
        get => _displayName;
        set => this.RaiseAndSetIfChanged(ref _displayName, value);
    }

    public string FirmwareVersion
    {
        get => _firmwareVersion;
        set => this.RaiseAndSetIfChanged(ref _firmwareVersion, value);
    }

    public IElgatoDeviceSettings Settings
    {
        get => _settings;
        set => this.RaiseAndSetIfChanged(ref _settings, value);
    }
    
    public ElgatoDeviceListViewModel(string displayName, string firmwareVersion, IElgatoDeviceSettings settings, ElgatoDeviceType deviceType)
    {
        DisplayName = displayName;
        FirmwareVersion = firmwareVersion;
        Settings = settings;
        _deviceType = deviceType;
    }

    private string _displayName = string.Empty;
    private string _firmwareVersion = string.Empty;
    private IElgatoDeviceSettings _settings = null!;
    private ElgatoDeviceType _deviceType;
}