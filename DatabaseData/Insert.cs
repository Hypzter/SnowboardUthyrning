using Microsoft.Data.SqlClient;
using SnowboardUthyrning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SnowboardUthyrning.Helpers;

namespace SnowboardUthyrning.DatabaseData
{
    internal class Insert
    {
        public static void NewSnowboard()
        {
            Console.Write("Brand: ");
            var brand = Console.ReadLine();
            Console.Write("Length of the snowboard in cm: ");
            var boardLength = Helpers.CheckInputValidation.CheckForDigits();
            Console.Write("Difficulty of the snowboard, Rookie/Veteran/Professional: ");
            var difficulty = Console.ReadLine();
            Console.Write("Is the snowboard Regular? Please answer true or false. ");
            var isRegular = Convert.ToBoolean(Console.ReadLine());

            using (var db = new MyDbContext())
            {
                var newSnowboard = new Snowboard
                {
                    Brand = brand,
                    Length = boardLength,
                    DifficultyOfBoard = difficulty,
                    IsRegular = isRegular
                };
                db.Add(newSnowboard);
                db.SaveChanges();
            }

        }
        public static void HireSnowboard(int weekNr)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Choose a day (1-7), where 1 is monday etc: ");
            int day = CheckInputValidation.CheckForDigits();
            Console.Write("Choose a snowboard by its Id: ");

            using (var db = new MyDbContext())
            {
                var snowboards = db.Snowboards.Include(x => x.Bookings).ToList();
                var snowboardId = CheckInputValidation.CheckForDigits();
                var booking = db.Bookings.Where(x => x.Weekday.Equals((Weekday)day) && x.Week.Equals(weekNr) && x.Snowboard.Id.Equals(snowboardId)).ToList().Any();

                if (booking == false)
                {

                    var newCustomer = new Customer()
                    {
                        Name = name
                    };
                    db.Add(newCustomer);
                    var newBooking = new Booking()
                    {
                        Weekday = (Weekday)day,
                        Snowboard = snowboards.Where(s => s.Id == snowboardId).FirstOrDefault(),
                        Customer = newCustomer,
                        Week = weekNr
                    };
                    db.Add(newBooking);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Sorry this snowboard is already booked this day.");
                    Console.WriteLine("Please press any key to continue");
                    Console.ReadKey();

                }
            }
        }
    }
}