using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonEscape.MazeGenerator;

namespace DungeonEscape
{
    public static class MazeDrawer
    {
        public static void Draw<T>(int playerX, int playerY, in T[,] rooms)
        {
            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for (int y = 0; y < rooms.GetLength(1); y++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (rooms[x, y])
                    {
                        case (int)RoomType.Exit:{
                            Console.ForegroundColor = ConsoleColor.Green;

                        }; break;
                        case (int)RoomType.Key: Console.ForegroundColor = ConsoleColor.Yellow; break;
                        case (int)RoomType.Trap: Console.ForegroundColor = ConsoleColor.Red; break;
                    }
                    if(x == playerX && y == playerY)
                    {
                        Console.Write("[P]\t");
                    }
                    else
                    {
                        Console.Write("X\t");
                    }
                }
                // Move to the next line after each row
                Console.WriteLine();
            }
        }
    }
}
