using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class CargoShip : Boat
    {
        public int NumberOfContainers { get; set; }
       
        
        public CargoShip(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int numberOfContainers) :base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            NumberOfContainers = numberOfContainers;
        }



    }
}
