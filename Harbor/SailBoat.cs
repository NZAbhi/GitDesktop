using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class SailBoat : Boat
    {


        public int BoatLength { get; set; }

        public SailBoat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int boatLength) : base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            BoatLength = boatLength;
        }
    }
}
