using System;

namespace SudokuSolver.Logic
{
    public sealed class Map : ICloneable
    {
        public Map() { }
        public Map(Map income) : this()
        {
            for (var w = 0; w < 9; w++)
                for (var h = 0; h < 9; h++)
                    map[w, h] = income[w, h];
        }

        public Map(byte[,] income)
        {
            for (var w = 0; w < 9; w++)
                for (var h = 0; h < 9; h++)                
                    map[w,h] = income[w,h];                
        }

        private readonly byte[,] map = new byte[9,9];

        public byte this[int w, int h]
        {
            get { return map[w, h]; }
            set { map[w, h] = value; }
        }

        public Candidate[,] Candidates { get; init; } = new Candidate[9,9];

        public object Clone() => new Map(this);
    }
}
