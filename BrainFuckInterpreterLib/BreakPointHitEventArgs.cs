using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreterLib
{
    public delegate void BreakPointHitEventHandler(object sender, BreakPointHitEventArgs e);
    public class BreakPointHitEventArgs : EventArgs
    {
        internal BreakState State { get; set; } = BreakState.Break;

        public DebuggerState ProgramState { get; }

        public BreakPointHitEventArgs(DebuggerState programState)
        {
            ProgramState = programState;
        }

        public void Continue()
        {
            State = BreakState.Continue;
        }

        public void Step()
        {
            State = BreakState.Step;
        }

        internal enum BreakState
        {
            Break,
            Step,
            Continue
        }
    }
}
