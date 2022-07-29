using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class RailWayContext : DbContext
    {
        public RailWayContext()
        {
        }

        public RailWayContext(DbContextOptions<RailWayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Doljnost> Doljnosts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<StaffOfTeam> StaffOfTeams { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<TypeOfTrain> TypeOfTrains { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-PTPI5LJ\\DESKTOPEGOR;Database=RailWay;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.IdCity);

                entity.Property(e => e.IdCity).HasColumnName("ID_City");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PlatformCount).HasColumnName("PlatformCount");
            });

            modelBuilder.Entity<Doljnost>(entity =>
            {
                entity.HasKey(e => e.IdDoljnost);

                entity.ToTable("Doljnost");

                entity.Property(e => e.IdDoljnost).HasColumnName("ID_Doljnost");

                entity.Property(e => e.NameOfDolj)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnType("money");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameOfRole)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.IdRoute);

                entity.ToTable("Route");

                entity.Property(e => e.IdRoute).HasColumnName("ID_Route");

                entity.Property(e => e.IdCityArrival).HasColumnName("ID_City_Arrival");

                entity.Property(e => e.IdCityDeparture).HasColumnName("ID_City_Departure");

                entity.Property(e => e.PlatformDeparture).HasColumnName("PlatformDeparture");

                entity.Property(e => e.PlatformArrival).HasColumnName("PlatformArrival");

                
            });

            modelBuilder.Entity<StaffOfTeam>(entity =>
            {
                entity.HasKey(e => e.IdSot);

                entity.ToTable("StaffOfTeam");

                entity.Property(e => e.IdSot).HasColumnName("ID_SOT");

                entity.Property(e => e.IdStaff1).HasColumnName("ID_Staff1");

                entity.Property(e => e.IdStaff2).HasColumnName("ID_Staff2");

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                
            });

            modelBuilder.Entity<Stop>(entity =>
            {
                entity.HasKey(e => e.IdStop);

                entity.Property(e => e.IdStop).HasColumnName("ID_Stop");

                entity.Property(e => e.IdCity).HasColumnName("ID_City");

                entity.Property(e => e.IdTimeTable).HasColumnName("Id_TimeTable");

                entity.Property(e => e.TimeOfStop).HasColumnType("datetime");

                entity.Property(e => e.Platform).HasColumnName("Platform");

            });

            modelBuilder.Entity<TimeTable>(entity =>
            {
                entity.HasKey(e => e.IdTimeTable);

                entity.ToTable("TimeTable");

                entity.Property(e => e.IdTimeTable).HasColumnName("ID_TimeTable");

                entity.Property(e => e.DateTimeArrived).HasColumnType("datetime");

                entity.Property(e => e.DateTimeDeparted).HasColumnType("datetime");

                entity.Property(e => e.IdRoute).HasColumnName("ID_Route");

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.HasKey(e => e.IdTrain);

                entity.ToTable("Train");

                entity.Property(e => e.IdTrain).HasColumnName("ID_Train");

                entity.Property(e => e.IdTypeOfTrain).HasColumnName("ID_TypeOfTrain");

                
            });

            modelBuilder.Entity<TypeOfTrain>(entity =>
            {
                entity.HasKey(e => e.IdTypeOfTrain);

                entity.ToTable("TypeOfTrain");

                entity.Property(e => e.IdTypeOfTrain).HasColumnName("ID_TypeOfTrain");

                entity.Property(e => e.MaxSpeed)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Firdname).IsUnicode(false);

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .IsUnicode(false);

                
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.IdStaff);

                entity.ToTable("Staff");

                entity.Property(e => e.IdStaff).HasColumnName("ID_Staff");

                entity.Property(e => e.Firdname).IsUnicode(false);

                entity.Property(e => e.IdDoljnost).HasColumnName("ID_Doljnost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .IsUnicode(false);

               
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
