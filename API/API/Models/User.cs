using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Firdname { get; set; }
        public string Snils { get; set; }
        public string INN { get; set; }
        public string SeriaPass { get; set; }
        public string NumberPass { get; set; }
        public bool Gender { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }

    }
}
