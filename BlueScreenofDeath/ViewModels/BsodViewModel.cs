using System;
using Avalonia.Input;
using Avalonia.Threading;
using BlueScreenofDeath.Extensions;
using BlueScreenofDeath.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlueScreenofDeath.ViewModels;

public partial class BsodViewModel : ViewModelBase, IDisposable
{
    private readonly IBsodService _bsodService;
        private DispatcherTimer? _progressTimer;
        private bool _disposed;

        [ObservableProperty]
        private string _errorCode;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private string _additionalInfo;

        [ObservableProperty]
        private bool _isVisible;

        [ObservableProperty]
        private double _progressValue;

        [ObservableProperty]
        private bool _isProgressComplete;

        public BsodViewModel(IBsodService bsodService)
        {
            _bsodService = bsodService;
            
            _bsodService.BsodTriggered += OnBsodTriggered;
            _bsodService.BsodClosed += OnBsodClosed;
            
            InitializeProgressTimer();
            LoadInitialData();
        }

        public BsodViewModel() : this(null!)
        { }

        private void OnBsodTriggered(object? sender, BsodEventArgs e)
        {
            Dispatcher.UIThread.Post(() =>
            {
                ErrorCode = e.Data.ErrorCode;
                ErrorMessage = e.Data.ErrorMessage;
                AdditionalInfo = e.Data.AdditionalInfo;
                ShowBsodCommand.Execute(null);
            });
        }

        private void OnBsodClosed(object? sender, EventArgs e)
        {
            Dispatcher.UIThread.Post(() =>
            {
                HideBsod();
            });
        }

        [RelayCommand]
        private void ShowBsod()
        {
            IsVisible = true;
            StartProgress();
        }

        [RelayCommand]
        private void HideBsod()
        {
            IsVisible = false;
            StopProgress();
            LoadInitialData(); // Generate new error for next time
        }

        private void LoadInitialData()
        {
            var errorData = GenerateRandom.GenerateRandomError();
            ErrorCode = errorData.ErrorCode;
            ErrorMessage = errorData.ErrorMessage;
            AdditionalInfo = errorData.AdditionalInfo;
        }

        private void InitializeProgressTimer()
        {
            _progressTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _progressTimer.Tick += (s, e) => UpdateProgress();
        }

        private void StartProgress()
        {
            ProgressValue = 0;
            IsProgressComplete = false;
            _progressTimer?.Start();
        }

        private void StopProgress()
        {
            _progressTimer?.Stop();
        }

        private void UpdateProgress()
        {
            if (ProgressValue < 100)
            {
                ProgressValue += 1;
            }
            else
            {
                IsProgressComplete = true;
                StopProgress();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _bsodService.BsodTriggered -= OnBsodTriggered;
            _bsodService.BsodClosed -= OnBsodClosed;
            
            _progressTimer?.Stop();
            _progressTimer = null;
            
            _disposed = true;
        }

}