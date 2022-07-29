using System;
using System.Collections.Generic;

namespace RailWay.Models
{
    public partial class Doljnost
    {
        public Doljnost()
        {
        }

        public Doljnost(string nameOfDolj, decimal salary)
        {
            NameOfDolj = nameOfDolj;
            Salary = salary;
        }

        public int IdDoljnost { get; set; }
        public string NameOfDolj { get; set; }
        public decimal Salary { get; set; }
    }
}
