using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Doljnost
    {
        public Doljnost()
        {
        }

        [Key]
        public int ID_Doljnost { get; set; }
        public string NameOfDolj { get; set; }
        public decimal Salary { get; set; }

    }
}
