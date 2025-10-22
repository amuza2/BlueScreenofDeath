using System;
using System.Threading.Tasks;
using BlueScreenofDeath.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlueScreenofDeath.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private readonly IBsodService _bsodService;
    private readonly IWindowService _windowService;
    private bool _disposed = false;

    [ObservableProperty]
    private string _statusMessage = "Ready to prank!";

    [ObservableProperty]
    private bool _isBsodActive;
    
    public MainWindowViewModel(IBsodService bsodService, IWindowService windowService)
    {
        _bsodService = bsodService;
        _windowService = windowService;
            
        _bsodService.BsodTriggered += OnBsodTriggered;
        _bsodService.BsodClosed += OnBsodClosed;
    }

    public MainWindowViewModel() : this(null!, null!)
    {
        
    }

    [RelayCommand]
    private async Task TriggerBsod()
    {
        if (!IsBsodActive)
        {
            StatusMessage = "Preparing BSOD...";
            _windowService.ShowBsodWindow();
            try
            {
                await _bsodService.TriggerBsodAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }
    }

    [RelayCommand]
    private void ResetPrank()
    {
        if (IsBsodActive)
        {
            _bsodService.CloseBsod();
        }
    }

    private void OnBsodTriggered(object? sender, BsodEventArgs e)
    {
        IsBsodActive = true;
        StatusMessage = $"BSOD Active - Error: {e.Data.ErrorCode}";
    }

    private void OnBsodClosed(object? sender, EventArgs e)
    {
        IsBsodActive = false;
        StatusMessage = "Prank completed! Ready for next one.";
    }

    public void Dispose()
    {
        if (_disposed) return;
            
        _bsodService.BsodTriggered -= OnBsodTriggered;
        _bsodService.BsodClosed -= OnBsodClosed;
            
        _disposed = true;
    }
}