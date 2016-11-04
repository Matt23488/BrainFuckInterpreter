namespace BrainFuckInterpreterLib.Extensions
{
    internal static class IntegerExtensions
    {
        public static int GetNumCharsOfSpace(this int i)
        {
            int value = i;
            int numSpaces = 1;

            if (value < 0)
            {
                numSpaces++;
                value *= -1;
            }

            while (value >= 10)
            {
                value /= 10;
                numSpaces++;
            }

            return numSpaces;
        }

        public static int GetNumCharsOfSpace(this uint b)
        {
            uint value = b;
            int numSpaces = 1;

            while (value >= 10)
            {
                value /= 10;
                numSpaces++;
            }

            return numSpaces;
        }
    }
}
