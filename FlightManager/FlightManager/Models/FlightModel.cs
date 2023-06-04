using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.UI.Models
{
    public class FlightModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the name")]
        public string Name { get; set; }
        public List<Airport> DepartureAirports { get; set; }
        public int SelectedDepartureAirportid { get; set; }
        public List<Airport> ArrivalAirports { get; set; }
        public int SelectedArrivalAirportId { get; set; }
    }
}
