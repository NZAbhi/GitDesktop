using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPoliceAndThief
{
    class Citizen : Person
    {       

        public List<Inventory> Assets = new List<Inventory>();
        public Citizen(int x, int y, int xDirection, int yDirection, string type, int personID)
        {
            X = x;
            Y = y;
            XDirection = xDirection;
            YDirection = yDirection;
            Type = type;
            PersonID = personID;

        }



    }
}
