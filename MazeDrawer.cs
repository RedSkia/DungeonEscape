using System;
using static DungeonEscape.MazeGenerator;

namespace DungeonEscape
{
    public static class MazeDrawer
    {
        public static void Draw(int playerX, int playerY, in int[,] maze, in int[,] visitedRooms, bool useCheats = false)
        {
            if (maze.Length != visitedRooms.Length) throw new IndexOutOfRangeException("MazeDrawer.Draw; maze & visitedRooms lengths dosen't match!");
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("P\t");
                        continue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("X\t");
                    }

                    if (!useCheats) continue; /*For Cheaters*/

                    RoomType room = (RoomType)maze[x, y];
                    if (room == RoomType.Exit)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("E\t");
                    }
                    else if (room == RoomType.Key)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("K\t");
                    }
                    else if (room == RoomType.Trap)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("T\t");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
