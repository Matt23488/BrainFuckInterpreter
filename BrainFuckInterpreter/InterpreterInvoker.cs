using BrainFuckInterpreterLib;
using System;
using System.IO;

namespace BrainFuckInterpreter
{
    internal class InterpreterInvoker
    {
        private InterpreterSettings _settings = InterpreterSettings.Default;
        private string _code = string.Empty;

        public void ParseArguments(params string[] args)
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

        public void RunProgram()
        {
            var interpreter = new BrainFuckInterpreterLib.BrainFuckInterpreter(_code);
            interpreter.VerifySyntaxIntegrity();
            interpreter.RunToCompletion();
        }
    }
}
