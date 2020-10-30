using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Harbor
{
    class Program
    {
        static void Main(string[] args)
        {

            Boat[] boatsArray = new Boat[20];
            bool addDone = false;

            while (addDone == false)
            {
                try
                {
                    if (ParkingFullOrNot(boatsArray) == false)
                    {
                        AddBoatsToParking(boatsArray, RandomBoat());
                        Quires(boatsArray);  
                        NextDay(boatsArray);
                        Quires(boatsArray);

                        if (ParkingFullOrNot(boatsArray) == true)
                        {
                            addDone = true;
                            Console.WriteLine("Parking Full Today: Come Tomorrow:!");
                            ViewParkedBoats(boatsArray);
                            NextDay(boatsArray);

                            Console.ReadLine();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Parking Full");
                    ViewParkedBoats(boatsArray);
                }


                Console.Clear();

            }


        }


        private static void AddBoatsToParking(Boat[] boatsArray, List<BoatType> waitingBoats)
        {


            int emptyParking = 0;
            for (int i = 0; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] == null)
                    emptyParking++;
            }

            Console.Write($"Waiting boats today: ");
            //int x = 1;
            foreach (var boat in waitingBoats)
            {
                Console.Write(boat + ", ");
            }
            Console.WriteLine();
            ViewParkedBoats(boatsArray);

            Console.Write($"Press [A] to Add ");
            Console.WriteLine();
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'a' && emptyParking != 0)
            {
                foreach (var boat in waitingBoats)
                {                        //Question

                    if (boat == BoatType.RowingBoat && emptyParking >= 1) //Rowing Boat
                    {
                        int i = 0;
                        for (; i < boatsArray.Length; i++)
                        {
                            if (boatsArray[i] == null)
                            {
                                break;
                            }
                        }
                        boatsArray[i] = new RowingBoat();
                        emptyParking -= 1;
                        ParkingFullOrNot(boatsArray);
                    }
                    else if (boat == BoatType.MotorBoat && emptyParking >= 1)//Motor Boat
                    {
                        int i = 0;
                        for (; i < boatsArray.Length; i++)
                        {
                            if (boatsArray[i] == null)
                            {
                                break;
                            }
                        }
                        boatsArray[i] = new MotorBoat();
                        emptyParking -= 1;
                        ParkingFullOrNot(boatsArray);

                    }
                    else if (boat == BoatType.SailBoat && emptyParking >= 2)  //Sail Boat
                    {
                        int i = 0;
                        for (; i < boatsArray.Length; i++)
                        {
                            if (boatsArray[i] == null && boatsArray[i + 1] == null)
                            {
                                break;
                            }

                        }
                        //Question

                        var sailBoat = new SailBoat();
                        boatsArray[i] = boatsArray[i + 1] = sailBoat;
                       // boatsArray[i + 1] = sailBoat;
                        emptyParking -= 2;
                        ParkingFullOrNot(boatsArray);

                    }
                    else if (boat == BoatType.CargoShip && emptyParking >= 4)  //Cargo Boat
                    {
                        int i = 0;
                        for (; i < boatsArray.Length; i++)
                        {
                            if (boatsArray[i] == null && boatsArray[i + 1] == null && boatsArray[i + 2] == null && boatsArray[i + 3] == null)
                            {
                                break;
                            }
                        }
                        //Question
                        var cargoShip = new CargoShip();
                        boatsArray[i] = boatsArray[i + 1] = boatsArray[i + 2] = boatsArray[i + 3] = cargoShip;
                        //boatsArray[i + 1] = cargoShip;
                        //boatsArray[i + 2] = cargoShip;
                        //boatsArray[i + 3] = cargoShip;
                        emptyParking -= 4;
                        ParkingFullOrNot(boatsArray);

                    }

                }
            }
            ViewParkedBoats(boatsArray);
        }

        private static void NextDay(Boat[] boatsArray)
        {
            int emptyParking = 0;
            for (int i = 0; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] == null)
                    emptyParking++;
            }
            for (int i = 0; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] != null)
                {
                    boatsArray[i].DaysCout--;
                    if (boatsArray[i].DaysCout <=0 )
                    {
                        boatsArray[i] = null;
                    }
                }
            }

            Console.WriteLine("Press [N] for next day");
            ConsoleKeyInfo key2 = Console.ReadKey();
            Console.WriteLine();
            if (key2.KeyChar == 'a' && emptyParking != 0)
            {
                AddBoatsToParking(boatsArray, RandomBoat());
              
            }
        }

        private static bool ParkingFullOrNot(Boat[] boatsArray)
        {

            int arrayCount = 0;
            for (int i = 0; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] == null)
                    break;
                else
                    arrayCount++;
            }

            if (arrayCount == boatsArray.Length)

                return true;

            else
                return false;
        }

        private static void AddToFile(Boat[] boatsArray)
        {
            using (StreamWriter sw = new StreamWriter("ParkedBoats.txt", false))
            {
                //string text = File.ReadAllText("ParkedBoats.txt");
                //Console.WriteLine(text);
                //Console.ReadLine();
                //Console.WriteLine($"TotalWeightInHarbor: {TotalWeightInHarbor}, AverageOfTheMaximumSpeed  ");
                foreach (var boat in boatsArray)
                {
                    if (boat is RowingBoat)
                    {
                        sw.WriteLine($"{boat.GetType().Name} => IdentityNumber = {boat.IdentityNumber} Weight = {boat.Weight} MaximumSpeed = {boat.MaximumSpeed} MaximumNumberOfPassengers = {((RowingBoat)boat).MaximumNumberOfPassengers}");
                    }
                    else if (boat is MotorBoat)
                    {
                        sw.WriteLine($"{boat.GetType().Name} => IdentityNumber = {boat.IdentityNumber} Weight = {boat.Weight} MaximumSpeed = {boat.MaximumSpeed} NumberOfHorsepower = {((MotorBoat)boat).NumberOfHorsepower}");
                    }
                    else if (boat is SailBoat)
                    {
                        sw.WriteLine($"{boat.GetType().Name} => IdentityNumber = {boat.IdentityNumber} Weight = {boat.Weight} MaximumSpeed = {boat.MaximumSpeed} BoatLength = {((SailBoat)boat).BoatLength}");
                    }
                    else if (boat is CargoShip)
                    {
                        sw.WriteLine($"{boat.GetType().Name} => IdentityNumber = {boat.IdentityNumber} Weight = {boat.Weight} MaximumSpeed = {boat.MaximumSpeed} NumberOfContainers = {((CargoShip)boat).NumberOfContainers}");
                    }
                }

            }
        }

        private static void Quires(Boat[] boatsArray)
        {
            Console.WriteLine();
            Console.WriteLine("....................................................................................................................");

            var TotalBoats = boatsArray
                             .Where(t => t != null)
                             .GroupBy(t => t.BoatType);

            foreach (var boats in TotalBoats)
            {
                
                Console.Write($"{boats.Key}: {boats.Count()}\t");
            }

            var Weights = boatsArray
                           .Where(w => w != null)
                           .Select(w => w.Weight)
                           .Sum();
            Console.Write($"Total Weights:{Weights}\t");
            var AverageSpeed = boatsArray
                         .Where(a => a != null)
                         .Select(a => a.MaximumSpeed)
                         .Average();
            Console.Write($"Total Weights:{Weights}\t");

            int EmptyParking = 0;
            for (int i = 0; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] == null)
                    EmptyParking++;
            }
            Console.Write($"EmptyParking: {EmptyParking}\t");
            Console.WriteLine();
            Console.WriteLine("....................................................................................................................");

        }

        private static void ViewParkedBoats(Boat[] boatsArray)
        {
            Console.WriteLine("P[N]--- BoatType....IdentityNumber.....Weight..... MaxSpeed......Others.........DaysIntheDock");
            Console.WriteLine();
            int i = 0;
            for (; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] is RowingBoat)
                {
                    Console.WriteLine($"{ i + 1}---{ boatsArray[i].GetType().Name}     { boatsArray[i].IdentityNumber}     { boatsArray[i].Weight}kg," +
                        $"     { boatsArray[i].MaximumSpeed}knots,     MaximumNumberOfPassengers-{((RowingBoat)boatsArray[i]).MaximumNumberOfPassengers},     { boatsArray[i].DaysCout}D");
                }
                else if (boatsArray[i] is MotorBoat)
                {
                    Console.WriteLine($"{ i + 1}---{ boatsArray[i].GetType().Name}     { boatsArray[i].IdentityNumber}     { boatsArray[i].Weight}kg, " +
                        $"    { boatsArray[i].MaximumSpeed}knots,     NumberOfHorsepower-{((MotorBoat)boatsArray[i]).NumberOfHorsepower}hp,       { boatsArray[i].DaysCout}D");
                }
                else if (boatsArray[i] is SailBoat)
                {
                    Console.WriteLine($"{ i + 1}---{ boatsArray[i].GetType().Name}     { boatsArray[i].IdentityNumber}     { boatsArray[i].Weight}kg, " +
                        $"    { boatsArray[i].MaximumSpeed}knots,     BoatLength-{((SailBoat)boatsArray[i]).BoatLength}feet,              { boatsArray[i].DaysCout}D");
                }
                else if (boatsArray[i] is CargoShip)
                {
                    Console.WriteLine($"{ i + 1}---{ boatsArray[i].GetType().Name}     { boatsArray[i].IdentityNumber}     { boatsArray[i].Weight}kg, " +
                        $"    { boatsArray[i].MaximumSpeed}knots,     NumberOfContainers-{((CargoShip)boatsArray[i]).NumberOfContainers},      { boatsArray[i].DaysCout}D");
                }
                else if (boatsArray[i] == null)
                    Console.WriteLine($"{ i + 1}   ----    ---Empty---     -------     -------      -------   -------    ");
            }


        }


        public enum BoatType { RowingBoat = 1, MotorBoat, SailBoat, CargoShip }
        public static List<BoatType> RandomBoat()
        {



            List<BoatType> randomBoats = new List<BoatType>();
            for (int i = 1; i < 6; i++)
            {
                var boatType = new Random();
                var randomBoat = (BoatType)boatType.Next(1, 5);
                randomBoats.Add(randomBoat);
            }

            return randomBoats;

        }

    }
}
