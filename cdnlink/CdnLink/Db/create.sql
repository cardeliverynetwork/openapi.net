CREATE TABLE [dbo].[CdnSendLoads] 
(
	[LoadId] [nvarchar] (40) NOT NULL,
	[AllocatedCarrierScac] [nvarchar] (10) NOT NULL,
	[AssignedDriverRemoteId] [nvarchar] (40) NULL,
	[BuyPrice] [int] NULL,
	[CustomerReference] [nvarchar] (50) NULL,
	[DriverPay] [int]  NULL,
	[JobInitiator] [nvarchar] (40) NULL,
	[Notes] [nvarchar] (256) NULL,
	[SellPrice] [int] NULL,
	[ServiceRequired] [int] NOT NULL,
	[ShipperScac] [nvarchar] (10) NULL,
	[TripId] [nvarchar] (20) NULL,
	[CustomerAddressLines] [nvarchar] (300) NULL,
	[CustomerCity] [nvarchar] (300) NULL,
	[CustomerContact] [nvarchar] (100) NULL,
	[CustomerEmail] [nvarchar] (MAX) NULL,
	[CustomerFax] [nvarchar] (30) NULL,
	[CustomerMobilePhone] [nvarchar] (30) NULL,
	[CustomerNotes] [nvarchar] (MAX) NULL,
	[CustomerOrganisationName] [nvarchar] (100) NULL,
	[CustomerOtherPhone] [nvarchar] (30) NULL,
	[CustomerPhone] [nvarchar] (30) NULL,
	[CustomerQuickCode] [nvarchar] (255) NULL,
	[CustomerStateRegion] [nvarchar] (255) NULL,
	[CustomerZipPostCode] [nvarchar] (10) NULL,
	[DropoffAddressLines] [nvarchar] (300) NULL,
	[DropoffCity] [nvarchar] (300) NULL,
	[DropoffContact] [nvarchar] (100) NULL,
	[DropoffEmail] [nvarchar] (MAX) NULL,
	[DropoffFax] [nvarchar] (30) NULL,
	[DropoffMobilePhone] [nvarchar] (30) NULL,
	[DropoffNotes] [nvarchar] (MAX) NULL,
	[DropoffOrganisationName] [nvarchar] (100) NULL,
	[DropoffOtherPhone] [nvarchar] (30) NULL,
	[DropoffPhone] [nvarchar] (30) NULL,
	[DropoffQuickCode] [nvarchar] (255) NULL,
	[DropoffStateRegion] [nvarchar] (255) NULL,
	[DropoffZipPostCode] [nvarchar] (10) NULL,
	[DropoffRequestedDate] [datetime] NULL,
	[DropoffRequestedDateIsExact] [bit] NULL,
	[PickupAddressLines] [nvarchar] (300) NULL,
	[PickupCity] [nvarchar] (300) NULL,
	[PickupContact] [nvarchar] (100) NULL,
	[PickupEmail] [nvarchar] (MAX) NULL,
	[PickupFax] [nvarchar] (30) NULL,
	[PickupMobilePhone] [nvarchar] (30) NULL,
	[PickupNotes] [nvarchar] (MAX) NULL,
	[PickupOrganisationName] [nvarchar] (100) NULL,
	[PickupOtherPhone] [nvarchar] (30) NULL,
	[PickupPhone] [nvarchar] (30) NULL,
	[PickupQuickCode] [nvarchar] (255) NULL,
	[PickupStateRegion] [nvarchar] (255) NULL,
	[PickupZipPostCode] [nvarchar] (10) NULL,
	[PickupRequestedDate] [datetime] NULL,
	[PickupRequestedDateIsExact] [bit] NULL,
	PRIMARY KEY CLUSTERED (LoadId)
)

CREATE TABLE [dbo].[CdnSendVehicles] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[LoadId] [nvarchar] (40) NOT NULL FOREIGN KEY REFERENCES CdnSendLoads (LoadId),
	[Location] [nvarchar] (50) NULL,
	[Make] [nvarchar] (20) NULL,
	[Model] [nvarchar] (20) NULL,
	[MovementNumber] [nvarchar] (50) NULL,
	[Notes] [nvarchar] (255) NULL,
	[Registration] [nvarchar] (10) NULL,
	[Variant] [nvarchar] (50) NULL,
	[Vin] [nvarchar] (17) NOT NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnSends] 
(
    [LoadId] [nvarchar] (40) NOT NULL FOREIGN KEY REFERENCES CdnSendLoads (LoadId),
	[QueuedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ProcessingDate] [datetime] NULL,
	[SentDate] [datetime] NULL,
	[FailedDate] [datetime] NULL,
	[ErrorMessage] [nvarchar] (MAX) NULL,
	[ErrorCode] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED (LoadId)
)

CREATE TABLE [dbo].[CdnReceivedFtpFiles] 
(
	[Id] [int] IDENTITY (1, 1),
	[JsonMessage] [nvarchar] (MAX) NULL,
	[Filename] [nvarchar] (MAX) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceives] 
(
	[FtpFileId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedFtpFiles (Id),
	[FetchedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[SuccessDate] [datetime] NULL,
	[FailedDate] [datetime] NULL,
	[ErrorMessage] [nvarchar] (MAX) NULL,
	[ErrorCode] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED (FtpFileId)
)

CREATE TABLE [dbo].[CdnReceivedLoads] 
(
	[FtpFileId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedFtpFiles (Id),
	[CdnId] [int] UNIQUE NOT NULL,
	[AllocatedCarrierScac] [nvarchar] (10) NOT NULL,
	[AssignedDriverRemoteId] [nvarchar] (40) NULL,
	[BuyPrice] [int] NULL,
	[CustomerReference] [nvarchar] (50) NULL,
	[DriverPay] [int]  NULL,
	[JobInitiator] [nvarchar] (40) NULL,
	[JobNumber]  [nvarchar] (50) NULL,
	[LoadId] [nvarchar] (40) NULL,
	[Mileage] [int] NULL,
	[Notes] [nvarchar] (256) NULL,
	[SellPrice] [int] NULL,
	[ServiceRequired] [int] NOT NULL,
	[ShipperScac] [nvarchar] (10) NULL,
	[Status] [int] NULL,
	[TripId] [nvarchar] (20) NULL,
	[CustomerAddressLines] [nvarchar] (300) NULL,
	[CustomerCity] [nvarchar] (300) NULL,
	[CustomerContact] [nvarchar] (100) NULL,
	[CustomerEmail] [nvarchar] (MAX) NULL,
	[CustomerFax] [nvarchar] (30) NULL,
	[CustomerMobilePhone] [nvarchar] (30) NULL,
	[CustomerNotes] [nvarchar] (MAX) NULL,
	[CustomerOrganisationName] [nvarchar] (100) NULL,
	[CustomerOtherPhone] [nvarchar] (30) NULL,
	[CustomerPhone] [nvarchar] (30) NULL,
	[CustomerQuickCode] [nvarchar] (255) NULL,
	[CustomerStateRegion] [nvarchar] (255) NULL,
	[CustomerZipPostCode] [nvarchar] (10) NULL,
	[DropoffAddressLines] [nvarchar] (300) NULL,
	[DropoffCity] [nvarchar] (300) NULL,
	[DropoffContact] [nvarchar] (100) NULL,
	[DropoffEmail] [nvarchar] (MAX) NULL,
	[DropoffFax] [nvarchar] (30) NULL,
	[DropoffMobilePhone] [nvarchar] (30) NULL,
	[DropoffNotes] [nvarchar] (MAX) NULL,
	[DropoffOrganisationName] [nvarchar] (100) NULL,
	[DropoffOtherPhone] [nvarchar] (30) NULL,
	[DropoffPhone] [nvarchar] (30) NULL,
	[DropoffQuickCode] [nvarchar] (255) NULL,
	[DropoffStateRegion] [nvarchar] (255)  NULL,
	[DropoffZipPostCode] [nvarchar] (10) NULL,
	[DropoffRequestedDate] [datetime] NULL,
	[DropoffRequestedDateIsExact] [bit] NULL,
	[DropoffNotSignedReason] [nvarchar] (MAX) NULL, 
	[DropoffSignedBy] [nvarchar] (100) NULL,
	[DropoffTime] [datetime] NULL, 
	[DropoffUrl] [nvarchar] (MAX) NULL,
	[PickupAddressLines] [nvarchar] (300) NULL,
	[PickupCity] [nvarchar] (300) NULL,
	[PickupContact] [nvarchar] (100) NULL,
	[PickupEmail] [nvarchar] (MAX) NULL,
	[PickupFax] [nvarchar] (30) NULL,
	[PickupMobilePhone] [nvarchar] (30) NULL,
	[PickupNotes] [nvarchar] (MAX) NULL,
	[PickupOrganisationName] [nvarchar] (100) NULL,
	[PickupOtherPhone] [nvarchar] (30) NULL,
	[PickupPhone] [nvarchar] (30) NULL,
	[PickupQuickCode] [nvarchar] (255) NULL,
	[PickupStateRegion] [nvarchar] (255) NULL,
	[PickupZipPostCode] [nvarchar] (10) NULL,
	[PickupRequestedDate] [datetime] NULL,
	[PickupRequestedDateIsExact] [bit] NULL,
	[PickupNotSignedReason] [nvarchar] (MAX) NULL,
	[PickupSignedBy] [nvarchar] (100) NULL,
	[PickupTime] [datetime] NULL, 
	[PickupUrl] [nvarchar] (MAX) NULL,
	PRIMARY KEY CLUSTERED (FtpFileId)
)

CREATE TABLE [dbo].[CdnReceivedDocuments] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[CdnId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedLoads (CdnId),
	[VehicleId] [int] NULL,
	[Comment] [nvarchar] (MAX) NULL,
	[Title] [nvarchar] (50) NULL,
	[Url] [nvarchar] (MAX) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceivedVehicles] 
(
	[VehicleId] [int] UNIQUE NOT NULL,
	[CdnId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedLoads (CdnId),
	[Location] [nvarchar] (50) NULL,
	[Make] [nvarchar] (20) NULL,
	[Model] [nvarchar] (20) NULL,
	[MovementNumber] [nvarchar] (50) NULL,
	[Notes] [nvarchar] (255) NULL,
	[Registration] [nvarchar] (10) NULL,
	[Variant] [nvarchar] (50) NULL,
	[Vin] [nvarchar] (17) NOT NULL,
	PRIMARY KEY CLUSTERED (VehicleId)
)

CREATE TABLE [dbo].[CdnReceivedDamage] 
(
	[DamageId] [int] NOT NULL,
	[VehicleId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedVehicles(VehicleId),
	[DamageAt] [nvarchar] (50) NULL,
	[AreaCode] [nvarchar] (3) NULL,
	[AreaDescription] [nvarchar] (50) NULL,
	[SeverityCode] [nvarchar] (3) NULL,
	[SeverityDescription] [nvarchar] (50) NULL,
	[TypeCode] [nvarchar] (3) NULL,
	[TypeDescription] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED (DamageId)
)