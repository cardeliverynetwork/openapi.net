SET ANSI_NULLS ON
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
            WHERE Name = N'Type' and Object_ID = Object_ID(N'CdnReceivedDocuments'))
BEGIN
   ALTER TABLE CdnReceivedDocuments
ADD 
	[Type] [int] NOT NULL,
	[FriendlyType] [nvarchar] (25) NULL
END
GO
