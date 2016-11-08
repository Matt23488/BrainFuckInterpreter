using BrainFuckInterpreterLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreterLib.Utilities
{
    internal class BrainFuckReader
    {
        private TextReader _reader;

        internal BrainFuckReader(TextReader reader)
        {
            _reader = reader;
        }

        public uint Read()
        {
            var input = _reader.Read();
            if (input == '\r' && _reader.Peek() == '\n')
            {
                _reader.Read();
                return 10;
            }
            else return (uint)input;
        }
    }
}
