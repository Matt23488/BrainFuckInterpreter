using System;
using System.Diagnostics;

namespace BrainFuckInterpreterLib.Utilities
{
    internal static class ConsoleHelper
    {
        [Conditional("DEBUG")]
        internal static void ClearLinesAndReturnCursor(int numLines)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < Console.BufferWidth; j++)
                {
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(left, top);
        }
    }
}
