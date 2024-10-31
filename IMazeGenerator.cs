using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonEscape
{
    public interface IMazeGenerator : IMaze
    {
        public void GenerateMaze(byte size = 5, byte maxTraps = 5);
    }
}
