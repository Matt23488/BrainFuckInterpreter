using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckDebugger.Extensions
{
    internal static class ConsoleKeyInfoExtensions
    {
        private static readonly ConsoleKey[] _preDebuggingKeys =
        {
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow,
            ConsoleKey.F9,
            ConsoleKey.F5
        };

        public static bool IsValidPreDebuggingKey(this ConsoleKeyInfo keyPressed)
        {
            return _preDebuggingKeys.Contains(keyPressed.Key);
        }
    }
}
