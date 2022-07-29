using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class staff
    {
        public staff()
        {
        }

        public staff(string surname, string name, string firdname, string snils, string iNN, string seriaPass, string numberPass, bool gender, int idDoljnost)
        {
            Surname = surname;
            Name = name;
            Firdname = firdname;
            Snils = snils;
            INN = iNN;
            SeriaPass = seriaPass;
            NumberPass = numberPass;
            Gender = gender;
            IdDoljnost = idDoljnost;
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
