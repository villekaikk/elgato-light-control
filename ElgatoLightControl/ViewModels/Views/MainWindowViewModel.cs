using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class MainWindowViewModel : ReactiveObject
{
    private DeviceListViewModel _deviceListViewModel;

    public MainWindowViewModel()
    {
        _deviceListViewModel = new DeviceListViewModel();
    }
    
    public MainWindowViewModel(DeviceListViewModel deviceListViewModel)
    {
        _deviceListViewModel = deviceListViewModel;
    }
    
    public DeviceListViewModel DeviceListViewModel
    {
        get => _deviceListViewModel;
        set => this.RaiseAndSetIfChanged(ref _deviceListViewModel, value);
    }
}