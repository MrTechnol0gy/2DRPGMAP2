using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DRPGMAP2
{
    internal class Program
    {
        static int scale;
        static int x, y, origx, origy;
        static string p;
        static int rows, columns;

        static char[,] map = new char[,] // dimensions defined by following data:
    {
        {'^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'^','^','`','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`'},
        {'^','^','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`','`','`'},
        {'^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`'},
        {'`','`','`','`','`','`','`','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
    };
        static void Main(string[] args)
        {
            scale = 1;
            x = 0;
            y = 0;
            origx = Console.CursorLeft;
            origy = Console.CursorTop;
            p = "P";
            rows = map.GetLength(0);
            columns = map.GetLength(1);

            Console.WriteLine("The array is " + rows + " rows tall and " + columns + " columns wide.");
            Console.WriteLine();

            DisplayMap();

            Console.ReadKey();
        }
        static void DisplayMap()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    switch (map[x, y]) //checks the characters in the array and assigns them colours
                    {
                        case '^':
                            Console.BackgroundColor = ConsoleColor.Gray;
                            break;
                        case '`':
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        case '~':
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case '*':
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;

                    }
                    Console.Write(map[x, y]); //writes the single character located at array index x, y
                }    
                Console.WriteLine(); //line breaks when the current row is done being written
            }

            Console.ResetColor();
        }
        static void DisplayMap(int scale)
        {

        }        
    }
}
