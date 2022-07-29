using System.Collections.Generic;

namespace WebRailwayApp.Models
{
    public class TimeTableDisplay
    {
        public TimeTable timeTable { get; set; }
        public List<Train> trains { get; set; }
        public List<Route> routes { get; set; }
        public List<Cities> cities { get; set; }
        public List<TimeTable> timeTables { get; set; }
        
        public bool ErrorDate { get; set; }
        public bool ErrorDateDepartureAlreadyExist { get; set; }
        public bool ErrorDateArrivalAlreadyExist { get; set; }


        public List<Stops> stops { get; set; }
        public Stops stopToAdd { get; set; }
        
        public bool ErrorCityAlreadyExist { get; set; }
        public bool ErrorDateAlreadyExist { get; set; }
        public bool ErrorDateStop{ get; set; }
        public bool ErrorPlatformCity { get; set; }
        public bool ErrorDateStopAlreadyExist { get; set; }

    }
}
