using System;

namespace Entities
{
    public class Airport
    {
        /// <summary>
        /// Unique identifier of the airport.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the airport
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Code or abbreviation of the airport
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Latitude coordinates of the airport
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude coordinates of the airport
        /// </summary>
        public double Longitude { get; set; }
    }
}
