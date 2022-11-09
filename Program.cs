﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
        static int PlayerPosx, PlayerPosy;
        static int OldPlayerPosy, OldPlayerPosx;
        static bool moveRollBack, dungeonMapSwitch;
        static int dungeonrows, dungeoncolumns;
        static int GetPOSx, GetPOSy;
        static bool firstmaprender, firstdungeonrender;

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
            {'`','`','`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','∩','^','^','^','^','`','`','`'},
            {'`','`','`','`','`','`','`','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
            {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
            {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        };

        static char[,] dungeonmap = new char[,]
        {
            {'█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█' },
            {'█',' ',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█',' ','∩',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ','π',' ','█' },
            {'█',' ',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█','█',' ','█','█','█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█','█','█',' ','█','█' },
            {'█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█' },
            {'█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ','█' },
            {'█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█' },
        };
        static void Main(string[] args)
        {
            scale = 1;
            origx = Console.CursorLeft;
            origy = Console.CursorTop;
            PlayerPosy = origy + 4 * scale;
            PlayerPosx = origx + 8 * scale;
            OldPlayerPosx = origx + 4 * scale;
            OldPlayerPosy = origy + 8 * scale;
            GetPOSx = 0;
            GetPOSy = 0;
            p = "P";
            gameOver = false;
            rows = map.GetLength(0);
            columns = map.GetLength(1);
            dungeonrows = dungeonmap.GetLength(0);
            dungeoncolumns = dungeonmap.GetLength(1);
            moveRollBack = false;
            dungeonMapSwitch = false;
            firstdungeonrender = true;
            firstmaprender = true;
                      
            while (gameOver == false)
            {
                
                if (dungeonMapSwitch == false)
                {
                    if (firstmaprender == true)
                    {
                        Console.Clear();
                        DisplayMap(scale);
                        firstmaprender = false;
                    }                                    
                    PlayerDraw(p, PlayerPosx, PlayerPosy);
                    Console.WriteLine();
                    Console.SetCursorPosition(0, rows * scale + 2); //places the cursor below the map for further printouts
                    GetPlayerPOS(); //gets current player position to be used later to print the map at this location
                    PlayerChoice();                
                    PlayerDraw(p, PlayerPosx, PlayerPosy);
                    Console.SetCursorPosition(GetPOSx, GetPOSy); //prepares the cursor to reprint the map location
                    ColourCode(GetPOSy - 1, GetPOSx - 1); //prepares the colour for the location to be printed
                    Console.Write(map[GetPOSy - 1, GetPOSx - 1]); //prints the map location based on the player position
                    Console.BackgroundColor = ConsoleColor.Black; //resets the background colour after the map print
                }
                else
                {
                    if (firstdungeonrender == true)
                    {
                        Console.Clear();
                        DisplayDungeonMap(scale);
                        firstdungeonrender = false;
                    }                    
                    PlayerDraw(p, PlayerPosx, PlayerPosy);
                    Console.WriteLine();
                    Console.SetCursorPosition(0, dungeonrows * scale + 2);
                    GetPlayerPOS(); 
                    PlayerChoice();
                    PlayerDraw(p, PlayerPosx, PlayerPosy);
                    Console.SetCursorPosition(GetPOSx, GetPOSy); //prepares the cursor to reprint the map location
                    ColourCodeDungeon(GetPOSy - 1, GetPOSx - 1); //prepares the colour for the location to be printed
                    Console.Write(dungeonmap[GetPOSy - 1, GetPOSx - 1]); //prints the map location based on the player position
                    Console.BackgroundColor = ConsoleColor.Black; //resets the background colour after the map print
                }
            }
        }
        static void DisplayMap()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int x = 0; x < rows; x++)
            {
                Console.Write("#");
                for (int y = 0; y < columns; y++)
                {
                    ColourCode(x, y);
                    Console.Write(map[x, y]); //writes the single character located at array index x, y
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("#");
                Console.WriteLine(); //line breaks when the current row is done being written
            }

            Console.ResetColor();
        }
        static void DisplayMap(int scale)
        {
            int bordersize = columns * scale;            
            Console.BackgroundColor = ConsoleColor.Black;

            for (int g = 0; g < 1; g++)
            {
                Console.Write("╔");
                for (int r = 0; r < bordersize; r++)
                {
                    Console.Write("═");
                }
                Console.Write("╗");
            }            

            Console.WriteLine();

            for (int x = 0; x < rows; x++)
            {
                for (int m = 0; m < scale; m++)
                {
                    Console.Write("║");
                    for (int y = 0; y < columns; y++)
                    {
                        for (int z = 0; z < scale; z++)
                        {                               
                            ColourCode(x, y);
                            Console.Write(map[x, y]);                            
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("║");
                    Console.WriteLine();
                }                
            }
            for (int g = 0; g < 1; g++)
            {
                Console.Write("╚");
                for (int r = 0; r < bordersize; r++)
                {
                    Console.Write("═");
                }
                Console.Write("╝");
            }
            Console.WriteLine();
        }
        static void DisplayDungeonMap(int scale)
        {
            int bordersize = columns * scale;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int g = 0; g < 1; g++)
            {
                Console.Write("╔");
                for (int r = 0; r < bordersize; r++)
                {
                    Console.Write("═");
                }
                Console.Write("╗");
            }

            Console.WriteLine();

            for (int x = 0; x < rows; x++)
            {
                for (int m = 0; m < scale; m++)
                {
                    Console.Write("║");
                    for (int y = 0; y < columns; y++)
                    {
                        for (int z = 0; z < scale; z++)
                        {
                            ColourCodeDungeon(x, y);
                            Console.Write(dungeonmap[x, y]);
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("║");
                    Console.WriteLine();
                }
            }
            for (int g = 0; g < 1; g++)
            {
                Console.Write("╚");
                for (int r = 0; r < bordersize; r++)
                {
                    Console.Write("═");
                }
                Console.Write("╝");
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
                case '∩':
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
        static void ColourCodeDungeon(int x, int y)
        {
            switch (dungeonmap[x, y]) //checks the characters in the array and assigns them colours
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
                case '∩':
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case 'π':
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
        static void PlayerChoice()
        {
            Console.WriteLine();
            Console.WriteLine("Press 'W' to move North, 'A' to move West, 'S' to move South, or 'D' to move East. Press 'U' to increase the scale, or 'N' to decrease it. Press 'ESC' to Quit.");

            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                gameOver = true;
            }
            else if (key.Key == ConsoleKey.W)
            {
                PlayerPosy--;
                Console.SetCursorPosition(0, rows * scale + 2);
                Console.WriteLine("Player position is: " + PlayerPosx + " " + PlayerPosy);
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosy++;
                }
            }
            else if (key.Key == ConsoleKey.A)
            {
                PlayerPosx--;
                Console.SetCursorPosition(0, rows * scale + 2);
                Console.WriteLine("Player position is: " + PlayerPosx + " " + PlayerPosy);
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosx++;
                }
            }
            else if (key.Key == ConsoleKey.S)
            {
                PlayerPosy++;
                Console.SetCursorPosition(0, rows * scale + 2);
                Console.WriteLine("Player position is: " + PlayerPosx + " " + PlayerPosy);
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosy--;
                }
            }
            else if (key.Key == ConsoleKey.D)
            {
                PlayerPosx++;
                Console.SetCursorPosition(0, rows * scale + 2);
                Console.WriteLine("Player position is: " + PlayerPosx + " " + PlayerPosy);
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosx--;
                }
            }
            else if (key.Key == ConsoleKey.U)
            {
                scale++;
            }
            else if (key.Key == ConsoleKey.N)
            {
                scale--;
            }
        }
        static void PlayerDraw(string p, int PlayerPosx, int PlayerPosy)
        {            
            Console.SetCursorPosition(origx + PlayerPosx, origy + PlayerPosy);
            Console.Write(p);
        }
        static void WallCheck(int x, int y) //checks to see if the player is allowed to move onto the map location
        {            
            if (x > columns || x < 0 + 1) //prevents player from moving outside bounds of border
            {
                moveRollBack = true;
            }
            else if (y > rows || y < 0 + 1) //prevents player from moving outside bounds of border
            {
                moveRollBack = true;
            }
            else if (dungeonMapSwitch == true)
            {
                switch(dungeonmap[y - 1, x - 1])
                {
                    case '∩':
                        PlayerPosx = OldPlayerPosx; //remembers where the player came in so they can leave at the right position on the map
                        PlayerPosy = OldPlayerPosy;
                        dungeonMapSwitch = false;
                        firstdungeonrender = true; //flips the switch so when the player comes back into the dungeon it loads the whole thing again
                        break;
                    case 'π':
                        //treasure call and print goes here!
                        break;
                    case '█':
                        moveRollBack = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch(map[y - 1, x - 1])
                {
                    case '∩':
                        OldPlayerPosx = PlayerPosx;
                        OldPlayerPosy = PlayerPosy;
                        GetDungeonEntrance();
                        dungeonMapSwitch = true;
                        firstmaprender = true; //flips the switch so when the player comes back into the world map it loads the whole thing again
                        break;
                    case '~':
                        moveRollBack = true;
                        break;
                    case '^':
                        moveRollBack = true;
                        break;
                    default:
                        moveRollBack = false;
                        break;
                }
            }
        }      
        static void GetDungeonEntrance()
        {
            for (int x = 0; x < dungeonrows; x++)
            {
                for (int y = 0; y < dungeoncolumns; y++)
                {
                    switch(dungeonmap[x,y])
                    {
                        case '∩':
                            PlayerPosy = y + 1;
                            PlayerPosx = x + 1;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        static void GetPlayerPOS()
        {
            GetPOSx = PlayerPosx;
            GetPOSy = PlayerPosy;
        }
    }
}
