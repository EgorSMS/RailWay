using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class City
    {
        public City(string name, int platformCount)
        {
            Name = name;
            PlatformCount = platformCount;
        }

        public int IdCity { get; set; }
        public string Name { get; set; }
        public int PlatformCount { get; set; }
    }
}
