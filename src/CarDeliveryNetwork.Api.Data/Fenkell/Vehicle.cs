using System.Collections.Generic;
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
        public Vehicle(Data.Vehicle vehicle)
        {
            VIN = vehicle.Vin;
            Damage = vehicle.DamageAtDropoff.Select(d => new Damage(d)).ToList();
            Photo = vehicle.Photos.Where(p => !p.Url.Contains("CollectionDamage"))
                                  .Select(p => new HostedDocument(p))
                                  .ToList();
        }
    }
}
