using System;

namespace CarDeliveryNetwork.Api.Data
{
    public class Position
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the lattitude.
        /// </summary>
        /// <value>
        /// The lattitude.
        /// </value>
        public virtual double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public virtual double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>
        /// The altitude.
        /// </value>
        public virtual float Altitude { get; set; }

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        public virtual float Heading { get; set; }

        /// <summary>
        /// Gets or sets the speed in mph.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public virtual float Speed { get; set; }
    }
}
