using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Route
    {
        public Route()
        {
            
        }

        [Key]
        public int ID_Route { get; set; }
        public int ID_City_Departure { get; set; }
        public int ID_City_Arrival { get; set; }

        [Required(ErrorMessage = "Не указано место отправления")]
        [Range(1, 30, ErrorMessage = "Количество платформ может быть от 1 до 30")]
        public int PlatformDeparture { get; set; }

        [Required(ErrorMessage = "Не указано место прибытия")]
        [Range(1, 30, ErrorMessage = "Количество платформ может быть от 1 до 30")]
        public int PlatformArrival { get; set; }

    }
}
