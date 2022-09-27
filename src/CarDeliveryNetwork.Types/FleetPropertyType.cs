namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network fleet properties
    /// </summary>
    public enum FleetPropertyType
    {
        /// <summary>
        /// ReceiveNewsletter
        /// </summary>
        ReceiveNewsletter,

        /// <summary>
        /// MenuProfile
        /// </summary>
        MenuProfile,

        // These used to be the SACFleetProperties before hierachy was flattened

        /// <summary>
        /// CanDoEuropeanMovements
        /// </summary>
        CanDoEuropeanMovements = 1000,

        /// <summary>
        /// HasSecureStorage
        /// </summary>
        HasSecureStorage,

        /// <summary>
        /// HasNonSecureStorage
        /// </summary>
        HasNonSecureStorage,

        /// <summary>
        /// HasPdiServices
        /// </summary>
        HasPdiServices,

        /// <summary>
        /// HasTechnicalServices
        /// </summary>
        HasTechnicalServices,

        /// <summary>
        /// HasAuctionServices
        /// </summary>
        HasAuctionServices,

        /// <summary>
        /// CanDoPrestige
        /// </summary>
        CanDoPrestige,

        /// <summary>
        /// CanDoClassic
        /// </summary>
        CanDoClassic,

        /// <summary>
        /// CanDoNonRunners
        /// </summary>
        CanDoNonRunners,

        /// <summary>
        /// MultiCarOpen
        /// </summary>
        MultiCarOpen,

        /// <summary>
        /// TwoCarOpen
        /// </summary>
        TwoCarOpen,

        /// <summary>
        /// OneCarOpen
        /// </summary>
        OneCarOpen,

        /// <summary>
        /// MultiCarEnclosed
        /// </summary>
        MultiCarEnclosed,

        /// <summary>
        /// TwoCarEnclosed
        /// </summary>
        TwoCarEnclosed,

        /// <summary>
        /// OneCarEnclosed
        /// </summary>
        OneCarEnclosed,

        /// <summary>
        /// OpenTrailer
        /// </summary>
        OpenTrailer,

        /// <summary>
        /// EnclosedTrailer
        /// </summary>
        EnclosedTrailer,

        /// <summary>
        /// DotNumber
        /// </summary>
        DotNumber,

        /// <summary>
        /// McNumber
        /// </summary>
        McNumber,

        /// <summary>
        /// EinTaxId
        /// </summary>
        EinTaxId,

        /// <summary>
        /// SaferStatRating
        /// </summary>
        SaferStatRating,

        /// <summary>
        /// StateDotNumber
        /// </summary>
        StateDotNumber,

        /// <summary>
        /// BusinessStartedYear
        /// </summary>
        BusinessStartedYear,

        /// <summary>
        /// AgentsTelephone
        /// </summary>
        AgentsTelephone,

        /// <summary>
        /// AgentsCompany
        /// </summary>
        AgentsCompany,

        /// <summary>
        /// PowerUnitDrivers
        /// </summary>
        PowerUnitDrivers,

        /// <summary>
        /// RegisteredState
        /// </summary>
        RegisteredState,

        /// <summary>
        /// BondCompany
        /// </summary>
        BondCompany,

        /// <summary>
        /// BondCompanyTelephone
        /// </summary>
        BondCompanyTelephone,

        /// <summary>
        /// BondAmount
        /// </summary>
        BondAmount,

        /// <summary>
        /// InsuranceSummary
        /// </summary>
        InsuranceSummary,

        /// <summary>
        /// EquipmentDescription
        /// </summary>
        EquipmentDescription,


        /// <summary>
        /// OperatingArea
        /// </summary>
        OperatingArea,

        /// <summary>
        /// CarrierType
        /// </summary>
        CarrierType,

        // Fleet settings


        /// <summary>
        /// SendsInstantQuoteOnRequest
        /// </summary>
        SendsInstantQuoteOnRequest = 1100,

        /// <summary>
        /// CanIssueCustomerLogin
        /// </summary>
        CanIssueCustomerLogin,

        /// <summary>
        /// ClaimOnDeviceComplete
        /// </summary>
        ClaimOnDeviceComplete,

        /// <summary>
        /// 7 or 8 day basis used to compute cumulative duty hours for ELD
        /// </summary>
        FleetConnectMultiDayBasis,

        /// <summary>
        /// Requires Driver Handover - overrides VinDispatch request setting
        /// </summary>
        RequiresDriverHandover,

        /// <summary>
        /// Requires Model - overrides VinDispatch request setting
        /// </summary>
        RequiresModel,

        /// <summary>
        /// Requires tracking - overrides VinDispatch request setting
        /// </summary>
        RequiresTracking,

        /// <summary>
        /// Requires Epod - overrides VinDispatch request setting
        /// </summary>
        RequiresEpod,

        /// <summary>
        /// Requires lease paperwork - overrides VinDispatch request setting
        /// </summary>
        RequiresLeasePaperwork,

        /// <summary>
        /// Vehicle is Road Legal to drive
        /// </summary>
        IsRoadLegal,

        /// <summary>
        /// Fuel is inclusive - overrides VinDispatch request setting
        /// </summary>
        FuelInclusive,

        /// <summary>
        ///Enclosed Transportation - overrides VinDispatch request setting
        /// </summary>
        EnclosedTransportation,

        /// <summary>
        ///In Operation
        /// </summary>
        InOp,

        /// <summary>
        /// Can Advertise Load
        /// </summary>
        CanAdvertiseLoads,

        /// <summary>
        /// Can Find Loads
        /// </summary>
        CanFindLoads,

        /// <summary>
        /// Fet Id provided by Central Dispatch
        /// </summary>
        CentralDispatchUniqueId,

        /// <summary>
        /// Dealer Portal feature enabled
        /// </summary>
        IsDealerPortalEnabled,

        /// <summary>
        /// Dispatch board feature enabled
        /// </summary>
        IsDispatchBoardEnabled,

        /// <summary>
        /// Driver devices should track movements
        /// </summary>        
        RequireTracking,

        /// <summary>
        /// Show buttons that allow manual job Pickup and Delivery
        /// </summary>
        ShowManualPickupDeliveryButtons
    }
}
