using System;
using System.Text;

namespace SudokuSolver.Logic;

public sealed class Map : ICloneable
{
    public Map() { }

    public const int Size = 9;

    public Map(Map income) : this()
    {
        for (var h = 0; h < Size; ++h)
            for (var w = 0; w < Size; ++w)
                map[w, h] = income[w, h];

        for (var h = 0; h < Size; ++h)
            for (var w = 0; w < Size; ++w)
                Candidates[w,h] = income.Candidates[w,h];
    }

    public Map(byte[,] income) : this()
    {
        for (var h = 0; h < Size; ++h)
            for (var w = 0; w < Size; ++w)
                map[w,h] = income[w,h];
    }

    private readonly byte[,] map = new byte[Size, Size];

    public byte this[int w, int h]
    {
        get { return map[w, h]; }
        set { map[w, h] = value; }
    }

    public Candidate[,] Candidates { get; init; } = new Candidate[Size, Size]
    {
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
        { new (), new (), new (), new (), new (), new (), new (), new (), new (),},
    };

    public object Clone() => new Map(this);

    public static Map LoadText(string text)
    {
        var map = new Map();
        int w = 0, h = 0;
        foreach (var c in text)
            if (c >= '0' && c <= '9')
            {
                map[w, h] = (byte)((byte)c - 0x30);
                if (++h == Size) { h = 0; ++w; }
            }

        return map;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        for (var w = 0; w < Size; w++)
        {
            if (w == 3 || w == 6) sb.AppendLine();
            for (var h = 0; h < Size; h++)
            {
                if (h == 3 || h == 6) sb.Append(" ");
                sb.Append(map[w, h]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
