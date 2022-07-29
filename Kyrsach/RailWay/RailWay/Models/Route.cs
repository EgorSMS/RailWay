using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class Route
    {
        public Route()
        {
            
        }

        public Route(int idCityDeparture, int idCityArrival, int platformDeparture, int platformArrival)
        {
            IdCityDeparture = idCityDeparture;
            IdCityArrival = idCityArrival;
            PlatformDeparture = platformDeparture;
            PlatformArrival = platformArrival;
        }

        public int IdRoute { get; set; }
        public int IdCityDeparture { get; set; }
        public int IdCityArrival { get; set; }
        public int PlatformDeparture { get; set; }
        public int PlatformArrival { get; set; }

    }
}
