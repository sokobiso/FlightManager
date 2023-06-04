using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using FlightManager.Business.Interfaces;
using FlightManager.Entities;
using FlightManager.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.UI.Controllers
{
    public class FlightController : BaseController
    {
        protected IFlightService FlightService { get; set; }


        public FlightController(IAirportService airportService, IFlightService flightService) : base(airportService)
        {
            FlightService = flightService;
        }
        public IActionResult Index()
        {
            var lstAirports = AirportService.GetAllAirports();

            FlightModel model = new FlightModel()
            {
                DepartureAirports = lstAirports,
                ArrivalAirports = lstAirports,
                Name = string.Empty
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Save(FlightModel model)
        {
            // Faut faire une validations données issue de notre model avent de continue
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var distance = GatDistanceInKm(model.SelectedDepartureAirportid, model.SelectedArrivalAirportId);
            if (model.Id > 0)
            {
                var flight = FlightService.GetFlightById(model.Id);
                flight.Name = model.Name;
                flight.DepartureAirport = AirportService.GetAirportById(model.SelectedDepartureAirportid);
                flight.ArrivalAirport = AirportService.GetAirportById(model.SelectedArrivalAirportId);
                flight.Distance = distance;
            }
            else
            {
                FlightService.AddFlight(new Flight
                {
                    Name = model.Name,
                    DepartureAirport = AirportService.GetAirportById(model.SelectedDepartureAirportid),
                    ArrivalAirport = AirportService.GetAirportById(model.SelectedArrivalAirportId),
                    Distance = distance,
                });
            }
            return Ok();
        }

        private decimal GatDistanceInKm(int airPortDepartureId, int airPortArrivalId)
        {
            var airPortDeparture = AirportService.GetAirportById(airPortDepartureId);
            var airPortArrival = AirportService.GetAirportById(airPortArrivalId);
            if (airPortDeparture != null && airPortArrival != null)
            {
                var distance = Helpers.GetDistanceInKM(airPortDeparture.Latitude, airPortDeparture.Longitude, airPortArrival.Latitude, airPortArrival.Longitude);
                return Math.Round(Convert.ToDecimal(distance), 2);
            }
            return 0;
        }


        [Route("FlightDistance")]
        [HttpGet]
        public JsonResult GetFlightDistance(int airPortDepartureId, int airPortArrivalId)
        {
            var info = GatDistanceInKm(airPortDepartureId, airPortArrivalId);
            return Json(info);
        }

        [HttpGet]
        [Route("_listFlights")]
        public IActionResult _listOfFlights()
        {
            var allFlights = FlightService.GetAllFlights();
            var model = GetModel(allFlights);
            return PartialView("_listFlights", model);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteFlight(int id)
        {
            // Faut ajouter des codes erreurs en cas de problemes ou bien la resource n'existe pas

            var flight = FlightService.GetFlightById(id);
            if (flight != null)
            {
                FlightService.DeleteFlight(flight.Id);
            }
            return Ok();
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult GetFlight(int id)
        {
            // Faut ajouter des codes erreurs en cas de problemes ou bien la resource n'existe pas

            var flight = FlightService.GetFlightById(id);
            if (flight != null)
            {
                FlightModel model = new FlightModel
                {
                    Id = flight.Id,
                    Name = flight.Name,
                    SelectedArrivalAirportId = flight.ArrivalAirport.Id,
                    SelectedDepartureAirportid = flight.DepartureAirport.Id
                };
                return Json(model);
            }

            return Json(null);
        }
        private FlightModelList GetModel(List<Flight> flights)
        {
            FlightModelList flightModelList = new FlightModelList();
            flightModelList.Flights = GetFlights(flights).ToList();
            return flightModelList;
        }

        private IEnumerable<FlightItemModel> GetFlights(List<Flight> flights)
        {
            foreach (var item in flights)
            {
                FlightItemModel f = new FlightItemModel();
                f.Distance = item.Distance;
                f.Name = item.Name;
                var airPortDeparture = item.DepartureAirport;
                if (airPortDeparture != null)
                {
                    f.AirPortDeparture = airPortDeparture.Code;
                }

                var airPortArrival = item.ArrivalAirport;
                if (airPortArrival != null)
                {
                    f.AirPortArrival = airPortArrival.Code;
                }
                f.Id = item.Id;
                yield return f;
            }
        }
    }
}