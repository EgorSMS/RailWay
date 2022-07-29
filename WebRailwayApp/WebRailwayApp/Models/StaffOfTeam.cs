using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class StaffOfTeam
    {
        [Key]
        public int ID_SOT { get; set; }
        public int ID_Staff1 { get; set; }
        public int ID_Staff2 { get; set; }
        public int ID_Train { get; set; }
    }
}
