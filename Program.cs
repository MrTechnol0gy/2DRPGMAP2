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
        static int origx, origy;
        static string p;
        static int rows, columns;
        static ConsoleKeyInfo key;
        static bool gameOver;
        static int PlayerPosx;
        static int PlayerPosy;

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
            scale = 3;
            origx = Console.CursorLeft;
            origy = Console.CursorTop;
            PlayerPosy = origy + 6 * scale;
            PlayerPosx = origx + 10 * scale;
            p = "P";
            gameOver = false;
            rows = map.GetLength(0);
            columns = map.GetLength(1);

            //Console.WriteLine("The array is " + rows + " rows tall and " + columns + " columns wide.");
            //Console.WriteLine();
            //DisplayMap();
            //Console.WriteLine();

            while (gameOver == false)
            {
                DisplayMap(scale);
                PlayerChoice();
                PlayerDraw(p, PlayerPosx, PlayerPosy);
                Console.ReadKey();
            }
        }
        //static void DisplayMap()
        //{
        //    Console.BackgroundColor = ConsoleColor.Black;
            
        //    for (int x = 0; x < rows; x++)
        //    {
        //        for (int y = 0; y < columns; y++)
        //        {
        //            ColourCode(x, y);
        //            Console.Write(map[x, y]); //writes the single character located at array index x, y
        //        }    
        //        Console.WriteLine(); //line breaks when the current row is done being written
        //    }

        //    Console.ResetColor();
        //}
        static void DisplayMap(int scale)
        {
            int bordersize = columns * scale;            
            Console.BackgroundColor = ConsoleColor.Black;

            for (int r = -2; r < bordersize; r++)
            {
                Console.Write("#");
            }            
            Console.WriteLine();
            for (int x = 0; x < rows; x++)
            {
                for (int m = 0; m < scale; m++)
                {
                    for (int y = 0; y < columns; y++)
                    {
                        for (int z = 0; z < scale; z++)
                        {                            
                            ColourCode(x, y);
                            Console.Write(map[x, y]);                            
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            for (int r = -2; r < bordersize; r++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }
        static void ColourCode(int x, int y)
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
        }
        static void PlayerChoice()
        {
            Console.WriteLine();
            Console.WriteLine("Press 'W' to move North, 'A' to move West, 'S' to move South, or 'D' to move East. Press 'ESC' to Quit.");

            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                gameOver = true;
            }
            else if (key.Key == ConsoleKey.W)
            {
                PlayerPosy--;
            }
            else if (key.Key == ConsoleKey.A)
            {
                PlayerPosx--;
            }
            else if (key.Key == ConsoleKey.S)
            {
                PlayerPosy++;
            }
            else if (key.Key == ConsoleKey.D)
            {
                PlayerPosx++;
            }

        }
        static void PlayerDraw(string p, int PlayerPosx, int PlayerPosy)
        {
            int OldPlayerPosx = PlayerPosx;
            int OldPlayerPosy = PlayerPosy;
            Console.SetCursorPosition(OldPlayerPosx + PlayerPosx, OldPlayerPosy + PlayerPosy);
            Console.Write(p);
        }
    }
}
