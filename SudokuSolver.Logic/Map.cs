using System;
using System.Text;

namespace SudokuSolver.Logic;

public sealed class Map : ICloneable
{
    public Map() { }

    public const int Size = 9;

    public Map(Map income) : this()
    {
        for (var w = 0; w < Size; w++)
            for (var h = 0; h < Size; h++)
                map[w, h] = income[w, h];

        //foreach(var candidate in income.Candidates)
        //{
        //}

        for (var w = 0; w < Size; w++)
            for (var h = 0; h < Size; h++)
                Candidates[w,h] = income.Candidates[w,h];
    }

    public Map(byte[,] income) : this()
    {
        for (var w = 0; w < Size; w++)
            for (var h = 0; h < Size; h++)                
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

    public static Map Load(string text)
    {
        var array = ParseText(text);
        var map = new Map(array);

        return map;
    }

    private static byte[,] ParseText(string text)
    {
        var sb = new ByteBuilder();
        foreach(var c in text)
        {
            if(c >= '0' && c<='9')
            {
                var num = (byte)((byte)c - 0x30);
                sb.Append(num);
            }
        }

        var array = new byte[Size, Size];
        for(var w=0; w<Size; ++w)
            for(var h=0; h<Size; ++h)        
                array[w, h] = sb.Buffer[w * Size + h];
        
        return array;
    }
}

internal class ByteBuilder
{
    private const int Capacity = Map.Size * Map.Size;
    readonly byte[] chunks = new byte[Capacity];
    int pointer = 0;

    public byte[] Buffer => chunks;

    internal void Append(byte num)
    {
        if(pointer > Capacity) throw new IndexOutOfRangeException();
        chunks[pointer++] = num;
    }
}