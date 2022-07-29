using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Doljnost
    {
        public Doljnost()
        {
        }

        public int IdDoljnost { get; set; }
        public string NameOfDolj { get; set; }
        public decimal Salary { get; set; }

    }
}
