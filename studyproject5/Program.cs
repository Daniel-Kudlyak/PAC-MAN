using System;
using System.IO;

namespace studyproject5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random random = new Random();
            bool isPlaing = true;
            bool isAlive = true;
            int pacmanX, pacmanY;
            int ghostX, ghostY;
            int pacmanDX  = 0, pacmanDY = -1;
            int ghostDX = 0, ghostDY = -1;
            string mapname = "map1";
            int point = 0;
            char[,] map = MapRead(mapname, out pacmanX, out pacmanY, out ghostX, out ghostY);       
            WriteMap(map);
            while (isPlaing)
            {
                if (map[pacmanX + pacmanDX, pacmanY + pacmanDY] != '#')
                {
                    MovePacmanComplite(map, ref pacmanDX, ref pacmanDY, ref pacmanX, ref pacmanY, ref isAlive, '@');
                }
                Move(ref pacmanDX, ref pacmanDY);
                
                Point(map, ref pacmanDX, ref pacmanDY, ref pacmanX, ref pacmanY, ref point);
              Console.SetCursorPosition(0, 20);
              Console.WriteLine($"Point ={point} ");
              CheckToWon(ref isPlaing, map);
                if (map[ghostX + ghostDX, ghostY + ghostDY] != '#')
                {
                    MovePacmanComplite(map, ref ghostDX, ref ghostDY, ref ghostX, ref ghostY, ref isAlive, '$');
                }
                else
                {
                    Move(random, ref ghostDX, ref ghostDY);
                }
                if (ghostX == pacmanX && ghostY == pacmanY)
                {
                    Console.SetCursorPosition(0, 21);
                    Console.WriteLine("You lozer");
                    isPlaing = false;
                }

            }
        }
        static void Move(Random random,ref int ghoshDX, ref int ghostDY)
        {
            int ghost = random.Next(1, 5);
                switch (ghost)
                {
                    case 1:
                        ghoshDX = -1;
                        ghostDY = 0;
                        break;
                    case 2:
                    ghoshDX = 1;
                    ghostDY = 0;
                        break;
                    case 3:
                    ghoshDX = 0;
                    ghostDY = -1;
                        break;
                    case 4:
                    ghoshDX = 0;
                    ghostDY = 1;
                        break;
                }
        }
        
        static void Move( ref int pacmanDX, ref int pacmanDY)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        pacmanDX = -1;
                        pacmanDY = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        pacmanDX = 1;
                        pacmanDY = 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        pacmanDX = 0;
                        pacmanDY = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        pacmanDX = 0;
                        pacmanDY = 1;
                        break;
                }
            }
        }
        static void MovePacmanComplite(char[,] map, ref int pacmanDX, ref int pacmanDY, ref int pacmanX, ref int pacmanY, ref bool isAlive, char sumbol)
        {
                Console.SetCursorPosition(pacmanY, pacmanX);
                Console.Write(map[pacmanX, pacmanY]);
                pacmanX += pacmanDX;
                pacmanY += pacmanDY;
                Console.SetCursorPosition(pacmanY, pacmanX);
                Console.Write(sumbol);
            System.Threading.Thread.Sleep(50);
        }
        static void Point(char[,] map, ref int pacmanDX, ref int pacmanDY, ref int pacmanX, ref int pacmanY,ref int point)
        {
            if (map[pacmanX + pacmanDX, pacmanY + pacmanDY] == '.')
            {
                point += 10;
                map[pacmanX + pacmanDX, pacmanY + pacmanDY] = ' ';
            }
        }
        static void CheckToWon(ref bool isPlaing, char[,] map)
        {
            int clc = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '.')
                    {
                        clc++;
                    }
                }
            }
            if (clc == 0)
            {
                Console.WriteLine("You won");
                isPlaing = false;
            }
        }
        static void WriteMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                }
                Console.WriteLine();
            }
        }

        static char[,] MapRead(string mapname, out int pacmanX, out int pacmanY, out int ghostX, out int ghostY)
        {
            pacmanX = 0;
            pacmanY = 0;
            ghostX = 0;
            ghostY = 0;
            string[] newfile = File.ReadAllLines($"maps/{mapname}.txt");
            char[,] map = new char[newfile.Length, newfile[0].Length];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newfile[i][j];
                        pacmanX = 1;
                        pacmanY = 26;
                        ghostX = 7;
                        ghostY = 12;
                     if (map[i, j] == ' ')
                        map[i, j] = '.';
                }
            }
            return map;
        }
     }
}
