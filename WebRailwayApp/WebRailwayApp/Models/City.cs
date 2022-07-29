using System;
using System.Collections.Generic;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class City
    {
        public City()
        {
            
        }

        public int IdCity { get; set; }
        public string Name { get; set; }
        public int PlatformCount { get; set; }       
    }
}
