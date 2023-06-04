using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Business.Interfaces
{
    public interface IAirportService: IDisposable
    {
        void AddAirport(Airport airport);
        List<Airport> GetAllAirports();
        Airport GetAirportById(int id);
    }
}
