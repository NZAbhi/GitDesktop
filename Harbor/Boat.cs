using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    public abstract class Boat
    {
        public abstract string BoatType { get; }

        public abstract string IdentityNumber { get; }
        public abstract int Weight { get; }
        public abstract int MaximumSpeed { get; }
        public abstract  int DaysCout { get; set; }

      
    }
}
