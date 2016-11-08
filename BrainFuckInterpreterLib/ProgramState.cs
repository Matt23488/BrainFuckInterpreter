using BrainFuckInterpreterLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrainFuckInterpreterLib
{
    public class ProgramState
    {
        private readonly uint _maxValue;
        private Dictionary<int, uint> _cellValues;

        private Stack<int> _loopStack;

        public IEnumerable<KeyValuePair<int, uint>> Cells => _cellValues.AsEnumerable();

        public int CurrentCodePosition { get; private set; }

        public int CurrentCell { get; private set; }
        public uint CurrentValue
        {
            get { return _cellValues[CurrentCell]; }
            set { _cellValues[CurrentCell] = value; }
        }

        public ProgramState(CellSize cellSize)
        {
            _cellValues = new Dictionary<int, uint> { { 0, 0 } };
            _loopStack = new Stack<int>();
            CurrentCodePosition = 0;
            CurrentCell = 0;

            switch (cellSize)
            {
                case CellSize.OneByte:
                    _maxValue = byte.MaxValue;
                    break;
                case CellSize.TwoBytes:
                    _maxValue = ushort.MaxValue;
                    break;
                case CellSize.FourBytes:
                    _maxValue = uint.MaxValue;
                    break;
            }
        }

        internal void MarkLoopLocation() => _loopStack.Push(CurrentCodePosition);
        internal void Loop() => CurrentCodePosition = _loopStack.Pop() - 1;
        internal void IncrementCodePosition() => CurrentCodePosition++;
        internal void IncrementCellValue()
        {
            unchecked
            {
                CurrentValue++;
                if (CurrentValue > _maxValue) CurrentValue = 0;
            }
        }
        internal void DecrementCellValue()
        {
            unchecked
            {
                CurrentValue--;
                if (CurrentValue > _maxValue) CurrentValue = _maxValue;
            }
        }
        internal void ShiftRight()
        {
            CurrentCell++;
            CreateCellIfNeeded();
        }
        internal void ShiftLeft()
        {
            CurrentCell--;
            CreateCellIfNeeded();
        }

        private void CreateCellIfNeeded()
        {
            if (!_cellValues.ContainsKey(CurrentCell))
            {
                _cellValues[CurrentCell] = 0;
            }
        }
    }
}
