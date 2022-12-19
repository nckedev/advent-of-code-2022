using System.Data;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace aoc2023;

public class Day8 : DayBase<int>
{
    private HashSet<int> Seen = new HashSet<int>();
    private int _currentMax = 0;

    public override int Solve()
    {
        var f = ReadFileAsLines(8);
        var sum = 0;
        var grid = new Grid<int>(f.Count());
        foreach (var row in f){
            grid.Add(row);
        }
        
        foreach (var a in Enumerable.Range(0, grid.Rows))
        {
            foreach (var b in grid.IterateHorizontal(a))
            {
                if (b.value > _currentMax)
                {
                    Seen.Add(b.value);
                }

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            Seen.Clear();
            _currentMax = 0;

            foreach (var b in grid.IterateHorizontal(a, true))
            {
                if (b.value > _currentMax)
                {
                    Seen.Add(b.value);
                }

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            Seen.Clear();
            _currentMax = 0;
        }

        foreach (var a in Enumerable.Range(0, grid.Cols))
        {
            foreach (var b in grid.IterateVertical(a))
            {
                if (b.value > _currentMax)
                {
                    Seen.Add(b.value);
                }

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            Seen.Clear();
            _currentMax = 0;
            foreach (var b in grid.IterateVertical(a, true))
            {
                if (b.value > _currentMax)
                {
                    Seen.Add(b.value);
                }

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            Seen.Clear();
            _currentMax = 0;
        }

        return sum;
        //1578 för lågt
        //1745 för högt
    }

    public override int Solve2()
    {
        return 0;
    }
}

public class Grid<T> where T : INumber<T>
{
    private T[][] grid;
    public int Rows { get; }
    public int Cols { get; private set; }

    private int _currentRow = 0;

    public Grid(int rows)
    {
        Rows = rows;
        grid = new T[rows][];
    }

    public void Add(string row)
    {
        grid[_currentRow] = row.Select(s => T.Parse(s.ToString(), null)).ToArray();
        Cols = grid[_currentRow].Length;
        _currentRow++;
        ;
    }

    public IEnumerable<(T value, int index)> IterateHorizontal(int row, bool startFromEnd = false)
    {
        if (!startFromEnd)
        {
            for (var i = 1; i < grid[row].Length -1; i++)
            {
                yield return (grid[row][i], i);
            }
        }

        else
        {
            for (var i = grid[row].Length - 2; i >= 1; i--)
            {
                yield return (grid[row][i], i);
            }
        }
    }

    public IEnumerable<(T value, int index)> IterateVertical(int col, bool startFromEnd = false)
    {
        if (!startFromEnd)
        {
            for (var i = 1; i < Rows -1; i++)
            {
                yield return (grid[i][col], i);
            }
        }
        else
        {
            for (var i = Rows - 2; i >= 1; i--)
            {
                yield return (grid[i][col], i);
            }
        }
    }
}