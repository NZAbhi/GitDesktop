using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPoliceAndThief
{
    class Thief : Person
    {
        public List<Inventory> StolenGoods = new List<Inventory>();
       

        public Thief(int x, int y, int xDirection, int yDirection, string type, int ID)
        {
            X = x;
            Y = y;
            XDirection = xDirection;
            YDirection = yDirection;
            Type = type;
            PersonID = ID;
            InPrison = false;


        }








    }
}
