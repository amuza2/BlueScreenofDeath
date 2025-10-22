using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BlueScreenofDeath.ViewModels;
using BlueScreenofDeath.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BlueScreenofDeath.Services;

public class WindowService : IWindowService
{
    private BsodWindow? _bsodWindow;
    private readonly IServiceProvider _serviceProvider;

    public WindowService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowBsodWindow()
    {
        if (_bsodWindow == null)
        {
            _bsodWindow = new BsodWindow();
            var viewModel = _serviceProvider.GetRequiredService<BsodViewModel>();
            _bsodWindow.DataContext = viewModel;
                
            _bsodWindow.Closed += (s, e) => 
            {
                _bsodWindow = null;
            };
        }
            
        _bsodWindow.Show();
        _bsodWindow.Activate();
    }

    public void CloseBsodWindow()
    {
        _bsodWindow?.Close();
        _bsodWindow = null;
    }

    public Window? GetMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow;
        }
        return null;
    }
}