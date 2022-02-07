using System;

namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Flags that represent job functionality
    /// </summary>
    [Flags]    
    public enum JobFunctionalityFlags
    {
        MustScanOnLoad = 1,
        MustScanOnUnload = 2,
        ScanNewCar = 4,
        AddNewCar = 8,
        AbortLoad = 16,
        AbortVehiclePickup = 32,
        LoadAllVehicles = 64,
        UnloadAllVehicles = 128,
        MustDoDutyOfCare = 256,
        MustDoPaperwork = 512,
        MustTakeOtherPhotos = 1024,
        MustGetDamageAgentSignoff = 2048
    }
}
