using System;
using static DungeonEscape.MazeGenerator;
using static DungeonEscape.MazeHelper;

namespace DungeonEscape
{
    public sealed class PlayerNavigator : IMaze
    {
        public event EventHandler? OnFoundKey;
        public event EventHandler? OnFoundExit;
        public event EventHandler? OnPlayerDied;
        public event EventHandler? OnGameEnd;

        public int[,] Maze { get; }
        public int[,] VisitedRooms
        {
            get
            {
                int[,] copy = new int[_visitedRooms.GetLength(0), _visitedRooms.GetLength(1)];
                Array.Copy(_visitedRooms, copy, _visitedRooms.Length);
                return copy;
            }
        }
        public int PlayerX => _playerX;
        public int PlayerY => _playerY;
        /// <summary>
        /// X
        /// Y
        /// HasVisited 0-1
        /// </summary>
        private int[,] _visitedRooms;
        private int _playerX, _playerY;
        private bool _hasKey;
        public PlayerNavigator(IMazeGenerator mazeGen)
        {
            if(mazeGen is null) throw new ArgumentNullException(nameof(mazeGen), "Is null");
            if(mazeGen.Maze is null) throw new ArgumentNullException(nameof(mazeGen.Maze), "Is null");
            Maze = mazeGen.Maze;
            _visitedRooms = new int[Maze.GetLength(0), Maze.GetLength(1)];
        }
        private bool CanNavigate(int x, int y) => x >= 0 && x < Maze.GetLength(0) && y >= 0 && y < Maze.GetLength(1);
        private void CheckRoom()
        {
            RoomType roomType = (RoomType)Maze[_playerX, _playerY];
            _visitedRooms[_playerX, _playerY] = 1;

            if (roomType == RoomType.Trap)
            {
                /*Player died*/
                OnPlayerDied?.Invoke(this, EventArgs.Empty);
            }
            if (!_hasKey && roomType == RoomType.Key)
            {
                _hasKey = true;
                Maze[_playerX, _playerY] = (int)RoomType.Empty;
                /*Player found the key*/
                OnFoundKey?.Invoke(this, EventArgs.Empty);
            }
            if (_hasKey && roomType == RoomType.Exit)
            {
                /*Player found the exit and got the key*/
                OnGameEnd?.Invoke(this, EventArgs.Empty);
            }
            else if (!_hasKey)
            {
                /*Player found the exit but missing the key*/
                OnFoundExit?.Invoke(this, EventArgs.Empty);
            }
        }
        public void SpawnPlayer()
        {
            do
            {
                _playerX = GetRandom(new(0, Maze.GetLength(0)));
                _playerY = GetRandom(new(0, Maze.GetLength(1)));
            } while (Maze[_playerX, _playerY] != (int)RoomType.Empty);
        }

        public void NavigateUp()
        {
            if (!CanNavigate(_playerX - 1, _playerY)) return;
            _playerX--;
            CheckRoom();
        }
        public void NavigateDown()
        {
            if (!CanNavigate(_playerX + 1, _playerY)) return;
            _playerX++;
            CheckRoom();
        }
        public void NavigateLeft()
        {
            if (!CanNavigate(_playerX, _playerY - 1)) return;
            _playerY--;
            CheckRoom();
        }
        public void NavigateRight()
        {
            if (!CanNavigate(_playerX, _playerY + 1)) return;
            _playerY++;
            CheckRoom();
        }
    }
}