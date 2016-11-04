using BrainFuckInterpreterLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreterLib
{
    public class DebuggerState
    {
        public int CurrentExecutionLocation { get; internal set; }
        public Dictionary<int, uint> CellValues { get; internal set; }
        public int CellPointer { get; internal set; }

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
                for (int i = 0; i < numTotalChars; i++) cursorBuilder.Append((CellPointer == cell.Key && i == numTotalChars / 2) ? '^' : ' ');

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
