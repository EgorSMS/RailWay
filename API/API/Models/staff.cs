using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class staff
    {
        public staff()
        {
        }

        public int IdStaff { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Firdname { get; set; }
        public string Snils { get; set; }
        public string INN { get; set; }
        public string SeriaPass { get; set; }
        public string NumberPass { get; set; }
        public bool Gender { get; set; }
        public int IdDoljnost { get; set; }

        
    }
}
