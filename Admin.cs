using Microsoft.EntityFrameworkCore.Storage;
using SnowboardUthyrning.DatabaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowboardUthyrning
{
    internal class Admin
    {
        internal static void Menu(int weekNr)
        {
            bool loop = true;

            while (loop)
            {
                Console.Clear();

                Console.WriteLine("Add [S]nowboard");
                Console.WriteLine("Check most [B]ooked snowboard");
                Console.WriteLine("Check most [P]opular week");
                Console.WriteLine("Check how many [H]ired snowboards there is");
                Console.WriteLine("Back to [M]ain menu");
                Console.WriteLine("----------------");

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.S:
                        Insert.NewSnowboard();
                        break;

                    case ConsoleKey.B:
                        Select.MostBookedSnowboard();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.P:
                        Select.MostOccupiedWeek();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.H:
                        Select.AmountBoardsHired(weekNr);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.M:
                        loop = false;
                        break;
                }
            }
        }
    }
}
