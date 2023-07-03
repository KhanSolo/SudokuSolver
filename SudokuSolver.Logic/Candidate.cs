using System.Collections.Generic;

namespace SudokuSolver.Logic
{
    public class Candidate
    {
        public HashSet<byte> Options { get; } = new(16) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public void Clear()
        {
            if (Options.Count > 0) Options.Clear();
        }

        internal void Subtract(IEnumerable<byte> existings)
        {
            foreach (var existing in existings)
                if (Options.TryGetValue(existing, out _))
                    Options.Remove(existing);
        }        
    }
}
