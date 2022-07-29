using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class User
    {
        public User(string surname, string name, string firdname, string snils, string iNN, string seriaPass, string numberPass, bool gender, string login, string password, int idRole)
        {
            Surname = surname;
            Name = name;
            Firdname = firdname;
            Snils = snils;
            INN = iNN;
            SeriaPass = seriaPass;
            NumberPass = numberPass;
            Login = login;
            Password = password;
            IdRole = idRole;
            Gender = gender;
        }

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
