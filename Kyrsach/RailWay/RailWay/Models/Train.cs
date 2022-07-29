using System;
using System.Collections.Generic;



namespace RailWay.Models
{
    public partial class Train
    {
        public Train()
        {
            
        }

        public Train(int idTypeOfTrain, int numberOfTrain)
        {
            IdTypeOfTrain = idTypeOfTrain;
            NumberOfTrain = numberOfTrain;
        }

        public int IdTrain { get; set; }
        public int IdTypeOfTrain { get; set; }
        public int NumberOfTrain { get; set; }

        
    }
}
