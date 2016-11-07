using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreterLib
{
    public class BrainFuckDebugger
    {
        private BrainFuckInterpreter _interpreter;

        private List<int> _breakPoints;

        public IEnumerable<int> BreakPoints
        {
            get { return _breakPoints.AsEnumerable(); }
        }

        public event BreakPointHitEventHandler BreakPointHit;

        public BrainFuckDebugger(BrainFuckInterpreter interpreter)
        {
            _interpreter = interpreter;

            _breakPoints = new List<int>();
        }

        public void ToggleBreakPoint(int position)
        {
            if (_breakPoints.Contains(position)) _breakPoints.Remove(position);
            else                                 _breakPoints.Add(position);
        }

        public void Start()
        {
            _interpreter.VerifySyntaxIntegrity();
            
            bool paused = false;
            while (!_interpreter.EndOfProgramReached)
            {
                if (paused || _breakPoints.Contains(_interpreter.InterpreterState.CurrentIndex))
                {
                    var programState = new DebuggerState
                    {
                        CurrentExecutionLocation = _interpreter.InterpreterState.CurrentIndex,
                        CellValues = _interpreter.ProgramState.CellValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                        CellPointer = _interpreter.ProgramState.CurrentCell
                    };

                    var eventArgs = new BreakPointHitEventArgs(programState);
                    BreakPointHit?.Invoke(this, eventArgs);
                    
                    paused = eventArgs.State == BreakPointHitEventArgs.BreakState.Step;
                }

                _interpreter.Advance();
            }
        }
    }
}
