using SudokuSolver.Logic;
using SudokuSolver.Tests.Extensions;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace SudokuSolver.Tests;

public sealed class SolverTests
{
    private readonly Solver _sut;
    private readonly ITestOutputHelper _output;

    public SolverTests(ITestOutputHelper output)
    {        
        _output = output;
        _sut = new()
        {
            WriteLine = _output.WriteLine
        };
    }

    [Fact]
    public void SolveEasy_Success()
    {
        var field = new byte[9, 9] {
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

        var map = new Map(field);
        var result = _sut.Solve(map);

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
        var option = expected.Compare(result);
        Assert.True(option.Result, option.Error.Message);
    }

    [Fact]
    public void SolveCandidates_Success()
    {
        var field = new byte[9, 9] {
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

        var map = new Map(field);
        var result = _sut.SolveCandidates(map);

        var c = result.Candidates;
        AssertCandidate(0, 0, result, 0);
        AssertCandidate(0, 1, result, 1);
        AssertCandidate(0, 2, result, 0);

        AssertCandidate(0, 3, result, 0);
        //Assert.True(c[0, 4].Count > 0, $"value is {result[0, 4]}");
        //Assert.True(c[0, 5].Count > 0, $"value is {result[0, 5]}");

        AssertCandidate(8, 8, result, 2);
    }

    private static void AssertCandidate(int w, int h, Map result, int count)
    {
        Assert.True(result.Candidates[w, h].Count == count, $"value is {result[w, h]}");
    }

    private static void AssertCandidate(int w, int h, Map result, IReadOnlyCollection<byte> expected)
    {
        var c = result.Candidates;
        Assert.True(c[w, h].Count == expected.Count);


    }

    [Fact]
    public void Solve()
    {
        var map = Map.LoadText(@"
    954 863 172
    678 512 493
    213 974 856

    562 498 317
    189 237 564
    437 156 289

    841 629 735
    305 741 628
    726 385 901
");
        var result = _sut.SolveCandidates(map);

        var c = result.Candidates;
    }
}

