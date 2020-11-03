using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    public class Boat
    {
        public string BoatType { get; set; }

        public string IdentityNumber { get; set; }
        public int Weight { get; set; }
        public int MaximumSpeed { get; set; }
        public int DaysCout { get; set; }
        public Boat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout)
        {
            BoatType = boatType;
            IdentityNumber = identityNumber;
            Weight = weight;
            MaximumSpeed = maximumSpeed;
            DaysCout = daysCout;


        }

    }
}
