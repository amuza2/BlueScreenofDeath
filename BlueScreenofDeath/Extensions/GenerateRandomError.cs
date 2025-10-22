using System;
using BlueScreenofDeath.Services;

namespace BlueScreenofDeath.Extensions;

public class GenerateRandom
{
    private static readonly string[] _errorCodes = 
    {
        "0x0000001A", "0x0000003B", "0x00000050", 
        "0x0000007E", "0x0000007F", "0x000000D1"
    };

    private static readonly string[] _errorMessages = 
    {
        "MEMORY_MANAGEMENT",
        "SYSTEM_SERVICE_EXCEPTION",
        "PAGE_FAULT_IN_NONPAGED_AREA",
        "SYSTEM_THREAD_EXCEPTION_NOT_HANDLED",
        "UNEXPECTED_KERNEL_MODE_TRAP",
        "DRIVER_IRQL_NOT_LESS_OR_EQUAL"
    };
    private static readonly Random _random = new Random();
    public static BsodData GenerateRandomError()
    {
                
        return new BsodData
        {
            ErrorCode = _errorCodes[_random.Next(_errorCodes.Length)],
            ErrorMessage = _errorMessages[_random.Next(_errorMessages.Length)],
            AdditionalInfo = "A fatal system error has occurred. The system will restart shortly."
        };
    }
}