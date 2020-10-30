using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class SailBoat : Boat
    {
        public override string IdentityNumber => $"S-{Utilities.GetRandomString(3)}";

        public override int Weight => Utilities.GetRandomNumber(1000, 6000);

        public override int MaximumSpeed => Utilities.GetRandomNumber(6, 12);

        public int BoatLength => Utilities.GetRandomNumber(20, 60);
        public override int  DaysCout { get; set; } = 4;
        public override string BoatType => "SailBoat";



    }
}
