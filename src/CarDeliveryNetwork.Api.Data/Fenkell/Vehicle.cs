﻿using System.Collections.Generic;
using System.Linq;

namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// 
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the VIN.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public List<Damage> Damage { get; set; }

        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        public List<HostedDocument> Photo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="atPickup">When true, populates pickup damage info, when false, delivery.</param>
        public Vehicle(Data.Vehicle vehicle, bool atPickup)
        {
            VIN = vehicle.Vin;

            var damage = atPickup
                ? vehicle.DamageAtPickup
                : vehicle.DamageAtDropoff;

            Damage = damage != null
                ? damage.Select(d => new Damage(d)).ToList()
                : new List<Damage>();

            Photo = vehicle.Photos != null
                ? vehicle.Photos.Where(p => p.Url.Contains(atPickup ? "CollectionDamage" : "DeliveryDamage"))
                        .Select(p => new HostedDocument(p))
                        .ToList()
                : new List<HostedDocument>();
        }
    }
}
