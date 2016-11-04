using BrainFuckInterpreterLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrainFuckInterpreterLib
{
    public class ProgramState
    {
        private readonly uint _maxValue;

        internal Dictionary<int, uint> CellValues = new Dictionary<int, uint> { { 0, 0 } };

        public ProgramState(CellSize cellSize)
        {
            switch (cellSize)
            {
                case CellSize.OneByte:
                    _maxValue = byte.MaxValue;
                    break;
                case CellSize.TwoBytes:
                    _maxValue = ushort.MaxValue;
                    break;
                case CellSize.FourBytes:
                    _maxValue = uint.MaxValue;
                    break;
            }
        }

        public int CurrentCell { get; private set; }
        public uint CurrentValue
        {
            get { return CellValues[CurrentCell]; }
            private set { CellValues[CurrentCell] = value; }
        }

        public void Increment()
        {
            CurrentValue++;
            if (CurrentValue > _maxValue) CurrentValue = 0;
        }

        public void Decrement()
        {
            CurrentValue--;
            if (CurrentValue > _maxValue) CurrentValue = _maxValue;
        }

        public void ShiftRight()
        {
            CurrentCell++;
            CreateCellIfNeeded();
        }

        public void ShiftLeft()
        {
            CurrentCell--;
            CreateCellIfNeeded();
        }

        public void ReadInput(TextReader reader)
        {
            var current = (uint)reader.Read();

            if (current == (byte)'\r' && reader.Peek() == '\n')
            {
                reader.Read();
                current = 10;
            }
            CurrentValue = current;
        }

        private void CreateCellIfNeeded()
        {
            if (!CellValues.ContainsKey(CurrentCell))
            {
                CellValues[CurrentCell] = 0;
            }
        }

        public override string ToString()
        {
            var keyRow = new List<string>();
            var valueRow = new List<string>();
            var cursorRow = new List<string>();

            foreach (var cell in CellValues.OrderBy(c => c.Key))
            {
                int numKeyChars = cell.Key.GetNumCharsOfSpace();
                int numValChars = cell.Value.GetNumCharsOfSpace();
                int numTotalChars = numKeyChars > numValChars ? numKeyChars : numValChars;

                var keyBuilder = new StringBuilder();
                for (int i = 0; i < numTotalChars - numKeyChars; i++) keyBuilder.Append(' ');
                keyBuilder.Append(cell.Key);

                var valBuilder = new StringBuilder();
                for (int i = 0; i < numTotalChars - numValChars; i++) valBuilder.Append(' ');
                valBuilder.Append(cell.Value);

                var cursorBuilder = new StringBuilder();
                for (int i = 0; i < numTotalChars; i++) cursorBuilder.Append((CurrentCell == cell.Key && i == numTotalChars / 2) ? '^' : ' ');

                keyRow.Add(keyBuilder.ToString());
                valueRow.Add(valBuilder.ToString());
                cursorRow.Add(cursorBuilder.ToString());
            }

            var keyRowString = string.Join(" | ", keyRow);
            var valueRowString = string.Join(" | ", valueRow);
            var cursorRowString = string.Join("   ", cursorRow);

            var separatorStringBuilder = new StringBuilder();
            for (int i = 0; i < keyRowString.Length; i++)
            {
                separatorStringBuilder.Append('-');
            }

            return $"{keyRowString}{Environment.NewLine}{separatorStringBuilder.ToString()}{Environment.NewLine}{valueRowString}{Environment.NewLine}{cursorRowString}";
        }
    }
}
