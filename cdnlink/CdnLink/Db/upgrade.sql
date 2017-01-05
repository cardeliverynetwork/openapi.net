SET ANSI_NULLS ON
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'NonCompletionReason' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[NonCompletionReason] [nvarchar](255) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'PickupProofDocUrl' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[PickupProofDocUrl] [nvarchar](1000) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'DropoffProofDocUrl' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[DropoffProofDocUrl] [nvarchar](1000) NULL
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
	[ActionMessage] [nvarchar] (100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'Type' and Object_ID = Object_ID(N'CdnReceivedDocuments'))
BEGIN
   ALTER TABLE CdnReceivedDocuments
ADD 
	[Type] [int] NULL,
	[FriendlyType] [nvarchar] (25) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'ContractedCarrierScac' and Object_ID = Object_ID(N'CdnSendLoads'))
BEGIN
   ALTER TABLE CdnSendLoads
ADD 
	[ContractedCarrierScac] [nvarchar](10) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'ContractedCarrierScac' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[ContractedCarrierScac] [nvarchar](10) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'AssignedTruckRemoteId' and Object_ID = Object_ID(N'CdnSendLoads'))
BEGIN
   ALTER TABLE CdnSendLoads
ADD 
	[AssignedTruckRemoteId] [nvarchar](40) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE Name = N'AssignedTruckRemoteId' and Object_ID = Object_ID(N'CdnReceivedLoads'))
BEGIN
   ALTER TABLE CdnReceivedLoads
ADD 
	[AssignedTruckRemoteId] [nvarchar](40) NULL
END
GO

ALTER TABLE CdnSendLoads 
ALTER COLUMN Notes NVARCHAR(MAX) NULL

ALTER TABLE CdnReceivedLoads 
ALTER COLUMN Notes NVARCHAR(MAX) NULL

ALTER TABLE CdnSendVehicles 
ALTER COLUMN Make NVARCHAR(50) NULL

ALTER TABLE CdnSendVehicles 
ALTER COLUMN Model NVARCHAR(50) NULL

ALTER TABLE CdnReceivedVehicles 
ALTER COLUMN Make NVARCHAR(50) NULL

ALTER TABLE CdnReceivedVehicles 
ALTER COLUMN Model NVARCHAR(50) NULL
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
	[GateOutCode] [nvarchar](25) NULL
END
GO