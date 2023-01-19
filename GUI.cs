using Microsoft.EntityFrameworkCore;
using SnowboardUthyrning.DatabaseData;
using SnowboardUthyrning.Helpers;
using SnowboardUthyrning.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SnowboardUthyrning
{
    internal class GUI
    {
        public static void MainMenu()
        {
            bool onGoing = true;
            int weekNr = 1;
            while (onGoing)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Available snowboards to hire ===");
                Console.ResetColor();
                var snowboards = Select.GetSnowboards();
                Console.WriteLine($"Id\tBrand\t\t   Length in cm\t\t   Difficulty\t\t   For regular rider");
                Console.WriteLine("--------------------------------------------------------------------------------------------");
                foreach (var s in snowboards)
                {
                    Console.WriteLine($"{s.Id}\t{s.Brand}\t\t   {s.Length}\t\t\t   {s.DifficultyOfBoard}\t\t   {s.IsRegular}");
                }
                Console.WriteLine();
                DrawBookingSystem(weekNr);
                Console.WriteLine();
                Console.WriteLine("---------------------------");
                Console.WriteLine("[H]ire snowboard");
                Console.WriteLine("Show [N]ext week");
                Console.WriteLine("Show [P]revious");
                Console.WriteLine("[Q]uit");
                Console.WriteLine("---------------------------");
                Console.WriteLine("[A]dmin");

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.H:
                        Insert.HireSnowboard(weekNr);
                        break;
                    case ConsoleKey.N:
                        if (weekNr < 4) {
                            weekNr++;
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("This booking app cant go above 4 weeks.");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.ResetColor();                 
                        }
                        break;
                    case ConsoleKey.P:
                        if (weekNr >= 2) {
                            weekNr--;
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("You cannot choose a week below week 1.");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                        break;
                    case ConsoleKey.A:
                        Admin.Menu(weekNr);
                        break;

                    case ConsoleKey.Q:
                        onGoing = false;
                        break;
                }
            }
        }
        public static void DrawBookingSystem(int weekNr)
        {
            using (var db = new MyDbContext())
            {
                var snowboards = db.Snowboards.Include(x => x.Bookings).ThenInclude(y => y.Customer).ToList();
                //var bookings = db.Bookings.ToList();
                foreach (Weekday weekday in Enum.GetValues(typeof(Weekday)))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"\t  {((int)weekday)}. {weekday}");
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Week: " + weekNr);
                Console.ResetColor();
                for (int i = 0; i < snowboards.Count; i++)
                {
                    Console.Write(snowboards[i].Brand);
                    foreach (Weekday weekday in Enum.GetValues(typeof(Weekday)))
                    {
                        Booking? booking = snowboards[i].Bookings.Where(booking => booking.Weekday.Equals(weekday) && booking.Week == weekNr).FirstOrDefault();
                        if (booking != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"\t  {(booking == null ? "---" :  booking.Customer.Name)}\t");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"\t  {(booking == null ? "---" : booking.Customer.Name)}\t");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }
}
