﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace SudokuSolver.Logic
{
    public class Solver
    {
        public Map Solve(Map map)
        {
            return map;
        }
        public Map SolveCandidates(Map map)
        {
            var solution = new Map(map);

            var candidates = CheckCandidates(solution);

            return candidates;
        }

        private static Map CheckCandidates(Map map)
        {
            var candidates = map.Candidates;

            var width = candidates.GetLength(0);
            var height = candidates.GetLength(1);
            for (var w = 0; w < width; w++)
                for (var h = 0; h < height; h++)
                {                    
                    CheckColumn(map, w, h);
                    CheckRow(map, w, h);
                    CheckRectangle(map, w, h);
                }
            return map;
        }

        private static void CheckRectangle(Map map, int width, int height)
        {
            var current = map[width, height];

            var leftUpperPoint = GetRectCoords(width, height);

            var existing = new List<byte>(16);
            for (var w = leftUpperPoint.X; w < leftUpperPoint.X + 3; w++)
                for (var h = leftUpperPoint.Y; h < leftUpperPoint.Y + 3; h++)
                {
                    if (map[w, height] != default)
                        existing.Add(map[w, height]);
                }

            for (var w = leftUpperPoint.X; w < leftUpperPoint.X + 3; w++)
                for (var h = leftUpperPoint.Y; h < leftUpperPoint.Y + 3; h++)
                {
                    var cellCandidate = map.Candidates[w, h];
                    if (current == default)
                    {
                        cellCandidate.Subtract(existing);
                    }
                    else
                    {
                        cellCandidate.Clear();
                    }
                }
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

        private static void CheckRow(Map map, int width, int height)
        {
            var current = map[width, height];

            var existing = new List<byte>(16);
            for (var w = 0; w < 9; w++)
                if (map[w, height] != default)
                    existing.Add(map[w, height]);

            for (var w = 0; w < 9; w++)
            {
                var cellCandidate = map.Candidates[w, height];
                if (current == default)
                {
                    cellCandidate.Subtract(existing);
                }
                else
                {
                    cellCandidate.Clear();
                }
            }
        }

        private static void CheckColumn(Map map, int width, int height)
        {
            var current = map[width, height];

            var existing = new List<byte>(16);
            for (var h = 0; h < 9; h++)            
                if (map[width, h] != default)
                    existing.Add(map[width, h]);            

            for(var h = 0; h<9; h++)
            {
                var cellCandidate = map.Candidates[width, h];
                if (current == default)
                {
                    cellCandidate.Subtract(existing);
                }
                else
                {
                    cellCandidate.Clear();
                }
            }
        }
    }
}