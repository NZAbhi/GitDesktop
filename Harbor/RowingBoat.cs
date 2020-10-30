using System;
using System.Collections.Generic;
using System.Text;

namespace Harbor
{
    class RowingBoat : Boat
    {
        public int MaximumNumberOfPassengers => Utilities.GetRandomNumber(1, 6);
        public override int DaysCout { get; set; } = 1;
        public override string IdentityNumber => $"R-{Utilities.GetRandomString(3)}";

        public override int Weight => Utilities.GetRandomNumber(100, 300);

        public override int MaximumSpeed => Utilities.GetRandomNumber(1, 3);
        public override string BoatType => "RowingBoat";

    }
}
