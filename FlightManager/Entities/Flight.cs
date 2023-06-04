using Entities;

namespace FlightManager.Entities
{
    public class Flight
    {
        /// <summary>
        /// Unique identifier of the flight
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name or identifier of the flight
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Departure airport of the flight
        /// </summary>
        public Airport DepartureAirport { get; set; }
        /// <summary>
        /// Arrival airport of the flight
        /// </summary>
        public Airport ArrivalAirport { get; set; }
        /// <summary>
        /// The computed distance between the departure and arrival airports
        /// </summary>
        public decimal Distance { get; set; }
    }
}
