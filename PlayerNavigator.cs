using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonEscape.MazeGenerator;
using static DungeonEscape.MazeHelper;

namespace DungeonEscape
{
    public sealed class PlayerNavigator : IMaze
    {
        public int[,] Maze { get; }
        private int playerX, playerY;
        private bool hasKey;
        public PlayerNavigator(IMazeGenerator mazeGen)
        {
            if(mazeGen is null) throw new ArgumentNullException(nameof(mazeGen), "Is null");
            if(mazeGen.Maze is null) throw new ArgumentNullException(nameof(mazeGen.Maze), "Is null");
            Maze = mazeGen.Maze;
        }
        private bool CanNavigate(int x = 0, int y = 0) => x >= 0 && y >= 0 && x < Maze.GetLength(0) && y < Maze.GetLength(1);
        private void CheckRoom()
        {
            if(Maze[playerX, playerY] == (int)RoomType.Trap)
            {
                /*Player died*/
            }
            if (!hasKey && Maze[playerX, playerY] == (int)RoomType.Key)
            {
                /*Found the key*/
            }
            if (hasKey && Maze[playerX, playerY] == (int)RoomType.Exit)
            {
                /*Has key and found the exit*/
            }
            else if(!hasKey)
            {
                /*Found exit but missing*/
            }

        }
        public void SpawnPlayer()
        {
            do
            {
                playerX = GetRandom(new(0, Maze.GetLength(0)));
                playerY = GetRandom(new(0, Maze.GetLength(2)));
            } while (Maze[playerX, playerY] != (int)MazeGenerator.RoomType.Empty);
        }



        //Y = Vertical; X = Horizontal
        public void NavigateUp()
        {
            if(!CanNavigate(y: playerY+1)) return;
            playerY++;
            CheckRoom();
        }
        public void NavigateDown()
        {
            if (!CanNavigate(y: playerY - 1)) return;
            playerY--;
        }
        public void NavigateLeft()
        {
            if (!CanNavigate(x: playerX + 1)) return;
            playerX++;
        }
        public void NavigateRight()
        {
            if (!CanNavigate(x: playerX - 1)) return;
            playerX--;
        }
    }
}
