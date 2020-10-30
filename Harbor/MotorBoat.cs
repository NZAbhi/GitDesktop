using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class MotorBoat : Boat
    {
        public override string IdentityNumber => $"M-{Utilities.GetRandomString(3)}";

        public override int Weight => Utilities.GetRandomNumber(200, 3000);

        public override int MaximumSpeed => Utilities.GetRandomNumber(40, 60);

        public int NumberOfHorsepower => Utilities.GetRandomNumber(500, 1000);
        public override int  DaysCout { get; set; } = 3;
        public override string BoatType => "MotorBoat";


    }
}
