using System.Collections.Generic;

namespace SudokuSolver.Logic;

public sealed class Candidate
{
    public Candidate()
    {
        Options = new(16) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }
    public HashSet<byte> Options { get; }

    public int Count => Options.Count;

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

    public override string ToString() => string.Join(", ", Options);
}
