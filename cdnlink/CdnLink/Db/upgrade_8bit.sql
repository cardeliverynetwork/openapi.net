SET ANSI_NULLS ON
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'NonCompletionReason' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[NonCompletionReason] [varchar](255) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'PickupProofDocUrl' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[PickupProofDocUrl] [varchar](1000) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'DropoffProofDocUrl' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[DropoffProofDocUrl] [varchar](1000) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Action' and Object_ID = Object_ID(N'CdnSends'))
BEGIN
   ALTER TABLE CdnSends
ADD 
	[Action] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'ActionMessage' and Object_ID = Object_ID(N'CdnSends'))
BEGIN
   ALTER TABLE CdnSends
ADD 
	[ActionMessage] [varchar] (100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Type' and Object_ID = Object_ID(N'CdnReceivedDocuments'))
BEGIN
   ALTER TABLE CdnReceivedDocuments
ADD 
	[Type] [int] NULL,
	[FriendlyType] [varchar] (25) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'ContractedCarrierScac' and Object_ID = Object_ID(N'CdnSendLoads'))
BEGIN
   ALTER TABLE CdnSendLoads
ADD 
	[ContractedCarrierScac] [varchar](10) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'ContractedCarrierScac' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[ContractedCarrierScac] [varchar](10) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'AssignedTruckRemoteId' and Object_ID = Object_ID(N'CdnSendLoads'))
BEGIN
   ALTER TABLE CdnSendLoads
ADD 
	[AssignedTruckRemoteId] [varchar](40) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'AssignedTruckRemoteId' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[AssignedTruckRemoteId] [varchar](40) NULL
END
GO

ALTER TABLE CdnSendLoads 
ALTER COLUMN Notes varchar(MAX) NULL

ALTER TABLE CdnReceivedLoads 
ALTER COLUMN Notes varchar(MAX) NULL

ALTER TABLE CdnSendVehicles 
ALTER COLUMN Make varchar(50) NULL

ALTER TABLE CdnSendVehicles 
ALTER COLUMN Model varchar(50) NULL

ALTER TABLE CdnReceivedVehicles 
ALTER COLUMN Make varchar(50) NULL

ALTER TABLE CdnReceivedVehicles 
ALTER COLUMN Model varchar(50) NULL
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'PickupEta' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[PickupEta] [datetime] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'DropoffEta' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[DropoffEta] [datetime] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'GateOutCode' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[GateOutCode] [varchar](25) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'CdnSendTranships')
BEGIN
	CREATE TABLE [dbo].[CdnSendTranships] 
	(
		[Id] [int] IDENTITY (1, 1) NOT NULL,
		[LoadId] [varchar] (40) NOT NULL FOREIGN KEY REFERENCES CdnSendLoads (LoadId),
		[TranshipNumber] [int]  NULL,
		[TripId] [varchar] (20) NULL,
		[AssignedDriverRemoteId] [varchar] (40) NULL,
		[AssignedTruckRemoteId] [varchar] (40) NULL,
		[AddressLines] [varchar] (300) NULL,
		[City] [varchar] (300) NULL,
		[Contact] [varchar] (100) NULL,
		[Email] [varchar] (1000) NULL,
		[Fax] [varchar] (30) NULL,
		[MobilePhone] [varchar] (30) NULL,
		[Notes] [varchar] (1000) NULL,
		[OrganisationName] [varchar] (100) NULL,
		[OtherPhone] [varchar] (30) NULL,
		[Phone] [varchar] (30) NULL,
		[QuickCode] [varchar] (255) NULL,
		[StateRegion] [varchar] (255) NULL,
		[ZipPostCode] [varchar] (10) NULL,
		[RequestedDate] [datetime] NULL,
		[RequestedDateIsExact] [bit] NULL,
		PRIMARY KEY CLUSTERED (Id)
	)
END

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Color' and Object_ID = Object_ID(N'CdnSendVehicles'))
BEGIN
   ALTER TABLE CdnSendVehicles
ADD 
	[Color] [varchar] (25) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'LoadDirection' and Object_ID = Object_ID(N'CdnSendVehicles'))
BEGIN
   ALTER TABLE CdnSendVehicles
ADD 
	[LoadDirection] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'LoadLevel' and Object_ID = Object_ID(N'CdnSendVehicles'))
BEGIN
   ALTER TABLE CdnSendVehicles
ADD 
	[LoadLevel] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'LoadPosition' and Object_ID = Object_ID(N'CdnSendVehicles'))
BEGIN
   ALTER TABLE CdnSendVehicles
ADD 
	[LoadPosition] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Weight' and Object_ID = Object_ID(N'CdnSendVehicles'))
BEGIN
   ALTER TABLE CdnSendVehicles
ADD 
	[Weight] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Color' and Object_ID = Object_ID(N'CdnReceivedVehicles'))
BEGIN
   ALTER TABLE CdnReceivedVehicles
ADD 
	[Color] [varchar] (25) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'LoadDirection' and Object_ID = Object_ID(N'CdnReceivedVehicles'))
BEGIN
   ALTER TABLE CdnReceivedVehicles
ADD 
	[LoadDirection] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'LoadLevel' and Object_ID = Object_ID(N'CdnReceivedVehicles'))
BEGIN
   ALTER TABLE CdnReceivedVehicles
ADD 
	[LoadLevel] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'LoadPosition' and Object_ID = Object_ID(N'CdnReceivedVehicles'))
BEGIN
   ALTER TABLE CdnReceivedVehicles
ADD 
	[LoadPosition] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Weight' and Object_ID = Object_ID(N'CdnReceivedVehicles'))
BEGIN
   ALTER TABLE CdnReceivedVehicles
ADD 
	[Weight] [int] NULL
END
GO

