using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ElgatoLightControl.ViewModels;

namespace ElgatoLightControl.Views;

public partial class DeviceListView : ReactiveUserControl<DeviceListViewModel>
{
    public DeviceListView()
    {
        InitializeComponent();
    }
}