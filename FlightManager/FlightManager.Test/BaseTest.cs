using Entities;
using FlightManager.Business.Interfaces;
using FlightManager.Business.Services;
using FlightManager.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightManager.Test
{

    public class BaseTest
    {
        protected IAirportService airportService;
        protected IFlightService flightService;
        private IUnitOfWork uow;

        [TestInitialize]
        public void Setup()
        {
            uow = new UnitOfWork();
            airportService = new AirportService(uow);
            flightService = new FlightService(uow);


            //Init air ports
            var allAirports = airportService.GetAllAirports();
            if (!allAirports.Any())
            {
                Airport airport = new Airport
                {
                    Code = "Charles de Gaulle - CDG",
                    Latitude = 49.0096906,
                    Longitude = 2.5479245
                };
                airportService.AddAirport(airport);

                Airport airportJfk = new Airport
                {
                    Code = "New York - JFK",
                    Latitude = 40.641766,
                    Longitude = -73.780968
                };
                airportService.AddAirport(airportJfk);

                Airport airPasablanca = new Airport
                {
                    Code = "Aéroport international Mohammed V de Casablanca - CMN",
                    Latitude = 33.366998532,
                    Longitude = -7.587164318
                };
                airportService.AddAirport(airPasablanca);
            }

        }
    }
}
