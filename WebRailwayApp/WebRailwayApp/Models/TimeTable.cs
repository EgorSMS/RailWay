using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class TimeTable
    {
        [Key]
        public int ID_TimeTable { get; set; }
        public DateTime DateTimeArrived { get; set; }
        public DateTime DateTimeDeparted { get; set; }
        public int ID_Train { get; set; }
        public int ID_Route { get; set; }

        
    }
}
