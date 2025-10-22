using Avalonia.Controls;

namespace BlueScreenofDeath.Services;

public interface IWindowService
{
    void ShowBsodWindow();
    void CloseBsodWindow();
    Window? GetMainWindow();
}