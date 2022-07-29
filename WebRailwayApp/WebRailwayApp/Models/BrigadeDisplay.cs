using System.Collections.Generic;

namespace WebRailwayApp.Models
{
    public class BrigadeDisplay
    {
        public List<staff> staffs { get; set; }
        public List<Train> trains { get; set; }
        public List<StaffOfTeam> staffOfTeams { get; set; }
        public StaffOfTeam StaffOfTeam { get; set; }
        public List<Doljnost> doljnosts { get; set; }
    }
}
