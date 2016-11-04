using System.Collections.Generic;

namespace BrainFuckInterpreterLib
{
    internal sealed class InterpreterState
    {
        internal Stack<int> LoopStack = new Stack<int>();

        internal int CurrentIndex { get; set; } = 0;

        internal void MarkLoopLocation() => LoopStack.Push(CurrentIndex);
        internal void Loop() => CurrentIndex = LoopStack.Pop() - 1;
    }
}
