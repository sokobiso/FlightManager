using Entities;
using FlightManager.Business.Interfaces;
using FlightManager.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightManager.Business.Services
{
    /// <summary>
    /// Service to manage airports 
    /// </summary>
    public class AirportService : BaseServices<Airport>, IAirportService, IDisposable 
    {
        
        public AirportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }
        // Add a flight
        public void AddAirport(Airport airport)
        {
            Repository.Add(airport);
        }
        // Get a airport by id
        public Airport GetAirportById(int id)
        {
            var airport = Repository.FirstOrDefault(air => air.Id == id);
            return airport;
        }

        // Get All airports
        public List<Airport> GetAllAirports()
        {
            var airports = Repository.Fetch().ToList();
            return airports;
        }
    }
}
