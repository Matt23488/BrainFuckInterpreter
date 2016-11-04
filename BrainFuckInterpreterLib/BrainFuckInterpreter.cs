using System;
using System.IO;

namespace BrainFuckInterpreterLib
{
    public sealed class BrainFuckInterpreter
    {
        private readonly TextWriter _writer;
        private readonly TextReader _reader;

        internal readonly ProgramState ProgramState;
        internal readonly InterpreterState InterpreterState;
        internal string Code;

        internal bool EndOfProgramReached => InterpreterState.CurrentIndex > Code.Length;

        public BrainFuckInterpreter(string code) : this(code, InterpreterSettings.Default) { }
        public BrainFuckInterpreter(string code, InterpreterSettings settings)
        {
            _writer = settings.Writer;
            _reader = settings.Reader;
            ProgramState = new ProgramState(settings.CellSize);
            InterpreterState = new InterpreterState();
            Code = code;
        }

        public void VerifySyntaxIntegrity()
        {
            int position;
            SyntaxError error = HasValidSyntax(Code, out position);
            if (error != SyntaxError.None)
            {
                throw new SyntaxException(error, position);
            }
        }

        public void Advance()
        {
            if (InterpreterState.CurrentIndex == Code.Length)
            {
                InterpreterState.CurrentIndex++;
                return;
            }

            switch (Code[InterpreterState.CurrentIndex])
            {
                case '<':
                    ProgramState.ShiftLeft();
                    break;
                case '>':
                    ProgramState.ShiftRight();
                    break;
                case '+':
                    ProgramState.Increment();
                    break;
                case '-':
                    ProgramState.Decrement();
                    break;
                case '[':
                    HandleOpenSquareBrace(Code);
                    break;
                case ']':
                    InterpreterState.Loop();
                    break;
                case '.':
                    _writer.Write(ProgramState.CurrentValue == 10 ? Environment.NewLine : ((char)ProgramState.CurrentValue).ToString());
                    break;
                case ',':
                    ProgramState.ReadInput(_reader);
                    break;
            }

            InterpreterState.CurrentIndex++;
        }

        public void RunToPosition(int position)
        {
            while (InterpreterState.CurrentIndex < position)
            {
                Advance();
            }
        }

        public void RunToCompletion()
        {
            RunToPosition(Code.Length);
        }

        private SyntaxError HasValidSyntax(string program, out int position)
        {
            position = program.Length;

            int level = 0;

            for (int i = 0; i < program.Length; i++)
            {
                switch (program[i])
                {
                    case '[':
                        level++;
                        break;
                    case ']':
                        if (level == 0)
                        {
                            position = i;
                            return SyntaxError.UnexpectedClosingSquareBrace;
                        }
                        level--;
                        break;
                }
            }

            if (level != 0) return SyntaxError.UnbalancedSquareBraces;
            else return SyntaxError.None;
        }

        private void HandleOpenSquareBrace(string program)
        {
            if (ProgramState.CurrentValue == 0)
            {
                int squareBraceCount = 1;
                while (squareBraceCount > 0)
                {
                    InterpreterState.CurrentIndex++;

                    switch (program[InterpreterState.CurrentIndex])
                    {
                        case '[': squareBraceCount++; break;
                        case ']': squareBraceCount--; break;
                    }
                }
            }
            else
            {
                InterpreterState.MarkLoopLocation();
            }
        }
    }
}
