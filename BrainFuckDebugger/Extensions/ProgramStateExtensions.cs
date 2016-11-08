using BrainFuckInterpreterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckDebugger.Extensions
{
    internal static class ProgramStateExtensions
    {
        public static string GetCellStateString(this ProgramState state)
        {
            var keyRow = new List<string>();
            var valueRow = new List<string>();
            var cursorRow = new List<string>();

            foreach (var cell in state.Cells.OrderBy(c => c.Key))
            {
                int numKeyChars = cell.Key.GetNumCharsOfSpace();
                int numValChars = cell.Value.GetNumCharsOfSpace();
                int numTotalChars = numKeyChars > numValChars ? numKeyChars : numValChars;

                var keyBuilder = new StringBuilder();
                for (int i = 0; i < numTotalChars - numKeyChars; i++) keyBuilder.Append(' ');
                keyBuilder.Append(cell.Key);

                var valBuilder = new StringBuilder();
                for (int i = 0; i < numTotalChars - numValChars; i++) keyBuilder.Append(' ');
                valBuilder.Append(cell.Value);

                var cursorBuilder = new StringBuilder();
                for (int i = 0; i < numTotalChars; i++)
                {
                    cursorBuilder.Append((state.CurrentCell == cell.Key && i == numTotalChars / 2) ? '^' : ' ');
                }

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

            return $"{keyRowString}{Environment.NewLine}{separatorStringBuilder}{Environment.NewLine}{valueRowString}{Environment.NewLine}{cursorRowString}";
        }
    }
}
