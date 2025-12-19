using Avalonia.ReactiveUI;
using ElgatoLightControl.ViewModels.Views;
using ReactiveUI;

namespace ElgatoLightControl.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}