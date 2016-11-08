using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreterLib.Utilities
{
    internal class BrainFuckWriter
    {
        private TextWriter _writer;

        internal BrainFuckWriter(TextWriter writer)
        {
            _writer = writer;
        }

        internal void Write(uint value)
        {
            if (value == 10) _writer.Write("\r\n");
            else             _writer.Write(((char)value).ToString());
        }
    }
}
