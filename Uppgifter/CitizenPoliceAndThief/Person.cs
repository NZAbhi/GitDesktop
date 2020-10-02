using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPoliceAndThief
{
    class Person
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public string Type { get; set; }
        public int PersonID { get; set; }
        public bool InPrison { get; set; }
        public Person() { }
        public Person(int x, int y, int xDirection, int yDirection, string type, int personID)
        {
            X = x;
            Y = y;
            XDirection = xDirection;
            YDirection = yDirection;
            Type = type;
            PersonID = personID;
            InPrison = false;
        }
    }
}
