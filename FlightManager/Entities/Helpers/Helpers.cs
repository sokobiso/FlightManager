using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FlightManager.Entities
{
    public class Helpers
    {
        /// <summary>
        /// Use dll GeoCoordinate to calculate the distance between to points
        /// Src from :https://github.com/cormaltes/GPSOAuthSharp
        /// </summary>
        /// <param name="sLatitude">Latitude first point</param>
        /// <param name="sLongitude">Longitude first point</param>
        /// <param name="eLatitude">Latitude second point</param>
        /// <param name="eLongitude">Longitude second point</param>
        /// <returns></returns>
        public static double GetDistanceInKM(double sLatitude, double sLongitude, double eLatitude, double eLongitude)
        {
            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);
            var result = sCoord.GetDistanceTo(eCoord);
            return result / 1000;
        }
    }
}
