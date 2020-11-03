using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class RowingBoat : Boat
    {
        public int MaximumNumberOfPassengers { get; set; }


        public RowingBoat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int maximumNumberOfPassengers) : base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            MaximumNumberOfPassengers = maximumNumberOfPassengers ;
        }

    }
}
