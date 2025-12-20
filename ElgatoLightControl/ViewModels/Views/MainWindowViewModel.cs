using ReactiveUI;

namespace ElgatoLightControl.ViewModels.Views;

public class MainWindowViewModel : ReactiveObject
{
    public MainWindowViewModel()
    {
        DeviceListViewModel = new DeviceListViewModel();
    }

    public MainWindowViewModel(DeviceListViewModel deviceListViewModel)
    {
        DeviceListViewModel = deviceListViewModel;
    }

    public DeviceListViewModel DeviceListViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
}