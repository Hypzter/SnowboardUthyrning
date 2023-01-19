using SnowboardUthyrning.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowboardUthyrning.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public Weekday Weekday { get; set; } 
        public virtual Customer Customer { get; set; }
        public virtual Snowboard Snowboard { get; set; }
        
        // Customer (1) --> (N) Booking (1) --> (1) Snowboard
    }
}
