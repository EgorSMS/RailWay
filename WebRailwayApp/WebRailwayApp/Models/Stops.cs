using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Stops
    {
        [Key]
        public int ID_Stop { get; set; }
        public int ID_City { get; set; }
        public int ID_TimeTable { get; set; }
        public DateTime TimeOfStop { get; set; }

        [Range(1, 30, ErrorMessage = "Количество платформ может быть от 1 до 30")]
        public int Platform { get; set; }

    }
}
