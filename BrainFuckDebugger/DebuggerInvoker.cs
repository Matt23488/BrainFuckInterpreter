using BrainFuckDebugger.Utilities;
using BrainFuckInterpreterLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckDebugger
{
    internal class DebuggerInvoker
    {
        private InterpreterSettings _settings = InterpreterSettings.Default;
        private string _code = string.Empty;

        private BrainFuckInterpreterLib.BrainFuckDebugger _debugger;

        public DebuggerInvoker(params string[] args)
        {
            ParseArguments(args);

            var interpreter = new BrainFuckInterpreterLib.BrainFuckInterpreter(_code);
            _debugger = new BrainFuckInterpreterLib.BrainFuckDebugger(interpreter);
            _debugger.BreakPointHit += BreakPointHit;
        }

        private void ParseArguments(params string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-s")
                {
                    i++;

                    if (i == args.Length)
                    {
                        throw new InvalidOperationException("Missing argument");
                    }

                    switch (args[i])
                    {
                        case "1":
                            _settings.CellSize = CellSize.OneByte;
                            break;
                        case "2":
                            _settings.CellSize = CellSize.TwoBytes;
                            break;
                        case "4":
                            _settings.CellSize = CellSize.FourBytes;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(args[i], "Invalid cell size parameter.");
                    }
                }
                else if (args[i] == "-c")
                {
                    i++;

                    if (i == args.Length)
                    {
                        throw new InvalidOperationException("Missing argument");
                    }

                    _code = args[i];
                }
                else
                {
                    if (!File.Exists(args[i]))
                    {
                        throw new FileNotFoundException("File not found.", args[i]);
                    }

                    _code = File.ReadAllText(args[i]);
                }
            }
        }

        //public void SetBreakPoint(int position)
        //{
        //    _debugger.AddBreakPoint(position);
        //}

        //public void RemoveBreakPoint(int position)
        //{
        //    _debugger.RemoveBreakPoint(position);
        //}
        public void ToggleBreakPoint(int position)
        {
            _debugger.ToggleBreakPoint(position);
        }

        public void RunProgramWithDebugging()
        {
            Console.WriteLine(); // Accounts for program state
            Console.WriteLine(); //
            Console.WriteLine(); //
            Console.WriteLine(); //
            Console.WriteLine(); // Empty line
            Console.WriteLine(); // F5/F10 line
            Console.WriteLine(); // Empty line
            Console.WriteLine("Program Output:");
            ConsoleHelper.PrintHorizontalRule();
            Console.WriteLine();

            _debugger.Start();
        }

        private void BreakPointHit(object sender, BreakPointHitEventArgs e)
        {
            ConsoleHelper.ClearLinesAndReturnCursor(0, 0, 6);

            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            Console.SetCursorPosition(0, 0);

            Console.WriteLine(e.ProgramState);
            Console.WriteLine();
            Console.Write("Press F5 to continue or F10 to step forward one instruction: ");

            ConsoleKeyInfo keyPressed;
            do
            {
                keyPressed = Console.ReadKey();
            } while (keyPressed.Key != ConsoleKey.F5 && keyPressed.Key != ConsoleKey.F10);

            Console.SetCursorPosition(left, top);

            if (keyPressed.Key == ConsoleKey.F5) e.Continue();
            else e.Step();
        }

        public override string ToString()
        {
            var codeBuilder = new StringBuilder();
            var breakPointBuilder = new StringBuilder();

            var breakPoints = _debugger.BreakPoints.ToList();
            for (int i = 0; i < _code.Length; i++)
            {
                codeBuilder.Append(_code[i]);
                breakPointBuilder.Append(breakPoints.Contains(i) ? '*' : ' ');
            }

            return $"{codeBuilder.ToString()}{Environment.NewLine}{breakPointBuilder.ToString()}";
        }
    }
}
