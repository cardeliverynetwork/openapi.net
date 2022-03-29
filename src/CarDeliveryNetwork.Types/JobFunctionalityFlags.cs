using System;

namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Flags that represent job functionality
    /// </summary>
    [Flags]    
    public enum JobFunctionalityFlags
    {
        DisableAbortLoad = 1,
        DisableExpenses = 2,
        DisableAddVehicle = 4,
        DisableAbortVehicle = 8
    }
}
