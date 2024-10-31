using System;
using System.Text;

namespace DungeonEscape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World! █");
            MazeGenerator mazeGenerator = new MazeGenerator();
            mazeGenerator.GenerateMaze();
            MazeDrawer.Draw(0, 0, mazeGenerator.Maze);
            //"op", "ned", "venstre" eller "højre"
            //1 key, 1 exit, multible traps
        }
    }
}
