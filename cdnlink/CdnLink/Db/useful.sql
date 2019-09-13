-- Clear all existing sends
delete CdnSends
delete CdnSendVehicles
delete CdnSendTranships
delete CdnSendLoads

-- Delete all receive data
delete CdnReceivedDamage
delete CdnReceivedVehicles
delete CdnReceivedDocuments
delete CdnReceives
delete CdnReceivedLoads
delete CdnReceivedFtpFiles;

-- Show all send data
select * from CdnSends
select * from CdnSendVehicles
select * from CdnSendTranships
select * from CdnSendLoads

-- Show all receive data
select * from CdnReceivedFtpFiles;
select * from CdnReceives
select * from CdnReceivedLoads
select * from CdnReceivedVehicles
select * from CdnReceivedDamage
select * from CdnReceivedDocuments