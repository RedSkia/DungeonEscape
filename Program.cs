namespace DungeonEscape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //"op", "ned", "venstre" eller "højre"
            //1 key, 1 exit, multible traps
        }
    }




    internal class Game
    {
        public enum RoomType
        {
            Empty = 0,
            Key = 1,
            Exit = 2,
            Trap = 3,
        }
        private int GetRandom(Range range) => new Random().Next(range.Start.Value, range.End.Value);
        public int[,] GenerateMaze(byte size = 5, byte maxTraps = 5) //5*5 = 25 rooms
        {
            var maze = new int[size, size];
            maxTraps = (byte)Math.Clamp(maxTraps, 0, Math.Pow(size, 2) - 2);
            byte trapsPlaced = 0;
            maze[GetRandom(new(0, maze.Length)), GetRandom(new(0, maze.Length))] = (int)RoomType.Key;
            maze[GetRandom(new(0, maze.Length)), GetRandom(new(0, maze.Length))] = (int)RoomType.Exit;
            while (trapsPlaced < maxTraps)
            {
                for (int x = 0; x < maze.Length; x++)
                {
                    for (int y = 0; y < maze.Length; y++)
                    {
                        bool ShouldPlaceTrap = GetRandom(new(0, 1)) == 1;
                        if(!ShouldPlaceTrap || maze[x, y] == (int)RoomType.Trap) continue;
                        maze[x, y] = (int)RoomType.Trap;
                    }
                }
            }
            return maze;
        }
    }
}
