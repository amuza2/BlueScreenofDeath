using System;
using System.Threading.Tasks;
using BlueScreenofDeath.Extensions;

namespace BlueScreenofDeath.Services;

public class BsodService : IBsodService
{
    public event EventHandler<BsodEventArgs> BsodTriggered;
    public event EventHandler BsodClosed;

    

    public async Task TriggerBsodAsync()
    {
        // Add realistic delay
        await Task.Delay(1000);
            
        var bsodData = GenerateRandom.GenerateRandomError();
        BsodTriggered?.Invoke(this, new BsodEventArgs { Data = bsodData });
    }

    public void CloseBsod()
    {
        BsodClosed?.Invoke(this, EventArgs.Empty);
    }
}