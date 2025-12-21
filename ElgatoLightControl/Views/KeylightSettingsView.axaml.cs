using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ElgatoLightControl.ViewModels.Views;
using ReactiveUI;

namespace ElgatoLightControl.Views;

public partial class KeylightSettingsView : ReactiveUserControl<KeylightSettingsViewModel>
{
    public KeylightSettingsView()
    {
        InitializeComponent();
    }
}