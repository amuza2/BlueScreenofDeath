using System;

namespace BlueScreenofDeath.Services;

public class BsodEventArgs : EventArgs
{
    public BsodData Data { get; set; }
}