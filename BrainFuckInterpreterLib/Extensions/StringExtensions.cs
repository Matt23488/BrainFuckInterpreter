using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreterLib.Extensions
{
    internal static class StringExtensions
    {
        public static string Join<T>(this IEnumerable<T> charList, string separator = "")
        {
            return string.Join(separator, charList);
        }
    }
}
