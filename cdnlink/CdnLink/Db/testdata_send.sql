
DECLARE @nextLoaid nvarchar(50);
SELECT @nextLoaid = SUBSTRING(CONVERT(nvarchar(50), NEWID()), 0, 8);

-- Insert test sends
INSERT INTO CdnSendLoads 
(
    LoadId,
    ServiceRequired,
    CustomerQuickCode,
    CustomerContact,
    CustomerOrganisationName,
    CustomerAddressLines,
    CustomerCity,
    CustomerStateRegion,
    CustomerZipPostCode,
    PickupQuickCode,
    PickupContact,
    PickupOrganisationName,
    PickupAddressLines,
    PickupCity,
    PickupStateRegion,
    PickupZipPostCode,
    PickupRequestedDate,
    PickupRequestedDateIsExact,
    DropoffQuickCode,
    DropoffContact,
    DropoffOrganisationName,
    DropoffAddressLines,
    DropoffCity,
    DropoffStateRegion,
    DropoffZipPostCode,
    DropoffRequestedDate,
    DropoffRequestedDateIsExact,
    AllocatedCarrierScac

--  Optional allocation fields.  Uncomment as required
--	
--	, ShipperScac
--	, ContractedCarrierScac
--	, AssignedDriverRemoteId
) 
VALUES 
(
    @nextLoaid,                               -- The use-once, unique to shipper load Id
    1,                                        -- Service required Transported (0 for driven)
    'CDN',                                    -- The customer quick code
    'Wayne Pollock',                             
    'Car Delivery Network, Inc.',
    '7280 NW 87th Terr.',
    'Kansas City',
    'MO',
    '64153',
    'MS123',                                  -- The pickup quick code
    'Bill',
    'Mirosoft',
    '1065 La Avenida',
    'Mountain View',
    'CA',
    '94043',
    convert(varchar(10), getdate(), 120),     -- Date only, Today	
    1,                                        -- Exact date
    'AP4242',                                 -- The dropoff quick code
    'Steve',
    'Apple',
    '1 Infinite Loop',
    'Cupertino',
    'CA',
    '95014',
    convert(varchar(10), getdate() + 2, 120), -- Date only, Today + 2 days
    0,                                        -- Non exact date (deliver by this date)
    'SAC'                                     -- Carrier to allocate to

--  Optional allocation fields.  Uncomment as required

--	, 'CHRY',                                 -- Used to originate job from entity other than the creating entity
--  , 'MID',                                  -- Used when specifying Shipper, Contracted Carrier and Subcontractor.  Must be the caller
--	, 'henry1234'                             -- Driver to assign to
);

INSERT INTO CdnSendVehicles(LoadId, Make, Model, Variant, Vin)
VALUES (@nextLoaid, 'Ford', 'Capri', '123i', 'A123456789123456A');

INSERT INTO CdnSendVehicles(LoadId, Make, Model, Variant, Vin)
VALUES (@nextLoaid, 'Renault', '5', '456 Turbo', 'B123456789123456B');

INSERT INTO CdnSends(LoadId, QueuedDate, [Status], [Action])
VALUES (@nextLoaid, GETDATE(), 10, 0);


SELECT @nextLoaid = SUBSTRING(CONVERT(nvarchar(50), NEWID()), 0, 8);

INSERT INTO CdnSendLoads 
(
    LoadId,
    ServiceRequired,
    CustomerQuickCode,
    CustomerContact,
    CustomerOrganisationName,
    CustomerAddressLines,
    CustomerCity,
    CustomerStateRegion,
    CustomerZipPostCode,
    PickupQuickCode,
    PickupContact,
    PickupOrganisationName,
    PickupAddressLines,
    PickupCity,
    PickupStateRegion,
    PickupZipPostCode,
    PickupRequestedDate,
    PickupRequestedDateIsExact,
    DropoffQuickCode,
    DropoffContact,
    DropoffOrganisationName,
    DropoffAddressLines,
    DropoffCity,
    DropoffStateRegion,
    DropoffZipPostCode,
    DropoffRequestedDate,
    DropoffRequestedDateIsExact,
    AllocatedCarrierScac
) 
VALUES 
(
    @nextLoaid,
    1,
    'CDN',
    'Wayne Pollock',
    'Car Delivery Network, Inc.',
    '7280 NW 87th Terr.',
    'Kansas City',
    'MO',
    '64153',
    'MS123',
    'Bill',
    'Mirosoft',
    '1065 La Avenida',
    'Mountain View',
    'CA',
    '94043',
    convert(varchar(10), getdate(), 120),	
    0,
    'AP4242',
    'Steve',
    'Apple',
    '1 Infinite Loop',
    'Cupertino',
    'CA',
    '95014',
    convert(varchar(10), getdate() + 2, 120),
    0,
    'SAC'
);

INSERT INTO CdnSendTranships
(
	LoadId,
	TranshipNumber,
	TripId,
	AssignedDriverRemoteId,
	AssignedTruckRemoteId,
	AddressLines,
	City,
	Contact,
	Email,
	Notes,
	OrganisationName,
	Phone,
	QuickCode,
	StateRegion,
	ZipPostCode
)
VALUES 
(
	@nextLoaid,
    1,
	'123',
	'', -- Driver 
	'',
	'2800 Lumber River Trail',
	'Apex', 
	'',
	'',
	'Place of tranship',
	'',
	'555 1234',
	'LRT',
	'NC',
	'27502'
)

INSERT INTO CdnSendVehicles(LoadId, Make, Model, Vin)
VALUES (@nextLoaid, 'Ford', 'Cortina', 'C123456789123456C');

INSERT INTO CdnSends(LoadId, QueuedDate, [Status], [Action])
VALUES (@nextLoaid, GETDATE(), 10, 0);