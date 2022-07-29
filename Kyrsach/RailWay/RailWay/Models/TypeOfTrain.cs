using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class TypeOfTrain
    {
        public TypeOfTrain()
        {
        }

        public TypeOfTrain(string name, string maxSpeed, int capacity)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            Capacity = capacity;
        }

        public int IdTypeOfTrain { get; set; }
        public string Name { get; set; }
        public string MaxSpeed { get; set; }
        public int Capacity { get; set; }

    }
}
