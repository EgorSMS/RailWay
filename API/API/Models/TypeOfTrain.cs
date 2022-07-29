using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class TypeOfTrain
    {
        public TypeOfTrain()
        {
        }

        public int IdTypeOfTrain { get; set; }
        public string Name { get; set; }
        public string MaxSpeed { get; set; }
        public int Capacity { get; set; }

    }
}
