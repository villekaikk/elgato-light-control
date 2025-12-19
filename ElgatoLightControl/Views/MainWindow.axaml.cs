using Avalonia.ReactiveUI;
using ElgatoLightControl.ViewModels;

namespace ElgatoLightControl.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}