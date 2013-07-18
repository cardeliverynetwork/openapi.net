-- Clear all existing sends
--delete CdnSends
--delete CdnSendVehicles
--delete CdnSendLoads

-- Insert some new sends
INSERT INTO CdnSendLoads 
(
	LoadId,
	AllocatedCarrierScac,
	ServiceRequired,
	CustomerContact,
	CustomerOrganisationName,
	CustomerAddressLines,
	CustomerCity,
	CustomerStateRegion,
	CustomerZipPostCode,
	PickupContact,
	PickupOrganisationName,
	PickupAddressLines,
	PickupCity,
	PickupStateRegion,
	PickupZipPostCode,
	DropoffContact,
	DropoffOrganisationName,
	DropoffAddressLines,
	DropoffCity,
	DropoffStateRegion,
	DropoffZipPostCode
) 
VALUES 
(
	'1',
	'SAC',
	1,
	'Wayne Pollock',
	'Car Delivery Network, Inc.',
	'7280 NW 87th Terr.',
	'Kansas City',
	'MO',
	'64153',
	'Bill',
    'Mirosoft',
    '1065 La Avenida',
    'Mountain View',
    'CA',
    '94043',
    'Steve',
    'Apple',
    '1 Infinite Loop',
    'Cupertino',
    'CA',
	'95014'
);

INSERT INTO CdnSendVehicles(LoadId, Make, Model, Vin)
VALUES (1, 'Ford', 'Capri', '123');

INSERT INTO CdnSendVehicles(LoadId, Make, Model, Vin)
VALUES (1, 'Renault', '5', '456')

INSERT INTO CdnSends(LoadId, QueuedDate, Status)
VALUES (1, GETDATE(), 10);

INSERT INTO CdnSendLoads 
(
	LoadId,
	AllocatedCarrierScac,
	ServiceRequired,
	CustomerContact,
	CustomerOrganisationName,
	CustomerAddressLines,
	CustomerCity,
	CustomerStateRegion,
	CustomerZipPostCode,
	PickupContact,
	PickupOrganisationName,
	PickupAddressLines,
	PickupCity,
	PickupStateRegion,
	PickupZipPostCode,
	DropoffContact,
	DropoffOrganisationName,
	DropoffAddressLines,
	DropoffCity,
	DropoffStateRegion,
	DropoffZipPostCode
) 
VALUES 
(
	'2',
	'SAC',
	1,
	'Wayne Pollock',
	'Car Delivery Network, Inc.',
	'7280 NW 87th Terr.',
	'Kansas City',
	'MO',
	'64153',
	'Bill',
    'Mirosoft',
    '1065 La Avenida',
    'Mountain View',
    'CA',
    '94043',
    'Steve',
    'Apple',
    '1 Infinite Loop',
    'Cupertino',
    'CA',
	'95014'
);

INSERT INTO CdnSendVehicles(LoadId, Make, Model, Vin)
VALUES (2, 'Ford', 'Cortina', '789');

INSERT INTO CdnSends(LoadId, QueuedDate, Status)
VALUES (2, GETDATE(), 10);