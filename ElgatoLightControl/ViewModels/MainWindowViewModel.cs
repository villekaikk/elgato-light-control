using System;
using System.Threading.Tasks;
using ElgatoLightControl.Services;
using ReactiveUI;

namespace ElgatoLightControl.ViewModels;

public class MainWindowViewModel : ViewModelBase
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