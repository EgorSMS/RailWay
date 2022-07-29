using System.Collections.Generic;

namespace WebRailwayApp.Models
{
    public class trainDisplay
    {
        public Train train { get; set; }
        public List<TypeOfTrain> typeOfTrains { get; set; }
        public List<Train> trains { get; set; }
    }
}