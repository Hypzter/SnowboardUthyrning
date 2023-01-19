using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowboardUthyrning.Helpers
{
    internal class CheckInputValidation
    {
        public static int CheckForDigits()
        {
            bool correctInput = false;
            int digit = 0;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out digit))
                {
                    Console.WriteLine("Wrong format, please try again");
                }
                else
                {
                    correctInput = true;
                }
            }
            return digit;
        }
    }
}