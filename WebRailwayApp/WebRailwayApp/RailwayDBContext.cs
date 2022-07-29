using Microsoft.EntityFrameworkCore;
using WebRailwayApp.Models;

namespace WebRailwayApp
{
    public class RailwayDBContext : DbContext
    {
        public RailwayDBContext(DbContextOptions<RailwayDBContext> options) : base(options) { }

        public DbSet<Cities> Cities { get; set; }
        public DbSet<Doljnost> Doljnost { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<staff> staff { get; set; }
        public DbSet<StaffOfTeam> StaffOfTeam { get; set; }
        public DbSet<Stops> Stops { get; set; }
        public DbSet<TimeTable> TimeTable { get; set; }
        public DbSet<Train> Train { get; set; }
        public DbSet<TypeOfTrain> TypeOfTrain { get; set; }
        public DbSet<User> User { get; set; }
    }
}
