namespace ElgatoLightControl.Models;

public abstract record IElgatoDevice(string IpAddress, string DisplayName)
{
    public ElgatoDeviceType DeviceType { get; }
}