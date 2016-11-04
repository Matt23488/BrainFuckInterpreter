using System;
using System.IO;

namespace BrainFuckInterpreterLib
{
    public sealed class InterpreterSettings
    {
        public TextWriter Writer { get; set; }
        public TextReader Reader { get; set; }
        public CellSize CellSize { get; set; }

        public static readonly InterpreterSettings Default = new InterpreterSettings
        {
            Writer = Console.Out,
            Reader = Console.In,
            CellSize = CellSize.OneByte
        };
    }
}
