using ElgatoLightControl.Services;
using ElgatoLightControl.Services.Controllers;
using ElgatoLightControl.Services.Factories;
using ElgatoLightControl.ViewModels.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ElgatoLightControl;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) 
        => services
            .AddSingleton<IElgatoDeviceService, ElgatoDeviceService>()
            .AddTransient<AccessoryInfoController>()
            .AddTransient<KeylightController>()
            .AddSingleton<IElgatoDeviceControllerFactory, ElgatoDeviceControllerFactory>();

    public static IServiceCollection AddViewModels(this IServiceCollection services)
        => services
            .AddSingleton<DeviceListViewModel>()
            .AddSingleton<NoDeviceSettingsViewModel>()
            .AddSingleton<KeylightSettingsViewModel>()
            .AddSingleton<MainWindowViewModel>();

}