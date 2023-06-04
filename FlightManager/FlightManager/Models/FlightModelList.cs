using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.UI.Models
{
    public class FlightModelList
    {
        public List<FlightItemModel> Flights { get; set; }
    }

    public class FlightItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AirPortDeparture { get; set; }
        public string AirPortArrival { get; set; }
        public string AirPlane { get; set; }
        public decimal Distance { get; set; }
    }
}
