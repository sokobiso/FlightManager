using FlightManager.Business.Interfaces;
using FlightManager.Entities;
using FlightManager.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightManager.Business.Services
{
    /// <summary>
    /// Service to manage flights
    /// </summary>
    public class FlightService : BaseServices<Flight>, IFlightService, IDisposable
    {

        public FlightService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        // Add a flight
        public void AddFlight(Flight flight)
        {
            Repository.Add(flight);
        }

        // Delete a flight
        public void DeleteFlight(int id)
        {
            var flight = GetFlightById(id);
            if (flight != null)
            {
                Repository.Delete(flight);
            }
        }

        // Get All flights
        public List<Flight> GetAllFlights()
        {
            var flights = Repository.Fetch().ToList();
            return flights;
        }

        // Get a flight by id
        public Flight GetFlightById(int id)
        {
            var flight = Repository.FirstOrDefault(fl => fl.Id == id);
            return flight;
        }
    }
}
