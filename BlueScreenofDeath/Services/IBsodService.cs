using System;
using System.Threading.Tasks;

namespace BlueScreenofDeath.Services;

public interface IBsodService
{
    event EventHandler<BsodEventArgs> BsodTriggered;
    event EventHandler BsodClosed;
        
    Task TriggerBsodAsync();
    void CloseBsod();
}