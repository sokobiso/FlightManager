using FlightManager.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Business.Interfaces
{
    public interface IFlightService : IDisposable
    {
        List<Flight> GetAllFlights();
        Flight GetFlightById(int id);
        void AddFlight(Flight flight);
        void DeleteFlight(int id);
    }
}
