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
        public StepAction Action { get; set; } = StepAction.Continue;

        public ProgramState ProgramState { get; }

        public BreakPointHitEventArgs(ProgramState programState)
        {
            ProgramState = programState;
        }
    }
}
