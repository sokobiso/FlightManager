using Entities;
using FlightManager.Business.Interfaces;
using FlightManager.Business.Services;
using FlightManager.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightManager.Test
{
    [TestClass]
    public class AirportServiceTest : BaseTest
    {
        [TestMethod]
        public void AddAirport_ReturnsTrueIfAirPortCreated()
        {
            var bogotaAirPort = new Airport
            {
                Name = "Aéroport Bogota-El Dorado",
                Code = "BOG",
                Latitude = 4.70083053,
                Longitude = -74.141499434
            };
            airportService.AddAirport(bogotaAirPort);
            Assert.IsTrue(bogotaAirPort.Id > 0);
        }
        [TestMethod]
        public void GetAirportById_ReturnIsNotNullIfAirPortExist()
        {
            var bogotaAirPort = airportService.GetAirportById(1);
            Assert.IsNotNull(bogotaAirPort);
        }

        
        /// <summary>
        /// Run all tests to verify existance of airports
        /// </summary>
        [TestMethod]
        public void GetAllAirports_ReturnsTrueIfExistAirPorts()
        {
            var allAirports = airportService.GetAllAirports();
            Assert.IsTrue(allAirports.Count > 0);
        }
    }
}
