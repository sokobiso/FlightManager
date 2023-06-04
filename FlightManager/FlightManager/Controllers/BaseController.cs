using Entities;
using FlightManager.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightManager.UI.Controllers
{
    public class BaseController : Controller
    {

        protected IAirportService AirportService { get; set; }

        public BaseController(IAirportService _airportService)
        {
            AirportService = _airportService;
            //Init air ports
            var allAirports = AirportService.GetAllAirports();
            if (!allAirports.Any())
            {
                Airport airport = new Airport
                {
                    Code = "Charles de Gaulle - CDG",
                    Latitude = 49.0096906,
                    Longitude = 2.5479245
                };
                AirportService.AddAirport(airport);

                Airport airportJfk = new Airport
                {
                    Code = "New York - JFK",
                    Latitude = 40.641766,
                    Longitude = -73.780968
                };
                AirportService.AddAirport(airportJfk);

                Airport airPasablanca = new Airport
                {
                    Code = "Aéroport international Mohammed V de Casablanca - CMN",
                    Latitude = 33.366998532,
                    Longitude = -7.587164318
                };
                AirportService.AddAirport(airPasablanca);
            }
        }
    }
}