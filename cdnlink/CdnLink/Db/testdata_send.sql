
-- Insert test sends
INSERT INTO CdnSendLoads 
(
    LoadId,
	TripId,
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
--	, AssignedDriverRemoteId
) 
VALUES 
(
    NEWID(),                                  -- The use-once, unique to shipper load Id
	42,                                       -- The Trip Id
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
--	, 'henry1234'                             -- Driver to assign to
);

INSERT INTO CdnSendVehicles(CdnSendLoadId, Make, Model, Variant, Vin)
VALUES (IDENT_CURRENT('CdnSendLoads'), 'Ford', 'Capri', '123i', '01234567891234567');

INSERT INTO CdnSendVehicles(CdnSendLoadId, Make, Model, Variant, Vin)
VALUES (IDENT_CURRENT('CdnSendLoads'), 'Renault', '5', '456 Turbo', '01234567891234567');

INSERT INTO CdnSends(CdnSendLoadId, QueuedDate, [Status])
VALUES (IDENT_CURRENT('CdnSendLoads'), GETDATE(), 10);

INSERT INTO CdnSendLoads 
(
    LoadId,
	TripId,
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
    NEWID(),
	42,
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

INSERT INTO CdnSendVehicles(CdnSendLoadId, Make, Model, Vin)
VALUES (IDENT_CURRENT('CdnSendLoads'), 'Ford', 'Cortina', '789');

INSERT INTO CdnSends(CdnSendLoadId, QueuedDate, [Status])
VALUES (IDENT_CURRENT('CdnSendLoads'), GETDATE(), 10);