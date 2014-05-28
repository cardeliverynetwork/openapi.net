Overview
========
CdnLink provides a link between your application and Car Delivery Network (CDN) by way of database tables, the CDN API and FTP.

CdnLink can be run in two modes, Send and Receive.

In Send mode, CdnLink extracts job data from the CdnLink 'Send' tables and sends them to CDN through its API.

As jobs progress in the field, and vehicles are picked up and delivered, updates are sent from CDN to your FTP server.

Running CdnLink in 'Receive' mode downloads these updates from your FTP server and inserts the job data into the CdnLink 'Receive' tables, where it can be queried by you application.

As with the rest of the CDN OpenApi clients, CdnLink is released under the MIT licence.

Format
======
CdnLink exists as an executable file that can be called from you application

Prerequsites
============
- Windows XP/Server 2003 or later
- .NET Framework 4
- SQL Server 2005 or later (earlier versions and other databases are untested but may function)

Getting the software
====================
Download the latest stable build from:
http://build.cardeliverynetwork.com:8080/guestAuth/repository/downloadAll/bt20/.lastPinned/artifacts.zip

Download the latest development build from:
http://build.cardeliverynetwork.com:8080/guestAuth/repository/downloadAll/bt20/.lastSuccessful/artifacts.zip

Installation
============

For new installs:
- Download the CdnLink from one of the links above
- Extract the whole archive and place contents into the location you would like to run it from
- Create an empty database for CdnLink or use an existing, non-CdnLink database
- Run create.sql from the 'db' directory in the extracted archive

For upgrades:
- Download the CdnLink from one of the links above
- Backup CdnLink.exe.config
- Extract the whole archive and copy contents over the existing installation, replacing all files
- Copy in the backed up CdnLink.exe.config
- Run upgrade.sql from the 'db' directory in the extracted archive

Configuration
=============
All CdnLink configuration is contained in the file, CdnLink.exe.config

The following fields are user configurable:

CdnLink.Settings.CDNLINK_CONNECTIONSTRING - Connection string for access to CdnSend... and CdnReceive... tables
CDNLINK_API_URL - URL of the Car Delivery Network API
CDNLINK_API_KEY - Your API key
CDNLINK_FTP_HOST - The FTP host CdnLink should connect to for job updates.
CDNLINK_FTP_USER - FTP Username
CDNLINK_FTP_PASS - FTP Password

Be sure to set the 'value' field and leave the 'name' field as it is above.  For the connection string, set the 'connectionString' field.

CdnLink: Send
=============

Create Job
----------
The calling application should write data to the CdnSendLoads and CdnSendVehicles tables.. It should then write to the CdnSends table (Status=SendStatus.Queued (10)) and call CdnLink:

    >cdnlink.exe /send

CdnLink will connect to the db and read CdnSends, to get the first record at SendStatus.Queued (10) out of the table and then get the data from CdnSendLoads.

- If it gets a record it will then change the Status=SendStatus.Processing (20) and send it to the CDN API. 
- If the API gets a successful result it will change the status to Status=SendStatus.Sent (30)
- If the API gets a error it will update the status to Status=SendStatus.Error (40)

The exe will then check the CdnSends table and if there is another record in the table it will send it otherwise it will close with a success code, non success errors could be:
- Invalid database connect string
- Invalid CDN API URL or key

The following Send status' are possible:

    Queued (10) - The send is queued, waiting to be processed by CdnLink
    Processing (20) - The send is being processed by CdnLink
    Sent (30) - The send is sent
    Error (40) - An error occurred when processing a send
    
Cancel Job
----------
To cancel a job that was previously created through CdnLink, set the CdnSends.Action field to 10 (Cancel) and re-set the CdnSends.Status field to 10 (Queued).  For example:

    UPDATE CdnSends SET [Action] = 10, [Status] = 10 WHERE LoadId = <theLoadId>
    
Then call CdnLink.exe with the /send switch to process the queued cancel action.

Update Job
----------
To update a job that was previously created through CdnLink, make your changes to job, then set the CdnSends.Action field to 20 (Update) and re-set the CdnSends.Status field to 10 (Queued).

The following updates are currently supported:

- ADD Vehicle(s) (available up to and including job status AtPickup). Example:

    INSERT INTO CdnSendVehicles(LoadId, Make, Model, Vin)
    VALUES (<theLoadId>, 'Ford', 'Focus', 'F123456789123456D');
    UPDATE CdnSends SET [Action] = 20, [Status] = 10 WHERE LoadId = <theLoadId>;
	
- DELETE Vehicle(s): (available up to and including job status Assigned). Example:

    DELETE CdnSendVehicles WHERE LoadId = <theLoadId> AND Vin = <theVin>
    UPDATE CdnSends SET [Action] = 20, [Status] = 10 WHERE LoadId = <theLoadId>;
	
	
CdnLink: Receive
================
Calling application calls CdnLink in receive mode:

    >cdnlink.exe /receive

CdnLink will firstly connect to the FTP server then to the database to ensure both are available.

CdnLink will then get the first ftp file and:
- Place the full JSON message into the CdnReceivedFtpFiles
- Remove the file from the FTP directory 
- Add a record to the CdnReceives table Status=ReceiveStatus.Processing (50)

CdnLink will then process the JSON message placing the data into the CdnReceivedLoad, CdnReceivedVehicles, CdnReceivedDocuments, CdnReceivedDamage tables
- If the exe gets a successful result it will update CdnReceives to the Status=ReceiveStatus.Queued (60)
- If the exe gets a error it will update CdnReceives to the Status=ReceiveStatus.Error (70)

The exe will then check the FTP directory again and if there is another record in the table it will get it otherwise it will close with a success code.  Non success errors could be:
- Invalid database connect string
- Invalid database user
- Invalid FTP server
- Invalid FTP user
- Invalid directory

The following Receive status' are possible:

    Processing (50) - The receive is being processed by CdnLink
    Queued (60) - The receive is queued, waiting to be processed by the calling application
    Error (70) - An error occurred whilst processing a receive
    ClientProcessed (80) - (Set by calling client) The client system has processed this record

SuccessDate: The calling client may choose to set SuccessDate when it sets ClientProcessed, to indicate when the record was handled.

Database Tables
===============
Send to CDN
-----------
CdnSends - table that calling application writes to and the exe reads from to trigger outbound data and track the status and delivery of the data
CdnSendLoads - table containing outbound load data, calling application should tidy as appropriate
CdnSendVehicles - table of vehicles attached to the Load, a load can have 1 or more vehicles, calling application should tidy as appropriate

Receive from CDN
------------
CdnReceivedFtpFiles - the raw file data as it was read from the FTP server. This is debugging data and shouldn't need to be used by the calling application
CdnReceives - the status of each received update.  Calling application should handle updates at status ReceiveStatus.Queued (60)
CdnReceivedLoads - load data extracted from JSON file
CdnReceivedVehicles - table of vehicles attached to the Load, a load can have 1 or more vehicles
CdnReceivedDocuments - table of documents attached to the Load, a load can have 1 or more documents
CdnReceivedDamage - table of damage codes attached to the Load, a load can have 1 or more damage items
