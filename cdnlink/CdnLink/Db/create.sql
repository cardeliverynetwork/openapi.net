﻿CREATE TABLE [dbo].[CdnSendLoads] 
(
	[LoadId] [nvarchar] (40) NOT NULL,
	[AllocatedCarrierScac] [nvarchar] (10) NOT NULL,
	[AssignedDriverRemoteId] [nvarchar] (40) NULL,
	[AssignedTruckRemoteId] [nvarchar] (40) NULL,
	[BuyPrice] [int] NULL,
	[ContractedCarrierScac] [nvarchar] (10) NULL,
	[CustomerReference] [nvarchar] (50) NULL,
	[DriverPay] [int]  NULL,
	[JobInitiator] [nvarchar] (40) NULL,
	[Notes] [nvarchar] (MAX) NULL,
	[SellPrice] [int] NULL,
	[ServiceRequired] [int] NOT NULL,
	[ShipperScac] [nvarchar] (10) NULL,
	[TripId] [nvarchar] (20) NULL,
	[CustomerAddressLines] [nvarchar] (300) NULL,
	[CustomerCity] [nvarchar] (300) NULL,
	[CustomerContact] [nvarchar] (100) NULL,
	[CustomerEmail] [nvarchar] (1000) NULL,
	[CustomerFax] [nvarchar] (30) NULL,
	[CustomerMobilePhone] [nvarchar] (30) NULL,
	[CustomerNotes] [nvarchar] (1000) NULL,
	[CustomerOrganisationName] [nvarchar] (100) NULL,
	[CustomerOtherPhone] [nvarchar] (30) NULL,
	[CustomerPhone] [nvarchar] (30) NULL,
	[CustomerQuickCode] [nvarchar] (255) NULL,
	[CustomerStateRegion] [nvarchar] (255) NULL,
	[CustomerZipPostCode] [nvarchar] (10) NULL,
	[DropoffAddressLines] [nvarchar] (300) NULL,
	[DropoffCity] [nvarchar] (300) NULL,
	[DropoffContact] [nvarchar] (100) NULL,
	[DropoffEmail] [nvarchar] (1000) NULL,
	[DropoffFax] [nvarchar] (30) NULL,
	[DropoffMobilePhone] [nvarchar] (30) NULL,
	[DropoffNotes] [nvarchar] (1000) NULL,
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
	[PickupEmail] [nvarchar] (1000) NULL,
	[PickupFax] [nvarchar] (30) NULL,
	[PickupMobilePhone] [nvarchar] (30) NULL,
	[PickupNotes] [nvarchar] (1000) NULL,
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
	[Make] [nvarchar] (50) NULL,
	[Model] [nvarchar] (50) NULL,
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
	[Action] [int] NULL,
	[ActionMessage] [nvarchar] (100) NULL,
	[ProcessingDate] [datetime] NULL,
	[SentDate] [datetime] NULL,
	[FailedDate] [datetime] NULL,
	[ErrorMessage] [nvarchar] (1000) NULL,
	[ErrorCode] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED (LoadId)
)

CREATE TABLE [dbo].[CdnReceivedFtpFiles] 
(
	[Id] [int] IDENTITY (1, 1),
	[JsonMessage] [nvarchar] (MAX) NULL,
	[Filename] [nvarchar] (100) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceives] 
(
	[FtpFileId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedFtpFiles (Id),
	[FetchedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[SuccessDate] [datetime] NULL,
	[FailedDate] [datetime] NULL,
	[ErrorMessage] [nvarchar] (1000) NULL,
	[ErrorCode] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED (FtpFileId)
)

CREATE TABLE [dbo].[CdnReceivedLoads] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[FtpFileId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedFtpFiles (Id),
	[CdnId] [int] NOT NULL,
	[AllocatedCarrierScac] [nvarchar] (10) NULL,
	[AssignedDriverRemoteId] [nvarchar] (40) NULL,
	[AssignedTruckRemoteId] [nvarchar] (40) NULL,
	[BuyPrice] [int] NULL,
	[ContractedCarrierScac] [nvarchar] (10) NULL,
	[CustomerReference] [nvarchar] (50) NULL,
	[DriverPay] [int]  NULL,
	[JobInitiator] [nvarchar] (40) NULL,
	[JobNumber]  [nvarchar] (50) NULL,
	[LoadId] [nvarchar] (40) NULL,
	[Mileage] [int] NULL,
	[Notes] [nvarchar] (MAX) NULL,
	[SellPrice] [int] NULL,
	[ServiceRequired] [int] NOT NULL,
	[ShipperScac] [nvarchar] (10) NULL,
	[Status] [int] NULL,
	[TripId] [nvarchar] (20) NULL,
	[CustomerAddressLines] [nvarchar] (300) NULL,
	[CustomerCity] [nvarchar] (300) NULL,
	[CustomerContact] [nvarchar] (100) NULL,
	[CustomerEmail] [nvarchar] (1000) NULL,
	[CustomerFax] [nvarchar] (30) NULL,
	[CustomerMobilePhone] [nvarchar] (30) NULL,
	[CustomerNotes] [nvarchar] (1000) NULL,
	[CustomerOrganisationName] [nvarchar] (100) NULL,
	[CustomerOtherPhone] [nvarchar] (30) NULL,
	[CustomerPhone] [nvarchar] (30) NULL,
	[CustomerQuickCode] [nvarchar] (255) NULL,
	[CustomerStateRegion] [nvarchar] (255) NULL,
	[CustomerZipPostCode] [nvarchar] (10) NULL,
	[DropoffAddressLines] [nvarchar] (300) NULL,
	[DropoffCity] [nvarchar] (300) NULL,
	[DropoffContact] [nvarchar] (100) NULL,
	[DropoffEmail] [nvarchar] (1000) NULL,
	[DropoffFax] [nvarchar] (30) NULL,
	[DropoffMobilePhone] [nvarchar] (30) NULL,
	[DropoffNotes] [nvarchar] (1000) NULL,
	[DropoffOrganisationName] [nvarchar] (100) NULL,
	[DropoffOtherPhone] [nvarchar] (30) NULL,
	[DropoffPhone] [nvarchar] (30) NULL,
	[DropoffQuickCode] [nvarchar] (255) NULL,
	[DropoffStateRegion] [nvarchar] (255)  NULL,
	[DropoffZipPostCode] [nvarchar] (10) NULL,
	[DropoffRequestedDate] [datetime] NULL,
	[DropoffRequestedDateIsExact] [bit] NULL,
	[DropoffNotSignedReason] [nvarchar] (1000) NULL, 
	[DropoffSignedBy] [nvarchar] (100) NULL,
	[DropoffTime] [datetime] NULL, 
	[DropoffUrl] [nvarchar] (1000) NULL,
	[DropoffProofDocUrl] [nvarchar] (1000) NULL,
	[PickupAddressLines] [nvarchar] (300) NULL,
	[PickupCity] [nvarchar] (300) NULL,
	[PickupContact] [nvarchar] (100) NULL,
	[PickupEmail] [nvarchar] (1000) NULL,
	[PickupFax] [nvarchar] (30) NULL,
	[PickupMobilePhone] [nvarchar] (30) NULL,
	[PickupNotes] [nvarchar] (1000) NULL,
	[PickupOrganisationName] [nvarchar] (100) NULL,
	[PickupOtherPhone] [nvarchar] (30) NULL,
	[PickupPhone] [nvarchar] (30) NULL,
	[PickupQuickCode] [nvarchar] (255) NULL,
	[PickupStateRegion] [nvarchar] (255) NULL,
	[PickupZipPostCode] [nvarchar] (10) NULL,
	[PickupRequestedDate] [datetime] NULL,
	[PickupRequestedDateIsExact] [bit] NULL,
	[PickupNotSignedReason] [nvarchar] (1000) NULL,
	[PickupSignedBy] [nvarchar] (100) NULL,
	[PickupTime] [datetime] NULL, 
	[PickupUrl] [nvarchar] (1000) NULL,
	[PickupProofDocUrl] [nvarchar] (1000) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceivedDocuments] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[ReceivedLoadId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedLoads (Id),
	[CdnVehicleId] [int] NULL,
	[Comment] [nvarchar] (1000) NULL,
	[FriendlyType] [nvarchar] (25) NULL,
	[Title] [nvarchar] (50) NULL,
	[Type] [int] NULL,
	[Url] [nvarchar] (1000) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceivedVehicles] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[CdnVehicleId] [int] NOT NULL,
	[ReceivedLoadId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedLoads (Id),
	[Location] [nvarchar] (50) NULL,
	[Make] [nvarchar] (50) NULL,
	[Model] [nvarchar] (50) NULL,
	[MovementNumber] [nvarchar] (50) NULL,
	[NonCompletionReason] [nvarchar] (255) NULL,
	[Notes] [nvarchar] (255) NULL,
	[Registration] [nvarchar] (10) NULL,
	[Status] [int] NOT NULL,
	[Variant] [nvarchar] (50) NULL,
	[Vin] [nvarchar] (17) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceivedDamage] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[CdnDamageId] [int] NOT NULL,
	[ReceivedVehicleId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedVehicles(Id),
	[DamageAt] [nvarchar] (50) NULL,
	[AreaCode] [nvarchar] (3) NULL,
	[AreaDescription] [nvarchar] (50) NULL,
	[SeverityCode] [nvarchar] (3) NULL,
	[SeverityDescription] [nvarchar] (50) NULL,
	[TypeCode] [nvarchar] (3) NULL,
	[TypeDescription] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED (Id)
)