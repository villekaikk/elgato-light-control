using Avalonia.ReactiveUI;
using ElgatoLightControl.ViewModels;
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