using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Train
    {
        public Train()
        {
            
        }

        [Key]
        public int ID_Train { get; set; }

        [ForeignKey(nameof(ID_TypeOfTrain))]
        public virtual TypeOfTrain TypeOfTrain { get; set; }
        public int ID_TypeOfTrain { get; set; }

        [Required(ErrorMessage = "Не указан номер поезда")]
        public int NumberOfTrain { get; set; }

        
    }
}
