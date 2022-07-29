using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Cities
    {
        public Cities()
        {
            
        }

        [Key]
        public int ID_City { get; set; }

        [Required(ErrorMessage = "Не указано наименование")]
        public string Name { get; set; }

        [Range(1, 30, ErrorMessage = "Количество платформ может быть от 1 до 30")]
        public int PlatformCount { get; set; }       
    }
}
