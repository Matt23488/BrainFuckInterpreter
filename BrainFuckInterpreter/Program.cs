using System;

namespace BrainFuckInterpreter
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

            var invoker = new InterpreterInvoker();

            try
            {
                invoker.ParseArguments(args);
                invoker.RunProgram();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void PrintHelpText()
        {
            Console.WriteLine("Usage: bf [-s cell-size] [file | [-c code]]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("    -s cell-size       Sets the cell size (in bytes) for Brain Fuck.");
            Console.WriteLine("                       Valid values are 1, 2, and 4. Default is 1.");
            Console.WriteLine("    -c code            Executes code directly.");
            Console.WriteLine("                       Surround code with quotes.");
        }
    }
}
