using System;
using System.Collections.Generic;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Stop
    {
        public int IdStop { get; set; }
        public int IdCity { get; set; }
        public int IdTimeTable { get; set; }
        public DateTime TimeOfStop { get; set; }      
        public int Platform { get; set; }

    }
}
