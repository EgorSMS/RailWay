using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class TypeOfTrain
    {
        public TypeOfTrain()
        {
        }

        [Key]
        public int ID_TypeOfTrain { get; set; }

        [Required(ErrorMessage = "Не указано наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана средняя скорость")]
        [Range(1, 300, ErrorMessage = "Скорость может быть равна от 1 до 300")]
        public string MaxSpeed { get; set; }

        [Required(ErrorMessage = "Не указана вместимость поезда")]
        [Range(1, 100, ErrorMessage = "Вместимость может быть равна от 1 до 100")]
        public int Capacity { get; set; }

    }
}
