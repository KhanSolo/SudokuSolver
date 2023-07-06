using SudokuSolver.Logic;
using SudokuSolver.Tests.Extensions;
using Xunit;

namespace SudokuSolver.Tests;

public class MapTests
{
    [Fact]
    public void LoadMap_Success()
    {
        var text = @"
409 700 032
281 690 005
730 482 006

000 270 900
000 008 200
002 005 613

900 010 378
150 300 000
000 009 560
";
        var map = Map.Load(text);

        Assert.NotNull(map);

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

        var result = field.Compare(map);

        Assert.True(result.IsSuccess);
    }
}
