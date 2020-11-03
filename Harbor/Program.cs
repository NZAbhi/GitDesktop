using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using IronXL;

namespace Harbor
{
    class Program
    {
        //Please Run the program and check what happen first!!!!!!

        public static double emptyParking = 0;
        public static int refusedToDock = 0;
        public static bool addBoatsToDock = true;
        public static Boat[] dockedBoats = new Boat[20];
        public static Dictionary<int, string> harbour = new Dictionary<int, string>();
        static void Main(string[] args)
        {
           // Tried to read the Ecel File that saves the info but that did not work 
            // YesterdaysBoats();
            GetHarbourStatus();
            GetParkingStatus();
            ViewParkedBoats();

            while (addBoatsToDock == true)
            {
                try
                {
                    if (ParkingAvailable())
                    {
                        AddBoatsToDock(RandomBoat());
                        ViewParkedBoats();
                        NextDay();
                        ViewParkedBoats();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Parking Full");
                    ViewParkedBoats();
                }
            }


        }
//Creating empty places for the dictionay as the array size,
        private static void GetHarbourStatus()
        {
            Console.WriteLine($"The Hourbour has 20 docking positions:");
            for (int i = 0; i < dockedBoats.Length; i++)
            {
                harbour.Add(i, null);
            }
        }
        // counting how many empty parking is Available
        private static void GetParkingStatus()
        {

            for (int i = 0; i < harbour.Count; i++)
            {
                if (harbour[i] == null)
                    emptyParking++;
            }
        }


        private static void AddBoatsToDock(List<Boat> waitingBoats)
        {
            Console.WriteLine("....................................................................................................................................");
            Console.Write($"Waiting boats today: ");
            foreach (var boat in waitingBoats)
            {
                Console.Write(boat.BoatType + ", ");
            }
            Console.WriteLine();
            Console.WriteLine("....................................................................................................................................");
            Console.Write($"Press [A] to Add and [Q] to Quit ");
            Console.WriteLine();
            ConsoleKeyInfo key = Console.ReadKey();

            int dockedCount = 0;
            if (key.KeyChar == 'a' && emptyParking != 0)
            {
                foreach (var boat in waitingBoats)
                {
                    //How to save 2Rowingboat in one place?? Could not fix.
                    if (boat.BoatType.Equals(BoatType.RowingBoat.ToString()) && emptyParking >= .5) //Rowing Boat
                    {
                        int i = 0;
                        for (; i < harbour.Count; i++)
                        {
                            //if find any empty place break the loop and put the boat in the place
                            if (harbour[i] == null)
                            {
                                break;
                            }
                        }
                        //Have to put 2Rowingboats ????How
                        harbour[i] = boat.IdentityNumber;
                        dockedBoats[i] = boat;
                        emptyParking -= 0.5;
                        dockedCount++;
                    }
                    else if (boat.BoatType.Equals(BoatType.MotorBoat.ToString()) && emptyParking >= 1)//Motor Boat
                    {
                        int i = 0;
                        for (; i < harbour.Count; i++)
                        {
                            if (harbour[i] == null)
                            {
                                break;
                            }
                        }
                        harbour[i] = boat.IdentityNumber;
                        dockedBoats[i] = boat;
                        emptyParking -= 1;
                        dockedCount++;
                    }
                    else if (boat.BoatType.Equals(BoatType.SailBoat.ToString()) && emptyParking >= 2)  //Sail Boat
                    {
                        int i = 0;
                        for (; i < harbour.Count; i++)
                        {
                            if (harbour[i] == null && harbour[i + 1] == null)
                            {
                                break;
                            }
                        }
                        //saving a key and only  IdentityNumber in harbour dictionary, in 2places for a sailBoat
                        harbour[i] = boat.IdentityNumber;
                        harbour[i + 1] = boat.IdentityNumber;
                        //saving in a only 1 place for the sailBoats instead of 2places for a sailBoat in the array
                        dockedBoats[i] = boat;
                        emptyParking -= 2;
                        dockedCount++;
                    }
                    //checking 
                    else if (boat.BoatType.Equals(BoatType.CargoShip.ToString()) && emptyParking >= 4)  //Cargo Boat
                    {
                        int i = 0;
                        for (; i < harbour.Count; i++)
                        {
                            //checking 4places in a row or not 
                            if (harbour[i] == null && harbour[i + 1] == null && harbour[i + 2] == null && harbour[i + 3] == null)
                            {
                                break;
                            }
                        }
                        //saving a key and only  IdentityNumber in harbour dictionary, in 4places for a cargo
                        harbour[i] = boat.IdentityNumber;
                        harbour[i + 1] = boat.IdentityNumber;
                        harbour[i + 2] = boat.IdentityNumber;
                        harbour[i + 3] = boat.IdentityNumber;
                        //saving in a only 1 place for the cargo instead of 4places for a cargo in the array
                        dockedBoats[i] = boat;
                        emptyParking -= 4;
                        dockedCount++;
                    }
                }
                refusedToDock += (waitingBoats.Count - dockedCount);
            }
            else if (!ParkingAvailable() || emptyParking == 0)
            {
                Console.WriteLine("Parking Full Today: Come Tomorrow:!");
                ViewParkedBoats();
                Console.WriteLine("Press [N] to Next Day ");
                ConsoleKeyInfo key2 = Console.ReadKey();
                if (key2.KeyChar == 'n')
                {
                    NextDay();
                }
            }
            else if (key.KeyChar == 'q')
            {
                //creating Excel File to save all the info

                CreatSpreadsheet();
                Environment.Exit(0);
            }


        }
        //try to read the Excel file but how not do it,have to read the Excel first and save back the data from excel to array.???? 
        private static void YesterdaysBoats()
        {
            //tried diffrent approche to read the Excel file what was save before but Did not work...

            string filePath = @"‪C:\Visual Stadio\Harbor\DockedBoatsInHarbor.xlsx";
            //Application excelFile = new Application();
            //Workbook excelBook = excelFile.Workbooks.Open(filePath);
            //Worksheet excelSheet = (Worksheet)excelBook.Worksheets[1];
            //Range excelRange = excelSheet.UsedRange;

            //for (int row = 1; row < excelRange.Rows.Count; row++)
            //{
            //    for (int col = 1; col < excelRange.Columns.Count; col++)
            //    {
            //        Console.WriteLine(excelSheet.Cells[row,col]);
            //    }

            //}
            var excelBook = new WorkBook("DockedBoatsInHarbor.xlsx");
            var excelSheet = excelBook.GetWorkSheet("WorkSheet");
            var range = excelSheet.GetRange("B2:B66");
            foreach (var cell in range)
            {
                Console.WriteLine(cell.Value);
            }





        }
        //Creating a Excel File when quit the program,I works fine, saves the file as aspected
        private static void CreatSpreadsheet()
        {
            string spreadsheetPath = @"C:\Visual Stadio\Harbor\DockedBoatsInHarbor.xlsx";
            File.Delete(spreadsheetPath);
            FileInfo spreadsheetInfo = new FileInfo(spreadsheetPath);

            ExcelPackage package = new ExcelPackage(spreadsheetInfo);
            var dockedBoatsWorksheets = package.Workbook.Worksheets.Add("DockedBoats");
            dockedBoatsWorksheets.Cells["A1"].Value = "PerkingNumber";
            dockedBoatsWorksheets.Cells["B1"].Value = "BoatType";
            dockedBoatsWorksheets.Cells["C1"].Value = "IdentityNumber";
            dockedBoatsWorksheets.Cells["D1"].Value = "Weight";
            dockedBoatsWorksheets.Cells["E1"].Value = "MaxSpeed";
            dockedBoatsWorksheets.Cells["F1"].Value = "Others";
            dockedBoatsWorksheets.Cells["G1"].Value = "DaysIntheDock";

            dockedBoatsWorksheets.Cells["A1:G1"].Style.Font.Bold = true;

            int currentRow = 2;
            //var docknumber = string.Empty;
            //for (int i = 0; i < dockedBoats.Length; i++)
            //{
            //    if (dockedBoats[i] != null)
            //    {
            //        docknumber = GetDockNumber(dockedBoats[i].IdentityNumber);
            //    }
            //}
            for (int i = 0; i < dockedBoats.Length; i++)
            {
                if (dockedBoats[i] != null)
                {

                    dockedBoatsWorksheets.Cells["A" + currentRow.ToString()].Value = i;
                    dockedBoatsWorksheets.Cells["B" + currentRow.ToString()].Value = dockedBoats[i].BoatType;
                    dockedBoatsWorksheets.Cells["C" + currentRow.ToString()].Value = dockedBoats[i].IdentityNumber;
                    dockedBoatsWorksheets.Cells["D" + currentRow.ToString()].Value = dockedBoats[i].Weight;
                    dockedBoatsWorksheets.Cells["E" + currentRow.ToString()].Value = dockedBoats[i].MaximumSpeed;
                    if (dockedBoats[i] is RowingBoat)
                    {
                        dockedBoatsWorksheets.Cells["F" + currentRow.ToString()].Value = ((RowingBoat)dockedBoats[i]).MaximumNumberOfPassengers;

                    }
                    else if (dockedBoats[i] is MotorBoat)
                    {
                        dockedBoatsWorksheets.Cells["F" + currentRow.ToString()].Value = ((MotorBoat)dockedBoats[i]).NumberOfHorsepower;

                    }
                    else if (dockedBoats[i] is SailBoat)
                    {
                        dockedBoatsWorksheets.Cells["F" + currentRow.ToString()].Value = ((SailBoat)dockedBoats[i]).BoatLength;

                    }
                    else if (dockedBoats[i] is CargoShip)
                    {
                        dockedBoatsWorksheets.Cells["F" + currentRow.ToString()].Value = ((CargoShip)dockedBoats[i]).NumberOfContainers;

                    }
                    dockedBoatsWorksheets.Cells["G" + currentRow.ToString()].Value = dockedBoats[i].DaysCout;
                    currentRow++;


                }
            }

            dockedBoatsWorksheets.View.FreezePanes(2, 1);

            package.Save();
        }

        private static void NextDay()
        {
            // have Problem look inside.....!! 
            Console.WriteLine("....................................................................................................................................");
            Console.WriteLine("Press [N] next Day and [Q] to Quit ");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'n')
            {
                for (int i = 0; i < dockedBoats.Length; i++)
                {
                    if (dockedBoats[i] != null)
                    {
                        if (dockedBoats[i].DaysCout > 0)
                        {
                            dockedBoats[i].DaysCout--;
                            if (dockedBoats[i].DaysCout == 0)
                            {
                                if (dockedBoats[i].BoatType == "RowingBoat")
                                {
                                    emptyParking += 0.5;

                                }
                                else
                                {
                                    emptyParking++;
                                }
                                if (dockedBoats[i] != null)
                                {
                                    List<KeyValuePair<int, string>> matches = harbour.Where(x => x.Value == dockedBoats[i].IdentityNumber).ToList();
                                    
                                    foreach (KeyValuePair<int, string> match in matches)
                                    {
                                        // when boats daycount is 0,removing the boats from the parking and tried to set back the place empty and free??
                                        //but only one place is coming back not others,like cargoship take 4places but when cargoship leave the place ??
                                        // it only gives back 1 place , same with SailBoats.
                                        harbour.Remove(match.Key);
                                        if (match.Value.StartsWith("R"))
                                        {
                                            harbour.Add(i, null);
                                        }
                                        else if (match.Value.StartsWith("M"))
                                        {
                                            harbour.Add(i, null);
                                        }
                                        else if (match.Value.StartsWith("S"))
                                        {
                                            harbour.Add(i, null);
                                            harbour.Add(i + 1, null);
                                        }
                                        else if (match.Value.StartsWith("C"))
                                        {
                                            harbour.Add(i, null);
                                            harbour.Add(i + 1, null);
                                            harbour.Add(i + 2, null);
                                            harbour.Add(i + 3, null);
                                        }


                                    }
                                }
                                dockedBoats[i] = null;

                            }
                        }
                    }

                }
            }
            else if (key.KeyChar == 'q')
            {
                //creating Excel File to save all the info
                CreatSpreadsheet();
                addBoatsToDock = false;
            }

        }
        //checking the parking is full or not
        private static bool ParkingAvailable()
        {
            for (int i = 0; i < dockedBoats.Length; i++)
            {
                if (dockedBoats[i] == null)
                    return true;
            }
            return false;
        }
        //not using that, frist I tried to save all the info in a text file,but thought Excel would be bater, so I am using the Excel.
        private static void AddToFile(Boat[] dockedBoats)
        {
            using (StreamWriter sw = new StreamWriter("ParkedBoats.txt", false))
            {
                //string text = File.ReadAllText("ParkedBoats.txt");
                //Console.WriteLine(text);
                //Console.ReadLine();
                //Console.WriteLine($"TotalWeightInHarbor: {TotalWeightInHarbor}, AverageOfTheMaximumSpeed  ");
                foreach (var boat in dockedBoats)
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
        // not using that either..
        private static void Quires()
        {
            Console.WriteLine();
            Console.WriteLine("....................................................................................................................");

            var TotalBoats = dockedBoats
                             .Where(t => t != null)
                             .GroupBy(t => t.BoatType);

            foreach (var boats in TotalBoats)
            {

                Console.Write($"{boats.Key}: {boats.Count()}\t");
            }

            var Weights = dockedBoats
                           .Where(w => w != null)
                           .Select(w => w.Weight)
                           .Sum();
            Console.Write($"Total Weights:{Weights}\t");
            var AverageSpeed = dockedBoats
                         .Where(a => a != null)
                         .Select(a => a.MaximumSpeed)
                         .Average();
            Console.Write($"Total Weights:{Weights}\t");

            int EmptyParking = 0;
            for (int i = 0; i < dockedBoats.Length; i++)
            {
                if (dockedBoats[i] == null)
                    EmptyParking++;
            }
            Console.Write($"EmptyParking: {EmptyParking}\t");
            Console.WriteLine();
            Console.WriteLine("....................................................................................................................");

        }
        // Printing all the information of the program,
        private static void ViewParkedBoats()
        {
            if (emptyParking < dockedBoats.Length)
            {
                Console.WriteLine();
                Console.WriteLine("....................................................................................................................................");

                var TotalBoats = dockedBoats
                                 .Where(t => t != null)
                                 .GroupBy(t => t.BoatType);

                foreach (var boats in TotalBoats)
                {

                    Console.Write($"{boats.Key}: {boats.Count()}\t");
                }

                var Weights = dockedBoats
                               .Where(w => w != null)
                               .Select(w => w.Weight)
                               .Sum();
                Console.Write($"Total Weights:{Weights}\t");
                var AverageSpeed = dockedBoats
                             .Where(a => a != null)
                             .Select(a => a.MaximumSpeed)
                             .Average();
                Console.Write($"AverageSpeed:{AverageSpeed}\t ");
                Console.Write($"EmptyParking: {emptyParking}\t");
                if (refusedToDock != 0)
                {
                    Console.Write($"RefusedBoats: {refusedToDock}\t");

                }
                Console.WriteLine();
                Console.WriteLine("....................................................................................................................................");


            }
            //int i = 0;
            //for (; i < dockedBoats.Length; i++)
            //{
            //    var docknumber = GetDockNumber(dockedBoats[i].IdentityNumber);

            //    if (dockedBoats[i] is RowingBoat)
            //    {
            //        Console.WriteLine($"{docknumber} -- -{ dockedBoats[i].GetType().Name}     { dockedBoats[i].IdentityNumber}     { dockedBoats[i].Weight}kg," +
            //            $"     { dockedBoats[i].MaximumSpeed}knots,     MaximumNumberOfPassengers-{((RowingBoat)dockedBoats[i]).MaximumNumberOfPassengers},     { dockedBoats[i].DaysCout}D");
            //    }
            //    else if (dockedBoats[i] is MotorBoat)
            //    {
            //        Console.WriteLine($"{ docknumber}---{ dockedBoats[i].GetType().Name}     { dockedBoats[i].IdentityNumber}     { dockedBoats[i].Weight}kg, " +
            //            $"    { dockedBoats[i].MaximumSpeed}knots,     NumberOfHorsepower-{((MotorBoat)dockedBoats[i]).NumberOfHorsepower}hp,       { dockedBoats[i].DaysCout}D");
            //    }
            //    else if (dockedBoats[i] is SailBoat)
            //    {
            //        Console.WriteLine($"{ docknumber}---{ dockedBoats[i].GetType().Name}     { dockedBoats[i].IdentityNumber}     { dockedBoats[i].Weight}kg, " +
            //            $"    { dockedBoats[i].MaximumSpeed}knots,     BoatLength-{((SailBoat)dockedBoats[i]).BoatLength}feet,              { dockedBoats[i].DaysCout}D");
            //    }
            //    else if (dockedBoats[i] is CargoShip)
            //    {
            //        Console.WriteLine($"{ docknumber}---{ dockedBoats[i].GetType().Name}     { dockedBoats[i].IdentityNumber}     { dockedBoats[i].Weight}kg, " +
            //            $"    { dockedBoats[i].MaximumSpeed}knots,     NumberOfContainers-{((CargoShip)dockedBoats[i]).NumberOfContainers},      { dockedBoats[i].DaysCout}D");
            //    }
            //    //else if (dockedBoats[i] == null)
            //    //    Console.WriteLine($"{ i + 1}   ----    ---Empty---     -------     -------      -------   -------    ");
            //}
            List<string> printedList = new List<string>();
            int j = 0;
            Console.WriteLine("P[N] \t  \t BoatType \t IdentityNumber\t Weight \t MaxSpeed \t  Others \t \t \t  DaysIntheDock");
            foreach (KeyValuePair<int, string> pair in harbour)
            {
                if (pair.Value == null)
                {
                    Console.WriteLine($"{ pair.Key}   ------    -----Empty-----     ---------      ---------       ---------      ---------    ");
                }
                else
                {
                    for (int i = 0; i < dockedBoats.Length; i++)
                    {
                        if (dockedBoats[i] != null)
                        {
                            var docknumber = GetDockNumber(dockedBoats[i].IdentityNumber);

                            if (dockedBoats[i].IdentityNumber.Equals(pair.Value) && !printedList.Contains(dockedBoats[i].IdentityNumber))
                            {
                                if (dockedBoats[i] is RowingBoat)
                                {
                                    Console.WriteLine($"{docknumber} \t  \t { dockedBoats[i].BoatType} \t { dockedBoats[i].IdentityNumber} \t {dockedBoats[i].Weight}kg," +
                                        $" \t { dockedBoats[i].MaximumSpeed}knots, \t MaximumNumberOfPassengers-{((RowingBoat)dockedBoats[i]).MaximumNumberOfPassengers}, \t\t{dockedBoats[i].DaysCout}D");
                                    printedList.Add(dockedBoats[i].IdentityNumber);
                                }
                                else if (dockedBoats[i] is MotorBoat)
                                {
                                    Console.WriteLine($"{docknumber} \t  \t { dockedBoats[i].BoatType} \t { dockedBoats[i].IdentityNumber} \t {dockedBoats[i].Weight}kg, " +
                                        $" \t { dockedBoats[i].MaximumSpeed}knots, \t NumberOfHorsepower-{((MotorBoat)dockedBoats[i]).NumberOfHorsepower}hp, \t\t{dockedBoats[i].DaysCout}D");
                                    printedList.Add(dockedBoats[i].IdentityNumber);
                                }
                                else if (dockedBoats[i] is SailBoat)
                                {
                                    Console.WriteLine($"{docknumber} \t  \t { dockedBoats[i].BoatType} \t { dockedBoats[i].IdentityNumber} \t {dockedBoats[i].Weight}kg, " +
                                        $" \t { dockedBoats[i].MaximumSpeed}knots, \t BoatLength-{((SailBoat)dockedBoats[i]).BoatLength}feet, \t\t\t{dockedBoats[i].DaysCout}D");
                                    printedList.Add(dockedBoats[i].IdentityNumber);
                                }
                                else if (dockedBoats[i] is CargoShip)
                                {
                                    Console.WriteLine($"{docknumber}  \t { dockedBoats[i].BoatType} \t { dockedBoats[i].IdentityNumber} \t {dockedBoats[i].Weight}kg, " +
                                        $" \t { dockedBoats[i].MaximumSpeed}knots, \t NumberOfContainers-{((CargoShip)dockedBoats[i]).NumberOfContainers}, \t\t {dockedBoats[i].DaysCout}D");
                                    printedList.Add(dockedBoats[i].IdentityNumber);
                                }
                            }
                        }

                    }
                }

            }


        }
        //checking in the dictionary,how many boats are thre withe the save identityNumber,and make it like 2,3,4,5--- Cargoship.
        private static string GetDockNumber(string identityNumber)
        {
            StringBuilder dockNumber = new StringBuilder();

            foreach (KeyValuePair<int, string> pair in harbour)
            {
                if (pair.Value != null && pair.Value.Equals(identityNumber))
                {
                    dockNumber.Append(pair.Key);
                    dockNumber.Append(',');
                }
            }
            string result = dockNumber.ToString();
            int lastIndex = result.LastIndexOf(',');

            if (lastIndex >= 0)
            {
                result = result.Substring(0, lastIndex);
            }
            return result;
        }
        //creating 5random boats every day.
        public enum BoatType { RowingBoat = 1, MotorBoat, SailBoat, CargoShip }
        public static List<Boat> RandomBoat()
        {
            List<Boat> randomBoats = new List<Boat>();
            for (int i = 1; i < 6; i++)
            {
                var boatType = new Random();
                var randomBoat = (BoatType)boatType.Next(1, 5);
                switch (randomBoat)
                {
                    //Using that approche because the other abstract class system checnges all the info all the time in every places continiously.........
                    case BoatType.RowingBoat:
                        randomBoats.Add(new RowingBoat("RowingBoat", $"R-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(100, 300), Utilities.GetRandomNumber(1, 3), 1, Utilities.GetRandomNumber(2, 6)));
                        break;
                    case BoatType.MotorBoat:
                        randomBoats.Add(new MotorBoat("MotorBoat", $"M-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(200, 3000), Utilities.GetRandomNumber(20, 60), 3, Utilities.GetRandomNumber(100, 1000)));
                        break;
                    case BoatType.SailBoat:
                        randomBoats.Add(new SailBoat("SailBoat", $"S-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(1000, 6000), Utilities.GetRandomNumber(6, 12), 4, Utilities.GetRandomNumber(10, 60)));
                        break;
                    case BoatType.CargoShip:
                        randomBoats.Add(new CargoShip("CargoShip", $"C-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(3000, 20000), Utilities.GetRandomNumber(3, 20), 6, Utilities.GetRandomNumber(50, 500)));
                        break;
                    default:
                        break;
                }
            }

            return randomBoats;
        }

    }
}
