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
        public bool[,] VisitedRooms
        {
            get
            {
                bool[,] copy = new bool[_visitedRooms.GetLength(0), _visitedRooms.GetLength(1)];
                Array.Copy(_visitedRooms, copy, _visitedRooms.Length);
                return copy;
            }
        }
        private bool[,] _visitedRooms; //X,Y,0-1
        private int _playerX, _playerY;
        private bool _hasKey;
        public PlayerNavigator(IMazeGenerator mazeGen)
        {
            if(mazeGen is null) throw new ArgumentNullException(nameof(mazeGen), "Is null");
            if(mazeGen.Maze is null) throw new ArgumentNullException(nameof(mazeGen.Maze), "Is null");
            Maze = mazeGen.Maze;
            _visitedRooms = new bool[Maze.GetLength(0), Maze.GetLength(1)];
        }
        private bool CanNavigate(int x = 0, int y = 0) => x >= 0 && y >= 0 && x < Maze.GetLength(0) && y < Maze.GetLength(1);
        private void CheckRoom()
        {
            _visitedRooms[_playerX, _playerY] = true;
            if(Maze[_playerX, _playerY] == (int)RoomType.Trap)
            {
                /*Player died*/
            }
            if (!_hasKey && Maze[_playerX, _playerY] == (int)RoomType.Key)
            {
                /*Found the key*/
                _hasKey = true;
                Maze[_playerX, _playerY] = (int)RoomType.Empty;
            }
            if (_hasKey && Maze[_playerX, _playerY] == (int)RoomType.Exit)
            {
                /*Has key and found the exit*/
            }
            else if(!_hasKey)
            {
                /*Found exit but missing*/
            }

        }
        public void SpawnPlayer()
        {
            do
            {
                _playerX = GetRandom(new(0, Maze.GetLength(0)));
                _playerY = GetRandom(new(0, Maze.GetLength(2)));
            } while (Maze[_playerX, _playerY] != (int)RoomType.Empty);
        }

        //Y = Vertical; X = Horizontal
        public void NavigateUp()
        {
            if(!CanNavigate(y: _playerY+1)) return;
            _playerY++;
            CheckRoom();
        }
        public void NavigateDown()
        {
            if (!CanNavigate(y: _playerY - 1)) return;
            _playerY--;
            CheckRoom();
        }
        public void NavigateLeft()
        {
            if (!CanNavigate(x: _playerX + 1)) return;
            _playerX++;
            CheckRoom();
        }
        public void NavigateRight()
        {
            if (!CanNavigate(x: _playerX - 1)) return;
            _playerX--;
            CheckRoom();
        }
    }
}
