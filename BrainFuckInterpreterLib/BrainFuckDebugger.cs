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

        public IEnumerable<int> BreakPoints => _breakPoints.AsEnumerable();

        public event BreakPointHitEventHandler BreakPointHit;

        public BrainFuckDebugger(string code) : this(code, BrainFuckSettings.Default) { }
        public BrainFuckDebugger(string code, BrainFuckSettings settings)
        {
            _interpreter = new BrainFuckInterpreter(code, settings);

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
                if (paused || _breakPoints.Contains(_interpreter.State.CurrentCodePosition))
                {
                    var eventArgs = new BreakPointHitEventArgs(_interpreter.State);
                    BreakPointHit?.Invoke(this, eventArgs);

                    paused = eventArgs.Action == StepAction.Step;
                }

                _interpreter.Advance();
            }
        }
    }
}
