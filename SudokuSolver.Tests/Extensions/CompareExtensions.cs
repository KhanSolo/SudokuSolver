using SudokuSolver.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Tests.Extensions;

internal static class CompareExtensions
{
    public static Option<bool, Error> Compare(this byte[,] expected, Map actual)
    {
        for (var w = 0; w < Map.Size; w++)
            for (var h = 0; h < Map.Size; h++)
                if (expected[w, h] != actual[w, h])
                    return new Option<bool, Error>(new Error($"Arrays differ at [{w}, {h}]"));
        return new Option<bool, Error>(true);
    }
}
