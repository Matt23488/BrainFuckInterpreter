using BrainFuckDebugger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || args[0] == "/?")
            {
                PrintHelpText();
                return;
            }

            var debugger = new DebuggerInvoker(args);

            int position = 0;
            ConsoleKeyInfo keyPressed;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                ConsoleHelper.ClearLinesAndReturnCursor(0, 0, 7);
                Console.WriteLine(debugger);
                DrawPointerLine(position);
                Console.WriteLine();
                Console.WriteLine("Press Left or Right to move the cursor.");
                Console.WriteLine("Press F9 to set a break point.");
                Console.WriteLine("Press F5 to start debugging.");

                do
                {
                    keyPressed = Console.ReadKey();
                } while (keyPressed.Key != ConsoleKey.LeftArrow && keyPressed.Key != ConsoleKey.RightArrow && keyPressed.Key != ConsoleKey.F9 && keyPressed.Key != ConsoleKey.F5);

                switch (keyPressed.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (position > 0) position--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (position < Console.BufferWidth) position++;
                        break;
                    case ConsoleKey.F9:
                        debugger.ToggleBreakPoint(position);
                        break;
                }

            } while (keyPressed.Key != ConsoleKey.F5);

            Console.SetCursorPosition(0, 0);
            ConsoleHelper.ClearLinesAndReturnCursor(0, 0, 10);
            debugger.RunProgramWithDebugging();
        }

        private static void PrintHelpText()
        {
            Console.WriteLine("Usage: bfd [-s cell-size] [file | [-c code]]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("    -s cell-size       Sets the cell size (in bytes) for Brain Fuck.");
            Console.WriteLine("                       Valid values are 1, 2, and 4. Default is 1.");
            Console.WriteLine("    -c code            Executes code directly.");
            Console.WriteLine("                       Surround code with quotes.");
        }

        private static void DrawPointerLine(int position)
        {
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write(i == position ? '^' : ' ');
            }
        }
    }
}
