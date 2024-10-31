﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonEscape
{
    public sealed class MazeGenerator
    {
        public enum RoomType
        {
            Empty = 0,
            Key = 1,
            Exit = 2,
            Trap = 3,
        }
        private int GetRandom(Range range) => new Random().Next(range.Start.Value, range.End.Value);
        public int[,] GenerateMaze(byte size = 5, byte maxTraps = 5)
        {
            var maze = new int[size, size];
            maxTraps = (byte)Math.Clamp(maxTraps, 0, Math.Pow(size, 2) - 2);
            byte trapsPlaced = 0;

            int keyX = GetRandom(new(0, size));
            int keyY = GetRandom(new(0, size));
            maze[keyX, keyY] = (int)RoomType.Key;

            int exitX, exitY;
            do
            {
                exitX = GetRandom(new(0, size));
                exitY = GetRandom(new(0, size));
            } while (exitX == keyX && exitY == keyY);
            maze[exitX, exitY] = (int)RoomType.Exit;

            while (trapsPlaced < maxTraps)
            {
                int trapX = GetRandom(new(0, size));
                int trapY = GetRandom(new(0, size));
                if (maze[trapX, trapY] != (int)RoomType.Empty) continue;
                maze[trapX, trapY] = (int)RoomType.Trap;
                trapsPlaced++;
            }

            return maze;
        }
    }
}