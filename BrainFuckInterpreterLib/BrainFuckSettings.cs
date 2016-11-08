using System;
using System.IO;

namespace BrainFuckInterpreterLib
{
    public sealed class BrainFuckSettings
    {
        public TextWriter Writer { get; set; }
        public TextReader Reader { get; set; }
        public CellSize CellSize { get; set; }

        public static readonly BrainFuckSettings Default = new BrainFuckSettings
        {
            Writer = Console.Out,
            Reader = Console.In,
            CellSize = CellSize.OneByte
        };
    }
}
