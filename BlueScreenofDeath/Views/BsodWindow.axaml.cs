using System;
using Avalonia.Controls;
using Avalonia.Input;
using BlueScreenofDeath.ViewModels;

namespace BlueScreenofDeath.Views;

public partial class BsodWindow : Window
{
    public BsodWindow()
    {
        InitializeComponent();
        Focusable = true;
        KeyDown += OnKeyDown;
    }
    
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (DataContext is BsodViewModel viewModel && viewModel.IsVisible)
        {
            if ((e.Key == Key.Q && e.KeyModifiers.HasFlag(KeyModifiers.Control) && e.KeyModifiers.HasFlag(KeyModifiers.Shift)) ||
                 (e.Key == Key.W && e.KeyModifiers.HasFlag(KeyModifiers.Control) && e.KeyModifiers.HasFlag(KeyModifiers.Alt)))
            {
                viewModel.HideBsodCommand.Execute(null);
                e.Handled = true;
                Close();
            }
        }
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        Focus(); // Ensure window has focus for key events
    }
}