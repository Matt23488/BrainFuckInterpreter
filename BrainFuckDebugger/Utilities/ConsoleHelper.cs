using System;
using System.Diagnostics;

namespace BrainFuckDebugger.Utilities
{
    internal static class ConsoleHelper
    {
        //[Conditional("DEBUG")]
        internal static void ClearLinesAndReturnCursor(int startingLeft, int startingTop, int numLines)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.SetCursorPosition(startingLeft, startingTop);

            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < Console.BufferWidth; j++)
                {
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(left, top);
        }

        internal static void PrintHorizontalRule()
        {
            for (int j = 0; j < Console.BufferWidth; j++)
            {
                Console.Write("-");
            }
        }

        internal static void MoveUp() => Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        internal static void MoveDown() => Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
        internal static void MoveLeft() => Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        internal static void MoveRight() => Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
    }
}
