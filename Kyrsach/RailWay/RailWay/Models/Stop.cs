using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class Stop
    {
        public Stop(int idCity, int idTimeTable, DateTime timeOfStop, int platform)
        {
            IdCity = idCity;
            IdTimeTable = idTimeTable;
            TimeOfStop = timeOfStop;
            Platform = platform;
        }

        public int IdStop { get; set; }
        public int IdCity { get; set; }
        public int IdTimeTable { get; set; }
        public DateTime TimeOfStop { get; set; }
        public int Platform { get; set; }

    }
}
