using System;

namespace BrainFuckInterpreterLib
{
    public class SyntaxException : Exception
    {
        public int Position { get; }

        internal SyntaxException(SyntaxError error, int position) : base(GetMessage(error, position))
        {
            Position = position;
        }

        private static string GetMessage(SyntaxError error, int position)
        {
            string prefix = "Syntax Error:";
            string suffix = $"Position: {position}";
            switch (error)
            {
                case SyntaxError.UnbalancedSquareBraces: return $"{prefix} Program contains unbalanced square braces. {suffix}";
                case SyntaxError.UnexpectedClosingSquareBrace: return $"{prefix} Program contains unexpected ']'. {suffix}";
                default: return null;
            }
        }
    }
}
