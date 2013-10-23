﻿CREATE TABLE [dbo].[CdnSendLoads] 
(
	[LoadId] [varchar] (40) NOT NULL,
	[AllocatedCarrierScac] [varchar] (10) NOT NULL,
	[AssignedDriverRemoteId] [varchar] (40) NULL,
	[BuyPrice] [int] NULL,
	[CustomerReference] [varchar] (50) NULL,
	[DriverPay] [int]  NULL,
	[JobInitiator] [varchar] (40) NULL,
	[Notes] [varchar] (256) NULL,
	[SellPrice] [int] NULL,
	[ServiceRequired] [int] NOT NULL,
	[ShipperScac] [varchar] (10) NULL,
	[TripId] [varchar] (20) NULL,
	[CustomerAddressLines] [varchar] (300) NULL,
	[CustomerCity] [varchar] (300) NULL,
	[CustomerContact] [varchar] (100) NULL,
	[CustomerEmail] [varchar] (1000) NULL,
	[CustomerFax] [varchar] (30) NULL,
	[CustomerMobilePhone] [varchar] (30) NULL,
	[CustomerNotes] [varchar] (1000) NULL,
	[CustomerOrganisationName] [varchar] (100) NULL,
	[CustomerOtherPhone] [varchar] (30) NULL,
	[CustomerPhone] [varchar] (30) NULL,
	[CustomerQuickCode] [varchar] (255) NULL,
	[CustomerStateRegion] [varchar] (255) NULL,
	[CustomerZipPostCode] [varchar] (10) NULL,
	[DropoffAddressLines] [varchar] (300) NULL,
	[DropoffCity] [varchar] (300) NULL,
	[DropoffContact] [varchar] (100) NULL,
	[DropoffEmail] [varchar] (1000) NULL,
	[DropoffFax] [varchar] (30) NULL,
	[DropoffMobilePhone] [varchar] (30) NULL,
	[DropoffNotes] [varchar] (1000) NULL,
	[DropoffOrganisationName] [varchar] (100) NULL,
	[DropoffOtherPhone] [varchar] (30) NULL,
	[DropoffPhone] [varchar] (30) NULL,
	[DropoffQuickCode] [varchar] (255) NULL,
	[DropoffStateRegion] [varchar] (255) NULL,
	[DropoffZipPostCode] [varchar] (10) NULL,
	[DropoffRequestedDate] [datetime] NULL,
	[DropoffRequestedDateIsExact] [bit] NULL,
	[PickupAddressLines] [varchar] (300) NULL,
	[PickupCity] [varchar] (300) NULL,
	[PickupContact] [varchar] (100) NULL,
	[PickupEmail] [varchar] (1000) NULL,
	[PickupFax] [varchar] (30) NULL,
	[PickupMobilePhone] [varchar] (30) NULL,
	[PickupNotes] [varchar] (1000) NULL,
	[PickupOrganisationName] [varchar] (100) NULL,
	[PickupOtherPhone] [varchar] (30) NULL,
	[PickupPhone] [varchar] (30) NULL,
	[PickupQuickCode] [varchar] (255) NULL,
	[PickupStateRegion] [varchar] (255) NULL,
	[PickupZipPostCode] [varchar] (10) NULL,
	[PickupRequestedDate] [datetime] NULL,
	[PickupRequestedDateIsExact] [bit] NULL,
	PRIMARY KEY CLUSTERED (LoadId)
)

CREATE TABLE [dbo].[CdnSendVehicles] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[LoadId] [varchar] (40) NOT NULL FOREIGN KEY REFERENCES CdnSendLoads (LoadId),
	[Location] [varchar] (50) NULL,
	[Make] [varchar] (20) NULL,
	[Model] [varchar] (20) NULL,
	[MovementNumber] [varchar] (50) NULL,
	[Notes] [varchar] (255) NULL,
	[Registration] [varchar] (10) NULL,
	[Variant] [varchar] (50) NULL,
	[Vin] [varchar] (17) NOT NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnSends] 
(
    [LoadId] [varchar] (40) NOT NULL FOREIGN KEY REFERENCES CdnSendLoads (LoadId),
	[QueuedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ProcessingDate] [datetime] NULL,
	[SentDate] [datetime] NULL,
	[FailedDate] [datetime] NULL,
	[ErrorMessage] [varchar] (1000) NULL,
	[ErrorCode] [varchar] (50) NULL,
	PRIMARY KEY CLUSTERED (LoadId)
)

CREATE TABLE [dbo].[CdnReceivedFtpFiles] 
(
	[Id] [int] IDENTITY (1, 1),
	[JsonMessage] [varchar] (8000) NULL,
	[Filename] [varchar] (100) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceives] 
(
	[FtpFileId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedFtpFiles (Id),
	[FetchedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[SuccessDate] [datetime] NULL,
	[FailedDate] [datetime] NULL,
	[ErrorMessage] [varchar] (1000) NULL,
	[ErrorCode] [varchar] (50) NULL,
	PRIMARY KEY CLUSTERED (FtpFileId)
)

CREATE TABLE [dbo].[CdnReceivedLoads] 
(
	[FtpFileId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedFtpFiles (Id),
	[CdnId] [int] UNIQUE NOT NULL,
	[AllocatedCarrierScac] [varchar] (10) NULL,
	[AssignedDriverRemoteId] [varchar] (40) NULL,
	[BuyPrice] [int] NULL,
	[CustomerReference] [varchar] (50) NULL,
	[DriverPay] [int]  NULL,
	[JobInitiator] [varchar] (40) NULL,
	[JobNumber]  [varchar] (50) NULL,
	[LoadId] [varchar] (40) NULL,
	[Mileage] [int] NULL,
	[Notes] [varchar] (256) NULL,
	[SellPrice] [int] NULL,
	[ServiceRequired] [int] NOT NULL,
	[ShipperScac] [varchar] (10) NULL,
	[Status] [int] NULL,
	[TripId] [varchar] (20) NULL,
	[CustomerAddressLines] [varchar] (300) NULL,
	[CustomerCity] [varchar] (300) NULL,
	[CustomerContact] [varchar] (100) NULL,
	[CustomerEmail] [varchar] (1000) NULL,
	[CustomerFax] [varchar] (30) NULL,
	[CustomerMobilePhone] [varchar] (30) NULL,
	[CustomerNotes] [varchar] (1000) NULL,
	[CustomerOrganisationName] [varchar] (100) NULL,
	[CustomerOtherPhone] [varchar] (30) NULL,
	[CustomerPhone] [varchar] (30) NULL,
	[CustomerQuickCode] [varchar] (255) NULL,
	[CustomerStateRegion] [varchar] (255) NULL,
	[CustomerZipPostCode] [varchar] (10) NULL,
	[DropoffAddressLines] [varchar] (300) NULL,
	[DropoffCity] [varchar] (300) NULL,
	[DropoffContact] [varchar] (100) NULL,
	[DropoffEmail] [varchar] (1000) NULL,
	[DropoffFax] [varchar] (30) NULL,
	[DropoffMobilePhone] [varchar] (30) NULL,
	[DropoffNotes] [varchar] (1000) NULL,
	[DropoffOrganisationName] [varchar] (100) NULL,
	[DropoffOtherPhone] [varchar] (30) NULL,
	[DropoffPhone] [varchar] (30) NULL,
	[DropoffQuickCode] [varchar] (255) NULL,
	[DropoffStateRegion] [varchar] (255)  NULL,
	[DropoffZipPostCode] [varchar] (10) NULL,
	[DropoffRequestedDate] [datetime] NULL,
	[DropoffRequestedDateIsExact] [bit] NULL,
	[DropoffNotSignedReason] [varchar] (1000) NULL, 
	[DropoffSignedBy] [varchar] (100) NULL,
	[DropoffTime] [datetime] NULL, 
	[DropoffUrl] [varchar] (1000) NULL,
	[PickupAddressLines] [varchar] (300) NULL,
	[PickupCity] [varchar] (300) NULL,
	[PickupContact] [varchar] (100) NULL,
	[PickupEmail] [varchar] (1000) NULL,
	[PickupFax] [varchar] (30) NULL,
	[PickupMobilePhone] [varchar] (30) NULL,
	[PickupNotes] [varchar] (1000) NULL,
	[PickupOrganisationName] [varchar] (100) NULL,
	[PickupOtherPhone] [varchar] (30) NULL,
	[PickupPhone] [varchar] (30) NULL,
	[PickupQuickCode] [varchar] (255) NULL,
	[PickupStateRegion] [varchar] (255) NULL,
	[PickupZipPostCode] [varchar] (10) NULL,
	[PickupRequestedDate] [datetime] NULL,
	[PickupRequestedDateIsExact] [bit] NULL,
	[PickupNotSignedReason] [varchar] (1000) NULL,
	[PickupSignedBy] [varchar] (100) NULL,
	[PickupTime] [datetime] NULL, 
	[PickupUrl] [varchar] (1000) NULL,
	PRIMARY KEY CLUSTERED (FtpFileId)
)

CREATE TABLE [dbo].[CdnReceivedDocuments] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[CdnId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedLoads (CdnId),
	[VehicleId] [int] NULL,
	[Comment] [varchar] (1000) NULL,
	[Title] [varchar] (50) NULL,
	[Url] [varchar] (1000) NULL,
	PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE [dbo].[CdnReceivedVehicles] 
(
	[VehicleId] [int] UNIQUE NOT NULL,
	[CdnId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedLoads (CdnId),
	[Location] [varchar] (50) NULL,
	[Make] [varchar] (20) NULL,
	[Model] [varchar] (20) NULL,
	[MovementNumber] [varchar] (50) NULL,
	[Notes] [varchar] (255) NULL,
	[Registration] [varchar] (10) NULL,
	[Variant] [varchar] (50) NULL,
	[Vin] [varchar] (17) NOT NULL,
	PRIMARY KEY CLUSTERED (VehicleId)
)

CREATE TABLE [dbo].[CdnReceivedDamage] 
(
	[DamageId] [int] NOT NULL,
	[VehicleId] [int] NOT NULL FOREIGN KEY REFERENCES CdnReceivedVehicles(VehicleId),
	[DamageAt] [varchar] (50) NULL,
	[AreaCode] [varchar] (3) NULL,
	[AreaDescription] [varchar] (50) NULL,
	[SeverityCode] [varchar] (3) NULL,
	[SeverityDescription] [varchar] (50) NULL,
	[TypeCode] [varchar] (3) NULL,
	[TypeDescription] [varchar] (50) NULL,
	PRIMARY KEY CLUSTERED (DamageId)
)