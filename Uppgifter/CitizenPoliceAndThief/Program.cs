using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CitizenPoliceAndThief
{

    class Program
    {

        public static List<Thief> ThiefsInPrizon = new List<Thief>();
        public static List<Person> persons = AddPersonToCity();
        static void Main(string[] args)
        {
            int ThiefRobbedCitizens = 0;
            BelongingsOfCitizens();
            ThiefRobbedCitizens = CheckingAllTheFactsInCity(ThiefRobbedCitizens);
        }

        private static int CheckingAllTheFactsInCity(int ThiefRobbedCitizens)
        {
            while (true)
            {
                PlotPersons(persons);
                PrintSummery(ThiefRobbedCitizens);
                foreach (var curentPerson in persons)
                {

                    foreach (var person in persons)
                    {

                        if (curentPerson.PersonID != person.PersonID && person.InPrison != true && curentPerson.InPrison != true)
                        {
                            if ((curentPerson.Type == "Citizen" && person.Type == "Thief") ||
                            (curentPerson.Type == "Thief" && person.Type == "Citizen") ||
                            (curentPerson.Type == "Thief" && person.Type == "Police") ||
                            (curentPerson.Type == "Police" && person.Type == "Thief"))
                            {

                                if (curentPerson.X == person.X && curentPerson.Y == person.Y)
                                {
                                    if (curentPerson.Type == "Thief" && person.Type == "Police")
                                    {
                                        Console.SetCursorPosition(0, 28);
                                        Console.WriteLine("Police caught a Thief.");
                                        Thread.Sleep(700);
                                        ((Police)person).RecoveredGoods = ((Thief)curentPerson).StolenGoods;
                                        ((Thief)curentPerson).StolenGoods.Clear();
                                        ThiefsInPrizon.Add((Thief)curentPerson);
                                        curentPerson.InPrison = true;
                                        var t = (Thief)curentPerson;
                                        new Thread(delegate () { TimeCounter(t); }).Start();
                                    }

                                    if (curentPerson.Type == "Citizen" && person.Type == "Thief")
                                    {
                                        Console.SetCursorPosition(0, 29);
                                        Console.WriteLine("Thief stole goods form a Citizen.");
                                        Thread.Sleep(700);
                                        ((Thief)person).StolenGoods = ((Citizen)curentPerson).Assets;
                                        ((Citizen)curentPerson).Assets.Clear();
                                        ThiefRobbedCitizens++;
                                    }
                                }
                            }
                        }
                    }
                }
                MovingEveryOneInTheCity();
            }
        }

        private static void MovingEveryOneInTheCity()
        {
            foreach (var person in persons)
            {
                Random rand = new Random();
                person.X += person.XDirection;
                person.Y += person.YDirection;

                if (person.X < 0 || person.Y < 0 || person.X >= 100 || person.Y >= 25)
                {
                    person.X = rand.Next(2, 90);
                    person.Y = rand.Next(2, 23);
                }

            }
        }

        private static void BelongingsOfCitizens()
        {
            foreach (var person in persons)
            {
                if (person.Type == "Citizen")
                {
                    ((Citizen)person).Assets.Add(new Inventory("Watch"));
                    ((Citizen)person).Assets.Add(new Inventory("Money"));
                    ((Citizen)person).Assets.Add(new Inventory("Keys"));
                    ((Citizen)person).Assets.Add(new Inventory("Mobile"));
                }
            }
        }

        private static void PrintSummery(int ThiefRobbedCitizens)
        {
            int NumberOFPolice = 0;
            int NumberOFCitizen = 0;
            int NumberOFThief = 0;
            foreach (var person in persons)
            {
                if (person is Citizen)
                {
                    NumberOFCitizen++;
                }
                if (person is Police)
                {
                    NumberOFPolice++;
                }
                if (person is Thief)
                {
                    if (person.InPrison == false)
                    {
                        NumberOFThief++;
                    }
                }
            }
            Console.SetCursorPosition(0, 25);
            Console.WriteLine($"City Z: Citizens: {NumberOFCitizen}, Polices: {NumberOFPolice}, Thieves: {NumberOFThief}");
            Console.WriteLine($"Number of robbed citizens: {ThiefRobbedCitizens}\nNumber of thieves in prison: {ThiefsInPrizon.Count}");
            Thread.Sleep(1000);
        }

        private static void PlotPersons(List<Person> persons)
        {
            Console.Clear();
            Console.SetWindowSize(100, 30);
            foreach (var person in persons)
            {
                if (person is Citizen)
                {
                    Console.SetCursorPosition(person.X, person.Y);
                    Console.Write("C");
                }
                if (person is Police)
                {
                    Console.SetCursorPosition(person.X, person.Y);
                    Console.Write("P");
                }
                if (person is Thief)
                {
                    Console.SetCursorPosition(person.X, person.Y);
                    Console.Write("T");
                }
            }
        }

        public static List<Person> AddPersonToCity()
        {

            Random rand = new Random();
            List<Person> persons = new List<Person>
            {
             new Citizen (rand.Next(1, 100), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",1),
             new Citizen (rand.Next(1, 100), rand.Next(1, 24),rand.Next(1,2),rand.Next(-1,2),"Citizen",2),
             new Citizen(rand.Next(1, 100), rand.Next(1, 24),rand.Next(1,2),rand.Next(-1,2),"Citizen",3),
             new Citizen(rand.Next(1, 90), rand.Next(1, 24),rand.Next(-1,2),rand.Next(-1,2),"Citizen",4),
             new Citizen(rand.Next(1, 100), rand.Next(1, 24),rand.Next(1,2),rand.Next(-1,2),"Citizen",5),
             new Citizen(rand.Next(1, 97), rand.Next(1, 20),rand.Next(-1,2),rand.Next(1,2),"Citizen",6),
             new Citizen(rand.Next(1, 100), rand.Next(1, 24),rand.Next(1,2),rand.Next(-1,2),"Citizen",7),
             new Citizen(rand.Next(2, 100), rand.Next(2, 24),rand.Next(-1,2),rand.Next(1,2),"Citizen",8),
             new Citizen(rand.Next(2, 100), rand.Next(2, 22),rand.Next(1,2),rand.Next(-7,7),"Citizen",9),
             new Thief(rand.Next(2, 100), rand.Next(2, 25),rand.Next(1,2),rand.Next(-3,4),"Thief",10),
             new Thief(rand.Next(2, 100), rand.Next(2, 25),rand.Next(-3,2),rand.Next(-1,2),"Thief",11),
             new Thief(rand.Next(2, 100), rand.Next(2, 25),rand.Next(-1,2),rand.Next(-3,5),"Thief",12),
             new Thief(rand.Next(2, 100), rand.Next(2, 25),rand.Next(1,2),rand.Next(-1,2),"Thief",13),
             new Thief(rand.Next(2, 100), rand.Next(2, 25),rand.Next(-1,2),rand.Next(1,2),"Thief",14),
             new Police(rand.Next(4, 100), rand.Next(1, 22),rand.Next(-1,2),rand.Next(-4,2),"Police",15),
             new Police(rand.Next(4, 90), rand.Next(1, 23),rand.Next(-1,2),rand.Next(1,2),"Police",16),
             new Police(rand.Next(4, 95), rand.Next(3, 25),rand.Next(1,2),rand.Next(-1,2),"Police",17),
             new Police(rand.Next(4, 100), rand.Next(3, 24),rand.Next(-1,2),rand.Next(1,2),"Police",18),
             new Police(rand.Next(1, 99), rand.Next(1, 24),rand.Next(1,2),rand.Next(-1,5),"Police",19),
             new Police(rand.Next(4, 100), rand.Next(3, 24),rand.Next(-1,2),rand.Next(-2,2),"Police",20),
             new Police(rand.Next(4, 100), rand.Next(3, 24),rand.Next(1,2),rand.Next(1,2),"Police",21),
             new Police(rand.Next(4, 100), rand.Next(3, 24),rand.Next(-1,5),rand.Next(1,2),"Police",22),
             new Citizen (rand.Next(1, 90), rand.Next(1, 25),rand.Next(-2,2),rand.Next(-1,2),"Citizen",23),
             new Citizen (rand.Next(1, 80), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",24),
             new Citizen (rand.Next(1, 100), rand.Next(1, 25),rand.Next(-9,2),rand.Next(-1,2),"Citizen",25),
            };
            return persons;
        }
        public static void TimeCounter(Thief p)
        {

            int timeCounter = 0;
            while (true)
            {
                Thread.Sleep(1000);
                timeCounter++;
                if (timeCounter >= 30)
                {
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine("One Thief got free from Prison with ID Number: {0}", p.PersonID);
                    Thread.Sleep(1000);
                    timeCounter = 0;
                    p.InPrison = false;
                    ThiefsInPrizon.Remove(p);
                    break;
                }
            }

        }


    }






}
