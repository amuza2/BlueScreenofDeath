using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BlueScreenofDeath.Services;
using BlueScreenofDeath.ViewModels;
using BlueScreenofDeath.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BlueScreenofDeath;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IBsodService, BsodService>();
        services.AddSingleton<IWindowService, WindowService>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<BsodViewModel>();
            
        _serviceProvider = services.BuildServiceProvider();
        services.AddSingleton(_serviceProvider);
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}