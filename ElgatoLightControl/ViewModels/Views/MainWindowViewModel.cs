using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class MainWindowViewModel(DeviceListViewModel deviceListViewModel) : ReactiveObject
{
    private DeviceListViewModel _deviceListViewModel = deviceListViewModel;

    public MainWindowViewModel() : this(new DeviceListViewModel()) { }

    public DeviceListViewModel DeviceListViewModel
    {
        get => _deviceListViewModel;
        set => this.RaiseAndSetIfChanged(ref _deviceListViewModel, value);
    }
}