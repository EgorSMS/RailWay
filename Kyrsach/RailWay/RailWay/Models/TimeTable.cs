using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class TimeTable
    {
        public TimeTable(DateTime dateTimeArrived, DateTime dateTimeDeparted, int idTrain, int idRoute)
        {
            DateTimeArrived = dateTimeArrived;
            DateTimeDeparted = dateTimeDeparted;
            IdTrain = idTrain;
            IdRoute = idRoute;
        }

        public int IdTimeTable { get; set; }
        public DateTime DateTimeArrived { get; set; }
        public DateTime DateTimeDeparted { get; set; }
        public int IdTrain { get; set; }
        public int IdRoute { get; set; }

        
    }
}
