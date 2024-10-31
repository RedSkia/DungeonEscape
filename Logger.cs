using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonEscape
{
    public static class Logger
    {
        public static void Log(string message, bool newLine = false)
        {
            switch (newLine)
            {
                case true: Console.WriteLine(message); break;
                default: Console.Write(message); break;
            }
        }
    }
}
