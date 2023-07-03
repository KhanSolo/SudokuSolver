﻿using SudokuSolver.Logic;
using System;
using System.Collections.Generic;
using Xunit;

//using Pair<K,V> = System.KeyValuePair<K,V>;

namespace SudokuSolver.Tests;

public class SolverTests
{
    private readonly Solver _sut = new();

    [Fact]
    public void SolveEasy_Success()
    {
        var map = new byte[9, 9] {
        { 4,0,9,/**/7,0,0,/**/0,3,2},
        { 2,8,1,/**/6,9,0,/**/0,0,5},
        { 7,3,0,/**/4,8,2,/**/0,0,6},
        //---------------------------
        { 0,0,0,/**/2,7,0,/**/9,0,0},
        { 0,0,0,/**/0,0,8,/**/2,0,0},
        { 0,0,2,/**/0,0,5,/**/6,1,3},
        //---------------------------
        { 9,0,0,/**/0,1,0,/**/3,7,8},
        { 1,5,0,/**/3,0,0,/**/0,0,0},
        { 0,0,0,/**/0,0,9,/**/5,6,0},
        };

        var result = _sut.Solve(new Map(map));

        var expected = new byte[9, 9] {
        { 4,6,9,/**/7,5,1,/**/8,3,2},
        { 2,8,1,/**/6,9,3,/**/7,4,5},
        { 7,3,5,/**/4,8,2,/**/1,9,6},
        //---------------------------
        { 5,1,3,/**/2,7,6,/**/9,8,4},
        { 6,9,4,/**/1,3,8,/**/2,5,7},
        { 8,7,2,/**/9,4,5,/**/6,1,3},
        //---------------------------
        { 9,2,6,/**/5,1,4,/**/3,7,8},
        { 1,5,8,/**/3,6,7,/**/4,2,9},
        { 3,4,7,/**/8,2,9,/**/5,6,1},
        };
        var option = Compare(expected, result);
        Assert.True(option.Result, option.Error.Message);
    }

    private static Option<bool, Error> Compare(byte[,] expected, Map actual)
    {
        var width = expected.GetLength(0);
        var height = expected.GetLength(1);
        for (var w = 0; w < width; w++)
            for (var h = 0; h < height; h++)
                if (expected[w, h] != actual[w, h])
                    return new Option<bool, Error>(new Error($"Arrays differ at [{w}, {h}]"));
        return new Option<bool, Error>(true);
    }

    [Fact]
    public void SolveCandidates_Success()
    {
        var map = new byte[9, 9] {
        { 4,0,9,/**/7,0,0,/**/0,3,2},
        { 2,8,1,/**/6,9,0,/**/0,0,5},
        { 7,3,0,/**/4,8,2,/**/0,0,6},
        //---------------------------
        { 0,0,0,/**/2,7,0,/**/9,0,0},
        { 0,0,0,/**/0,0,8,/**/2,0,0},
        { 0,0,2,/**/0,0,5,/**/6,1,3},
        //---------------------------
        { 9,0,0,/**/0,1,0,/**/3,7,8},
        { 1,5,0,/**/3,0,0,/**/0,0,0},
        { 0,0,0,/**/0,0,9,/**/5,6,0},
        };

        var result = _sut.SolveCandidates(new Map(map));
    }
}
