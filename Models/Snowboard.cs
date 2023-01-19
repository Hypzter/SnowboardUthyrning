using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowboardUthyrning.Models
{
    public class Snowboard
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int Length { get; set; }
        public string DifficultyOfBoard { get; set; }

        //If the snowboarder rides with left foot first, its called "regular". Right foot first is called "goofy"
        public bool IsRegular { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }

    }
}
