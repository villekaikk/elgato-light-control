using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ElgatoLightControl.ViewModels.Views;

namespace ElgatoLightControl.Views;

public partial class NoDeviceSettingsView : ReactiveUserControl<NoDeviceSettingsViewModel>
{
    public NoDeviceSettingsView()
    {
        InitializeComponent();
    }
}