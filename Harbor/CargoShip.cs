using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class CargoShip : Boat
    {
        public override string IdentityNumber => $"C-{Utilities.GetRandomString(3)}";

        public override int Weight => Utilities.GetRandomNumber(3000, 20000);

        public override int MaximumSpeed => Utilities.GetRandomNumber(10, 20);

        public int NumberOfContainers => Utilities.GetRandomNumber(50, 500);
        public override int  DaysCout { get; set; } = 6;
        public override string BoatType => "CargoShip";


    }
}
