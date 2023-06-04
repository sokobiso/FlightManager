using FlightManager.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FlightManager.Test
{
    [TestClass]
    public class FlightServiceTest : BaseTest
    {
        private Flight InitAflight()
        {
            //get airport CDG;
            var airportCDG = airportService.GetAirportById(1);
            var airportJFK = airportService.GetAirportById(2);
            // Calculate Distance
            var distanceInKm = Helpers.GetDistanceInKM(airportCDG.Latitude, airportCDG.Longitude, airportJFK.Latitude, airportJFK.Longitude);

            Flight flight = new Flight()
            {
                DepartureAirport = airportCDG,
                ArrivalAirport = airportJFK,
                Distance = Math.Round(Convert.ToDecimal(distanceInKm), 2),
                Name = "Flight from CDG to JFK",
            };

            flightService.AddFlight(flight);
            return flight;
        }
        [TestMethod]
        public void AddFlight_ReturnsTrueIfAirPortCreated()
        {
            var flight = InitAflight();
            Assert.IsTrue(flight.Id > 0);
        }
        [TestMethod]
        public void DeleteFlight_ReturnIsNullIfFlightIsDeleted()
        {
            var flight = InitAflight();
            flightService.DeleteFlight(flight.Id);
            var existingFlight = flightService.GetFlightById(flight.Id);
            Assert.IsNull(existingFlight);
        }
        [TestMethod]
        public void GetAllFlights_ReturnIsTrue()
        {
            InitAflight();
            var flights = flightService.GetAllFlights();
            Assert.IsTrue(flights.Count > 0);
        }
        [TestMethod]
        public void GetFlightById_ReturnIsNotNullIfFlightExist()
        {
            var flight = InitAflight();
            var existingFlight = flightService.GetFlightById(flight.Id);
            Assert.IsNotNull(existingFlight);
        }
    }
}
