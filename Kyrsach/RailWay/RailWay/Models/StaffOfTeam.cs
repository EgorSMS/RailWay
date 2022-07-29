using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class StaffOfTeam
    {
        public StaffOfTeam(int idStaff1, int idStaff2, int idTrain)
        {
            IdStaff1 = idStaff1;
            IdStaff2 = idStaff2;
            IdTrain = idTrain;
        }

        public int IdSot { get; set; }
        public int IdStaff1 { get; set; }
        public int IdStaff2 { get; set; }
        public int IdTrain { get; set; }
    }
}
