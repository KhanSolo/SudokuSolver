using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SudokuSolver.Logic;

public sealed class Solver
{
    public Action<string> WriteLine = default;

    public Map Solve(Map map)
    {
        return map;
    }

    public Map SolveCandidates(Map map)
    {
        var candidates = CheckCandidates(map);

        return candidates;
    }

    private Map CheckCandidates(Map map)
    {
        for (var w = 0; w < Map.Size; ++w)
            for (var h = 0; h < Map.Size; ++h)            
            {
                CheckColumn(map, w, h);
                CheckRow(map, w, h);
                CheckRectangle(map, w, h);

                if (map[w,h] == default && WriteLine != null)
                {
                    var can = string.Join(", ", map.Candidates[w, h].Options.Select(c => c.ToString()));
                    WriteLine($"{w}, {h}; can : {can}");
                }
            }
        return map;
    }

    private void CheckRectangle(Map map, int width, int height)
    {
        var current = map[width, height];
        var currentCellCandidate = map.Candidates[width, height];

        if (current != default)
        {
            //WriteLine?.Invoke($"{width}, {height} : {current}");
            currentCellCandidate.Clear();
            return;
        }

        var leftUpperPoint = GetRectCoords(width, height);

        var existing = new List<byte>(16);
        for (var h = leftUpperPoint.Y; h < leftUpperPoint.Y + 3; ++h)
            for (var w = leftUpperPoint.X; w < leftUpperPoint.X + 3; ++w)
            {
                if (map[w, h] != default)
                    existing.Add(map[w, h]);
            }

        currentCellCandidate.Subtract(existing);

        //if (WriteLine != default)
        //{
        //    var exi = string.Join(",", existing.Select(e => e.ToString()));
        //    var can = string.Join(",", currentCellCandidate.Options.Select(o => o.ToString()));
        //    WriteLine($"{width}, {height} : {current}; exi : {exi}; can : {can}");
        //}
    }

    /// <summary>
    /// Determine a rectangle
    /// </summary>
    private static Point GetRectCoords(int width, int height)
    {
        int x, y;
        if (width < 3) { x = 0; y = GetY(height); } else if (width < 6) { x = 3; y = GetY(height); } else { x = 6; y = GetY(height); }
        return new Point(x, y);

        static int GetY(int h)
        {
            if (h < 3) return 0;
            if (h < 6) return 3;
            return 6;
        }
    }

    private void CheckRow(Map map, int width, int height)
    {
        CheckInternal(map, width, height, (m, i, exi) =>
        {
            if (map[i, height] != default)
                exi.Add(map[i, height]);
        });
    }

    private void CheckColumn(Map map, int width, int height)
    {
        CheckInternal(map, width, height, (m, i, exi) =>
        {
            if (map[width, i] != default)
                exi.Add(map[width, i]);
        });
    }

    private void CheckInternal(Map map, int width, int height, Action<Map, int, List<byte>> action)
    {
        var current = map[width, height];
        var currentCellCandidate = map.Candidates[width, height];

        if (current != default)
        {
            //WriteLine?.Invoke($"{width}, {height} : {current}");
            currentCellCandidate.Clear();
            return;
        }

        var existing = new List<byte>(16);
        for (var i = 0; i < Map.Size; ++i)
        {
            action(map, i, existing);
        }

        currentCellCandidate.Subtract(existing);

        //if (WriteLine != default)
        //{
        //    var exi = string.Join(",", existing.Select(e => e.ToString()));
        //    var can = string.Join(",", currentCellCandidate.Options.Select(o => o.ToString()));
        //    WriteLine($"{width}, {height} : {current}; exi : {exi}; can : {can}");
        //}
    }
}
