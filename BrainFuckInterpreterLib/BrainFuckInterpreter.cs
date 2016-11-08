using BrainFuckInterpreterLib.Extensions;
using BrainFuckInterpreterLib.Utilities;
using System;
using System.IO;
using System.Linq;

namespace BrainFuckInterpreterLib
{
    public sealed class BrainFuckInterpreter
    {
        private readonly BrainFuckWriter _writer;
        private readonly BrainFuckReader _reader;

        private bool _syntaxChecked;

        public ProgramState State { get; private set; }

        public string Code { get; internal set; }

        public bool EndOfProgramReached => State.CurrentCodePosition > Code.Length;

        public BrainFuckInterpreter(string code) : this(code, BrainFuckSettings.Default) { }
        public BrainFuckInterpreter(string code, BrainFuckSettings settings)
        {
            _writer = new BrainFuckWriter(settings.Writer);
            _reader = new BrainFuckReader(settings.Reader);
            _syntaxChecked = false;
            State = new ProgramState(settings.CellSize);
            Code = code;
        }

        public void Advance()
        {
            VerifySyntaxIntegrity();

            if (State.CurrentCodePosition == Code.Length)
            {
                State.IncrementCodePosition();
                return;
            }

            switch (Code[State.CurrentCodePosition])
            {
                case '<':
                    State.ShiftLeft();
                    break;
                case '>':
                    State.ShiftRight();
                    break;
                case '+':
                    State.IncrementCellValue();
                    break;
                case '-':
                    State.DecrementCellValue();
                    break;
                case '[':
                    HandleLoopConditionCheck();
                    break;
                case ']':
                    State.Loop();
                    break;
                case '.':
                    _writer.Write(State.CurrentValue);
                    break;
                case ',':
                    State.CurrentValue = _reader.Read();
                    break;
            }

            State.IncrementCodePosition();
        }

        public void RunToPosition(int position)
        {
            while (State.CurrentCodePosition < position)
            {
                Advance();
            }
        }

        public void RunToCompletion()
        {
            RunToPosition(Code.Length);
        }

        internal void VerifySyntaxIntegrity()
        {
            // TODO: This will have to be changed once debugging is added again.
            // If we allow on-the-fly code changes, we will have to clear the flag.
            if (!_syntaxChecked)
            {
                int position;
                var error = HasValidSyntax(out position);
                if (error != SyntaxError.None)
                {
                    throw new SyntaxException(error, position);
                }
                _syntaxChecked = true;
            }
        }

        private SyntaxError HasValidSyntax(out int position)
        {
            position = Code.Length;

            int nestedLoopLevel = 0;

            for (int i = 0; i < Code.Length; i++)
            {
                switch (Code[i])
                {
                    case '[':
                        nestedLoopLevel++;
                        break;
                    case ']':
                        if (nestedLoopLevel == 0)
                        {
                            position = i;
                            return SyntaxError.UnexpectedClosingSquareBrace;
                        }
                        nestedLoopLevel--;
                        break;
                }
            }

            if (nestedLoopLevel != 0) return SyntaxError.UnbalancedSquareBraces;
            else                      return SyntaxError.None;
        }

        private void HandleLoopConditionCheck()
        {
            if (State.CurrentValue == 0)
            {
                int nestedLoopLevel = 1;
                while (nestedLoopLevel > 0)
                {
                    State.IncrementCodePosition();

                    switch (Code[State.CurrentCodePosition])
                    {
                        case '[': nestedLoopLevel++; break;
                        case ']': nestedLoopLevel--; break;
                    }
                }
            }
            else
            {
                State.MarkLoopLocation();
            }
        }
    }
}
