using Microsoft.Data.SqlClient;
using SnowboardUthyrning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Runtime.ConstrainedExecution;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using SnowboardUthyrning.Helpers;

namespace SnowboardUthyrning.DatabaseData
{
    internal class Select
    {
        public static List<Snowboard> GetSnowboards()
        {
            using (var db = new MyDbContext())
            {
                var snowboards = db.Snowboards;
                return snowboards.ToList();
            }
        }
        public static void MostBookedSnowboard()
        {
            using (var db = new MyDbContext())
            {
                var snowboards = db.Snowboards.Include(x => x.Bookings).ThenInclude(y => y.Customer).ToList();

                var result = snowboards.OrderByDescending(x => x.Bookings.Count).ToList();

                foreach (var s in result)
                {
                    Console.WriteLine(s.Brand + " has been booked " + s.Bookings.Count + " times.");
                }
            }
        }
        public static void MostOccupiedWeek()
        {
            using (var db = new MyDbContext())
            {
                var bookings = db.Bookings.ToList();
                var snowboards = db.Snowboards.ToList();
                var customers = db.Customers.ToList();

                var result = from b in bookings.ToList()
                             join s in snowboards on b.Snowboard.Id equals s.Id
                             join c in customers on b.Customer.Id equals c.Id
                             orderby b.Week descending
                             group b by b.Week;

                foreach (var res in result)
                {
                    Console.WriteLine("Week: " + res.Key);
                    foreach (var r in res)
                    {
                        Console.WriteLine($"\t* {r.Customer.Name} has a {r.Snowboard.Brand} on {r.Weekday}");
                    }
                }
            }
        }
        public static void AmountBoardsHired(int weekNr)
        {
            using (var db = new MyDbContext())
            {

                var bookings = db.Bookings.ToList();

                var result = from b in bookings
                             select new
                             { 
                                 b.Week,
                             };

                var amountOfSnowboards = db.Bookings.ToList().Count();
                var amountBooked = result.Where(x => x.Week == weekNr).Count();
                
                Console.WriteLine("Week " + weekNr + " there is " + amountBooked + " booked snowboards out of " + (amountOfSnowboards * 7));
            }
        }
    }
}
