using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class MotorBoat : Boat
    {
        public int NumberOfHorsepower { get; set; }

        public MotorBoat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int numberOfHorsepower) : base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            NumberOfHorsepower = numberOfHorsepower;
        }

    }
}
