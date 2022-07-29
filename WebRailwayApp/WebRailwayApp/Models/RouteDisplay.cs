using System.Collections.Generic;

namespace WebRailwayApp.Models
{
    public class RouteDisplay
    {
        public Route route { get; set; }
        public List<Cities> cities { get; set; }

        public bool ErrorArrivePlatform { get; set; }
        public bool ErrorDeparturePlatform { get; set; }
        public bool ErrorCities { get; set; }

    }
}
