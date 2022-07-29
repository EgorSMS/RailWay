using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Route
    {
        public Route()
        {
            
        }

        public int IdRoute { get; set; }
        public int IdCityDeparture { get; set; }
        public int IdCityArrival { get; set; }
        public int PlatformDeparture { get; set; }
        public int PlatformArrival { get; set; }

    }
}
